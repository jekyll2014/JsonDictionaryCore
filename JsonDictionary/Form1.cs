// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using JsonEditorForm;

using JsonPathParserLib;

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

using static JsonDictionaryCore.JsonIo;

namespace JsonDictionaryCore
{
    public partial class Form1 : Form
    {
        // pre-defined constants
        private readonly string[] _exampleGridColumnNames = { "Version", "Example", "File Name", "Json Path" };

        // global variables
        Config appConfig = new Config("appsettings.json");
        private readonly StringBuilder _textLog = new StringBuilder();
        private readonly DataTable _examplesTable;
        private TreeNode _rootNodeExamples = new TreeNode();
        private TreeNode _rootNodeLeftSchema = new TreeNode();
        private TreeNode _rootNodeRightSchema = new TreeNode();

        private Dictionary<string, List<JsonProperty>> _exampleLinkCollection =
            new Dictionary<string, List<JsonProperty>>(); // tree path, list of examples

        private volatile bool _isDoubleClick;
        private JsonViewer _sideViewer;
        private WinPosition _editorPosition;
        private Dictionary<string, string> _nodeDescription;
        private ISchemaTreeBase _rightSchema;
        private ISchemaTreeBase _leftSchema;
        private UserControl _rightDataPanel;
        private UserControl _leftDataPanel;

        // last used values for UI processing optimization
        private TreeNode _lastSelectedExamplesNode;
        private TreeNode _lastSelectedLeftSchemaNode;
        private TreeNode _lastSelectedRightSchemaNode;
        private Color _lastSelectedLeftSchemaNodeColor;
        private Color _lastSelectedRightSchemaNodeColor;
        private TreeView _lastTreeViewSelected;
        private string _lastSearchString = "";
        private int _searchListPositionLeft = -1;
        private List<string> _lastSearchListLeft;
        private int _searchListPositionRight = -1;
        private List<string> _lastSearchListRight;

        private List<SearchItem> _lastSearchList = new List<SearchItem>();

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
            FormCaption = appConfig.ConfigStorage.DefaultEditorFormCaption;
            checkBox_beautifyJson.Checked = appConfig.ConfigStorage.BeautifyJson;
            checkBox_reformatJsonBrackets.Checked = appConfig.ConfigStorage.ReformatJson;
            checkBox_showPreview.Checked = appConfig.ConfigStorage.ShowPreview;
            checkBox_alwaysOnTop.Checked = appConfig.ConfigStorage.AlwaysOnTop;
            checkBox_loadDbOnStart.Checked = appConfig.ConfigStorage.LoadDbOnStart;
            checkBox_vsCode.Checked = appConfig.ConfigStorage.UseVsCode;
            checkBox_schemaSelectionSync.Checked = appConfig.ConfigStorage.SchemaFollowSelection;
            folderBrowserDialog1.SelectedPath = appConfig.ConfigStorage.LastRootFolder;

            _nodeDescription = LoadJson<Dictionary<string, string>>(appConfig.ConfigStorage.DefaultDescriptionFileName);
            if (_nodeDescription == null)
                _nodeDescription = new Dictionary<string, string>();

            if (appConfig.ConfigStorage.MainWindowPosition.Initialized)
            {
                this.Location = new Point
                {
                    X = appConfig.ConfigStorage.MainWindowPosition.WinX,
                    Y = appConfig.ConfigStorage.MainWindowPosition.WinY
                };
                this.Width = appConfig.ConfigStorage.MainWindowPosition.WinW;
                this.Height = appConfig.ConfigStorage.MainWindowPosition.WinH;
            }

            if (appConfig.ConfigStorage.EditorPosition.Initialized)
                _editorPosition = appConfig.ConfigStorage.EditorPosition;

            TopMost = appConfig.ConfigStorage.AlwaysOnTop;

            comboBox_ExCondition.Items.AddRange(typeof(SearchItem.SearchCondition).GetEnumNames());
            comboBox_ExCondition.SelectedIndex = 0;

            _examplesTable = new DataTable("Examples");
            for (var i = 0; i < _exampleGridColumnNames.Length; i++)
            {
                _examplesTable.Columns.Add(_exampleGridColumnNames[i]);
            }

            dataGridView_examples.DataError += delegate { };
            dataGridView_examples.DataSource = _examplesTable;

            comboBox_ExVersions.SelectedIndexChanged -= ComboBox_ExVersions_SelectedIndexChanged;
            comboBox_ExVersions.Items.Clear();
            comboBox_ExVersions.Items.Add(appConfig.ConfigStorage.DefaultVersionCaption);
            comboBox_ExVersions.SelectedIndex = 0;
            comboBox_ExVersions.SelectedIndexChanged += ComboBox_ExVersions_SelectedIndexChanged;

            if (appConfig.ConfigStorage.LoadDbOnStart)
            {
                LoadDb(appConfig.ConfigStorage.LastDbName).ConfigureAwait(true);
                GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);
            }
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            splitContainer_tree.SplitterDistance = appConfig.ConfigStorage.TreeSplitterDistance;
            splitContainer_description.SplitterDistance = appConfig.ConfigStorage.DescriptionSplitterDistance;
            splitContainer_fileList.SplitterDistance = appConfig.ConfigStorage.FileListSplitterDistance;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveJson(_nodeDescription, appConfig.ConfigStorage.DefaultDescriptionFileName, true);

            appConfig.ConfigStorage.MainWindowPosition = new WinPosition()
            {
                WinX = Location.X,
                WinY = Location.Y,
                WinW = Width,
                WinH = Height,
            };

            appConfig.ConfigStorage.EditorPosition = _editorPosition;
            appConfig.ConfigStorage.LastRootFolder = folderBrowserDialog1.SelectedPath;
            appConfig.ConfigStorage.TreeSplitterDistance = splitContainer_tree.SplitterDistance;
            appConfig.ConfigStorage.DescriptionSplitterDistance = splitContainer_description.SplitterDistance;
            appConfig.ConfigStorage.FileListSplitterDistance = splitContainer_fileList.SplitterDistance;
            appConfig.SaveConfig();
        }

        #endregion

        #region Main page

        private void Button_loadDb_Click(object sender, EventArgs e)
        {
            this.openFileDialog1.FileOk -= this.OpenFileDialog1_FileOk;
            this.openFileDialog1.FileOk -= this.OpenFileDialog1_FileOk_Schema;
            this.openFileDialog1.FileOk += new CancelEventHandler(this.OpenFileDialog1_FileOk);

            openFileDialog1.FileName = "";
            openFileDialog1.Title = "Open " + appConfig.ConfigStorage.DefaultFiledialogFormCaption;
            openFileDialog1.DefaultExt = appConfig.ConfigStorage.DefaultTreeFileExtension;
            openFileDialog1.Filter =
                "Binary files|*." + appConfig.ConfigStorage.DefaultTreeFileExtension + "|All files|*.*";
            openFileDialog1.ShowDialog();
        }

        private async void OpenFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            ActivateUiControls(false);
            if (await LoadDb(openFileDialog1.FileName).ConfigureAwait(true))
            {
                FormCaption = appConfig.ConfigStorage.DefaultEditorFormCaption + " " +
                              GetShortFileName(openFileDialog1.FileName);
                tabControl1.SelectedTab = tabControl1.TabPages[1];
                appConfig.ConfigStorage.LastDbName = openFileDialog1.FileName;
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
                var jsonPropertiesCollection = RunFileCollection(startPath, appConfig.ConfigStorage.FileMask);
                Invoke((MethodInvoker)delegate
               {
                   endTime = DateTime.Now;
                   _textLog.AppendLine("Collection time: " + endTime.Subtract(startOperationTime).TotalSeconds);
                   startOperationTime = DateTime.Now;
                   FlushLog();
                   toolStripStatusLabel1.Text = "Processing events collection";
               });

                Parallel.ForEach(appConfig.ConfigStorage.FlattenParameters, param =>
                //foreach (var param in appConfig.ConfigStorage.FlattenParameters)
                {
                    Invoke((MethodInvoker)delegate
                   {
                       startOperationTime = DateTime.Now;
                       FlushLog();
                       toolStripStatusLabel1.Text = "Processing " + param.ContentType + " collection";
                   });

                    FlattenCollection(jsonPropertiesCollection, param.ContentType, param.ItemName, param.MoveToPath, param.ParentNames);

                    Invoke((MethodInvoker)delegate
                   {
                       endTime = DateTime.Now;
                       _textLog.AppendLine(param.ContentType + " processing time: " +
                                           endTime.Subtract(startOperationTime).TotalSeconds);
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
            saveFileDialog1.Title = "Save " + appConfig.ConfigStorage.DefaultFiledialogFormCaption;
            saveFileDialog1.DefaultExt = appConfig.ConfigStorage.DefaultTreeFileExtension;
            saveFileDialog1.Filter =
                "Binary files|*." + appConfig.ConfigStorage.DefaultTreeFileExtension + "|All files|*.*";
            saveFileDialog1.FileName =
                appConfig.ConfigStorage.DefaultSaveFileName + DateTime.Today.ToShortDateString().Replace("/", "_") +
                "." + appConfig.ConfigStorage.DefaultTreeFileExtension;

            this.saveFileDialog1.FileOk -= new System.ComponentModel.CancelEventHandler(this.SaveFileDialog1_FileOk);
            this.saveFileDialog1.FileOk -= new System.ComponentModel.CancelEventHandler(this.SaveFileDialog1_FileOk_LeftSchema);
            this.saveFileDialog1.FileOk -= new System.ComponentModel.CancelEventHandler(this.SaveFileDialog1_FileOk_RightSchema);
            this.saveFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.SaveFileDialog1_FileOk);

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
                    var treeFile = Path.ChangeExtension(saveFileDialog1.FileName,
                        appConfig.ConfigStorage.DefaultTreeFileExtension);
                    var examplesFile = Path.ChangeExtension(saveFileDialog1.FileName,
                        appConfig.ConfigStorage.DefaultExamplesFileExtension);
                    var m = new CustomTreeNode(_rootNodeExamples);
                    File.WriteAllBytes(treeFile, m.MessagePack);
                    SaveBinary(_exampleLinkCollection, examplesFile);
                    appConfig.ConfigStorage.LastDbName = saveFileDialog1.FileName;

                    if (appConfig.ConfigStorage.SaveJsonTree)
                    {
                        File.WriteAllText(treeFile + ".json", m.GetJsonTree(_rootNodeExamples));
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("File write exception [" + saveFileDialog1.FileName + "]: " + ex.Message);
                }
            }).ContinueWith((t) => { toolStripStatusLabel1.Text = ""; }).ConfigureAwait(false);
            GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);
        }

        private void CheckBox_beautifyJson_CheckedChanged(object sender, EventArgs e)
        {
            checkBox_reformatJsonBrackets.Enabled = appConfig.ConfigStorage.BeautifyJson = checkBox_beautifyJson.Checked;
        }

        private void CheckBox_reformatJsonBrackets_CheckedChanged(object sender, EventArgs e)
        {
            appConfig.ConfigStorage.ReformatJson = checkBox_reformatJsonBrackets.Checked;
        }

        private void CheckBox_reformatJsonBrackets_EnabledChanged(object sender, EventArgs e)
        {
            if (!checkBox_reformatJsonBrackets.Enabled) checkBox_reformatJsonBrackets.Checked = false;
        }

        private void CheckBox_showPreview_CheckedChanged(object sender, EventArgs e)
        {
            appConfig.ConfigStorage.ShowPreview = checkBox_showPreview.Checked;
        }

        private void CheckBox_alwaysOnTop_CheckedChanged(object sender, EventArgs e)
        {
            appConfig.ConfigStorage.AlwaysOnTop = checkBox_alwaysOnTop.Checked;
            TopMost = appConfig.ConfigStorage.AlwaysOnTop;
        }

        private void CheckBox_loadDbOnStart_CheckedChanged(object sender, EventArgs e)
        {
            appConfig.ConfigStorage.LoadDbOnStart = checkBox_loadDbOnStart.Checked;
        }

        private void CheckBox_vsCode_CheckedChanged(object sender, EventArgs e)
        {
            appConfig.ConfigStorage.UseVsCode = checkBox_vsCode.Checked;
        }

        private void CheckBox_schemaSelectionSync_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is CheckBox cb)
                appConfig.ConfigStorage.SchemaFollowSelection = cb.Checked;
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
            if (e == null) return;

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
                _lastSearchList.Clear();
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

            if (MessageBox.Show("Are you sure to remove the selected node?", "Remove node", MessageBoxButtons.YesNo) !=
                DialogResult.Yes)
                return;

            ActivateUiControls(false);

            var records = _exampleLinkCollection.Where(n => n.Key.StartsWith(treeView_examples.SelectedNode.Name))
                .Select(n => n.Key).ToArray();
            for (var i = 0; i < records.Length; i++)
            {
                _exampleLinkCollection.Remove(records[i]);
            }

            treeView_examples.Nodes.Remove(treeView_examples.SelectedNode);
            ActivateUiControls(true);
            _lastSearchList.Clear();
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

                var fileNames = dataGrid.Rows[e.RowIndex].Cells[2]?.Value?.ToString().Split(
                    new[] { appConfig.ConfigStorage.TableListDelimiter }, StringSplitOptions.RemoveEmptyEntries);

                this.listBox_fileList.SelectedValueChanged -= this.ListBox_fileList_SelectedValueChanged;

                listBox_fileList.Items.Clear();
                listBox_fileList.Items.AddRange(fileNames ?? Array.Empty<string>());
                if (fileNames?.Length > 0)
                    listBox_fileList.SetSelected(0, true);

                this.listBox_fileList.SelectedValueChanged += this.ListBox_fileList_SelectedValueChanged;

                if (appConfig.ConfigStorage.ShowPreview)
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
            if (dataGridView_examples == null) return;

            if (dataGridView_examples.SelectedCells.Count == 1)
            {
                Clipboard.SetText(dataGridView_examples.SelectedCells[0].Value.ToString());
            }
            else
                Clipboard.SetText(dataGridView_examples.SelectedCells.ToString());
        }

        // doesn't work. need tree path to find all samples. Change ListToTree to add actual paths
        private void DeleteSamples()
        {
            if (MessageBox.Show("Do you want to delete all selected samples for all files?", "Delete samples",
                MessageBoxButtons.YesNo) == DialogResult.No)
                return;

            var cellRowList = new List<int>();
            foreach (var cell in dataGridView_examples.SelectedCells)
            {
                cellRowList.Add(((DataGridViewCell)cell).RowIndex);
            }

            var rowList = cellRowList.Distinct().ToArray();
            foreach (var rowNumber in rowList)
            {
                var jsonPaths = dataGridView_examples.Rows[rowNumber].Cells[3]?.Value?.ToString()
                    .Split(new[] { appConfig.ConfigStorage.TableListDelimiter }, StringSplitOptions.RemoveEmptyEntries);
                var jsonSample = dataGridView_examples.Rows[rowNumber].Cells[1]?.Value?.ToString();

                if (jsonPaths?.Length > 0)
                {
                    var jsonPath = appConfig.ConfigStorage.JsonPathDiv + jsonPaths?[0];
                    var samplesCollection = _exampleLinkCollection.Where(n => n.Key == jsonPath);
                    samplesCollection.FirstOrDefault().Value.RemoveAll(n => n.Value == CompactJson(jsonSample));
                }
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
            if (!appConfig.ConfigStorage.ShowPreview)
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
            if (listBox_fileList == null) return;

            Clipboard.SetText(listBox_fileList.SelectedItem.ToString());
        }

        // doesn't work. need tree path to find all samples. Change ListToTree to add actual paths
        private void DeleteSampleForFile()
        {
            if (MessageBox.Show("Do you want to delete all selected samples for all files?", "Delete samples",
                MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;

            var jsonPaths = dataGridView_examples.Rows[dataGridView_examples.SelectedCells[0].RowIndex].Cells[3]?.Value
                ?.ToString().Split(new[] { appConfig.ConfigStorage.TableListDelimiter },
                    StringSplitOptions.RemoveEmptyEntries);
            var jsonSample = dataGridView_examples.Rows[dataGridView_examples.SelectedCells[0].RowIndex].Cells[3]?.Value
                ?.ToString();
            var fileNumber = listBox_fileList.SelectedIndex;
            var fileName = listBox_fileList.Items[fileNumber].ToString();
            var jsonPath = "";

            if (jsonPaths?.Length >= fileNumber)
            {
                jsonPath = jsonPaths?[fileNumber];
            }

            _exampleLinkCollection[jsonPath].RemoveAll(n => n.FullFileName == fileName);
            listBox_fileList.Items.RemoveAt(fileNumber);
        }

        #endregion

        #region Schema

        private void Button_loadSchema_Click(object sender, EventArgs e)
        {
            this.openFileDialog1.FileOk -= this.OpenFileDialog1_FileOk;
            this.openFileDialog1.FileOk -= this.OpenFileDialog1_FileOk_Schema;
            this.openFileDialog1.FileOk += new CancelEventHandler(this.OpenFileDialog1_FileOk_Schema);

            openFileDialog1.FileName = "";
            openFileDialog1.Title = "Open JSON schema";
            openFileDialog1.DefaultExt = appConfig.ConfigStorage.DefaultTreeFileExtension;
            openFileDialog1.Filter = "JSON files|*.json|All files|*.*";
            openFileDialog1.ShowDialog();
        }

        private void OpenFileDialog1_FileOk_Schema(object sender, CancelEventArgs e)
        {
            var rootName = "#";

            var schemaData = File.ReadAllText(openFileDialog1.FileName);

            var parser = new JsonPathParser
            {
                TrimComplexValues = false,
                SaveComplexValues = true,
                RootName = rootName,
                JsonPathDivider = appConfig.ConfigStorage.JsonPathDiv
            };

            var schemaProperties = parser.ParseJsonToPathList(schemaData, out var endPos, out var errorFound)
                .Where(n =>
                    n.JsonPropertyType == JsonPropertyTypes.Array
                    || n.JsonPropertyType == JsonPropertyTypes.Object
                    || n.JsonPropertyType == JsonPropertyTypes.Property
                    || n.JsonPropertyType == JsonPropertyTypes.ArrayValue
                    || n.JsonPropertyType == JsonPropertyTypes.KeywordOrNumberProperty);

            var treeSchemaProperties = parser.ConvertForTreeProcessing(schemaProperties);
            _rightSchema = JsonPropertyListToSchemaObject(treeSchemaProperties, rootName, rootName);
            _rootNodeRightSchema = ConvertSchemaNodesToTreeNode(_rightSchema);
            treeView_rightSchema.Nodes.Clear();
            treeView_rightSchema.Nodes.Add(_rootNodeRightSchema);
            treeView_rightSchema.Sort();
            if (_rightDataPanel != null) splitContainer_schemaRight.Panel2.Controls.Remove(_rightDataPanel);

            GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);
        }

        private void SplitContainer_schemaRight_SplitterMoved(object sender, SplitterEventArgs e)
        {
            splitContainer_schemaLeft.SplitterDistance = splitContainer_schemaRight.SplitterDistance;
        }

        private void SplitContainer_schemaLeft_SplitterMoved(object sender, SplitterEventArgs e)
        {
            splitContainer_schemaRight.SplitterDistance = splitContainer_schemaLeft.SplitterDistance;
        }

        private void TreeView_leftSchema_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e == null || e.Node == null) return;

            if (_leftDataPanel != null)
            {
                if (_rightDataPanel is PropertyDataPanel dp) dp.OnRefClick -= FollowTheRefLinkLeft;
                else if (_rightDataPanel is ObjectDataPanel dp1) dp1.OnRefClick += FollowTheRefLinkLeft;
                else if (_rightDataPanel is ArrayDataPanel dp2) dp2.OnRefClick += FollowTheRefLinkLeft;
                splitContainer_schemaLeft.Panel2.Controls.Remove(_leftDataPanel);
            }

            var nodePath = e.Node.FullPath.Replace("{", "").Replace("}", "").Replace("[", "").Replace("]", "");
            var node = FindDefinition(nodePath, _leftSchema);

            if (node == null) return;

            if (node is SchemaTreeProperty schemaProperty)
            {
                _leftDataPanel = new PropertyDataPanel(schemaProperty, nodePath);
            }
            else if (node is SchemaTreeObject schemaObject)
            {
                _leftDataPanel = new ObjectDataPanel(schemaObject, nodePath);
            }
            else if (node is SchemaTreeArray schemaArray)
            {
                _leftDataPanel = new ArrayDataPanel(schemaArray, nodePath);
            }

            if (_leftDataPanel != null)
            {
                _leftDataPanel.Dock = DockStyle.Fill;
                splitContainer_schemaLeft.Panel2.Controls.Add(_leftDataPanel);
                if (_rightDataPanel is PropertyDataPanel dp) dp.OnRefClick += FollowTheRefLinkLeft;
                else if (_rightDataPanel is ObjectDataPanel dp1) dp1.OnRefClick += FollowTheRefLinkLeft;
                else if (_rightDataPanel is ArrayDataPanel dp2) dp2.OnRefClick += FollowTheRefLinkLeft;
            }

            SelectSchemaNode(e.Node.Name, true);

            if (_lastSelectedLeftSchemaNode != null)
            {
                _lastSelectedLeftSchemaNode.BackColor = _lastSelectedLeftSchemaNodeColor;
            }

            _lastSelectedLeftSchemaNode = e.Node;
            _lastSelectedLeftSchemaNodeColor = e.Node.BackColor;
            e.Node.BackColor = Color.DodgerBlue;
        }

        private void FollowTheRefLinkLeft(object sender, RefLinkClickEventArgs e)
        {
            var treeNode = FindTreeNodeByPath(e.LinkText.Split('/').ToList(), _rootNodeLeftSchema);
            treeView_leftSchema.SelectedNode = treeNode;
            treeView_leftSchema.SelectedNode?.Expand();
        }

        private void TreeView_rightSchema_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e == null || e.Node == null) return;

            if (_rightDataPanel != null)
            {
                if (_rightDataPanel is PropertyDataPanel dp) dp.OnRefClick -= FollowTheRefLinkRight;
                else if (_rightDataPanel is ObjectDataPanel dp1) dp1.OnRefClick += FollowTheRefLinkRight;
                else if (_rightDataPanel is ArrayDataPanel dp2) dp2.OnRefClick += FollowTheRefLinkRight;
                splitContainer_schemaRight.Panel2.Controls.Remove(_rightDataPanel);
            }

            var nodePath = e.Node.FullPath.Replace("{", "").Replace("}", "").Replace("[", "").Replace("]", "");
            var node = FindDefinition(nodePath, _rightSchema);

            if (node == null) return;

            if (node is SchemaTreeProperty schemaProperty)
            {
                _rightDataPanel = new PropertyDataPanel(schemaProperty, nodePath);
            }
            else if (node is SchemaTreeObject schemaObject)
            {
                _rightDataPanel = new ObjectDataPanel(schemaObject, nodePath);
            }
            else if (node is SchemaTreeArray schemaArray)
            {
                _rightDataPanel = new ArrayDataPanel(schemaArray, nodePath);
            }

            if (_rightDataPanel != null)
            {
                _rightDataPanel.Dock = DockStyle.Fill;
                splitContainer_schemaRight.Panel2.Controls.Add(_rightDataPanel);
                if (_rightDataPanel is PropertyDataPanel dp) dp.OnRefClick += FollowTheRefLinkRight;
                else if (_rightDataPanel is ObjectDataPanel dp1) dp1.OnRefClick += FollowTheRefLinkRight;
                else if (_rightDataPanel is ArrayDataPanel dp2) dp2.OnRefClick += FollowTheRefLinkRight;
            }

            SelectSchemaNode(e.Node.Name, false);

            if (_lastSelectedRightSchemaNode != null)
            {
                _lastSelectedRightSchemaNode.BackColor = _lastSelectedRightSchemaNodeColor;
            }

            _lastSelectedRightSchemaNode = e.Node;
            _lastSelectedRightSchemaNodeColor = e.Node.BackColor;
            e.Node.BackColor = Color.DodgerBlue;
        }

        private void FollowTheRefLinkRight(object sender, RefLinkClickEventArgs e)
        {
            var treeNode = FindTreeNodeByPath(e.LinkText.Split('/').ToList(), _rootNodeRightSchema);
            treeView_rightSchema.SelectedNode = treeNode;
            treeView_rightSchema.SelectedNode?.Expand();
        }

        private void Button_generateSchema_Click(object sender, EventArgs e)
        {
            if (treeView_examples.SelectedNode == null)
                return;

            var rootName = "#";
            _leftSchema = GenerateSchemaFromTree(treeView_examples.SelectedNode, _exampleLinkCollection, rootName);
            _rootNodeLeftSchema = ConvertSchemaNodesToTreeNode(_leftSchema);

            if (_rootNodeLeftSchema == null)
                return;

            treeView_leftSchema.Nodes.Clear();
            treeView_leftSchema.Nodes.Add(_rootNodeLeftSchema);
            treeView_leftSchema.Sort();

            if (_leftDataPanel != null)
                splitContainer_schemaRight.Panel2.Controls.Remove(_leftDataPanel);

            GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);
        }

        private void SelectSchemaNode(string path, bool toRightPanel)
        {
            if (!appConfig.ConfigStorage.SchemaFollowSelection || string.IsNullOrEmpty(path)) return;

            if (toRightPanel)
            {
                var nodes = treeView_rightSchema.Nodes.Find(path, true);
                if (nodes.Any())
                {
                    treeView_rightSchema.SelectedNode = nodes.First();
                    ComparePanels();
                }
            }
            else
            {
                var nodes = treeView_leftSchema.Nodes.Find(path, true);
                if (nodes.Any())
                {
                    treeView_leftSchema.SelectedNode = nodes.First();
                }
            }
        }

        private void Button_saveLeftSchema_Click(object sender, EventArgs e)
        {
            this.saveFileDialog1.FileOk -= new System.ComponentModel.CancelEventHandler(this.SaveFileDialog1_FileOk);
            this.saveFileDialog1.FileOk -= new System.ComponentModel.CancelEventHandler(this.SaveFileDialog1_FileOk_LeftSchema);
            this.saveFileDialog1.FileOk -= new System.ComponentModel.CancelEventHandler(this.SaveFileDialog1_FileOk_RightSchema);
            this.saveFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.SaveFileDialog1_FileOk_LeftSchema);

            saveFileDialog1.Title = "Save schema";
            saveFileDialog1.DefaultExt = "json";
            saveFileDialog1.Filter =
                "JSON files|*.json|All files|*.*";
            saveFileDialog1.FileName = "Schema_"
                + appConfig.ConfigStorage.DefaultSaveFileName
                + DateTime.Today.ToShortDateString().Replace("/", "_")
                + ".json";

            saveFileDialog1.ShowDialog();
        }

        private void SaveFileDialog1_FileOk_LeftSchema(object sender, CancelEventArgs e)
        {
            if (_leftSchema == null || string.IsNullOrEmpty(_leftSchema.ToJson()))
                return;

            File.WriteAllText(saveFileDialog1.FileName, ReformatJson(_leftSchema.ToJson(), Newtonsoft.Json.Formatting.Indented));

            GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);
        }

        private void Button_saveRightSchema_Click(object sender, EventArgs e)
        {
            this.saveFileDialog1.FileOk -= new System.ComponentModel.CancelEventHandler(this.SaveFileDialog1_FileOk);
            this.saveFileDialog1.FileOk -= new System.ComponentModel.CancelEventHandler(this.SaveFileDialog1_FileOk_LeftSchema);
            this.saveFileDialog1.FileOk -= new System.ComponentModel.CancelEventHandler(this.SaveFileDialog1_FileOk_RightSchema);
            this.saveFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.SaveFileDialog1_FileOk_RightSchema);

            saveFileDialog1.Title = "Save schema";
            saveFileDialog1.DefaultExt = "json";
            saveFileDialog1.Filter =
                "JSON files|*.json|All files|*.*";
            saveFileDialog1.FileName = "Schema_"
                + appConfig.ConfigStorage.DefaultSaveFileName
                + DateTime.Today.ToShortDateString().Replace("/", "_")
                + ".json";

            saveFileDialog1.ShowDialog();
        }

        private void SaveFileDialog1_FileOk_RightSchema(object sender, CancelEventArgs e)
        {
            if (_rightSchema == null || string.IsNullOrEmpty(_rightSchema.ToJson()))
                return;

            File.WriteAllText(saveFileDialog1.FileName, ReformatJson(_rightSchema.ToJson(), Newtonsoft.Json.Formatting.Indented));

            GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);
        }

        private void Button_compare_Click(object sender, EventArgs e)
        {
            if (_rootNodeLeftSchema == null || _rootNodeRightSchema == null) return;

            var deep = checkBox_deepCompare.Checked;
            var result = CompareNode(_rootNodeLeftSchema, _rootNodeRightSchema, deep);

            // find missing nodes in left tree
            treeView_leftSchema.AfterSelect -= new TreeViewEventHandler(this.TreeView_leftSchema_AfterSelect);
            foreach (var nodePath in result)
            {
                var missingNode = _rootNodeLeftSchema.Nodes.Find(nodePath, true).FirstOrDefault();
                missingNode.BackColor = Color.Red;
                treeView_leftSchema.SelectedNode = missingNode;
            }
            treeView_leftSchema.AfterSelect += new TreeViewEventHandler(this.TreeView_leftSchema_AfterSelect);

            // find missing nodes in right tree
            result = CompareNode(_rootNodeRightSchema, _rootNodeLeftSchema, deep);
            treeView_rightSchema.AfterSelect -= new TreeViewEventHandler(this.TreeView_rightSchema_AfterSelect);
            foreach (var nodePath in result)
            {
                var missingNode = _rootNodeRightSchema.Nodes.Find(nodePath, true).FirstOrDefault();
                missingNode.BackColor = Color.Red;
                treeView_rightSchema.SelectedNode = missingNode;
            }
            treeView_rightSchema.AfterSelect += new TreeViewEventHandler(this.TreeView_rightSchema_AfterSelect);
        }

        private List<string> CompareNode(TreeNode leftNode, TreeNode rightNode, bool deepCompare = false)
        {
            var result = new List<string>();

            if (leftNode == null || rightNode == null) return result;

            foreach (TreeNode node in leftNode.Nodes)
            {
                var notSame = false;
                if (!rightNode.Nodes.ContainsKey(node.Name)) notSame = true;
                else if (deepCompare)
                {
                    var nodePath = node.FullPath.Replace("{", "").Replace("}", "").Replace("[", "").Replace("]", "");
                    var leftSchemaNode = FindDefinition(nodePath, _leftSchema);

                    nodePath = rightNode.Nodes[node.Name].FullPath.Replace("{", "").Replace("}", "").Replace("[", "").Replace("]", "");
                    var rightSchemaNode = FindDefinition(nodePath, _rightSchema);

                    if (!(leftSchemaNode == null && rightSchemaNode == null))
                    {
                        if (leftSchemaNode == null || rightSchemaNode == null) notSame = true;
                        else if (leftSchemaNode.Name != rightSchemaNode.Name) notSame = true;
                        else if (leftSchemaNode.Path != rightSchemaNode.Path) notSame = true;
                        else if (leftSchemaNode.Description != rightSchemaNode.Description) notSame = true;
                        else if (leftSchemaNode.Reference != rightSchemaNode.Reference) notSame = true;
                        else
                        {
                            if (leftSchemaNode.Type.Count == rightSchemaNode.Type.Count)
                            {
                                foreach (var t1 in leftSchemaNode.Type)
                                {
                                    if (!rightSchemaNode.Type.Contains(t1))
                                    {
                                        notSame = true;
                                        break;
                                    }
                                }
                            }
                            else notSame = true;

                            /*if (!notSame && leftSchemaNode.Examples.Count == rightSchemaNode.Examples.Count)
                            {
                                foreach (var t1 in leftSchemaNode.Examples)
                                {
                                    if (!rightSchemaNode.Examples.Contains(t1))
                                    {
                                        notSame = true;
                                        break;
                                    }
                                }
                            }
                            else notSame = true;*/
                        }

                        // check specific properties
                        if (!notSame && leftSchemaNode is SchemaTreeProperty lSchemaProperty && rightSchemaNode is SchemaTreeProperty rSchemaProperty)
                        {
                            if (lSchemaProperty.Default != rSchemaProperty.Default) notSame = true;
                            else if (lSchemaProperty.Pattern != rSchemaProperty.Pattern) notSame = true;
                            else if (lSchemaProperty.Enum.Count == rSchemaProperty.Enum.Count)
                            {
                                foreach (var t1 in lSchemaProperty.Enum)
                                {
                                    if (!rSchemaProperty.Enum.Contains(t1))
                                    {
                                        notSame = true;
                                        break;
                                    }
                                }
                            }
                            else notSame = true;
                        }
                        else if (!notSame && leftSchemaNode is SchemaTreeObject lSchemaObject && rightSchemaNode is SchemaTreeObject rSchemaObject)
                        {
                            if (lSchemaObject.AdditionalProperties != rSchemaObject.AdditionalProperties) notSame = true;
                            else
                            {
                                if (lSchemaObject.Required.Count == rSchemaObject.Required.Count)
                                {
                                    foreach (var t1 in lSchemaObject.Required)
                                    {
                                        if (!rSchemaObject.Required.Contains(t1))
                                        {
                                            notSame = true;
                                            break;
                                        }
                                    }
                                }
                                else notSame = true;

                                if (lSchemaObject.Properties.Count != rSchemaObject.Properties.Count) notSame = true;
                            }
                        }
                        else if (!notSame && leftSchemaNode is SchemaTreeArray lSchemaArray && rightSchemaNode is SchemaTreeArray rSchemaArray)
                        {
                            if (lSchemaArray.UniqueItemsOnly != rSchemaArray.UniqueItemsOnly) notSame = true;
                            else if (lSchemaArray.Items == null && rSchemaArray.Items != null
                                || lSchemaArray.Items != null && rSchemaArray.Items == null) notSame = true;
                        }
                    }
                }

                if (notSame)
                    result.Add(node.Name);

                var res = CompareNode(node, rightNode.Nodes[node.Name], deepCompare);
                result.AddRange(res);
            }

            return result;
        }

        private void TextBox_find_Leave(object sender, EventArgs e)
        {
            if (textBox_find.Text != _lastSearchString)
            {
                _lastSearchString = textBox_find.Text;
                _searchListPositionLeft = -1;
                _lastSearchListLeft = new List<string>();
                _searchListPositionRight = -1;
                _lastSearchListRight = new List<string>();
            }
        }

        private void Button_findNext_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(_lastSearchString)
                && (_lastSearchListLeft == null || _lastSearchListLeft.Count <= 0
                || _lastSearchListRight == null || _lastSearchListRight.Count <= 0))
                SearchInTrees(_lastSearchString);

            if (_lastTreeViewSelected != null)
            {
                if (_lastTreeViewSelected.Name.Contains("left", StringComparison.InvariantCultureIgnoreCase))
                {
                    if (_lastSearchListLeft == null || _lastSearchListLeft.Count <= 0) return;

                    _searchListPositionLeft++;
                    if (_searchListPositionLeft >= _lastSearchListLeft.Count)
                        _searchListPositionLeft = 0;

                    var nodeToSelect = _lastTreeViewSelected.Nodes.Find(_lastSearchListLeft[_searchListPositionLeft], true)?.FirstOrDefault();
                    if (nodeToSelect != null)
                    {
                        _lastTreeViewSelected.SelectedNode = nodeToSelect;
                    }
                }
                else
                {
                    if (_lastSearchListRight == null || _lastSearchListRight.Count <= 0) return;

                    _searchListPositionRight++;
                    if (_searchListPositionRight >= _lastSearchListRight.Count)
                        _searchListPositionRight = 0;

                    var nodeToSelect = _lastTreeViewSelected.Nodes.Find(_lastSearchListRight[_searchListPositionRight], true)?.FirstOrDefault();
                    if (nodeToSelect != null)
                    {
                        _lastTreeViewSelected.SelectedNode = nodeToSelect;
                    }
                }
            }
        }

        private void Button_findPrev_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(_lastSearchString)
                && (_lastSearchListLeft == null || _lastSearchListLeft.Count <= 0
                || _lastSearchListRight == null || _lastSearchListRight.Count <= 0))
                SearchInTrees(_lastSearchString);

            if (_lastTreeViewSelected != null)
            {
                if (_lastTreeViewSelected.Name.Contains("left", StringComparison.InvariantCultureIgnoreCase))
                {
                    if (_lastSearchListLeft == null || _lastSearchListLeft.Count <= 0) return;

                    _searchListPositionLeft--;
                    if (_searchListPositionLeft < 0)
                        _searchListPositionLeft = _lastSearchListLeft.Count - 1;

                    var nodeToSelect = _lastTreeViewSelected.Nodes.Find(_lastSearchListLeft[_searchListPositionLeft], true)?.FirstOrDefault();
                    if (nodeToSelect != null)
                    {
                        _lastTreeViewSelected.SelectedNode = nodeToSelect;
                    }
                }
                else
                {
                    if (_lastSearchListRight == null || _lastSearchListRight.Count <= 0) return;

                    _searchListPositionRight--;
                    if (_searchListPositionRight < 0)
                        _searchListPositionRight = _lastSearchListRight.Count - 1;

                    var nodeToSelect = _lastTreeViewSelected.Nodes.Find(_lastSearchListRight[_searchListPositionRight], true)?.FirstOrDefault();
                    if (nodeToSelect != null)
                    {
                        _lastTreeViewSelected.SelectedNode = nodeToSelect;
                    }
                }
            }
        }

        private void SearchInTrees(string token)
        {
            if (_rootNodeLeftSchema != null && _rootNodeRightSchema != null)
            {
                _lastSearchListLeft = FindNode(_rootNodeLeftSchema, token);
                _lastSearchListRight = FindNode(_rootNodeRightSchema, token);
            }
        }

        private List<string> FindNode(TreeNode rootNode, string token)
        {
            var result = new List<string>();
            if (rootNode.Text.Contains(token))
                result.Add(rootNode.Name);

            foreach (TreeNode node in rootNode.Nodes)
                result.AddRange(FindNode(node, token));

            return result;
        }

        private void Button_clearCompare_Click(object sender, EventArgs e)
        {
            ClearBackground(_rootNodeLeftSchema);
            ClearBackground(_rootNodeRightSchema);
        }

        private void ClearBackground(TreeNode rootNode)
        {
            rootNode.BackColor = Color.Empty;

            foreach (TreeNode node in rootNode.Nodes)
                ClearBackground(node);
        }

        private void TreeView_rightSchema_Enter(object sender, EventArgs e)
        {
            _lastTreeViewSelected = treeView_rightSchema;
        }

        private void TreeView_leftSchema_Enter(object sender, EventArgs e)
        {
            _lastTreeViewSelected = treeView_leftSchema;
        }

        private void Button_compareNode_Click(object sender, EventArgs e)
        {
            ComparePanels();
        }

        private void ComparePanels()
        {
            if (_leftDataPanel is PropertyDataPanel dpl && _rightDataPanel is PropertyDataPanel dpr)
            {
                if (dpl.ObjectPathText != dpr.ObjectPathText)
                {
                    dpl.ObjectPathBackColor = dpr.ObjectPathBackColor = Color.LightPink;
                }
                if (dpl.ObjectTypeText != dpr.ObjectTypeText)
                {
                    dpl.ObjectTypeBackColor = dpr.ObjectTypeBackColor = Color.LightPink;
                }
                if (dpl.ObjectDescText != dpr.ObjectDescText)
                {
                    dpl.ObjectDescBackColor = dpr.ObjectDescBackColor = Color.LightPink;
                }
                if (dpl.ObjectRefText != dpr.ObjectRefText)
                {
                    dpl.ObjectRefBackColor = dpr.ObjectRefBackColor = Color.LightPink;
                }
                if (dpl.ObjectDefaultText != dpr.ObjectDefaultText)
                {
                    dpl.ObjectDefaultBackColor = dpr.ObjectDefaultBackColor = Color.LightPink;
                }
                if (dpl.ObjectEnumText != dpr.ObjectEnumText)
                {
                    dpl.ObjectEnumBackColor = dpr.ObjectEnumBackColor = Color.LightPink;
                }
            }
            else if (_leftDataPanel is ObjectDataPanel dpl1 && _rightDataPanel is ObjectDataPanel dpr1)
            {
                if (dpl1.ObjectPathText != dpr1.ObjectPathText)
                {
                    dpl1.ObjectPathBackColor = dpr1.ObjectPathBackColor = Color.LightPink;
                }
                if (dpl1.ObjectTypeText != dpr1.ObjectTypeText)
                {
                    dpl1.ObjectTypeBackColor = dpr1.ObjectTypeBackColor = Color.LightPink;
                }
                if (dpl1.ObjectDescText != dpr1.ObjectDescText)
                {
                    dpl1.ObjectDescBackColor = dpr1.ObjectDescBackColor = Color.LightPink;
                }
                if (dpl1.ObjectRefText != dpr1.ObjectRefText)
                {
                    dpl1.ObjectRefBackColor = dpr1.ObjectRefBackColor = Color.LightPink;
                }
                if (dpl1.ObjectAdditionalText != dpr1.ObjectAdditionalText)
                {
                    dpl1.ObjectAdditionalBackColor = dpr1.ObjectAdditionalBackColor = Color.LightPink;
                }
                if (dpl1.ObjectRequiredText != dpr1.ObjectRequiredText)
                {
                    dpl1.ObjectRequiredBackColor = dpr1.ObjectRequiredBackColor = Color.LightPink;
                }
            }
            else if (_leftDataPanel is ArrayDataPanel dpl2 && _rightDataPanel is ArrayDataPanel dpr2)
            {
                if (dpl2.ObjectPathText != dpr2.ObjectPathText)
                {
                    dpl2.ObjectPathBackColor = dpr2.ObjectPathBackColor = Color.LightPink;
                }
                if (dpl2.ObjectTypeText != dpr2.ObjectTypeText)
                {
                    dpl2.ObjectTypeBackColor = dpr2.ObjectTypeBackColor = Color.LightPink;
                }
                if (dpl2.ObjectDescText != dpr2.ObjectDescText)
                {
                    dpl2.ObjectDescBackColor = dpr2.ObjectDescBackColor = Color.LightPink;
                }
                if (dpl2.ObjectRefText != dpr2.ObjectRefText)
                {
                    dpl2.ObjectRefBackColor = dpr2.ObjectRefBackColor = Color.LightPink;
                }
                if (dpl2.ObjectUniqueText != dpr2.ObjectUniqueText)
                {
                    dpl2.ObjectUniqueBackColor = dpr2.ObjectUniqueBackColor = Color.LightPink;
                }

            }
        }

        #endregion

        #region Others

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

        /*private void OnResizeEditor(object sender, EventArgs e)
        {
            if (sender is Form s)
            {
                _editorPosition.WinX = s.Location.X;
                _editorPosition.WinY = s.Location.Y;
                _editorPosition.WinW = s.Width;
                _editorPosition.WinH = s.Height;
            }
        }*/

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
            if (_lastSearchList.Contains(searchParam))
                return;

            ActivateUiControls(false);
            await FilterExamples(_exampleLinkCollection, searchParam).ConfigureAwait(true);
            ActivateUiControls(true);
            e.SuppressKeyPress = true;
        }

        private void Button_ExClearSearch_Click(object sender, EventArgs e)
        {
            ClearSearch();
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
            if (!textBox_description.ReadOnly)
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
            //foreach (var fileName in filesList)
            {
                var fileType = GetFileTypeFromFileName(fileName, appConfig.ConfigStorage.FileTypes);
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
            string longFileName,
            string fileType,
            BlockingCollection<JsonProperty> rootCollection)
        {
            string jsonStr;
            try
            {
                jsonStr = File.ReadAllText(longFileName);
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
                JsonPathDivider = appConfig.ConfigStorage.JsonPathDiv
            };

            var jsonProperties = parser.ParseJsonToPathList(jsonStr, out var endPos, out var errorFound)
                .Where(item => item.JsonPropertyType != JsonPropertyTypes.Comment
                               && item.JsonPropertyType != JsonPropertyTypes.EndOfArray
                               && item.JsonPropertyType != JsonPropertyTypes.EndOfObject
                               && item.JsonPropertyType != JsonPropertyTypes.Error
                               && item.JsonPropertyType != JsonPropertyTypes.Unknown)
                .ToList();
            var version = jsonProperties.FirstOrDefault(n => n.Path == appConfig.ConfigStorage.VersionTagName)
                ?.Value ?? "";

            var rootObject = jsonProperties.FirstOrDefault(n =>
                n.JsonPropertyType == JsonPropertyTypes.Object
                && string.IsNullOrEmpty(n.Name)
                && string.IsNullOrEmpty(n.Path));
            jsonProperties.Remove(rootObject);

            Parallel.ForEach(jsonProperties, item =>
            //foreach (var item in jsonProperties)
            {
                item.Path = item.Path.TrimStart(appConfig.ConfigStorage.JsonPathDiv);

                foreach (var fType in appConfig.ConfigStorage.FileTypes)
                {
                    if (item.Path.StartsWith(fType.PropertyTypeName))
                    {
                        fileType = fType.ContentType;
                        break;
                    }
                }

                var newItem = new JsonProperty
                {
                    PathDelimiter = appConfig.ConfigStorage.JsonPathDiv,
                    Name = item.Name,
                    Value = item.Value,
                    ContentType = fileType,
                    FullFileName = longFileName,
                    Version = version,
                    JsonPath = item.Path,
                    VariableType = item.ValueType,
                    ObjectType = item.JsonPropertyType,
                };
                rootCollection.Add(newItem);
            });
        }

        private void FlattenCollection(IEnumerable<JsonProperty> propertiesCollection,
            string contentType,
            string itemName,
            string moveToPath,
            string[] parentName)
        {
            if (propertiesCollection == null
                || !propertiesCollection.Any()
                || string.IsNullOrEmpty(itemName)
                || parentName == null
                || parentName.Length <= 0
                || string.IsNullOrEmpty(parentName[0]))
                return;

            //group collection by ContentType
            var contentTypeGroupedProperties = propertiesCollection
                .Where(n => n.ContentType == contentType)
                .ToArray();

            for (var i = 0; i < parentName.Length; i++)
            {
                parentName[i] = parentName[i].ToLower();
            }

            // group collection of selected actions (parent + name) grouped by by filenames
            IEnumerable<IGrouping<string, JsonProperty>> fileGroupedItems;
            if (parentName.Length > 1)
            {
                fileGroupedItems = contentTypeGroupedProperties
                    .Where(n => n.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase)
                                && parentName.Contains(n.UnifiedParent.ToLower()))
                    .GroupBy(n => n.FullFileName)
                    .ToArray();
            }
            else
            {
                fileGroupedItems = contentTypeGroupedProperties
                    .Where(n =>
                        n.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase)
                        && parentName[0].Equals(n.UnifiedParent, StringComparison.OrdinalIgnoreCase))
                    .GroupBy(n => n.FullFileName)
                    .ToArray();
            }

            var processedItemsNumber = 0;
            var totalItemsNumber = fileGroupedItems.Count();

            // process items in every single file
            foreach (var actionItem in fileGroupedItems)
            {
                // get properties for single file
                var fileProperties = contentTypeGroupedProperties
                    .Where(n =>
                        n.FullFileName == actionItem.Key)
                    .ToArray();

                // iterate through single file item after item
                foreach (var actionProperty in actionItem)
                {
                    var moveToPathTmp = new StringBuilder();

                    if (string.IsNullOrEmpty(moveToPath))
                        moveToPathTmp.Append(actionProperty.ParentPath);
                    else
                        moveToPathTmp.Append(moveToPath);

                    moveToPathTmp.Append($".<{actionProperty.Value.Replace(appConfig.ConfigStorage.JsonPathDiv, '_')}>");

                    //get clildren in the file for single item
                    var actionMembers = fileProperties
                        .Where(n => n.JsonPath.Contains(actionProperty.ParentPath))
                        .ToArray();

                    foreach (var actionMember in actionMembers)
                    {
                        var flattenedPath = actionMember.JsonPath.Replace(actionProperty.ParentPath, moveToPathTmp.ToString());
                        actionMember.FlattenedJsonPath = flattenedPath;
                    }
                }

                if (processedItemsNumber % 50 == 0)
                {
                    Invoke((MethodInvoker)delegate
                   {
                       toolStripStatusLabel1.Text =
                           contentType.ToString() + " converted " + processedItemsNumber + "/" + totalItemsNumber;
                   });
                }

                processedItemsNumber++;
            }
        }

        private TreeNode GenerateTreeFromList(IEnumerable<JsonProperty> rootCollection)
        {
            var node = new TreeNode(appConfig.ConfigStorage.RootNodeName);
            var itemNumber = 0;
            var totalItemNumber = rootCollection.Count();

            foreach (var propertyItem in rootCollection)
            {
                var itemName =
                    $"<{propertyItem.ContentType}>{appConfig.ConfigStorage.JsonPathDiv}{propertyItem.UnifiedFlattenedJsonPath}"
                        .TrimEnd(appConfig.ConfigStorage.JsonPathDiv);

                if (propertyItem.ObjectType != JsonPropertyTypes.Array)
                {
                    if (!_exampleLinkCollection.ContainsKey(itemName))
                    {
                        _exampleLinkCollection.Add(itemName, new List<JsonProperty>() { propertyItem });
                    }
                    else
                    {
                        _exampleLinkCollection[itemName].Add(propertyItem);
                    }
                }

                var tmpNode = node;
                var tmpPath = new StringBuilder();

                foreach (var token in itemName.Split(new[] { appConfig.ConfigStorage.JsonPathDiv },
                    StringSplitOptions.RemoveEmptyEntries))
                {
                    if (tmpPath.Length > 0)
                        tmpPath.Append(appConfig.ConfigStorage.JsonPathDiv + token);
                    else
                        tmpPath.Append(token);

                    if (!tmpNode.Nodes.ContainsKey(tmpPath.ToString()))
                    {
                        var nodeName = token;
                        var tagItem = rootCollection
                            .FirstOrDefault(n =>
                                n.FullFileName == propertyItem.FullFileName
                                && n.Name == nodeName
                                && n.UnifiedPath == tmpPath
                                    .ToString()
                                    .Replace($"<{propertyItem.ContentType}>{appConfig.ConfigStorage.JsonPathDiv}", ""));

                        if (tagItem == null)
                        {
                            tagItem = new JsonProperty()
                            {
                                VariableType = JsonValueTypes.Object,
                                FullFileName = propertyItem.FullFileName,
                                ObjectType = JsonPropertyTypes.Object,
                                Name = $"<{propertyItem.ContentType}>{appConfig.ConfigStorage.JsonPathDiv}",
                                JsonPath = "",
                                Value = "",
                                Version = propertyItem.Version,
                                ContentType = propertyItem.ContentType,
                                PathDelimiter = appConfig.ConfigStorage.JsonPathDiv
                            };
                        }

                        if (tagItem.ObjectType == JsonPropertyTypes.Array)
                            nodeName += "[]";
                        else if (tagItem.ObjectType == JsonPropertyTypes.Object)
                            nodeName += "{}";

                        var newNode = new TreeNode(nodeName)
                        {
                            Name = tmpPath.ToString(),
                            Text = nodeName,
                            Tag = tagItem
                        };

                        tmpNode.Nodes.Add(newNode);
                    }

                    tmpNode = tmpNode.Nodes[tmpPath.ToString()];
                }

                if (itemNumber % 1000 == 0)
                {
                    Invoke((MethodInvoker)delegate
                   {
                       toolStripStatusLabel1.Text =
                           "Properties processed " + itemNumber + "/" + totalItemNumber;
                   });
                }

                itemNumber++;
            }

            return node;
        }

        private async Task<bool> LoadDb(string longFileName)
        {
            var treeFile = Path.ChangeExtension(longFileName, appConfig.ConfigStorage.DefaultTreeFileExtension);
            var examplesFile = Path.ChangeExtension(longFileName, appConfig.ConfigStorage.DefaultExamplesFileExtension);

            FormCaption = appConfig.ConfigStorage.DefaultEditorFormCaption;
            if (string.IsNullOrEmpty(longFileName))
                return false;

            ActivateUiControls(false, false);
            toolStripStatusLabel1.Text = "Loading database...";
            var rootNodeExamples = new TreeNode();
            var exampleLinkCollection = new Dictionary<string, List<JsonProperty>>();
            try
            {
                var t = Task.Run(() =>
                    {
                        var m = new CustomTreeNode();
                        rootNodeExamples = m.GetTreeFromMsgPack(treeFile);
                    }
                );
                await Task.WhenAll(t).ConfigureAwait(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show("File read exception [" + longFileName + "]: " + ex.Message);
                toolStripStatusLabel1.Text = "Failed to load database";
            }

            if (rootNodeExamples != null)
            {
                _rootNodeExamples = rootNodeExamples;
            }

            tabControl1.TabPages[1].Enabled = true;
            FormCaption = appConfig.ConfigStorage.DefaultEditorFormCaption + " " + GetShortFileName(longFileName);
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
                MessageBox.Show("File read exception [" + longFileName + "]: " + ex.Message);
                toolStripStatusLabel1.Text = "Failed to load database";
            }

            if (exampleLinkCollection != null)
            {
                _exampleLinkCollection = exampleLinkCollection;
            }

            toolStripStatusLabel1.Text = "";

            return true;
        }

        private bool FillExamplesGrid(Dictionary<string, List<JsonProperty>> exampleLinkCollection,
            TreeNode currentNode,
            SearchItem searchParam = null)
        {
            if (currentNode == null || exampleLinkCollection == null || exampleLinkCollection.Count <= 0)
                return false;

            if (!exampleLinkCollection.ContainsKey(currentNode.Name))
                return false;

            var records = exampleLinkCollection[currentNode.Name];
            toolStripStatusLabel1.Text = "Displaying " + records.Count + " records";

            var parentNode = _lastSelectedExamplesNode?.Parent;
            if (_lastSelectedExamplesNode != null)
            {
                _lastSelectedExamplesNode.BackColor = Color.White;

                while (parentNode != null)
                {
                    parentNode.BackColor = Color.White;
                    parentNode = parentNode.Parent;
                }
            }

            _lastSelectedExamplesNode = currentNode;
            currentNode.BackColor = Color.DodgerBlue;
            parentNode = currentNode.Parent;
            while (parentNode != null)
            {
                parentNode.BackColor = Color.DodgerBlue;
                parentNode = parentNode.Parent;
            }

            ClearSearch();

            _examplesTable.Rows.Clear();
            comboBox_ExVersions.Items.Clear();
            comboBox_ExVersions.Items.Add(appConfig.ConfigStorage.DefaultVersionCaption);
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
                        recordValue = appConfig.ConfigStorage.BeautifyJson ? BeautifyJson(valueGroup.Key, appConfig.ConfigStorage.ReformatJson) : valueGroup.Key;
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

                        fileNameList.Append(record.FullFileName +
                                            appConfig.ConfigStorage.TableListDelimiter.ToString());
                        pathList.Append(record.JsonPath + appConfig.ConfigStorage.TableListDelimiter.ToString());
                        //newRow[4] = newRow[4]
                        //+ Delimiter.ToString()
                        //+ record.SourceLineNumber;
                    }

                    var newRow = _examplesTable.NewRow();
                    newRow[_exampleGridColumnNames[0]] = currentVersion;
                    newRow[_exampleGridColumnNames[1]] = recordValue;
                    newRow[_exampleGridColumnNames[2]] = fileNameList.ToString();
                    newRow[_exampleGridColumnNames[3]] = pathList.ToString();
                    //newRow[_exampleGridColumnsNames[4]] = "";
                    _examplesTable.Rows.Add(newRow);
                }
            }

            if (searchParam == null)
                searchParam = new SearchItem(appConfig.ConfigStorage.DefaultVersionCaption);
            if (!_lastSearchList.Contains(searchParam))
                _lastSearchList.Add(searchParam);

            SetSearchText(textBox_ExSearchHistory, _lastSearchList);
            comboBox_ExVersions.Items.AddRange(versionCollection.ToArray());
            toolStripStatusLabel1.Text = "";

            return true;
        }

        private async Task FilterExamples(Dictionary<string, List<JsonProperty>> exampleLinkCollection,
            SearchItem searchParam)
        {
            if (_lastSearchList.Contains(searchParam))
                return;

            _lastSearchList.Add(searchParam);

            if (searchParam == null || string.IsNullOrEmpty(searchParam.Value))
            {
                FillExamplesGrid(exampleLinkCollection, _lastSelectedExamplesNode, searchParam);
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

            SetSearchText(textBox_ExSearchHistory, _lastSearchList);
            toolStripStatusLabel1.Text = "";
        }

        private async Task FilterExamplesVersion(Dictionary<string, List<JsonProperty>> exampleLinkCollection,
            SearchItem searchParam)
        {
            if (_lastSearchList == null)
                _lastSearchList = new List<SearchItem>();

            if (!_lastSearchList.Any())
                _lastSearchList.Add(new SearchItem(appConfig.ConfigStorage.DefaultVersionCaption));
            var lastSearch = _lastSearchList.Last();
            if (lastSearch.Version != appConfig.ConfigStorage.DefaultVersionCaption)
                FillExamplesGrid(exampleLinkCollection, _lastSelectedExamplesNode, searchParam);

            if (comboBox_ExVersions.Items.Contains(searchParam.Version))
            {
                comboBox_ExVersions.SelectedItem = searchParam.Version;
            }
            else
            {
                comboBox_ExVersions.SelectedItem = appConfig.ConfigStorage.DefaultVersionCaption;
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

            SetSearchText(textBox_ExSearchHistory, _lastSearchList);
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

            Invoke((MethodInvoker)delegate { textBox.Text = searchString.ToString(); });
        }

        private void ClearSearch()
        {
            _lastSearchList.Clear();
            Invoke((MethodInvoker)delegate { textBox_ExSearchHistory.Clear(); });
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
                       if (newHeight == row.Height &&
                           newHeight <= currentHeight * appConfig.ConfigStorage.CellSizeAdjust)
                           return;

                       if (newHeight > currentHeight * appConfig.ConfigStorage.CellSizeAdjust)
                           newHeight = (ushort)(currentHeight * appConfig.ConfigStorage.CellSizeAdjust);
                       row.Height = newHeight;
                   }

                   for (var columnNumber = 0; columnNumber < dgView.ColumnCount; columnNumber++)
                   {
                       var column = dgView.Columns[columnNumber];
                       var newWidth = column.GetPreferredWidth(DataGridViewAutoSizeColumnMode.AllCells, true);
                       var currentWidth = dgView.Width;
                       if (newWidth == column.Width &&
                           newWidth <= currentWidth * appConfig.ConfigStorage.CellSizeAdjust)
                           return;

                       if (newWidth > currentWidth * appConfig.ConfigStorage.CellSizeAdjust)
                           newWidth = (ushort)(currentWidth * appConfig.ConfigStorage.CellSizeAdjust);
                       column.Width = newWidth;
                   }
               });
            }).ContinueWith((t) => { toolStripStatusLabel1.Text = ""; });
        }

        private void ReadjustRow(DataGridView dgView, int rowNumber)
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
            if (newHeight == row.Height && newHeight <= currentHeight * appConfig.ConfigStorage.CellSizeAdjust)
                return;

            if (newHeight > currentHeight * appConfig.ConfigStorage.CellSizeAdjust)
                newHeight = (ushort)(currentHeight * appConfig.ConfigStorage.CellSizeAdjust);
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

            var jsonPaths = dataGridView_examples.Rows[dataGridView_examples.SelectedCells[0].RowIndex].Cells[3]?.Value
                ?.ToString().Split(new[] { appConfig.ConfigStorage.TableListDelimiter },
                    StringSplitOptions.RemoveEmptyEntries);
            var jsonSample = dataGridView_examples.Rows[dataGridView_examples.SelectedCells[0].RowIndex].Cells[3]?.Value
                ?.ToString();
            var fileNumber = listBox_fileList.SelectedIndex;
            var fileName = listBox_fileList.Items[fileNumber].ToString();

            if (jsonPaths?.Length >= fileNumber)
            {
                var jsonPath = jsonPaths?[fileNumber];
                ShowPreviewEditor(fileName, jsonPath, jsonSample, standAloneEditor);
            }
        }

        private void ShowPreviewEditor(string longFileName, string jsonPath, string sampleText,
            bool standAloneEditor = false)
        {
            if (appConfig.ConfigStorage.UseVsCode)
            {
                var lineNumber = GetLineNumberForPath(longFileName, jsonPath) + 1;
                var execParams = "-r -g " + longFileName + ":" + lineNumber;
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
                if (textEditor.SingleLineBrackets != appConfig.ConfigStorage.ReformatJson ||
                    textEditor.Text != appConfig.ConfigStorage.PreViewCaption + longFileName)
                {
                    textEditor.SingleLineBrackets = appConfig.ConfigStorage.ReformatJson;
                    fileLoaded = textEditor.LoadJsonFromFile(longFileName);
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
                    SingleLineBrackets = appConfig.ConfigStorage.ReformatJson
                };

                newWindow = true;
                fileLoaded = textEditor.LoadJsonFromFile(longFileName);
            }

            if (!standAloneEditor) _sideViewer = textEditor;

            textEditor.AlwaysOnTop = appConfig.ConfigStorage.AlwaysOnTop;
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
                //textEditor.ResizeEnd += OnResizeEditor;
            }

            if (!fileLoaded)
            {
                textEditor.Text = "Failed to load " + longFileName;
                return;
            }

            if (!standAloneEditor)
            {
                textEditor.Text = appConfig.ConfigStorage.PreViewCaption + longFileName;
            }
            else
            {
                textEditor.Text = longFileName;
            }

            if (!textEditor.HighlightPathJson(jsonPath))
            {
                textEditor.HighlightText(sampleText);
            }
        }

        #endregion

        #region Utilities

        private static string GetFileTypeFromFileName(string longFileName, IEnumerable<Form1.ContentTypeItem> fileTypes)
        {
            if (string.IsNullOrEmpty(longFileName))
                return "?";

            if (fileTypes.Any(n => n.FileTypeSign.EndsWith('\\') || n.FileTypeSign.EndsWith('/')))
            {
                var dirName = GetDirectoryName(longFileName);
                return
                    fileTypes.Where(n => dirName.EndsWith(n.FileTypeSign.TrimEnd(new char[] { '\\', '/' }))).Select(n => n.ContentType).FirstOrDefault() ?? "?";
            }
            else
            {
                var shortFileName = GetShortFileName(longFileName);
                return fileTypes.Where(n => shortFileName.EndsWith(n.FileTypeSign)).Select(n => n.ContentType).FirstOrDefault() ?? "?";
            }
        }

        private static string GetShortFileName(string longFileName)
        {
            if (string.IsNullOrEmpty(longFileName))
                return longFileName;

            var file = new FileInfo(longFileName);
            return file.Name;
        }

        private static string GetDirectoryName(string longFileName)
        {
            if (string.IsNullOrEmpty(longFileName))
                return longFileName;

            var file = new FileInfo(longFileName);
            return file.Directory.Name;
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

        private int GetLineNumberForPath(string longFileName, string jsonPath)
        {
            string jsonStr;
            try
            {
                jsonStr = File.ReadAllText(longFileName);
            }
            catch
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
                JsonPathDivider = appConfig.ConfigStorage.JsonPathDiv,
                SearchStartOnly = true
            };

            var startLine = 0;
            var property = parser.SearchJsonPath(jsonStr, appConfig.ConfigStorage.JsonPathDiv + jsonPath);
            if (property != null)
                JsonPathParser.GetLinesNumber(jsonStr, property.StartPosition, property.EndPosition, out startLine,
                    out var _);

            return startLine;
        }

        private TreeNode FindTreeNodeByPath(List<string> pathItems, TreeNode rootNode)
        {
            if (pathItems == null || pathItems.Count <= 0) return null;

            TreeNode result = null;

            if (rootNode.Text.TrimEnd(new[] { ']', '}' }).TrimEnd(new[] { '[', '{' }) == pathItems.First())
            {
                if (pathItems.Count() == 1)
                {
                    return rootNode;
                }
                else
                {
                    pathItems.RemoveAt(0);
                    foreach (TreeNode node in rootNode.Nodes)
                    {
                        result = FindTreeNodeByPath(pathItems, node);
                        if (result != null) return result;
                    }
                }
            }
            return result;
        }

        #endregion

        #region Schema generation

        private ISchemaTreeBase JsonPropertyListToSchemaObject(
            IEnumerable<ParsedProperty> rootCollection,
            string startPath,
            string propertyName)
        {
            if (rootCollection == null) return null;

            var properties = rootCollection.Where(n => n.ParentPath == startPath);

            if (!properties.Any()) return null;

            var nodeTypes = new List<string>();
            var typePropertyPathSample = startPath + appConfig.ConfigStorage.JsonPathDiv + "type";
            var currnetNodeType = properties.FirstOrDefault(n => n.Path == typePropertyPathSample) ?? new ParsedProperty();

            if (currnetNodeType.JsonPropertyType == JsonPropertyTypes.Array)
            {
                var childNodesTypes = rootCollection
                    .Where(n => n.ParentPath == typePropertyPathSample)?
                    .Select(n => n.Value);
                nodeTypes.AddRange(childNodesTypes);
            }
            else
            {
                nodeTypes.Add(currnetNodeType.Value);
            }
            nodeTypes.Sort();

            var nodePath = properties.FirstOrDefault(n => n.Name == "$id")?.Value;
            var nodeDescription = properties.FirstOrDefault(n => n.Name == "title")?.Value;
            var reference = properties.FirstOrDefault(n => n.Name == "$ref")?.Value;
            var nodeExamples = rootCollection
                .Where(n => n.ParentPath == startPath + appConfig.ConfigStorage.JsonPathDiv + "examples")?
                .Select(n => n.Value)?
                .OrderBy(n => n)
                .ToList();

            // to do - get all available properties even if there is a mix of object/array/property
            if (nodeTypes.Any(n => n == "array"))
            {
                var arrayNode = new SchemaTreeArray(propertyName)
                {
                    Type = nodeTypes,
                    Path = nodePath,
                    Description = nodeDescription,
                    Reference = reference,
                    Examples = nodeExamples,
                };

                if (bool.TryParse(properties.FirstOrDefault(n => n.Name == "uniqueItems")?.Value, out var ap))
                    arrayNode.UniqueItemsOnly = ap;
                else
                    arrayNode.UniqueItemsOnly = null;

                var newItem = JsonPropertyListToSchemaObject(rootCollection,
                    startPath + appConfig.ConfigStorage.JsonPathDiv + "items",
                    "items");
                arrayNode.Items = newItem;

                return arrayNode;
            }

            if (nodeTypes.Any(n => n == "object"))
            {
                var objectNode = new SchemaTreeObject(propertyName)
                {
                    Name = propertyName,
                    Type = nodeTypes,
                    Path = nodePath,
                    Description = nodeDescription,
                    Reference = reference,
                    Examples = nodeExamples,
                    Required = rootCollection
                    .Where(n => n.ParentPath == startPath + appConfig.ConfigStorage.JsonPathDiv + "required")?
                    .Select(n => n.Value)?
                    .OrderBy(n => n)
                    .ToList()
                };

                if (bool.TryParse(properties.FirstOrDefault(n => n.Name == "additionalProperties")?.Value, out var ap))
                    objectNode.AdditionalProperties = ap;
                else
                    objectNode.AdditionalProperties = null;

                foreach (var item in rootCollection
                    .Where(n => n.ParentPath == startPath + appConfig.ConfigStorage.JsonPathDiv + "properties")
                    .OrderBy(n => n.Name))
                {
                    var newProperty = JsonPropertyListToSchemaObject(rootCollection, item.Path, item.Name);
                    objectNode.Properties.Add(newProperty);
                }

                if (startPath == "#")
                {
                    objectNode.SchemaName = properties.FirstOrDefault(n => n.Name == "$schema")?.Value;

                    foreach (var item in rootCollection
                        .Where(n => n.ParentPath == startPath + appConfig.ConfigStorage.JsonPathDiv + "definitions")
                        .OrderBy(n => n.Name))
                    {
                        var newProperty = JsonPropertyListToSchemaObject(rootCollection, item.Path, item.Name);
                        objectNode.Definitions.Add(newProperty);
                    }
                }

                return objectNode;
            }

            var propertyNode = new SchemaTreeProperty(propertyName)
            {
                Type = nodeTypes,
                Path = nodePath,
                Description = nodeDescription,
                Reference = reference,
                Examples = nodeExamples,
                Default = properties.FirstOrDefault(n => n.Name == "default")?.Value,
                Pattern = properties.FirstOrDefault(n => n.Name == "pattern")?.Value,
                Enum = rootCollection
                    .Where(n => n.ParentPath == startPath + appConfig.ConfigStorage.JsonPathDiv + "enum")?
                    .Select(n => n.Value)?
                    .OrderBy(n => n)
                    .ToList(),
            };

            return propertyNode;
        }

        private ISchemaTreeBase GenerateSchemaFromTree(TreeNode treeRoot,
            Dictionary<string, List<JsonProperty>> examples, string nodeName)
        {
            if (treeRoot == null) return null;

            var descText = "";
            _nodeDescription?.TryGetValue(treeRoot.Name, out descText);

            var newSchemaRoot = new SchemaTreeObject(nodeName)
            {
                Description = descText,
                Path = nodeName,
                Type = new List<string> { "object" },
                Name = nodeName,
                SchemaName = "http://json-schema.org/draft-04/schema#"
            };

            foreach (TreeNode node in treeRoot.Nodes)
            {
                var result = TreeNodeToSchemaObject(node, nodeName + "/properties", examples);
                if (result != null) newSchemaRoot.Properties.Add(result);
            }

            return newSchemaRoot;
        }

        private ISchemaTreeBase TreeNodeToSchemaObject(TreeNode node, string parentPath, Dictionary<string, List<JsonProperty>> examples)
        {
            if (node == null) return null;

            var propertyName = node.Text.TrimEnd(new[] { ']', '}' }).TrimEnd(new[] { '[', '{' });
            var nodePath = parentPath + "/" + propertyName;

            var nodeDescription = "";

            var nodeType = JsonPropertyTypes.Unknown;
            if (node.Tag is JsonProperty p)
            {
                nodeType = p.ObjectType;
                _nodeDescription?.TryGetValue(node.Name, out nodeDescription);
            }

            nodeDescription = nodeDescription?.Replace("\r", "\\r").Replace("\n", "\\n").Replace("\"", "\\\"");

            examples.TryGetValue(node.Name, out var nodeExamples);

            if (nodeType == JsonPropertyTypes.Array)
            {
                var arrayNode = new SchemaTreeArray(propertyName)
                {
                    Type = new List<string> { nodeType.ToString().ToLower() },
                    Path = nodePath,
                    Description = nodeDescription,
                    Examples = nodeExamples?
                    .Where(n => n.ObjectType == JsonPropertyTypes.Property
                    || n.ObjectType == JsonPropertyTypes.ArrayValue
                    || n.VariableType == JsonValueTypes.String)
                    .Select(n => n.Value)
                    .Distinct()
                    .OrderBy(n => n)
                    .ToList(),
                    UniqueItemsOnly = true
                };

                // check all items in array and create some average object
                var objList = new List<ISchemaTreeBase>();
                foreach (TreeNode item in node.Nodes)
                {
                    var newItemsNode = TreeNodeToSchemaObject(item, nodePath + "/items/properties", examples);
                    objList.Add(newItemsNode);
                }

                if (!objList.Any())
                {
                    var arrayExamples = examples.Where(n => n.Key.StartsWith(node.Name)).ToArray();

                    var enumList = arrayExamples.FirstOrDefault().Value.Select(n => n.Value).Distinct().OrderBy(n => n).ToList();

                    var enumListTypes = arrayExamples.FirstOrDefault().Value.Select(n => n.VariableType).Distinct();
                    var typesList = enumListTypes.Select(n => n.ToString().ToLower()).OrderBy(n => n).ToList();

                    arrayNode.Items = new SchemaTreeProperty("items")
                    {
                        Path = nodePath + "/items",
                        Type = typesList,
                        Enum = enumList,
                        Description = nodeDescription,
                        Examples = nodeExamples?.Select(n => n.Value).Distinct().OrderBy(n => n).ToList()
                    };

                    return arrayNode;
                }
                arrayNode.Items = new SchemaTreeObject()
                {
                    Path = nodePath + "/items",
                    Name = "items",
                    Type = new List<string> { "object" },
                    Properties = objList
                };

                return arrayNode;
            }

            var nodeVariableTypes = nodeExamples?
                .Select(n => n.VariableType)
                .Distinct()
                .Where(n => n == JsonValueTypes.String
                || n == JsonValueTypes.Integer
                || n == JsonValueTypes.Number
                || n == JsonValueTypes.Null
                || n == JsonValueTypes.Boolean)
                .Select(n => n.ToString().ToLower())
                .ToList() ?? new List<string>();

            var nodeObjectTypes = nodeExamples?
                .Select(n => n.ObjectType)
                .Distinct()
                .Where(n => n == JsonPropertyTypes.Object
                || n == JsonPropertyTypes.Array
                || n == JsonPropertyTypes.ArrayValue)
                .Select(n => n.ToString().ToLower().Replace("arrayvalue", "array")) ?? new List<string>();

            nodeVariableTypes.AddRange(nodeObjectTypes);
            nodeVariableTypes = nodeVariableTypes.Distinct().OrderBy(n => n).ToList();

            if (nodeType == JsonPropertyTypes.Object)
            {
                var objectNode = new SchemaTreeObject
                {
                    Name = propertyName,
                    Type = nodeVariableTypes,
                    Path = nodePath,
                    Description = nodeDescription,
                    Examples = nodeExamples?
                    .Where(n => n.ObjectType == JsonPropertyTypes.Property
                    || n.ObjectType == JsonPropertyTypes.ArrayValue
                    || n.VariableType == JsonValueTypes.String)
                    .Select(n => n.Value)
                    .Distinct()
                    .OrderBy(n => n)
                    .ToList(),
                    AdditionalProperties = false
                };

                foreach (TreeNode item in node.Nodes)
                {
                    var newProperty = TreeNodeToSchemaObject(item, nodePath + "/properties", examples);
                    objectNode.Properties.Add(newProperty);
                }

                return objectNode;
            }

            var propertyNode = new SchemaTreeProperty(propertyName)
            {
                Type = nodeVariableTypes,
                Path = nodePath,
                Description = nodeDescription,
                Examples = nodeExamples?
                    .Where(n => n.ObjectType == JsonPropertyTypes.Property
                    || n.ObjectType == JsonPropertyTypes.ArrayValue
                    || n.VariableType == JsonValueTypes.String)
                    .Select(n => n.Value)
                    .Distinct()
                    .OrderBy(n => n)
                    .ToList(),
            };

            // if all property types are "string" create Enum
            if (nodeVariableTypes.Any(n => n == "string"))
            {
                propertyNode.Enum = nodeExamples?
                    .Where(n => n.VariableType == JsonValueTypes.String)?
                    .Select(n => n.Value)?
                    .Distinct()?
                    .OrderBy(n => n)?
                    .ToList();

                if (propertyNode.Type.Contains("boolean"))
                {
                    propertyNode.Enum?.Add("true");
                    propertyNode.Enum?.Add("false");
                }

                if (propertyNode.Type.Contains("null"))
                {
                    propertyNode.Enum?.Add("null");
                }
            }

            return propertyNode;
        }

        private TreeNode ConvertSchemaNodesToTreeNode(ISchemaTreeBase schemaTree)
        {
            if (schemaTree == null) return null;

            var node = new TreeNode(schemaTree.Name)
            {
                Text = schemaTree.Name,
                Name = schemaTree.Path
            };

            if (schemaTree is SchemaTreeObject schemaObject)
            {
                node.Text += "{}";

                if (schemaObject.Properties != null && schemaObject.Properties.Count > 0)
                {
                    var newNode = new TreeNode("properties{}")
                    {
                        Text = "properties{}",
                        Name = schemaTree.Path + "/properties"
                    };

                    foreach (var property in schemaObject.Properties)
                    {
                        var newNodes = ConvertSchemaNodesToTreeNode(property);
                        if (newNodes != null) newNode.Nodes.Add(newNodes);
                    }

                    node.Nodes.Add(newNode);
                }

                if (schemaObject.Definitions != null && schemaObject.Definitions.Count > 0)
                {
                    var newNode = new TreeNode("definitions{}")
                    {
                        Text = "definitions{}",
                        Name = schemaTree.Path + "/definitions"
                    };

                    foreach (var definition in schemaObject.Definitions)
                    {
                        var newNodes = ConvertSchemaNodesToTreeNode(definition);
                        if (newNodes != null) newNode.Nodes.Add(newNodes);
                    }

                    node.Nodes.Add(newNode);
                }

            }
            else if (schemaTree is SchemaTreeArray schemaArray)
            {
                node.Text += "[]";
                var newNodes = ConvertSchemaNodesToTreeNode(schemaArray.Items);
                if (newNodes != null) node.Nodes.Add(newNodes);
            }

            return node;
        }

        private ISchemaTreeBase FindDefinition(string path, ISchemaTreeBase rootSchemaNode)
        {
            if (string.IsNullOrEmpty(path) || rootSchemaNode == null) return null;

            ISchemaTreeBase result = null;
            var definitionsFlag = false;
            foreach (var token in path.Split('\\'))
            {
                if (token == "#")
                {
                    result = rootSchemaNode;
                }
                else
                {
                    if (result is SchemaTreeObject currentObject)
                    {
                        if (token == "properties") continue;
                        if (token == "definitions")
                        {
                            definitionsFlag = true;
                            continue;
                        }

                        if (definitionsFlag)
                        {
                            result = currentObject.Definitions.FirstOrDefault(n => n.Name == token);
                            definitionsFlag = false;
                        }
                        else
                        {
                            result = currentObject.Properties.FirstOrDefault(n => n.Name == token);
                        }
                    }
                    else if (result is SchemaTreeArray currentArray)
                    {
                        if (token == "items") result = currentArray.Items;
                    }
                    else if (result is SchemaTreeProperty currentProperty)
                    {
                        if (currentProperty.Name == token) result = currentProperty;
                    }
                }

                if (result == null) return null;
            }

            return result;
        }

        #endregion

    }
}