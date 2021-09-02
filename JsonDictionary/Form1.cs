// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using JsonDictionaryCore.Properties;

using JsonEditorForm;

using JsonPathParserLib;

using static JsonDictionaryCore.JsonIo;

namespace JsonDictionaryCore
{
    public partial class Form1 : Form
    {
        private readonly List<ContentTypeItem> _fileTypes = new List<ContentTypeItem>
        {
            new ContentTypeItem
            {
                FileTypeMask = "dataviews.jsonc",
                PropertyTypeName = "dataviews",
                FileType = JsoncContentType.DataViews
            },
            new ContentTypeItem
            {
                FileTypeMask = "events.jsonc",
                PropertyTypeName = "events",
                FileType = JsoncContentType.Events
            },
            new ContentTypeItem
            {
                FileTypeMask = "layout.jsonc",
                PropertyTypeName = "layout",
                FileType = JsoncContentType.Layout
            },
            new ContentTypeItem
            {
                FileTypeMask = "rules.jsonc",
                PropertyTypeName = "rules",
                FileType = JsoncContentType.Rules
            },
            new ContentTypeItem
            {
                FileTypeMask = "tools.jsonc",
                PropertyTypeName = "tools",
                FileType = JsoncContentType.Tools
            },
            new ContentTypeItem
            {
                FileTypeMask = "strings.jsonc",
                PropertyTypeName = "strings",
                FileType = JsoncContentType.Strings
            },
            new ContentTypeItem
            {
                FileTypeMask = "patch.jsonc",
                PropertyTypeName = "patch",
                FileType = JsoncContentType.Patch
            },
            new ContentTypeItem
            {
                FileTypeMask = "search.jsonc",
                PropertyTypeName = "search",
                FileType = JsoncContentType.Search
            },
            new ContentTypeItem
            {
                FileTypeMask = "combo.jsonc",
                PropertyTypeName = "combo",
                FileType = JsoncContentType.Combo
            },
        };

        // pre-defined constants
        private const string DefaultVersionCaption = "Any";
        private const string VersionTagName = ".contentVersion";
        private const char TableListDelimiter = ';';
        private const char JsonPathDiv = '.';
        private const string RootNodeName = "Kinetic";
        private const float CellSizeAdjust = 0.7f;
        private const string PreViewCaption = "[Preview] ";
        private const string DefaultEditorFormCaption = "JsonDictionary";
        private const string DefaultTreeFileExtension = "tree";
        private const string DefaultExamplesFileExtension = "examples";
        private readonly string[] _exampleGridColumnsNames = { "Version", "Example", "File Name", "Json Path" };
        private string DefaultDescriptionFileName = "descriptions.json";
        private string _fileMask = "*.jsonc";

        // behavior options
        private static bool _reformatJsonBrackets;
        private static bool _beautifyJson;
        private bool _showPreview;
        private bool _alwaysOnTop;
        private bool _loadDbOnStart;
        private bool _useVsCode;

        // global variables
        private readonly StringBuilder _textLog = new StringBuilder();
        private readonly DataTable _examplesTable;
        private TreeNode _rootNodeExamples = new TreeNode();
        private Dictionary<string, List<JsonProperty>> _exampleLinkCollection = new Dictionary<string, List<JsonProperty>>();
        private volatile bool _isDoubleClick;

        // last used values for UI processing optimization
        private TreeNode _lastExSelectedNode;
        private List<SearchItem> _lastExSearchList = new List<SearchItem>();

        private JsonViewer _sideViewer;

        private struct WinPosition
        {
            public int WinX;
            public int WinY;
            public int WinW;
            public int WinH;

            public bool Initialized
            {
                get => !(WinX == 0 && WinY == 0 && WinW == 0 && WinH == 0);
            }
        }

        private WinPosition _editorPosition;

        private Dictionary<string, string> _nodeDescription = new Dictionary<string, string>();

        private struct ProcessingOptions
        {
            public JsoncContentType ContentType;
            public string ItemName;
            public string[] ParentNames;
        }

        private readonly ProcessingOptions[] _flattenParameters = new ProcessingOptions[]
        {
            new ProcessingOptions
            {
                ContentType = JsoncContentType.Events,
                ItemName = "type",
                ParentNames = new[]
                {
                    "actions",
                    "onsuccess",
                    "onfailure",
                    "onerror",
                    "onsinglematch",
                    "onmultiplematch",
                    "onnomatch",
                    "onyes",
                    "onno",
                    "onok",
                    "oncancel",
                    "onabort",
                    "onempty"
                }
            },
            new ProcessingOptions
            {
                ContentType = JsoncContentType.Layout,
                ItemName = "sourcetypeid",
                ParentNames = new[] { "components" }
            },
            new ProcessingOptions
            {
                ContentType = JsoncContentType.Rules,
                ItemName = "action",
                ParentNames = new[] { "actions" }
            },
            new ProcessingOptions
            {
                ContentType = JsoncContentType.Search,
                ItemName = "sourcetypeid",
                ParentNames = new[] { "component" }
            }
        };

        private string FormCaption
        {
            get => base.Text;
            set => base.Text = value;
        }

        #region GUI
        #region System events
        public Form1()
        {
            InitializeComponent();

            FormCaption = DefaultEditorFormCaption;

            checkBox_reformatJsonBrackets.Checked = _reformatJsonBrackets = Settings.Default.ReformatJson;
            checkBox_beautifyJson.Checked = _beautifyJson = Settings.Default.BeautifyJson;
            checkBox_showPreview.Checked = _showPreview = Settings.Default.ShowPreview;
            checkBox_alwaysOnTop.Checked = _alwaysOnTop = Settings.Default.AlwaysOnTop;
            checkBox_loadDbOnStart.Checked = _loadDbOnStart = Settings.Default.LoadDbOnStartUp;
            checkBox_vsCode.Checked = _useVsCode = Settings.Default.UseVsCode;
            folderBrowserDialog1.SelectedPath = Settings.Default.LastRootFolder;
            _fileMask = Settings.Default.FileMask;
            DefaultDescriptionFileName = Settings.Default.DefaultDescriptionFileName;
            _nodeDescription = LoadJson<Dictionary<string, string>>(DefaultDescriptionFileName);
            if (_nodeDescription == null)
                _nodeDescription = new Dictionary<string, string>();

            _editorPosition = new WinPosition()
            {
                WinX = Settings.Default.EditorPositionX,
                WinY = Settings.Default.EditorPositionY,
                WinW = Settings.Default.EditorWidth,
                WinH = Settings.Default.EditorHeight,
            };

            var mainFormPosition = new WinPosition()
            {
                WinX = Settings.Default.MainWindowPositionX,
                WinY = Settings.Default.MainWindowPositionY,
                WinW = Settings.Default.MainWindowWidth,
                WinH = Settings.Default.MainWindowHeight,
            };

            if (mainFormPosition.Initialized)
            {
                this.Location = new Point
                { X = mainFormPosition.WinX, Y = mainFormPosition.WinY };
                this.Width = mainFormPosition.WinW;
                this.Height = mainFormPosition.WinH;
            }

            splitContainer_tree.SplitterDistance = Settings.Default.TreeSplitterDistance;
            splitContainer_description.SplitterDistance = Settings.Default.DescriptionSplitterDistance;
            splitContainer_fileList.SplitterDistance = Settings.Default.FileListSplitterDistance;

            TopMost = _alwaysOnTop;

            comboBox_ExCondition.Items.AddRange(typeof(SearchItem.SearchCondition).GetEnumNames());
            comboBox_ExCondition.SelectedIndex = 0;

            _examplesTable = new DataTable("Examples");
            for (var i = 0; i < _exampleGridColumnsNames.Length; i++)
            {
                _examplesTable.Columns.Add(_exampleGridColumnsNames[i]);
            }

            dataGridView_examples.DataError += delegate
            { };
            dataGridView_examples.DataSource = _examplesTable;

            comboBox_ExVersions.SelectedIndexChanged -= ComboBox_ExVersions_SelectedIndexChanged;
            comboBox_ExVersions.Items.Clear();
            comboBox_ExVersions.Items.Add(DefaultVersionCaption);
            comboBox_ExVersions.SelectedIndex = 0;
            comboBox_ExVersions.SelectedIndexChanged += ComboBox_ExVersions_SelectedIndexChanged;

            if (_loadDbOnStart)
                LoadDb(Settings.Default.LastDbName).ConfigureAwait(true);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveJson(_nodeDescription, DefaultDescriptionFileName, true);

            Settings.Default.LastRootFolder = folderBrowserDialog1.SelectedPath;
            Settings.Default.ReformatJson = _reformatJsonBrackets;
            Settings.Default.BeautifyJson = _beautifyJson;
            Settings.Default.ShowPreview = _showPreview;
            Settings.Default.AlwaysOnTop = _alwaysOnTop;
            Settings.Default.LoadDbOnStartUp = _loadDbOnStart;

            Settings.Default.MainWindowPositionX = Location.X;
            Settings.Default.MainWindowPositionY = Location.Y;
            Settings.Default.MainWindowWidth = Width;
            Settings.Default.MainWindowHeight = Height;

            if (_sideViewer != null && !_sideViewer.IsDisposed)
            {
                _editorPosition.WinX = _sideViewer.Location.X;
                _editorPosition.WinY = _sideViewer.Location.Y;
                _editorPosition.WinW = _sideViewer.Width;
                _editorPosition.WinH = _sideViewer.Height;
            }

            Settings.Default.EditorPositionX = _editorPosition.WinX;
            Settings.Default.EditorPositionY = _editorPosition.WinY;
            Settings.Default.EditorWidth = _editorPosition.WinW;
            Settings.Default.EditorHeight = _editorPosition.WinH;

            Settings.Default.TreeSplitterDistance = splitContainer_tree.SplitterDistance;
            Settings.Default.DescriptionSplitterDistance = splitContainer_description.SplitterDistance;
            Settings.Default.FileListSplitterDistance = splitContainer_fileList.SplitterDistance;

            Settings.Default.Save();
        }
        #endregion

        #region Main page
        private void Button_loadDb_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "";
            openFileDialog1.Title = "Open KineticScheme data";
            openFileDialog1.DefaultExt = DefaultTreeFileExtension;
            openFileDialog1.Filter = "Binary files|*." + DefaultTreeFileExtension + "|All files|*.*";
            openFileDialog1.ShowDialog();
        }

        private async void OpenFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            ActivateUiControls(false);
            if (await LoadDb(openFileDialog1.FileName).ConfigureAwait(true))
            {
                FormCaption = DefaultEditorFormCaption + " " + ShortFileName(openFileDialog1.FileName);
                tabControl1.SelectedTab = tabControl1.TabPages[1];
                Settings.Default.LastDbName = openFileDialog1.FileName;
                Settings.Default.Save();
            }

            ActivateUiControls(true);
        }

        private async void Button_collectDatabase_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() != DialogResult.OK)
                return;

            ActivateUiControls(false);
            treeView_examples.Nodes.Clear();
            _examplesTable.Clear();
            _exampleLinkCollection = new Dictionary<string, List<JsonProperty>>();
            var startPath = folderBrowserDialog1.SelectedPath;
            toolStripStatusLabel1.Text = "Collecting files...";
            var startTime = DateTime.Now;
            var startOperationTime = DateTime.Now;
            var endTime = DateTime.Now;

            await Task.Run(() =>
            {
                var jsonPropertiesCollection = RunFileCollection(startPath, _fileMask);
                Invoke((MethodInvoker)delegate
               {
                   endTime = DateTime.Now;
                   _textLog.AppendLine("Collection time: " + endTime.Subtract(startOperationTime).TotalSeconds);
                   startOperationTime = DateTime.Now;
                   FlushLog();
                   toolStripStatusLabel1.Text = "Processing events collection";
               });

                Parallel.ForEach(_flattenParameters, param =>
                {
                    Invoke((MethodInvoker)delegate
                    {
                        startOperationTime = DateTime.Now;
                        FlushLog();
                        toolStripStatusLabel1.Text = "Processing " + param.ContentType + " collection";
                    });

                    FlattenCollection(jsonPropertiesCollection, param.ContentType, param.ItemName, param.ParentNames);

                    Invoke((MethodInvoker)delegate
                    {
                        endTime = DateTime.Now;
                        _textLog.AppendLine(param.ContentType + " processing time: " + endTime.Subtract(startOperationTime).TotalSeconds);
                    });
                });

                Invoke((MethodInvoker)delegate
                {
                    startOperationTime = DateTime.Now;
                    FlushLog();
                    toolStripStatusLabel1.Text = "Generating tree";
                });

                _rootNodeExamples = GenerateTreeFromList(jsonPropertiesCollection);
            }).ConfigureAwait(true);

            endTime = DateTime.Now;
            _textLog.AppendLine("Tree generating time: " + endTime.Subtract(startOperationTime).TotalSeconds);
            FlushLog();

            treeView_examples.Nodes.Add(_rootNodeExamples);
            treeView_examples.Sort();
            treeView_examples.Nodes[0].Expand();

            GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);

            endTime = DateTime.Now;
            _textLog.AppendLine("Total processing time: " + endTime.Subtract(startTime).TotalSeconds);
            FlushLog();

            toolStripStatusLabel1.Text = "";
            ActivateUiControls(true);
        }

        private void Button_saveDb_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Title = "Save KineticScheme data";
            saveFileDialog1.DefaultExt = DefaultTreeFileExtension;
            saveFileDialog1.Filter = "Binary files|*." + DefaultTreeFileExtension + "|All files|*.*";
            saveFileDialog1.FileName =
                "KineticDictionary_" + DateTime.Today.ToShortDateString().Replace("/", "_") + "." + DefaultTreeFileExtension;
            saveFileDialog1.ShowDialog();
        }

        private async void SaveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            if (!_exampleLinkCollection.Any() || _rootNodeExamples?.Nodes.Count <= 0)
                return;

            toolStripStatusLabel1.Text = "Saving database...";
            await Task.Run(() =>
             {
                 try
                 {
                     var treeFile = Path.ChangeExtension(saveFileDialog1.FileName, DefaultTreeFileExtension);
                     var examplesFile = Path.ChangeExtension(saveFileDialog1.FileName, DefaultExamplesFileExtension);
                     SaveBinaryTree(_rootNodeExamples, treeFile);
                     SaveBinary(_exampleLinkCollection, examplesFile);
                     Settings.Default.LastDbName = saveFileDialog1.FileName;
                     Settings.Default.Save();

                     // test Tree->JSON
                     File.WriteAllText(treeFile + ".json", CustomNode.GetJsonTree(_rootNodeExamples));
                     // end test
                 }
                 catch (Exception ex)
                 {
                     MessageBox.Show("File write exception [" + saveFileDialog1.FileName + "]: " + ex.Message);
                 }
             }).ContinueWith((t) =>
             {
                 toolStripStatusLabel1.Text = "";
             }).ConfigureAwait(false);
            GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);
        }

        private void CheckBox_beautifyJson_CheckedChanged(object sender, EventArgs e)
        {
            checkBox_reformatJsonBrackets.Enabled = _beautifyJson = checkBox_beautifyJson.Checked;
        }

        private void CheckBox_reformatJsonBrackets_CheckedChanged(object sender, EventArgs e)
        {
            _reformatJsonBrackets = checkBox_reformatJsonBrackets.Checked;
        }

        private void CheckBox_showPreview_CheckedChanged(object sender, EventArgs e)
        {
            _showPreview = checkBox_showPreview.Checked;
        }

        private void CheckBox_alwaysOnTop_CheckedChanged(object sender, EventArgs e)
        {
            _alwaysOnTop = checkBox_alwaysOnTop.Checked;
            TopMost = _alwaysOnTop;
        }

        private void CheckBox_loadDbOnStart_CheckedChanged(object sender, EventArgs e)
        {
            _loadDbOnStart = checkBox_loadDbOnStart.Checked;
        }

        private void CheckBox_vsCode_CheckedChanged(object sender, EventArgs e)
        {
            _useVsCode = checkBox_vsCode.Checked;
        }
        #endregion

        #region Tree
        private void TreeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button != MouseButtons.Right)
                return;

            if (sender is TreeView tree)
                tree.SelectedNode = e.Node;
        }

        private void TreeView_examples_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ShowSamples();
            }
            else if (e.KeyCode == Keys.Add)
            {
                UnfoldNode();
            }
            else if (e.KeyCode == Keys.Subtract)
            {
                FoldNode();
            }
            else if (e.KeyCode == Keys.Delete)
            {
                DeleteNode();
            }
            else if (e.KeyCode == Keys.C && e.Control)
            {
                CopyNodeText();
            }
        }

        private void TreeView_examples_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            ShowSamples();
        }

        private void TreeView_examples_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e == null)
                return;

            textBox_description.ReadOnly = true;
            label_descSave.Visible = false;
            var descText = "";
            _nodeDescription?.TryGetValue(e.Node.Name, out descText);
            textBox_description.Text = descText;
        }

        private void ToolStripMenuItem_treeShow_Click(object sender, EventArgs e)
        {
            ShowSamples();
        }

        private void ToolStripMenuItem_unfoldAll_Click(object sender, EventArgs e)
        {
            if (treeView_examples.SelectedNode == null)
                return;

            UnfoldNode();
        }

        private void ToolStripMenuItem_foldAll_Click(object sender, EventArgs e)
        {
            if (treeView_examples.SelectedNode == null)
                return;

            FoldNode();
        }

        private void ToolStripMenuItem_treeCopy_Click(object sender, EventArgs e)
        {
            CopyNodeText();
        }

        private void ToolStripMenuItem_treeDelete_Click(object sender, EventArgs e)
        {
            DeleteNode();
        }

        private void ShowSamples()
        {
            if (treeView_examples.SelectedNode == null)
                return;

            ActivateUiControls(false);
            if (FillExamplesGrid(_exampleLinkCollection, treeView_examples.SelectedNode))
            {
                _lastExSearchList.Clear();
                ActivateUiControls(true);
                ActivateUiControls(false, false);
                dataGridView_examples.Invalidate();
                //await ReadjustRows(dataGridView_examples).ConfigureAwait(true);
            }
            ActivateUiControls(true, false);
        }

        private void UnfoldNode()
        {
            if (treeView_examples.SelectedNode == null)
                return;

            treeView_examples.SelectedNode.ExpandAll();
        }

        private void FoldNode()
        {
            if (treeView_examples.SelectedNode == null)
                return;

            treeView_examples.SelectedNode.Collapse(false);
        }

        private void CopyNodeText()
        {
            if (treeView_examples.SelectedNode == null)
                return;

            Clipboard.SetText(treeView_examples.SelectedNode.Text);
        }

        private void DeleteNode()
        {
            if (treeView_examples.SelectedNode == null)
                return;

            if (MessageBox.Show("Are you sure to remove the selected node and all of it's samples?", "Remove node", MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;

            ActivateUiControls(false);

            var records = _exampleLinkCollection.Where(n => n.Key.StartsWith(treeView_examples.SelectedNode.Name)).Select(n => n.Key).ToArray();
            for (var i = 0; i < records.Length; i++)
            {
                _exampleLinkCollection.Remove(records[i]);
            }

            treeView_examples.Nodes.Remove(treeView_examples.SelectedNode);
            ActivateUiControls(true);
            _lastExSearchList.Clear();
        }

        #region Prevent_treenode_collapse
        private void TreeView_MouseDown(object sender, MouseEventArgs e)
        {
            _isDoubleClick = e.Clicks > 1;
        }

        private void TreeView_No_Expand_Collapse(object sender, TreeViewCancelEventArgs e)
        {
            if (!_isDoubleClick)
                return;

            _isDoubleClick = false;
            e.Cancel = true;
        }
        #endregion
        #endregion

        #region Examples Grid
        private void DataGridView_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right)
                return;

            if (sender is DataGridView dataGrid)
            {
                dataGrid.ClearSelection();
                dataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected = true;
            }
        }

        private void DataGridView_examples_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            OpenFile(true);
        }

        private void DataGridView_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (sender is DataGridView dataGrid)
                ReadjustRow(dataGrid, e.RowIndex);
        }

        private void DataGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (sender is DataGridView dataGrid)
            {
                if (dataGrid.Rows.Count <= 0 || e.RowIndex < 0)
                    return;

                var fileNames = dataGrid.Rows[e.RowIndex].Cells[2]?.Value?.ToString().Split(new[] { TableListDelimiter }, StringSplitOptions.RemoveEmptyEntries);

                this.listBox_fileList.SelectedValueChanged -= this.ListBox_fileList_SelectedValueChanged;

                listBox_fileList.Items.Clear();
                listBox_fileList.Items.AddRange(fileNames ?? Array.Empty<string>());
                if (fileNames?.Length > 0)
                    listBox_fileList.SetSelected(0, true);

                this.listBox_fileList.SelectedValueChanged += this.ListBox_fileList_SelectedValueChanged;

                if (_showPreview)
                {
                    OpenFile();
                    dataGrid.Focus();
                }
            }
        }

        private void DataGridView_examples_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.C && e.Control)
            {
                CopySample();
            }
            /*else if (e.KeyCode == Keys.Delete)
            {
                DeleteSamples();
            }*/
            else if (e.KeyCode == Keys.Enter)
            {
                OpenFile(true);
            }
        }

        private void ToolStripMenuItem_SampleCopy_Click(object sender, EventArgs e)
        {
            CopySample();
        }

        private void ToolStripMenuItem_SampleOpen_Click(object sender, EventArgs e)
        {
            OpenFile(true);
        }

        private void ToolStripMenuItem_SampleDelete_Click(object sender, EventArgs e)
        {
            //DeleteSamples();
        }

        private void CopySample()
        {
            if (dataGridView_examples == null)
                return;

            if (dataGridView_examples.SelectedCells.Count == 1)
            {
                Clipboard.SetText(dataGridView_examples.SelectedCells[0].Value.ToString());
            }
            else
                Clipboard.SetText(dataGridView_examples.SelectedCells.ToString());
        }

        // don't work. need tree path to find all samples. Change ListToTree to add actual paths
        private void DeleteSamples()
        {
            if (MessageBox.Show("Do you want to delete all selected samples for all files?", "Delete samples", MessageBoxButtons.YesNo) == DialogResult.No)
                return;

            var cellRowList = new List<int>();
            foreach (var cell in dataGridView_examples.SelectedCells)
            {
                cellRowList.Add(((DataGridViewCell)cell).RowIndex);
            }
            var rowList = cellRowList.Distinct().ToArray();
            foreach (var rowNumber in rowList)
            {
                var jsonPath = _exampleLinkCollection.FirstOrDefault(n => n.Key == _lastExSelectedNode.Name).Key;
                var jsonSample = dataGridView_examples.Rows[rowNumber].Cells[1]?.Value?.ToString();

                //var jsonPath = JsonPathDiv + jsonPaths[0];
                var samplesCollection = _exampleLinkCollection.Where(n => n.Key == jsonPath);
                samplesCollection.FirstOrDefault().Value.RemoveAll(n => n.Value == CompactJson(jsonSample));
            }

        }
        #endregion

        #region FileList
        private void ListBox_fileList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.C && e.Control)
            {
                CopyFileName();
            }
            else if (e.KeyCode == Keys.Enter)
            {
                OpenFile(true);
            }
            /*else if (e.KeyCode == Keys.Delete)
            {
                DeleteSampleForFile();
            }*/
        }

        private void ListBox_fileList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            OpenFile(true);
        }

        private void ListBox_fileList_SelectedValueChanged(object sender, EventArgs e)
        {
            if (!_showPreview)
            {
                return;
            }

            OpenFile();
            listBox_fileList.Focus();
        }

        private void ToolStripMenuItem_copy_Click(object sender, EventArgs e)
        {
            CopyFileName();
        }

        private void ToolStripMenuItem_openFile_Click(object sender, EventArgs e)
        {
            OpenFile(true);
        }

        private void ToolStripMenuItem_openFolder_Click(object sender, EventArgs e)
        {
            var fileNumber = listBox_fileList.SelectedIndex;
            var fileName = listBox_fileList.Items[fileNumber].ToString();

            Process.Start("explorer.exe", $"/select, {fileName}");
        }

        private void ToolStripMenuItem_FileDelete_Click(object sender, EventArgs e)
        {
            //DeleteSampleForFile();
        }

        private void CopyFileName()
        {
            if (listBox_fileList == null)
                return;

            Clipboard.SetText(listBox_fileList.SelectedItem.ToString());
        }

        // don't work. need tree path to find all samples. Change ListToTree to add actual paths
        private void DeleteSampleForFile()
        {
            if (MessageBox.Show("Do you want to delete all selected samples for selected file?", "Delete samples", MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;

            var jsonPaths = dataGridView_examples.Rows[dataGridView_examples.SelectedCells[0].RowIndex].Cells[3]?.Value?.ToString().Split(new[] { TableListDelimiter }, StringSplitOptions.RemoveEmptyEntries);
            var jsonSample = dataGridView_examples.Rows[dataGridView_examples.SelectedCells[0].RowIndex].Cells[3]?.Value?.ToString();
            var fileNumber = listBox_fileList.SelectedIndex;
            var fileName = listBox_fileList.Items[fileNumber].ToString();
            var jsonPath = "";

            if (jsonPaths?.Length >= fileNumber)
            {
                jsonPath = jsonPaths[fileNumber];
            }

            _exampleLinkCollection[jsonPath].RemoveAll(n => n.FullFileName == fileName);
            listBox_fileList.Items.RemoveAt(fileNumber);
        }

        #endregion

        private void OnClosingEditor(object sender, CancelEventArgs e)
        {
            if (sender is Form s)
            {
                _editorPosition.WinX = s.Location.X;
                _editorPosition.WinY = s.Location.Y;
                _editorPosition.WinW = s.Width;
                _editorPosition.WinH = s.Height;
            }
        }

        private async void Button_ExAdjustRows_Click(object sender, EventArgs e)
        {
            //ActivateUiControls(false, false);
            await ReadjustRows(dataGridView_examples).ConfigureAwait(false);
            //ActivateUiControls(true);
        }

        private async void TextBox_ExSearchString_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
                return;

            var searchParam = new SearchItem(comboBox_ExVersions.SelectedItem?.ToString())
            {
                CaseSensitive = checkBox_ExCaseSensitive.Checked
                    ? StringComparison.Ordinal
                    : StringComparison.OrdinalIgnoreCase,
                Value = textBox_ExSearchString.Text,
                Condition = (SearchItem.SearchCondition)comboBox_ExCondition.SelectedIndex
            };
            if (_lastExSearchList.Contains(searchParam))
                return;

            ActivateUiControls(false);
            await FilterExamples(_exampleLinkCollection, searchParam).ConfigureAwait(true);
            ActivateUiControls(true);
            e.SuppressKeyPress = true;
        }

        private void Button_ExClearSearch_Click(object sender, EventArgs e)
        {
            ExClearSearch();
            FillExamplesGrid(_exampleLinkCollection, treeView_examples.SelectedNode);
        }

        private async void ComboBox_ExVersions_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActivateUiControls(false);
            var searchParam = new SearchItem(comboBox_ExVersions.SelectedItem.ToString());

            await FilterExamplesVersion(_exampleLinkCollection, searchParam).ConfigureAwait(true);
            //dataGridView_examples.Invalidate();

            ActivateUiControls(true);
        }

        private void TextBox_description_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (textBox_description.ReadOnly)
            {
                if (string.IsNullOrEmpty(textBox_description.Text))
                {
                    textBox_description.Text = "Description: \r\n*Note: ";
                }
                textBox_description.ReadOnly = false;
                label_descSave.Visible = true;
                label_edit.Visible = false;
            }
        }

        private void TextBox_description_KeyDown(object sender, KeyEventArgs e)
        {
            if (textBox_description.ReadOnly == false)
            {
                if (e.KeyCode == Keys.Escape)
                {
                    _nodeDescription.TryGetValue(treeView_examples?.SelectedNode?.Name ?? "", out var descText);
                    textBox_description.Text = descText;
                    textBox_description.ReadOnly = true;
                    label_descSave.Visible = false;
                    label_edit.Visible = true;
                }
                else if (e.KeyCode == Keys.Enter && e.Control)
                {
                    try
                    {
                        _nodeDescription[treeView_examples?.SelectedNode?.Name ?? ""] = textBox_description.Text;
                    }
                    catch
                    {
                        _nodeDescription.Add(treeView_examples?.SelectedNode?.Name ?? "", textBox_description.Text);
                    }

                    textBox_description.ReadOnly = true;
                    label_descSave.Visible = false;
                    label_edit.Visible = true;
                }
            }
        }
        #endregion

        #region Helpers
        private BlockingCollection<JsonProperty> RunFileCollection(string collectionPath, string fileMask)
        {
            // Searching project files                        
            var filesList = Directory.GetFiles(collectionPath, fileMask, SearchOption.AllDirectories);
            _textLog.AppendLine(
                $"Files found: {filesList.Length}");
            FlushLog();

            var jsonPropertiesCollection = new BlockingCollection<JsonProperty>();
            // parse all files            
            var fileNumber = 0;
            Parallel.ForEach(filesList, fileName =>
            {
                var fileType = GetFileTypeFromFileName(fileName, _fileTypes);
                DeserializeFile(fileName, fileType, jsonPropertiesCollection);

                if (fileNumber % 10 == 0)
                {
                    Invoke((MethodInvoker)delegate
                    {
                        var fnum = fileNumber.ToString();
                        toolStripStatusLabel1.Text = "Files parsed " + fnum + "/" + filesList.Length;
                    });
                }

                fileNumber++;
            });

            return jsonPropertiesCollection;
        }

        private void DeserializeFile(
            string fullFileName,
            JsoncContentType fileType,
            BlockingCollection<JsonProperty> rootCollection)
        {
            string jsonStr;
            try
            {
                jsonStr = CompactJson(File.ReadAllText(fullFileName));
            }
            catch (Exception ex)
            {
                return;
            }

            if (string.IsNullOrEmpty(jsonStr))
            {
                return;
            }

            var parser = new JsonPathParser
            {
                TrimComplexValues = false,
                SaveComplexValues = true,
                RootName = "",
                JsonPathDivider = JsonPathDiv
            };

            var jsonProperties = parser.ParseJsonToPathList(jsonStr, out var endPos, out var errorFound)
                .Where(item => item.JsonPropertyType != JsonPropertyTypes.Comment
                    && item.JsonPropertyType != JsonPropertyTypes.EndOfArray
                    && item.JsonPropertyType != JsonPropertyTypes.EndOfObject
                    && item.JsonPropertyType != JsonPropertyTypes.Error
                    && item.JsonPropertyType != JsonPropertyTypes.Unknown)
                .ToList();
            var version = jsonProperties.Where(n => n.Path == VersionTagName).FirstOrDefault()?.Value ?? "";

            var rootObject = jsonProperties.Where(n => n.JsonPropertyType == JsonPropertyTypes.Object && string.IsNullOrEmpty(n.Name) && string.IsNullOrEmpty(n.Path)).FirstOrDefault();
            jsonProperties.Remove(rootObject);

            Parallel.ForEach(jsonProperties, item =>
            {
                item.Path = item.Path.TrimStart(JsonPathDiv);

                foreach (var fType in _fileTypes)
                {
                    if (item.Path.StartsWith(fType.PropertyTypeName))
                    {
                        fileType = fType.FileType;
                        break;
                    }
                }

                var newItem = new JsonProperty
                {
                    PathDelimiter = JsonPathDiv,
                    Name = item.Name,
                    Value = item.Value,
                    ContentType = fileType,
                    FullFileName = fullFileName,
                    Version = version,
                    JsonPath = item.Path,
                    VariableType = item.ValueType,
                };
                rootCollection.Add(newItem);
            });
        }

        private bool FlattenCollection(IEnumerable<JsonProperty> propertiesCollection,
            JsoncContentType contentType,
            string elementName,
            string[] parentName)
        {
            if (propertiesCollection == null
                || propertiesCollection.Count() <= 0
                || string.IsNullOrEmpty(elementName)
                || parentName == null
                || parentName.Length <= 0
                || string.IsNullOrEmpty(parentName[0]))
                return false;

            var typedCollection = propertiesCollection
                .Where(n => n.ContentType == contentType)
                .ToArray();

            for (var i = 0; i < parentName.Length; i++)
            {
                parentName[i] = parentName[i].ToLower();
            }

            IEnumerable<IGrouping<string, JsonProperty>> fileGroupedCollection;
            if (parentName.Length > 1)
            {
                fileGroupedCollection = typedCollection
                    .Where(n =>
                        n.Name.Equals(elementName, StringComparison.OrdinalIgnoreCase)
                        && parentName.Contains(n.UnifiedParent.ToLower()))
                    .ToArray()
                    .GroupBy(n => n.FullFileName);
            }
            else
            {
                fileGroupedCollection = typedCollection
                   .Where(n =>
                        n.Name.Equals(elementName, StringComparison.OrdinalIgnoreCase)
                        && parentName[0].Equals(n.UnifiedParent, StringComparison.OrdinalIgnoreCase))
                   .ToArray()
                   .GroupBy(n => n.FullFileName);
            }

            var processedFilesNumber = 0;
            var totalFilesNumber = fileGroupedCollection.Count();

            //get every file name
            //Parallel.ForEach(FileGroupedCollection, actionCollection =>
            foreach (var actionCollection in fileGroupedCollection)
            {
                var fileActionCollection = typedCollection
                    .Where(n =>
                            n.FullFileName == actionCollection.Key)
                    .ToArray();

                // iterate through single file one by one
                foreach (var actionProperty in actionCollection)
                {
                    //get a collection of events in the file
                    var actionMembers = fileActionCollection
                        .Where(n =>
                            n.FullFileName == actionProperty.FullFileName &&
                            n.JsonPath.Contains(actionProperty.ParentPath))
                        .ToArray();

                    foreach (var actionMember in actionMembers)
                    {
                        /*var flattenedPath = actionMember.JsonPath.Replace(actionProperty.ParentPath,
                            "events.actions.<" + actionProperty.Value.Replace(JsonPathDiv, '_') + ">");*/
                        string flattenedPath;
                        if (contentType == JsoncContentType.Events)
                        {
                            flattenedPath = actionMember.JsonPath.Replace(actionProperty.ParentPath,
                            "events.actions.<" + actionProperty.Value.Replace('.', '_') + ">");
                        }
                        else
                        {
                            flattenedPath = actionMember.JsonPath.Replace(actionProperty.ParentPath,
                                actionProperty.ParentPath + ".<" + actionProperty.Value.Replace('.', '_') + ">");
                        }
                        actionMember.FlattenedJsonPath = flattenedPath;
                    }
                }

                if (processedFilesNumber % 20 == 0)
                {
                    Invoke((MethodInvoker)delegate
                    {
                        toolStripStatusLabel1.Text =
                            contentType.ToString() + " converted " + processedFilesNumber + "/" + totalFilesNumber;
                    });
                }

                processedFilesNumber++;
            }
            //);

            return true;
        }

        private TreeNode GenerateTreeFromList(IEnumerable<JsonProperty> rootCollection)
        {
            var node = new TreeNode(RootNodeName);
            var itemNumber = 0;
            var totalItemNumber = rootCollection.Count();

            foreach (var propertyItem in rootCollection)
            {
                var itemName = $"<{propertyItem.ContentType}>{JsonPathDiv}{propertyItem.UnifiedFlattenedJsonPath}".TrimEnd(JsonPathDiv);
                if (!_exampleLinkCollection.ContainsKey(itemName))
                {
                    _exampleLinkCollection.Add(itemName, new List<JsonProperty>() { propertyItem });
                }
                else
                {
                    _exampleLinkCollection[itemName].Add(propertyItem);
                }

                var tmpNode = node;
                var tmpPath = new StringBuilder();

                foreach (var token in itemName.Split(new[] { JsonPathDiv }, StringSplitOptions.RemoveEmptyEntries))
                {
                    if (tmpPath.Length > 0)
                        tmpPath.Append(JsonPathDiv + token);
                    else
                        tmpPath.Append(token);

                    if (!tmpNode.Nodes.ContainsKey(tmpPath.ToString()))
                    {
                        tmpNode.Nodes.Add(tmpPath.ToString(), token);
                    }

                    tmpNode = tmpNode.Nodes[tmpPath.ToString()];
                }

                if (itemNumber % 1000 == 0)
                {
                    Invoke((MethodInvoker)delegate
                   {
                       toolStripStatusLabel1.Text =
                           "Properties processed  " + itemNumber + "/" + totalItemNumber;
                   });
                }

                itemNumber++;
            }

            return node;
        }

        private async Task<bool> LoadDb(string fileName)
        {
            var treeFile = Path.ChangeExtension(fileName, DefaultTreeFileExtension);
            var examplesFile = Path.ChangeExtension(fileName, DefaultExamplesFileExtension);

            FormCaption = DefaultEditorFormCaption;
            if (string.IsNullOrEmpty(fileName))
                return false;

            ActivateUiControls(false, false);
            toolStripStatusLabel1.Text = "Loading database...";
            var rootNodeExamples = new TreeNode();
            var exampleLinkCollection = new Dictionary<string, List<JsonProperty>>();
            try
            {
                var t = Task.Run(() =>
                    {
                        rootNodeExamples = LoadBinaryTree<TreeNode>(treeFile);
                    }
                );
                await Task.WhenAll(t).ConfigureAwait(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show("File read exception [" + fileName + "]: " + ex.Message);
                toolStripStatusLabel1.Text = "Failed to load database";
            }

            if (rootNodeExamples != null)
            {
                _rootNodeExamples = rootNodeExamples;
            }

            GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);

            tabControl1.TabPages[1].Enabled = true;
            FormCaption = DefaultEditorFormCaption + " " + ShortFileName(fileName);
            treeView_examples.Nodes.Clear();
            treeView_examples.Nodes.Add(_rootNodeExamples);
            treeView_examples.Nodes[0].Expand();
            tabControl1.SelectedTab = tabControl1.TabPages[1];
            ActivateUiControls(true, false);

            try
            {
                var t = Task.Run(() =>
                {
                    exampleLinkCollection = LoadBinary<Dictionary<string, List<JsonProperty>>>(examplesFile);
                }
                );
                await Task.WhenAll(t).ConfigureAwait(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show("File read exception [" + fileName + "]: " + ex.Message);
                toolStripStatusLabel1.Text = "Failed to load database";
            }

            if (exampleLinkCollection != null)
            {
                _exampleLinkCollection = exampleLinkCollection;
            }

            GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);

            toolStripStatusLabel1.Text = "";

            return true;
        }

        private bool FillExamplesGrid(Dictionary<string, List<JsonProperty>> exampleLinkCollection, TreeNode currentNode,
            SearchItem searchParam = null)
        {
            if (currentNode == null || exampleLinkCollection == null || exampleLinkCollection.Count <= 0)
                return false;

            if (!exampleLinkCollection.ContainsKey(currentNode.Name))
                return false;

            var records = exampleLinkCollection[currentNode.Name];
            toolStripStatusLabel1.Text = "Displaying " + records.Count + " records";

            var parentNode = _lastExSelectedNode?.Parent;
            if (_lastExSelectedNode != null)
            {
                _lastExSelectedNode.BackColor = Color.White;

                while (parentNode != null)
                {
                    parentNode.BackColor = Color.White;
                    parentNode = parentNode.Parent;
                }
            }

            _lastExSelectedNode = currentNode;
            currentNode.BackColor = Color.DodgerBlue;
            parentNode = currentNode.Parent;
            while (parentNode != null)
            {
                parentNode.BackColor = Color.DodgerBlue;
                parentNode = parentNode.Parent;
            }

            ExClearSearch();

            _examplesTable.Rows.Clear();
            comboBox_ExVersions.Items.Clear();
            comboBox_ExVersions.Items.Add(DefaultVersionCaption);
            comboBox_ExVersions.SelectedIndex = 0;

            var versionCollection = new List<string>();
            var groupedByVersionRecords = records.GroupBy(n => n.Version);
            foreach (var versionGroup in groupedByVersionRecords)
            {
                var currentVersion = versionGroup.Key ?? "";
                versionCollection.Add(currentVersion);

                var groupedByValueRecords = versionGroup.GroupBy(n => n.Value);
                foreach (var valueGroup in groupedByValueRecords)
                {
                    var pathList = new StringBuilder();
                    var fileNameList = new StringBuilder();
                    var recordValue = "";

                    try
                    {
                        recordValue = _beautifyJson ? BeautifyJson(valueGroup.Key, _reformatJsonBrackets) : valueGroup.Key;
                    }
                    catch
                    {
                        recordValue = valueGroup.Key;
                    }

                    foreach (var record in valueGroup)
                    {
                        if (record == null)
                            continue;

                        if ((searchParam == null || string.IsNullOrEmpty(searchParam.Value))
                            && FilterCellOut(recordValue, searchParam))
                            continue;

                        fileNameList.Append(record.FullFileName + TableListDelimiter.ToString());
                        pathList.Append(record.JsonPath + TableListDelimiter.ToString());
                        //newRow[4] = newRow[4]
                        //+ Delimiter.ToString()
                        //+ record.SourceLineNumber;
                    }

                    var newRow = _examplesTable.NewRow();
                    newRow[_exampleGridColumnsNames[0]] = currentVersion;
                    newRow[_exampleGridColumnsNames[1]] = recordValue;
                    newRow[_exampleGridColumnsNames[2]] = fileNameList.ToString();
                    newRow[_exampleGridColumnsNames[3]] = pathList.ToString();
                    //newRow[_exampleGridColumnsNames[4]] = "";
                    _examplesTable.Rows.Add(newRow);
                }
            }

            if (searchParam == null)
                searchParam = new SearchItem(DefaultVersionCaption);
            if (!_lastExSearchList.Contains(searchParam))
                _lastExSearchList.Add(searchParam);

            SetSearchText(textBox_ExSearchHistory, _lastExSearchList);
            comboBox_ExVersions.Items.AddRange(versionCollection.ToArray());
            toolStripStatusLabel1.Text = "";

            return true;
        }

        private async Task FilterExamples(Dictionary<string, List<JsonProperty>> exampleLinkCollection,
            SearchItem searchParam)
        {
            if (_lastExSearchList.Contains(searchParam))
                return;

            _lastExSearchList.Add(searchParam);

            if (searchParam == null || string.IsNullOrEmpty(searchParam.Value))
            {
                FillExamplesGrid(exampleLinkCollection, _lastExSelectedNode, searchParam);
                return;
            }

            var rows = _examplesTable.Rows;
            var rowsNumber = rows.Count;
            toolStripStatusLabel1.Text = "Filtering " + rowsNumber + " rows";

            await Task.Run(() =>
            {
                for (var i = 0; i < rows.Count; i++)
                {
                    var cellValue = rows[i].ItemArray[1];
                    if (cellValue == null || FilterCellOut(cellValue.ToString(), searchParam))
                    {
                        rows.RemoveAt(i);
                        i--;
                    }
                }
            }).ConfigureAwait(true);

            SetSearchText(textBox_ExSearchHistory, _lastExSearchList);
            toolStripStatusLabel1.Text = "";
        }

        private async Task FilterExamplesVersion(Dictionary<string, List<JsonProperty>> exampleLinkCollection, SearchItem searchParam)
        {
            if (_lastExSearchList == null)
                _lastExSearchList = new List<SearchItem>();

            if (!_lastExSearchList.Any())
                _lastExSearchList.Add(new SearchItem(DefaultVersionCaption));
            var lastSearch = _lastExSearchList.Last();
            if (lastSearch.Version != DefaultVersionCaption)
                FillExamplesGrid(exampleLinkCollection, _lastExSelectedNode, searchParam);

            if (comboBox_ExVersions.Items.Contains(searchParam.Version))
            {
                comboBox_ExVersions.SelectedItem = searchParam.Version;
                lastSearch.Version = searchParam.Version;
            }
            else
            {
                comboBox_ExVersions.SelectedItem = DefaultVersionCaption;
                return;
            }

            lastSearch.Version = searchParam.Version;
            var rows = _examplesTable.Rows;
            var rowsNumber = rows.Count;
            toolStripStatusLabel1.Text = "Filtering " + rowsNumber + " rows";

            await Task.Run(() =>
            {
                for (var i = 0; i < rows.Count; i++)
                {
                    var cellValue = rows[i].ItemArray[0];
                    if (cellValue == null || string.IsNullOrEmpty(cellValue.ToString()) ||
                        cellValue.ToString() != searchParam.Version)
                    {
                        rows.RemoveAt(i);
                        i--;
                    }
                }
            }).ConfigureAwait(true);

            SetSearchText(textBox_ExSearchHistory, _lastExSearchList);
            toolStripStatusLabel1.Text = "";
        }

        private void SetSearchText(TextBox textBox, List<SearchItem> searchList)
        {
            var searchString = new StringBuilder();
            foreach (var lastSearch in searchList)
            {
                if (lastSearch == null || string.IsNullOrEmpty(lastSearch.Value))
                    continue;

                var cs = lastSearch.CaseSensitive == StringComparison.Ordinal ? "'CS'" : "";
                searchString.Append(searchString.Length <= 0 ? "[" : " -> [");
                searchString.Append("ver.:\"" + lastSearch.Version + "\";");
                searchString.Append(lastSearch.Condition + cs + ":\"" + lastSearch.Value + "\"]");
            }

            Invoke((MethodInvoker)delegate
            { textBox.Text = searchString.ToString(); });
        }

        private void ExClearSearch()
        {
            _lastExSearchList.Clear();
            Invoke((MethodInvoker)delegate
            { textBox_ExSearchHistory.Clear(); });
        }

        private async Task ReadjustRows(DataGridView dgView)
        {
            var rowsNumber = dgView.RowCount;
            toolStripStatusLabel1.Text = "Adjusting height for " + rowsNumber + " rows";

            await Task.Run(() =>
            {
                Invoke((MethodInvoker)delegate
               {
                   if (dgView.AutoSizeRowsMode != DataGridViewAutoSizeRowsMode.None)
                   {
                       LoopThroughRows(dgView);
                       dgView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
                   }

                   dgView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

                   for (var rowNumber = 0; rowNumber < dgView.RowCount; rowNumber++)
                   {
                       var row = dgView.Rows[rowNumber];
                       row.HeaderCell.Value = (rowNumber + 1).ToString();
                       var newHeight = row.GetPreferredHeight(rowNumber,
                           DataGridViewAutoSizeRowMode.AllCellsExceptHeader, true);
                       var currentHeight = dgView.Height;
                       if (newHeight == row.Height && newHeight <= currentHeight * CellSizeAdjust)
                           return;

                       if (newHeight > currentHeight * CellSizeAdjust)
                           newHeight = (ushort)(currentHeight * CellSizeAdjust);
                       row.Height = newHeight;
                   }

                   for (var columnNumber = 0; columnNumber < dgView.ColumnCount; columnNumber++)
                   {
                       var column = dgView.Columns[columnNumber];
                       var newWidth = column.GetPreferredWidth(DataGridViewAutoSizeColumnMode.AllCells, true);
                       var currentWidth = dgView.Width;
                       if (newWidth == column.Width && newWidth <= currentWidth * CellSizeAdjust)
                           return;

                       if (newWidth > currentWidth * CellSizeAdjust)
                           newWidth = (ushort)(currentWidth * CellSizeAdjust);
                       column.Width = newWidth;
                   }
               });
            }).ContinueWith((t) => { toolStripStatusLabel1.Text = ""; });
        }

        private static void ReadjustRow(DataGridView dgView, int rowNumber)
        {
            if (dgView.AutoSizeRowsMode != DataGridViewAutoSizeRowsMode.None)
            {
                LoopThroughRows(dgView);
                dgView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            }

            var row = dgView.Rows[rowNumber];
            row.HeaderCell.Value = (rowNumber + 1).ToString();
            var newHeight = row.GetPreferredHeight(rowNumber, DataGridViewAutoSizeRowMode.AllCellsExceptHeader, true);
            var currentHeight = dgView.Height;
            if (newHeight == row.Height && newHeight <= currentHeight * CellSizeAdjust)
                return;

            if (newHeight > currentHeight * CellSizeAdjust)
                newHeight = (ushort)(currentHeight * CellSizeAdjust);
            row.Height = newHeight;
        }

        private void ActivateUiControls(bool active, bool processTable = true)
        {
            if (!active)
            {
                comboBox_ExVersions.SelectedIndexChanged -= ComboBox_ExVersions_SelectedIndexChanged;
            }

            if (active)
                FlushLog();

            dataGridView_examples.Enabled = active;

            if (processTable)
            {
                if (active)
                {
                    dataGridView_examples.DataSource = _examplesTable;
                    dataGridView_examples.Columns[2].Visible = false;
                    dataGridView_examples.Columns[3].Visible = false;
                    //dataGridView_examples.Columns[4].Visible = false;
                    dataGridView_examples.Invalidate();
                }
                else
                {
                    dataGridView_examples.DataSource = null;
                }
            }

            button_collectDatabase.Enabled = active;
            button_loadDb.Enabled = active;
            button_saveDb.Enabled = active;

            comboBox_ExVersions.Enabled = active;
            comboBox_ExCondition.Enabled = active;
            textBox_ExSearchString.Enabled = active;
            checkBox_ExCaseSensitive.Enabled = active;
            dataGridView_examples.Enabled = active;
            treeView_examples.Enabled = active;
            button_ExAdjustRows.Enabled = active;

            tabControl1.Enabled = active;

            if (active)
            {
                comboBox_ExVersions.SelectedIndexChanged += ComboBox_ExVersions_SelectedIndexChanged;
            }

            Refresh();
        }

        private void OpenFile(bool standAloneEditor = false)
        {
            if (dataGridView_examples.SelectedCells.Count <= 0 || listBox_fileList.SelectedItems.Count <= 0)
                return;

            var jsonPaths = dataGridView_examples.Rows[dataGridView_examples.SelectedCells[0].RowIndex].Cells[3]?.Value?.ToString().Split(new[] { TableListDelimiter }, StringSplitOptions.RemoveEmptyEntries);
            var jsonSample = dataGridView_examples.Rows[dataGridView_examples.SelectedCells[0].RowIndex].Cells[3]?.Value?.ToString();
            var fileNumber = listBox_fileList.SelectedIndex;
            var fileName = listBox_fileList.Items[fileNumber].ToString();

            if (jsonPaths?.Length >= fileNumber)
            {
                var jsonPath = jsonPaths[fileNumber];
                ShowPreviewEditor(fileName, jsonPath, jsonSample, standAloneEditor);
            }
        }

        private void ShowPreviewEditor(string fileName, string jsonPath, string sampleText, bool standAloneEditor = false)
        {
            if (_useVsCode)
            {
                var lineNumber = GetLineNumberForPath(fileName, jsonPath) + 1;
                var execParams = "-r -g " + fileName + ":" + lineNumber;
                VsCodeOpenFile(execParams);
                return;
            }

            var textEditor = _sideViewer;
            if (standAloneEditor)
            {
                textEditor = null;
            }

            var fileLoaded = false;
            var newWindow = false;
            if (textEditor != null && !textEditor.IsDisposed)
            {
                if (textEditor.SingleLineBrackets != _reformatJsonBrackets || textEditor.Text != PreViewCaption + fileName)
                {
                    textEditor.SingleLineBrackets = _reformatJsonBrackets;
                    fileLoaded = textEditor.LoadJsonFromFile(fileName);
                }
                else
                {
                    fileLoaded = true;
                }
            }
            else
            {
                if (textEditor != null)
                {
                    textEditor.Close();
                    textEditor.Dispose();
                }

                textEditor = new JsonViewer("", "", standAloneEditor)
                {
                    SingleLineBrackets = _reformatJsonBrackets
                };

                newWindow = true;
                fileLoaded = textEditor.LoadJsonFromFile(fileName);
            }

            if (!standAloneEditor)
                _sideViewer = textEditor;

            textEditor.AlwaysOnTop = _alwaysOnTop;
            textEditor.Show();


            if (!standAloneEditor && newWindow)
            {

                if (!(_editorPosition.WinX == 0
                      && _editorPosition.WinY == 0
                      && _editorPosition.WinW == 0
                      && _editorPosition.WinH == 0))
                {
                    textEditor.Location = new Point(_editorPosition.WinX, _editorPosition.WinY);
                    textEditor.Width = _editorPosition.WinW;
                    textEditor.Height = _editorPosition.WinH;
                }

                textEditor.Closing += OnClosingEditor;
            }

            if (!fileLoaded)
            {
                textEditor.Text = "Failed to load " + fileName;
                return;
            }

            if (!standAloneEditor)
            {
                textEditor.Text = PreViewCaption + fileName;
            }
            else
            {
                textEditor.Text = fileName;
            }

            if (!textEditor.HighlightPathJson(jsonPath))
            {
                textEditor.HighlightText(sampleText);
            }
        }
        #endregion

        #region Utilities
        private static JsoncContentType GetFileTypeFromFileName(string fullFileName,
    IEnumerable<Form1.ContentTypeItem> fileTypes)
        {
            var shortFileName = GetShortFileName(fullFileName);

            return (from item in fileTypes where shortFileName.EndsWith(item.FileTypeMask) select item.FileType).FirstOrDefault();
        }

        private static string GetShortFileName(string longFileName)
        {
            if (string.IsNullOrEmpty(longFileName))
                return longFileName;

            var i = longFileName.LastIndexOf('\\');
            if (i < 0)
                return longFileName;

            if (i + 1 >= 0 && longFileName.Length > i + 1)
            {
                return longFileName.Substring(i + 1);
            }

            return longFileName;
        }

        // this is to get rid of exception on "dataGridView_examples.AutoSizeRowsMode" change
        [MethodImpl(MethodImplOptions.NoOptimization | MethodImplOptions.NoInlining)]
        private static void LoopThroughRows(DataGridView dgv)
        {
            var rows = dgv.Rows;
            var rowsCount = rows.Count;
            for (var i = rowsCount - 1; i >= 0; i--)
            {
                var row = rows[i];
            }
        }

        private static bool FilterCellOut(string cellValue, SearchItem searchParam)
        {
            if (cellValue == null || string.IsNullOrEmpty(cellValue))
                return true;

            if (searchParam == null || string.IsNullOrEmpty(searchParam.Value))
                return false;

            switch (searchParam.Condition)
            {
                case SearchItem.SearchCondition.Contains:
                    if (cellValue.IndexOf(searchParam.Value, searchParam.CaseSensitive) < 0)
                        return true;
                    break;
                case SearchItem.SearchCondition.StartsWith:
                    if (!cellValue.StartsWith(searchParam.Value, searchParam.CaseSensitive))
                        return true;
                    break;
                case SearchItem.SearchCondition.EndsWith:
                    if (!cellValue.EndsWith(searchParam.Value, searchParam.CaseSensitive))
                        return true;
                    break;
            }

            return false;
        }

        private static string ShortFileName(string longFileName)
        {
            var i = longFileName.LastIndexOf('\\');
            return i < 0 ? longFileName : longFileName.Substring(i + 1);
        }

        private void FlushLog()
        {
            if (_textLog.Length > 0)
            {
                Invoke((MethodInvoker)delegate
                {
                    textBox_logText.Text += _textLog.ToString();
                    _textLog.Clear();
                    textBox_logText.SelectionStart = textBox_logText.Text.Length;
                    textBox_logText.ScrollToCaret();
                });
            }
        }

        private void VsCodeOpenFile(string command)
        {
            var processInfo = new ProcessStartInfo("code", command)
            {
                CreateNoWindow = true,
                UseShellExecute = true,
                WindowStyle = ProcessWindowStyle.Hidden
            };

            try
            {
                Process.Start(processInfo);
            }
            catch (Exception ex)
            {
                textBox_logText.Text += ex.Message;
            }
        }

        private int GetLineNumberForPath(string fullFileName, string jsonPath)
        {
            string jsonStr;
            try
            {
                jsonStr = File.ReadAllText(fullFileName);
            }
            catch (Exception ex)
            {
                return 0;
            }

            if (string.IsNullOrEmpty(jsonStr))
            {
                return 0;
            }

            var parser = new JsonPathParser
            {
                TrimComplexValues = false,
                SaveComplexValues = false,
                RootName = "",
                JsonPathDivider = JsonPathDiv,
                SearchStartOnly = true
            };

            var startLine = 0;
            var property = parser.SearchJsonPath(jsonStr, JsonPathDiv + jsonPath);
            if (property != null)
                parser.GetLinesNumber(jsonStr, property.StartPosition, property.EndPosition, out startLine, out var _);

            return startLine;
        }

        #endregion

    }
}
