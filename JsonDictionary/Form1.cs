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
using Newtonsoft.Json;
using static JsonDictionaryCore.JsonIo;
using JsonDictionaryCore.DictionaryGenerator;
using JsonDictionaryCore.SchemaGenerator;
using JsonDictionaryCore.Panels;

namespace JsonDictionaryCore
{
    public partial class Form1 : Form
    {
        // pre-defined constants
        private readonly string[] _exampleGridColumnNames = { "Version", "Example", "File Name", "Json Path" };

        // global variables
        private readonly Config _appConfig = new Config("appsettings.json");
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
        private readonly Dictionary<string, string> _nodeDescriptions;
        private ISchemaBase _rightSchema;
        private ISchemaBase _leftSchema;
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
            FormCaption = _appConfig.ConfigStorage.DefaultEditorFormCaption;
            checkBox_beautifyJson.Checked = _appConfig.ConfigStorage.BeautifyJson;
            checkBox_reformatJsonBrackets.Checked = _appConfig.ConfigStorage.ReformatJson;
            checkBox_showPreview.Checked = _appConfig.ConfigStorage.ShowPreview;
            checkBox_alwaysOnTop.Checked = _appConfig.ConfigStorage.AlwaysOnTop;
            checkBox_loadDbOnStart.Checked = _appConfig.ConfigStorage.LoadDbOnStart;
            checkBox_vsCode.Checked = _appConfig.ConfigStorage.UseVsCode;
            checkBox_schemaSelectionSync.Checked = _appConfig.ConfigStorage.SchemaFollowSelection;
            folderBrowserDialog1.SelectedPath = _appConfig.ConfigStorage.LastRootFolder;

            _nodeDescriptions =
                LoadJson<Dictionary<string, string>>(_appConfig.ConfigStorage.DefaultDescriptionFileName);
            if (_nodeDescriptions == null)
                _nodeDescriptions = new Dictionary<string, string>();

            if (_appConfig.ConfigStorage.MainWindowPosition.Initialized)
            {
                Location = new Point
                {
                    X = _appConfig.ConfigStorage.MainWindowPosition.WinX,
                    Y = _appConfig.ConfigStorage.MainWindowPosition.WinY
                };
                Width = _appConfig.ConfigStorage.MainWindowPosition.WinW;
                Height = _appConfig.ConfigStorage.MainWindowPosition.WinH;
            }

            if (_appConfig.ConfigStorage.EditorPosition.Initialized)
                _editorPosition = _appConfig.ConfigStorage.EditorPosition;

            TopMost = _appConfig.ConfigStorage.AlwaysOnTop;

            comboBox_ExCondition.Items.AddRange(typeof(SearchItem.SearchCondition).GetEnumNames());
            comboBox_ExCondition.SelectedIndex = 0;

            _examplesTable = new DataTable("Examples");
            foreach (var columnName in _exampleGridColumnNames)
                _examplesTable.Columns.Add(columnName);

            dataGridView_examples.DataError += delegate { };
            dataGridView_examples.DataSource = _examplesTable;

            comboBox_ExVersions.SelectedIndexChanged -= ComboBox_ExVersions_SelectedIndexChanged;
            comboBox_ExVersions.Items.Clear();
            comboBox_ExVersions.Items.Add(_appConfig.ConfigStorage.DefaultVersionCaption);
            comboBox_ExVersions.SelectedIndex = 0;
            comboBox_ExVersions.SelectedIndexChanged += ComboBox_ExVersions_SelectedIndexChanged;

            if (_appConfig.ConfigStorage.LoadDbOnStart)
            {
                LoadDb(_appConfig.ConfigStorage.LastDbName).ConfigureAwait(true);
                GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);
            }
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            splitContainer_tree.SplitterDistance = _appConfig.ConfigStorage.TreeSplitterDistance;
            splitContainer_description.SplitterDistance = _appConfig.ConfigStorage.DescriptionSplitterDistance;
            splitContainer_fileList.SplitterDistance = _appConfig.ConfigStorage.FileListSplitterDistance;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveJson(_nodeDescriptions, _appConfig.ConfigStorage.DefaultDescriptionFileName, true);

            _appConfig.ConfigStorage.MainWindowPosition = new WinPosition
            {
                WinX = Location.X,
                WinY = Location.Y,
                WinW = Width,
                WinH = Height
            };

            _appConfig.ConfigStorage.EditorPosition = _editorPosition;
            _appConfig.ConfigStorage.LastRootFolder = folderBrowserDialog1.SelectedPath;
            _appConfig.ConfigStorage.TreeSplitterDistance = splitContainer_tree.SplitterDistance;
            _appConfig.ConfigStorage.DescriptionSplitterDistance = splitContainer_description.SplitterDistance;
            _appConfig.ConfigStorage.FileListSplitterDistance = splitContainer_fileList.SplitterDistance;
            _appConfig.SaveConfig();
        }

        #endregion

        #region Main page

        private void Button_loadDb_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileOk -= OpenFileDialog1_FileOk;
            openFileDialog1.FileOk -= OpenFileDialog1_FileOk_Schema;
            openFileDialog1.FileOk += OpenFileDialog1_FileOk;

            openFileDialog1.FileName = "";
            openFileDialog1.Title = "Open " + _appConfig.ConfigStorage.DefaultFiledialogFormCaption;
            openFileDialog1.DefaultExt = _appConfig.ConfigStorage.DefaultTreeFileExtension;
            openFileDialog1.Filter =
                "Binary files|*." + _appConfig.ConfigStorage.DefaultTreeFileExtension + "|All files|*.*";
            openFileDialog1.ShowDialog();
        }

        private async void OpenFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            ActivateUiControls(false);
            if (await LoadDb(openFileDialog1.FileName).ConfigureAwait(true))
            {
                FormCaption = _appConfig.ConfigStorage.DefaultEditorFormCaption + " " +
                              GetShortFileName(openFileDialog1.FileName);
                tabControl1.SelectedTab = tabControl1.TabPages[1];
                _appConfig.ConfigStorage.LastDbName = openFileDialog1.FileName;
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
                var jsonPropertiesCollection = RunFileCollection(startPath, _appConfig.ConfigStorage.FileMask);
                Invoke((MethodInvoker)delegate
               {
                   endTime = DateTime.Now;
                   _textLog.AppendLine("Collection time: " + endTime.Subtract(startOperationTime).TotalSeconds);
                   startOperationTime = DateTime.Now;
                   FlushLog();
                   toolStripStatusLabel1.Text = "Processing events collection";
               });

                Parallel.ForEach(_appConfig.ConfigStorage.FlattenParameters, param =>
                //foreach (var param in appConfig.ConfigStorage.FlattenParameters)
                {
                    Invoke((MethodInvoker)delegate
                   {
                       startOperationTime = DateTime.Now;
                       FlushLog();
                       toolStripStatusLabel1.Text = "Processing " + param.ContentType + " collection";
                   });

                    FlattenCollection(jsonPropertiesCollection, param.ContentType, param.ItemName, param.MoveToPath,
                        param.ParentNames);

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
            saveFileDialog1.Title = "Save " + _appConfig.ConfigStorage.DefaultFiledialogFormCaption;
            saveFileDialog1.DefaultExt = _appConfig.ConfigStorage.DefaultTreeFileExtension;
            saveFileDialog1.Filter =
                "Binary files|*." + _appConfig.ConfigStorage.DefaultTreeFileExtension + "|All files|*.*";
            saveFileDialog1.FileName =
                _appConfig.ConfigStorage.DefaultSaveFileName + DateTime.Today.ToShortDateString().Replace("/", "_") +
                "." + _appConfig.ConfigStorage.DefaultTreeFileExtension;

            saveFileDialog1.FileOk -= SaveFileDialog1_FileOk;
            saveFileDialog1.FileOk -=
                SaveFileDialog1_FileOk_LeftSchema;
            saveFileDialog1.FileOk -=
                SaveFileDialog1_FileOk_RightSchema;
            saveFileDialog1.FileOk += SaveFileDialog1_FileOk;

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
                        _appConfig.ConfigStorage.DefaultTreeFileExtension);
                    var examplesFile = Path.ChangeExtension(saveFileDialog1.FileName,
                        _appConfig.ConfigStorage.DefaultExamplesFileExtension);
                    var treeExport = new CustomTreeNode(_rootNodeExamples);

                    if (treeFile == null)
                        throw new ArgumentNullException($"Can't use filename{saveFileDialog1.FileName}");

                    File.WriteAllBytes(treeFile, treeExport.MessagePack);
                    SaveBinary(_exampleLinkCollection, examplesFile);
                    _appConfig.ConfigStorage.LastDbName = saveFileDialog1.FileName;

                    if (_appConfig.ConfigStorage.SaveJsonTree)
                        File.WriteAllText(treeFile + ".json",
                            treeExport.GetJsonTree(_rootNodeExamples, _nodeDescriptions));
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"File write exception [{saveFileDialog1.FileName}]: {ex.Message}");
                }
            }).ContinueWith(t => { toolStripStatusLabel1.Text = ""; }).ConfigureAwait(false);
            GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);
        }

        private void CheckBox_beautifyJson_CheckedChanged(object sender, EventArgs e)
        {
            checkBox_reformatJsonBrackets.Enabled =
                _appConfig.ConfigStorage.BeautifyJson = checkBox_beautifyJson.Checked;
        }

        private void CheckBox_reformatJsonBrackets_CheckedChanged(object sender, EventArgs e)
        {
            _appConfig.ConfigStorage.ReformatJson = checkBox_reformatJsonBrackets.Checked;
        }

        private void CheckBox_reformatJsonBrackets_EnabledChanged(object sender, EventArgs e)
        {
            if (!checkBox_reformatJsonBrackets.Enabled) checkBox_reformatJsonBrackets.Checked = false;
        }

        private void CheckBox_showPreview_CheckedChanged(object sender, EventArgs e)
        {
            _appConfig.ConfigStorage.ShowPreview = checkBox_showPreview.Checked;
        }

        private void CheckBox_alwaysOnTop_CheckedChanged(object sender, EventArgs e)
        {
            _appConfig.ConfigStorage.AlwaysOnTop = checkBox_alwaysOnTop.Checked;
            TopMost = _appConfig.ConfigStorage.AlwaysOnTop;
        }

        private void CheckBox_loadDbOnStart_CheckedChanged(object sender, EventArgs e)
        {
            _appConfig.ConfigStorage.LoadDbOnStart = checkBox_loadDbOnStart.Checked;
        }

        private void CheckBox_vsCode_CheckedChanged(object sender, EventArgs e)
        {
            _appConfig.ConfigStorage.UseVsCode = checkBox_vsCode.Checked;
        }

        private void CheckBox_schemaSelectionSync_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is CheckBox cb)
                _appConfig.ConfigStorage.SchemaFollowSelection = cb.Checked;
        }

        #endregion

        #region Examples Tree

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
                ShowSamples();
            else if (e.KeyCode == Keys.Add)
                UnfoldNode(treeView_examples);
            else if (e.KeyCode == Keys.Subtract)
                FoldNode(treeView_examples);
            else if (e.KeyCode == Keys.Delete)
                DeleteNode();
            else if (e.KeyCode == Keys.C && e.Control) CopyNodeText(treeView_examples);
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
            _nodeDescriptions?.TryGetValue(e.Node.Name, out descText);
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

            UnfoldNode(treeView_examples);
        }

        private void ToolStripMenuItem_foldAll_Click(object sender, EventArgs e)
        {
            if (treeView_examples.SelectedNode == null)
                return;

            FoldNode(treeView_examples);
        }

        private void ToolStripMenuItem_treeCopy_Click(object sender, EventArgs e)
        {
            CopyNodeText(treeView_examples);
        }

        private void ToolStripMenuItem_treeDelete_Click(object sender, EventArgs e)
        {
            DeleteNode();
        }

        private void ToolStripMenuItem_generateSchema_Click(object sender, EventArgs e)
        {
            Button_generateSchema_Click(this, EventArgs.Empty);
            tabControl1.SelectedTab = tabPage_Schema;

            if (treeView_leftSchema != null && treeView_leftSchema.Nodes != null && treeView_leftSchema.Nodes.Count > 0)
                treeView_leftSchema.Nodes[0].Expand();
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
            }

            ActivateUiControls(true, false);
        }

        private void DeleteNode()
        {
            if (treeView_examples.SelectedNode == null)
                return;

            if (MessageBox.Show("Are you sure to remove the selected node?", "Remove node", MessageBoxButtons.YesNo) !=
                DialogResult.Yes)
                return;

            ActivateUiControls(false);

            var records = _exampleLinkCollection?
                .Where(n => n.Key.StartsWith(treeView_examples.SelectedNode.Name))
                .Select(n => n.Key)
                .ToArray() ?? Array.Empty<string>();

            foreach (var example in records)
                _exampleLinkCollection?.Remove(example);

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

                var fileNames = dataGrid.Rows[e.RowIndex].Cells[2]?.Value?.ToString()?.Split(
                    new[] { _appConfig.ConfigStorage.TableListDelimiter }, StringSplitOptions.RemoveEmptyEntries);

                listBox_fileList.SelectedValueChanged -= ListBox_fileList_SelectedValueChanged;

                listBox_fileList.Items.Clear();
                listBox_fileList.Items.AddRange(fileNames ?? Array.Empty<string>());
                if (fileNames?.Length > 0)
                    listBox_fileList.SetSelected(0, true);

                listBox_fileList.SelectedValueChanged += ListBox_fileList_SelectedValueChanged;

                if (_appConfig.ConfigStorage.ShowPreview)
                {
                    OpenFile();
                    dataGrid.Focus();
                }
            }
        }

        private void DataGridView_examples_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.C && e.Control)
                CopySample();
            /*else if (e.KeyCode == Keys.Delete)
            {
                DeleteSamples();
            }*/
            else if (e.KeyCode == Keys.Enter) OpenFile(true);
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
                Clipboard.SetText(dataGridView_examples.SelectedCells[0].Value.ToString() ?? string.Empty);
            }
            else
            {
                var copyText = new StringBuilder();
                foreach (DataGridViewCell cell in dataGridView_examples.SelectedCells)
                {
                    copyText.Append(cell.ToString());
                }

                Clipboard.SetText(copyText.ToString());
            }
        }

        // doesn't work. need tree path to find all samples. Change ListToTree to add actual paths
        private void DeleteSamples()
        {
            if (MessageBox.Show("Do you want to delete all selected samples for all files?", "Delete samples",
                MessageBoxButtons.YesNo) == DialogResult.No)
                return;

            var cellRowList = new List<int>();
            foreach (var cell in dataGridView_examples.SelectedCells)
                cellRowList.Add(((DataGridViewCell)cell).RowIndex);

            var rowList = cellRowList.Distinct().ToArray();
            foreach (var rowNumber in rowList)
            {
                var jsonPaths = dataGridView_examples
                    .Rows[rowNumber]
                    .Cells[3]?
                    .Value?
                    .ToString()?
                    .Split(new[] { _appConfig.ConfigStorage.TableListDelimiter }, StringSplitOptions.RemoveEmptyEntries);

                var jsonSample = dataGridView_examples.Rows[rowNumber].Cells[1]?.Value?.ToString();

                if (jsonPaths != null && jsonPaths.Length > 0)
                {
                    var jsonPath = _appConfig.ConfigStorage.JsonPathDiv + jsonPaths[0];
                    var samplesCollection = _exampleLinkCollection?.Where(n => n.Key == jsonPath);
                    samplesCollection?.FirstOrDefault()
                        .Value?
                        .RemoveAll(n => n.Value == CompactJson(jsonSample));
                }
            }
        }

        #endregion

        #region FileList

        private void ListBox_fileList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.C && e.Control)
                CopyFileName();
            else if (e.KeyCode == Keys.Enter)
                OpenFile(true);
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
            if (!_appConfig.ConfigStorage.ShowPreview)
                return;

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

            Clipboard.SetText(listBox_fileList.SelectedItem.ToString() ?? string.Empty);
        }

        // doesn't work. need tree path to find all samples. Change ListToTree to add actual paths
        private void DeleteSampleForFile()
        {
            if (MessageBox.Show("Do you want to delete all selected samples for all files?", "Delete samples",
                MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;

            var jsonPaths = dataGridView_examples
                .Rows[dataGridView_examples.SelectedCells[0].RowIndex]
                .Cells[3]?
                .Value?
                .ToString()?
                .Split(new[] { _appConfig.ConfigStorage.TableListDelimiter },
                    StringSplitOptions.RemoveEmptyEntries);

            var jsonSample = dataGridView_examples.Rows[dataGridView_examples.SelectedCells[0].RowIndex].Cells[3]?.Value
                ?.ToString();
            var fileNumber = listBox_fileList.SelectedIndex;
            var fileName = listBox_fileList.Items[fileNumber].ToString();
            var jsonPath = "";

            if (jsonPaths != null && jsonPaths.Length >= fileNumber)
                jsonPath = jsonPaths[fileNumber];

            _exampleLinkCollection[jsonPath].RemoveAll(n => n.FullFileName == fileName);
            listBox_fileList.Items.RemoveAt(fileNumber);
        }

        #endregion

        #region Schema

        private void Button_loadSchema_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileOk -= OpenFileDialog1_FileOk;
            openFileDialog1.FileOk -= OpenFileDialog1_FileOk_Schema;
            openFileDialog1.FileOk += OpenFileDialog1_FileOk_Schema;

            openFileDialog1.FileName = "";
            openFileDialog1.Title = "Open JSON schema";
            openFileDialog1.DefaultExt = _appConfig.ConfigStorage.DefaultTreeFileExtension;
            openFileDialog1.Filter = "JSON files|*.json|All files|*.*";
            openFileDialog1.ShowDialog();
        }

        private void OpenFileDialog1_FileOk_Schema(object sender, CancelEventArgs e)
        {
            const string rootName = "#";

            var schemaData = File.ReadAllText(openFileDialog1.FileName);

            var parser = new JsonPathParser
            {
                TrimComplexValues = false,
                SaveComplexValues = true,
                RootName = rootName,
                JsonPathDivider = _appConfig.ConfigStorage.JsonPathDiv
            };

            var schemaProperties = parser.ParseJsonToPathList(schemaData, out _, out _)?
                .Where(n =>
                    n.JsonPropertyType == JsonPropertyType.Array
                    || n.JsonPropertyType == JsonPropertyType.Object
                    || n.JsonPropertyType == JsonPropertyType.Property
                    || n.JsonPropertyType == JsonPropertyType.ArrayValue
                    || n.JsonPropertyType == JsonPropertyType.KeywordOrNumberProperty);

            var treeSchemaProperties = parser.ConvertForTreeProcessing(schemaProperties);
            _rightSchema = JsonPropertyListToSchemaObject(null,
                treeSchemaProperties,
                rootName,
                rootName,
                _appConfig.ConfigStorage.JsonPathDiv);
            _rootNodeRightSchema = ConvertSchemaObjectToTreeNodeRecursive(_rightSchema);
            treeView_rightSchema?.Nodes?.Clear();
            treeView_rightSchema?.Nodes?.Add(_rootNodeRightSchema);

            if (treeView_rightSchema != null && treeView_rightSchema.Nodes?.Count > 0)
            {
                treeView_rightSchema.Sort();
                treeView_rightSchema.Nodes[0].Expand();
            }

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
            if (e == null || e.Node == null)
                return;

            if (_leftDataPanel != null)
            {
                if (_rightDataPanel is PropertyDataPanel dp)
                    dp.OnRefClick -= FollowTheRefLinkLeft;
                else if (_rightDataPanel is ObjectDataPanel dp1)
                    dp1.OnRefClick += FollowTheRefLinkLeft;
                else if (_rightDataPanel is ArrayDataPanel dp2)
                    dp2.OnRefClick += FollowTheRefLinkLeft;

                splitContainer_schemaLeft.Panel2.Controls.Remove(_leftDataPanel);
            }

            if (_lastSelectedLeftSchemaNode != null)
                _lastSelectedLeftSchemaNode.BackColor = _lastSelectedLeftSchemaNodeColor;

            _lastSelectedLeftSchemaNode = e.Node;
            _lastSelectedLeftSchemaNodeColor = e.Node.BackColor;
            e.Node.BackColor = Color.DodgerBlue;

            //SelectSchemaNode(e.Node.Name, true);
            SelectSchemaNode(e.Node.FullPath.Replace("{}", "").Replace("[]", "").Replace('\\', '/'), true);

            var node = e.Node.Tag;
            if (node == null) return;

            if (node is SchemaProperty schemaProperty)
                _leftDataPanel = new PropertyDataPanel(schemaProperty);//, nodePath);
            else if (node is SchemaObject schemaObject)
                _leftDataPanel = new ObjectDataPanel(schemaObject);//, nodePath);
            else if (node is SchemaArray schemaArray)
                _leftDataPanel = new ArrayDataPanel(schemaArray);//, nodePath);

            if (_leftDataPanel != null)
            {
                _leftDataPanel.Dock = DockStyle.Fill;
                splitContainer_schemaLeft.Panel2.Controls.Add(_leftDataPanel);

                if (_rightDataPanel is PropertyDataPanel dp)
                    dp.OnRefClick += FollowTheRefLinkLeft;
                else if (_rightDataPanel is ObjectDataPanel dp1)
                    dp1.OnRefClick += FollowTheRefLinkLeft;
                else if (_rightDataPanel is ArrayDataPanel dp2)
                    dp2.OnRefClick += FollowTheRefLinkLeft;
            }
        }

        private void FollowTheRefLinkLeft(object sender, RefLinkClickEventArgs e)
        {
            var treeNode = FindTreeNodeByPath(e.LinkText.Split('/').ToList(), _rootNodeLeftSchema);
            treeView_leftSchema.SelectedNode = treeNode;
            treeView_leftSchema.SelectedNode?.Expand();
        }

        private void TreeView_rightSchema_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e == null || e.Node == null)
                return;

            if (_rightDataPanel != null)
            {
                if (_rightDataPanel is PropertyDataPanel dp)
                    dp.OnRefClick -= FollowTheRefLinkRight;
                else if (_rightDataPanel is ObjectDataPanel dp1)
                    dp1.OnRefClick += FollowTheRefLinkRight;
                else if (_rightDataPanel is ArrayDataPanel dp2)
                    dp2.OnRefClick += FollowTheRefLinkRight;

                splitContainer_schemaRight.Panel2.Controls.Remove(_rightDataPanel);
            }

            if (_lastSelectedRightSchemaNode != null)
                _lastSelectedRightSchemaNode.BackColor = _lastSelectedRightSchemaNodeColor;

            _lastSelectedRightSchemaNode = e.Node;
            _lastSelectedRightSchemaNodeColor = e.Node.BackColor;
            e.Node.BackColor = Color.DodgerBlue;

            //SelectSchemaNode(e.Node.Name, false);
            SelectSchemaNode(e.Node.FullPath.Replace("{}", "").Replace("[]", "").Replace('\\', '/'), false);

            var node = e.Node.Tag;
            if (node == null) return;

            if (node is SchemaProperty schemaProperty)
                _rightDataPanel = new PropertyDataPanel(schemaProperty);//, nodePath);
            else if (node is SchemaObject schemaObject)
                _rightDataPanel = new ObjectDataPanel(schemaObject);//, nodePath);
            else if (node is SchemaArray schemaArray)
                _rightDataPanel = new ArrayDataPanel(schemaArray);//, nodePath);

            if (_rightDataPanel != null)
            {
                _rightDataPanel.Dock = DockStyle.Fill;
                splitContainer_schemaRight.Panel2.Controls.Add(_rightDataPanel);

                if (_rightDataPanel is PropertyDataPanel dp) dp.OnRefClick += FollowTheRefLinkRight;
                else if (_rightDataPanel is ObjectDataPanel dp1) dp1.OnRefClick += FollowTheRefLinkRight;
                else if (_rightDataPanel is ArrayDataPanel dp2) dp2.OnRefClick += FollowTheRefLinkRight;
            }
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
            _leftSchema = GenerateSchemaFromSamplesTree(treeView_examples.SelectedNode, _exampleLinkCollection, rootName, null);
            _rootNodeLeftSchema = ConvertSchemaObjectToTreeNodeRecursive(_leftSchema);

            if (_rootNodeLeftSchema == null)
                return;

            treeView_leftSchema.Nodes.Clear();
            treeView_leftSchema.Nodes.Add(_rootNodeLeftSchema);
            treeView_leftSchema.Sort();

            if (_leftDataPanel != null)
                splitContainer_schemaRight.Panel2.Controls.Remove(_leftDataPanel);

            GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);
        }

        private void Button_saveLeftSchema_Click(object sender, EventArgs e)
        {
            saveFileDialog1.FileOk -= SaveFileDialog1_FileOk;
            saveFileDialog1.FileOk -=
                SaveFileDialog1_FileOk_LeftSchema;
            saveFileDialog1.FileOk -=
                SaveFileDialog1_FileOk_RightSchema;
            saveFileDialog1.FileOk +=
                SaveFileDialog1_FileOk_LeftSchema;
            saveFileDialog1.Title = "Save schema";
            saveFileDialog1.DefaultExt = "json";
            saveFileDialog1.Filter =
                "JSON files|*.json|All files|*.*";
            saveFileDialog1.FileName = "Schema_"
                                       + _appConfig.ConfigStorage.DefaultSaveFileName
                                       + DateTime.Today.ToShortDateString().Replace("/", "_")
                                       + ".json";

            saveFileDialog1.ShowDialog();
        }

        private void SaveFileDialog1_FileOk_LeftSchema(object sender, CancelEventArgs e)
        {
            if (_leftSchema == null)
                return;

            var jsonText = _leftSchema.ToJson();

            if (!string.IsNullOrEmpty(jsonText))
                File.WriteAllText(saveFileDialog1.FileName,
                    ReformatJson(jsonText, Formatting.Indented));

            GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);
        }

        private void Button_saveRightSchema_Click(object sender, EventArgs e)
        {
            saveFileDialog1.FileOk -= SaveFileDialog1_FileOk;
            saveFileDialog1.FileOk -=
                SaveFileDialog1_FileOk_LeftSchema;
            saveFileDialog1.FileOk -=
                SaveFileDialog1_FileOk_RightSchema;
            saveFileDialog1.FileOk +=
                SaveFileDialog1_FileOk_RightSchema;
            saveFileDialog1.Title = "Save schema";
            saveFileDialog1.DefaultExt = "json";
            saveFileDialog1.Filter =
                "JSON files|*.json|All files|*.*";
            saveFileDialog1.FileName = "Schema_"
                                       + _appConfig.ConfigStorage.DefaultSaveFileName
                                       + DateTime.Today.ToShortDateString().Replace("/", "_")
                                       + ".json";

            saveFileDialog1.ShowDialog();
        }

        private void SaveFileDialog1_FileOk_RightSchema(object sender, CancelEventArgs e)
        {
            if (_rightSchema == null || string.IsNullOrEmpty(_rightSchema.ToJson()))
                return;

            File.WriteAllText(saveFileDialog1.FileName,
                ReformatJson(_rightSchema.ToJson(), Formatting.Indented));

            GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);
        }

        private void Button_compare_Click(object sender, EventArgs e)
        {
            if (_rootNodeLeftSchema == null || _rootNodeRightSchema == null)
                return;

            var deep = checkBox_deepCompare.Checked;
            var checkedNodes = checkBox_selectedSchema.Checked;
            var leftNode = _rootNodeLeftSchema;
            var rightNode = _rootNodeRightSchema;
            if (checkedNodes)
            {
                if (treeView_leftSchema.SelectedNode == null || treeView_rightSchema.SelectedNode == null)
                {
                    MessageBox.Show("Please select nodes on both left and right panel first.");
                    return;
                }

                leftNode = treeView_leftSchema.SelectedNode;
                rightNode = treeView_rightSchema.SelectedNode;
            }

            treeView_leftSchema.AfterSelect -= TreeView_leftSchema_AfterSelect;
            treeView_rightSchema.AfterSelect -= TreeView_rightSchema_AfterSelect;

            // highlight missing nodes in left tree
            var result = CompareNode(leftNode, rightNode, deep);
            foreach (var nodePath in result)
            {
                var missingNode = _rootNodeLeftSchema.Nodes.Find(nodePath, true).FirstOrDefault();
                if (missingNode != null)
                {
                    missingNode.BackColor = Color.Red;
                    treeView_leftSchema.SelectedNode = missingNode;
                }
            }

            // find missing nodes in right tree
            result = CompareNode(rightNode, leftNode, deep);
            foreach (var nodePath in result)
            {
                var missingNode = _rootNodeRightSchema.Nodes.Find(nodePath, true).FirstOrDefault();
                if (missingNode != null)
                {
                    missingNode.BackColor = Color.Red;
                    treeView_rightSchema.SelectedNode = missingNode;
                }
            }

            treeView_leftSchema.AfterSelect += TreeView_leftSchema_AfterSelect;
            treeView_rightSchema.AfterSelect += TreeView_rightSchema_AfterSelect;
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
                && (_lastSearchListLeft == null
                    || _lastSearchListLeft.Count <= 0
                    || _lastSearchListRight == null
                    || _lastSearchListRight.Count <= 0))
                SearchNodeNameInTrees(_lastSearchString);

            if (_lastTreeViewSelected != null)
            {
                if (_lastTreeViewSelected.Name.Contains("left", StringComparison.InvariantCultureIgnoreCase))
                {
                    if (_lastSearchListLeft == null || _lastSearchListLeft.Count <= 0)
                        return;

                    _searchListPositionLeft++;
                    if (_searchListPositionLeft >= _lastSearchListLeft.Count)
                        _searchListPositionLeft = 0;

                    var nodeToSelect = _lastTreeViewSelected.Nodes
                        .Find(_lastSearchListLeft[_searchListPositionLeft], true)
                        .FirstOrDefault();

                    if (nodeToSelect != null)
                        _lastTreeViewSelected.SelectedNode = nodeToSelect;
                }
                else
                {
                    if (_lastSearchListRight == null || _lastSearchListRight.Count <= 0)
                        return;

                    _searchListPositionRight++;
                    if (_searchListPositionRight >= _lastSearchListRight.Count)
                        _searchListPositionRight = 0;

                    var nodeToSelect = _lastTreeViewSelected.Nodes
                        .Find(_lastSearchListRight[_searchListPositionRight], true)
                        .FirstOrDefault();

                    if (nodeToSelect != null)
                        _lastTreeViewSelected.SelectedNode = nodeToSelect;
                }
            }
        }

        private void Button_findPrev_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(_lastSearchString)
                && (_lastSearchListLeft == null
                    || _lastSearchListLeft.Count <= 0
                    || _lastSearchListRight == null
                    || _lastSearchListRight.Count <= 0))
                SearchNodeNameInTrees(_lastSearchString);

            if (_lastTreeViewSelected != null)
            {
                if (_lastTreeViewSelected.Name.Contains("left", StringComparison.InvariantCultureIgnoreCase))
                {
                    if (_lastSearchListLeft == null || _lastSearchListLeft.Count <= 0)
                        return;

                    _searchListPositionLeft--;
                    if (_searchListPositionLeft < 0)
                        _searchListPositionLeft = _lastSearchListLeft.Count - 1;

                    var nodeToSelect = _lastTreeViewSelected.Nodes
                        .Find(_lastSearchListLeft[_searchListPositionLeft], true)
                        .FirstOrDefault();

                    if (nodeToSelect != null)
                        _lastTreeViewSelected.SelectedNode = nodeToSelect;
                }
                else
                {
                    if (_lastSearchListRight == null || _lastSearchListRight.Count <= 0)
                        return;

                    _searchListPositionRight--;
                    if (_searchListPositionRight < 0)
                        _searchListPositionRight = _lastSearchListRight.Count - 1;

                    var nodeToSelect = _lastTreeViewSelected.Nodes
                        .Find(_lastSearchListRight[_searchListPositionRight], true)
                        .FirstOrDefault();

                    if (nodeToSelect != null)
                        _lastTreeViewSelected.SelectedNode = nodeToSelect;
                }
            }
        }

        private void Button_clearCompare_Click(object sender, EventArgs e)
        {
            ClearBackground(_rootNodeLeftSchema);
            ClearBackground(_rootNodeRightSchema);
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
            CompareDataPanels();
        }

        private void ToolStripMenuItem_unfoldLS_Click(object sender, EventArgs e)
        {
            UnfoldNode(treeView_leftSchema);
        }

        private void ToolStripMenuItem_foldLS_Click(object sender, EventArgs e)
        {
            FoldNode(treeView_leftSchema);
        }

        private void ToolStripMenuItem_renameLS_Click(object sender, EventArgs e)
        {
            treeView_leftSchema.SelectedNode.BeginEdit();
        }

        private void TreeView_BeforeLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if (e.Node.Tag == null)
                e.CancelEdit = true;
        }

        private void TreeView_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if (e.Label == null)
            {
                e.CancelEdit = true;
                return;
            }

            try
            {
                var newName = e.Label.Trim();

                if (string.IsNullOrEmpty(newName))
                    e.CancelEdit = true;
                else if (newName != e.Node.Text)
                {
                    if (newName.EndsWith("{}") || newName.EndsWith("[]"))
                        newName = newName[..^2];

                    RenameSchemaNode(e.Node, newName);
                }

                if (e.Node.Tag is SchemaObject)
                {
                    e.Node.Text = newName + "{}";
                }
                else if (e.Node.Tag is SchemaArray)
                {
                    e.Node.Text = newName + "[]";
                }
            }
            catch
            {
            }

            e.CancelEdit = true;
        }

        private void ToolStripMenuItem_copyLS_Click(object sender, EventArgs e)
        {
            CopyNodeText(treeView_leftSchema);
        }

        private void ToolStripMenuItem_deleteLS_Click(object sender, EventArgs e)
        {
            DeleteNodeFromSchema(treeView_leftSchema);
        }

        private void ToolStripMenuItem_unfoldRS_Click(object sender, EventArgs e)
        {
            UnfoldNode(treeView_rightSchema);
        }

        private void ToolStripMenuItem_foldRS_Click(object sender, EventArgs e)
        {
            FoldNode(treeView_rightSchema);
        }

        private void ToolStripMenuItem_renameRS_Click(object sender, EventArgs e)
        {
            treeView_rightSchema.SelectedNode.BeginEdit();
        }

        private void ToolStripMenuItem_copyRS_Click(object sender, EventArgs e)
        {
            CopyNodeText(treeView_rightSchema);
        }

        private void ToolStripMenuItem_deleteRS_Click(object sender, EventArgs e)
        {
            DeleteNodeFromSchema(treeView_rightSchema);
        }

        private static void AddItemToSchemaTree(string newNodeName, TreeView tree, int itemType)
        {
            if (tree.SelectedNode != null
                && tree.SelectedNode.Tag == null
                && tree.SelectedNode.Text == "properties{}")
            {
                ISchemaBase newSchemaNode = null;
                if (itemType == 0)
                {
                    newSchemaNode = new SchemaProperty(newNodeName);
                }
                else if (itemType == 1)
                {
                    newSchemaNode = new SchemaObject(newNodeName)
                    {
                        Type = new List<string>() { "object" },
                        Properties = new List<ISchemaBase>()
                    };
                }
                else if (itemType == 2)
                {
                    newSchemaNode = new SchemaArray(newNodeName)
                    {
                        Type = new List<string>() { "array" },
                        Items = new SchemaObject("properties") { Parent = newSchemaNode }
                    };
                }
                else
                    return;

                //add node to the schema
                ISchemaBase currentSchemaNode;
                var currentTreeNode = tree.SelectedNode.Parent;

                if (tree.SelectedNode.Parent?.Tag is SchemaObject schemaObject)
                {
                    //check if there is already a node with this name and rename new with counter
                    var counter = 0;
                    while (schemaObject.Properties.Any(n => n.Name == newSchemaNode.Name))
                    {
                        counter++;
                        newSchemaNode.Name = newNodeName + counter;
                    }

                    newSchemaNode.Id = schemaObject.GetReferencePath() + "/properties/" + newSchemaNode.Name;
                    newSchemaNode.Parent = schemaObject;
                    schemaObject.Properties.Add(newSchemaNode);
                    currentSchemaNode = schemaObject;
                }
                else return;


                var currentNodePath = tree.SelectedNode.Name;

                // refresh tree from current point
                var newTree = ConvertSchemaObjectToTreeNodeRecursive(currentSchemaNode);
                currentTreeNode.Nodes.Clear();
                if (newTree != null && newTree.Nodes != null)
                {
                    foreach (TreeNode node in newTree.Nodes)
                    {
                        currentTreeNode.Nodes.Add(node);
                    }
                }

                // select the current node again
                var nodes = tree.Nodes.Find(currentNodePath + "/" + "newNode", true);
                if (nodes.Any())
                {
                    tree.SelectedNode = nodes.First();
                }
            }
        }

        private void ContextMenuStrip_leftSchema_Opening(object sender, CancelEventArgs e)
        {
            bool enableAdd = treeView_leftSchema.SelectedNode != null
                             && treeView_leftSchema.SelectedNode.Tag == null
                             && treeView_leftSchema.SelectedNode.Text == "properties{}";

            contextMenuStrip_leftSchema.Items["toolStripMenuItem_addPropertyLS"].Enabled = enableAdd;
            contextMenuStrip_leftSchema.Items["toolStripMenuItem_addObjectLS"].Enabled = enableAdd;
            contextMenuStrip_leftSchema.Items["toolStripMenuItem_addArrayLS"].Enabled = enableAdd;
            contextMenuStrip_leftSchema.Items["toolStripMenuItem_renameLS"].Enabled = !enableAdd;
            contextMenuStrip_leftSchema.Items["toolStripMenuItem_deleteLS"].Enabled = !enableAdd;
        }

        private void ToolStripMenuItem_addPropertyLS_Click(object sender, EventArgs e)
        {
            AddItemToSchemaTree("newNode", treeView_leftSchema, 0);
        }

        private void ToolStripMenuItem_addObjectLS_Click(object sender, EventArgs e)
        {
            AddItemToSchemaTree("newNode", treeView_leftSchema, 1);
        }

        private void ToolStripMenuItem_addArrayLS_Click(object sender, EventArgs e)
        {
            AddItemToSchemaTree("newNode", treeView_leftSchema, 2);
        }

        private void ContextMenuStrip_rightSchema_Opening(object sender, CancelEventArgs e)
        {
            bool enableAdd = treeView_rightSchema.SelectedNode != null
                             && treeView_rightSchema.SelectedNode.Tag == null
                             && treeView_rightSchema.SelectedNode.Text == "properties{}";

            contextMenuStrip_rightSchema.Items["toolStripMenuItem_addPropertyRS"].Enabled = enableAdd;
            contextMenuStrip_rightSchema.Items["toolStripMenuItem_addObjectRS"].Enabled = enableAdd;
            contextMenuStrip_rightSchema.Items["toolStripMenuItem_addArrayRS"].Enabled = enableAdd;
            contextMenuStrip_rightSchema.Items["toolStripMenuItem_renameRS"].Enabled = !enableAdd;
            contextMenuStrip_rightSchema.Items["toolStripMenuItem_deleteRS"].Enabled = !enableAdd;
        }

        private void ToolStripMenuItem_addPropertyRS_Click(object sender, EventArgs e)
        {
            AddItemToSchemaTree("newNode", treeView_rightSchema, 0);
        }

        private void ToolStripMenuItem_addObjectRS_Click(object sender, EventArgs e)
        {
            AddItemToSchemaTree("newNode", treeView_rightSchema, 1);
        }

        private void ToolStripMenuItem_addArrayRS_Click(object sender, EventArgs e)
        {
            AddItemToSchemaTree("newNode", treeView_rightSchema, 2);
        }

        private void SearchNodeNameInTrees(string token)
        {
            if (_rootNodeLeftSchema != null && _rootNodeRightSchema != null)
            {
                _lastSearchListLeft = FindNodeName(_rootNodeLeftSchema, token);
                _lastSearchListRight = FindNodeName(_rootNodeRightSchema, token);
            }
        }

        private void SelectSchemaNode(string path, bool toRightPanel)
        {
            if (!_appConfig.ConfigStorage.SchemaFollowSelection || string.IsNullOrEmpty(path))
                return;

            if (toRightPanel)
            {
                var nodes = treeView_rightSchema.Nodes.Find(path, true);
                if (nodes.Any())
                {
                    treeView_rightSchema.SelectedNode = nodes.First();
                    CompareDataPanels();
                }
            }
            else
            {
                var nodes = treeView_leftSchema.Nodes.Find(path, true);

                if (nodes.Any())
                    treeView_leftSchema.SelectedNode = nodes.First();
            }
        }

        private List<string> CompareNode(TreeNode leftNode, TreeNode rightNode, bool deepCompare = false)
        {
            var result = new List<string>();

            if (leftNode == null || rightNode == null)
                return result;

            foreach (TreeNode node in leftNode.Nodes)
            {
                var notSame = false;
                var rightChild = GetNodeChildByPath(rightNode, node.FullPath);
                if (rightChild == null)
                {
                    notSame = true;
                    var nodes = GetNodeNamesRecursive(node);
                    result.AddRange(nodes);
                }
                else if (deepCompare)
                {
                    var leftSchemaNode = FindNodeByTreePath(node.FullPath, _leftSchema);

                    var rightSchemaNode = FindNodeByTreePath(rightChild.FullPath, _rightSchema);

                    if (!(leftSchemaNode == null && rightSchemaNode == null))
                    {
                        if (leftSchemaNode == null || rightSchemaNode == null)
                        {
                            notSame = true;
                        }
                        else if (leftSchemaNode.Name != rightSchemaNode.Name)
                        {
                            notSame = true;
                        }
                        else if (leftSchemaNode.Id != rightSchemaNode.Id)
                        {
                            notSame = true;
                        }
                        else if (leftSchemaNode.Description != rightSchemaNode.Description)
                        {
                            notSame = true;
                        }
                        else if (leftSchemaNode.Reference != rightSchemaNode.Reference)
                        {
                            notSame = true;
                        }
                        else
                        {
                            if (leftSchemaNode.Type.Count == rightSchemaNode.Type.Count)
                            {
                                foreach (var t1 in leftSchemaNode.Type)
                                    if (!rightSchemaNode.Type.Contains(t1))
                                    {
                                        notSame = true;
                                        break;
                                    }
                            }
                            else
                            {
                                notSame = true;
                            }
                        }

                        // check specific properties
                        if (!notSame && leftSchemaNode is SchemaProperty lSchemaProperty &&
                            rightSchemaNode is SchemaProperty rSchemaProperty)
                        {
                            if (lSchemaProperty.Default != rSchemaProperty.Default)
                            {
                                notSame = true;
                            }
                            else if (lSchemaProperty.Pattern != rSchemaProperty.Pattern)
                            {
                                notSame = true;
                            }
                            else if (lSchemaProperty.Enum.Count == rSchemaProperty.Enum.Count)
                            {
                                foreach (var t1 in lSchemaProperty.Enum)
                                    if (!rSchemaProperty.Enum.Contains(t1))
                                    {
                                        notSame = true;
                                        break;
                                    }
                            }
                            else
                            {
                                notSame = true;
                            }
                        }
                        else if (!notSame && leftSchemaNode is SchemaObject lSchemaObject &&
                                 rightSchemaNode is SchemaObject rSchemaObject)
                        {
                            if (lSchemaObject.AdditionalProperties != rSchemaObject.AdditionalProperties)
                            {
                                notSame = true;
                            }
                            else
                            {
                                if (lSchemaObject.Required.Count == rSchemaObject.Required.Count)
                                {
                                    foreach (var t1 in lSchemaObject.Required)
                                        if (!rSchemaObject.Required.Contains(t1))
                                        {
                                            notSame = true;
                                            break;
                                        }
                                }
                                else
                                {
                                    notSame = true;
                                }

                                if (lSchemaObject.Properties.Count != rSchemaObject.Properties.Count)
                                    notSame = true;
                            }
                        }
                        else if (!notSame && leftSchemaNode is SchemaArray lSchemaArray &&
                                 rightSchemaNode is SchemaArray rSchemaArray)
                        {
                            if (lSchemaArray.UniqueItemsOnly != rSchemaArray.UniqueItemsOnly)
                                notSame = true;
                            else if (lSchemaArray.Items == null && rSchemaArray.Items != null
                                     || lSchemaArray.Items != null && rSchemaArray.Items == null)
                                notSame = true;
                        }
                    }
                }

                if (notSame)
                    result.Add(node.Name);

                var res = CompareNode(node, rightChild, deepCompare);
                result.AddRange(res);
            }

            return result;
        }

        private List<string> GetNodeNamesRecursive(TreeNode node)
        {
            var result = new List<string>();

            if (node == null || node.Nodes == null)
                return result;

            foreach (TreeNode subNode in node.Nodes)
            {
                result.Add(subNode.Name);
                result.AddRange(GetNodeNamesRecursive(subNode));
            }

            return result;
        }


        private static TreeNode GetNodeChildByPath(TreeNode node, string path)
        {
            if (node == null || string.IsNullOrEmpty(path))
                return null;

            foreach (TreeNode childNode in node.Nodes)
            {
                if (childNode.FullPath == path)
                    return childNode;
            }

            return null;
        }

        private static List<string> FindNodeName(TreeNode rootNode, string token)
        {
            var result = new List<string>();
            if (rootNode.Text.Contains(token))
                result.Add(rootNode.Name);

            foreach (TreeNode node in rootNode.Nodes)
                result.AddRange(FindNodeName(node, token));

            return result;
        }

        private static ISchemaBase FindNodeByTreePath(string fullPath, ISchemaBase rootSchemaNode)
        {
            if (string.IsNullOrEmpty(fullPath) || rootSchemaNode == null)
                return null;

            var path = fullPath
                .Replace("{", "")
                .Replace("}", "")
                .Replace("[", "")
                .Replace("]", "");

            var propertyNames = new SchemaPropertyNames();
            ISchemaBase result = null;
            var definitionsFlag = false;
            foreach (var token in path.Split('\\'))
            {
                if (token == "#")
                {
                    result = rootSchemaNode;
                }
                else
                {
                    if (result is SchemaObject currentObject)
                    {
                        if (token == propertyNames.Properties)
                            continue;

                        if (token == propertyNames.Definitions)
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
                    else if (result is SchemaArray currentArray)
                    {
                        if (token == propertyNames.Items)
                            result = currentArray.Items;
                    }
                    else if (result is SchemaProperty currentProperty)
                    {
                        if (currentProperty.Name == token)
                            result = currentProperty;
                    }
                }

                if (result == null)
                    return null;
            }

            return result;
        }

        private static void ClearBackground(TreeNode rootNode)
        {
            rootNode.BackColor = Color.Empty;

            foreach (TreeNode node in rootNode.Nodes)
                ClearBackground(node);
        }

        private void CompareDataPanels()
        {
            if (_leftDataPanel is PropertyDataPanel dpl && _rightDataPanel is PropertyDataPanel dpr)
            {
                if (dpl.ObjectPathText != dpr.ObjectPathText)
                    dpl.ObjectPathBackColor = dpr.ObjectPathBackColor = Color.LightPink;

                if (dpl.ObjectTypeText != dpr.ObjectTypeText)
                    dpl.ObjectTypeBackColor = dpr.ObjectTypeBackColor = Color.LightPink;

                if (dpl.ObjectDescText != dpr.ObjectDescText)
                    dpl.ObjectDescBackColor = dpr.ObjectDescBackColor = Color.LightPink;

                if (dpl.ObjectRefText != dpr.ObjectRefText)
                    dpl.ObjectRefBackColor = dpr.ObjectRefBackColor = Color.LightPink;

                if (dpl.ObjectDefaultText != dpr.ObjectDefaultText)
                    dpl.ObjectDefaultBackColor = dpr.ObjectDefaultBackColor = Color.LightPink;

                if (dpl.ObjectEnumText != dpr.ObjectEnumText)
                    dpl.ObjectEnumBackColor = dpr.ObjectEnumBackColor = Color.LightPink;
            }
            else if (_leftDataPanel is ObjectDataPanel dpl1 && _rightDataPanel is ObjectDataPanel dpr1)
            {
                if (dpl1.ObjectPathText != dpr1.ObjectPathText)
                    dpl1.ObjectPathBackColor = dpr1.ObjectPathBackColor = Color.LightPink;

                if (dpl1.ObjectTypeText != dpr1.ObjectTypeText)
                    dpl1.ObjectTypeBackColor = dpr1.ObjectTypeBackColor = Color.LightPink;

                if (dpl1.ObjectDescText != dpr1.ObjectDescText)
                    dpl1.ObjectDescBackColor = dpr1.ObjectDescBackColor = Color.LightPink;

                if (dpl1.ObjectRefText != dpr1.ObjectRefText)
                    dpl1.ObjectRefBackColor = dpr1.ObjectRefBackColor = Color.LightPink;

                if (dpl1.ObjectAdditionalText != dpr1.ObjectAdditionalText)
                    dpl1.ObjectAdditionalBackColor = dpr1.ObjectAdditionalBackColor = Color.LightPink;

                if (dpl1.ObjectRequiredText != dpr1.ObjectRequiredText)
                    dpl1.ObjectRequiredBackColor = dpr1.ObjectRequiredBackColor = Color.LightPink;
            }
            else if (_leftDataPanel is ArrayDataPanel dpl2 && _rightDataPanel is ArrayDataPanel dpr2)
            {
                if (dpl2.ObjectPathText != dpr2.ObjectPathText)
                    dpl2.ObjectPathBackColor = dpr2.ObjectPathBackColor = Color.LightPink;

                if (dpl2.ObjectTypeText != dpr2.ObjectTypeText)
                    dpl2.ObjectTypeBackColor = dpr2.ObjectTypeBackColor = Color.LightPink;

                if (dpl2.ObjectDescText != dpr2.ObjectDescText)
                    dpl2.ObjectDescBackColor = dpr2.ObjectDescBackColor = Color.LightPink;

                if (dpl2.ObjectRefText != dpr2.ObjectRefText)
                    dpl2.ObjectRefBackColor = dpr2.ObjectRefBackColor = Color.LightPink;

                if (dpl2.ObjectUniqueText != dpr2.ObjectUniqueText)
                    dpl2.ObjectUniqueBackColor = dpr2.ObjectUniqueBackColor = Color.LightPink;
            }
        }

        private void RenameSchemaNode(TreeNode treeNode, string newName)
        {
            if (treeNode == null || treeNode.Tag == null)
                return;

            ActivateUiControls(false);

            if (treeNode.Tag is ISchemaBase currentNode)
            {
                var oldPath = currentNode.GetReferencePath();

                currentNode.RenameNode(newName);

                if (currentNode.Id == oldPath)
                    currentNode.Id = currentNode.GetReferencePath();
            }

            ActivateUiControls(true);
            _searchListPositionLeft = -1;
            _lastSearchListLeft = new List<string>();
            _searchListPositionRight = -1;
            _lastSearchListRight = new List<string>();
        }

        private void DeleteNodeFromSchema(TreeView tree)
        {
            if (tree.SelectedNode == null)
                return;

            if (MessageBox.Show("Are you sure to remove the selected node?",
                "Remove node",
                MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;

            ActivateUiControls(false);

            var success = false;
            if (tree.SelectedNode.Tag is ISchemaBase currentNode)
            {
                success = currentNode.DeleteNode();
            }

            if (success)
                treeView_examples.Nodes.Remove(tree.SelectedNode);

            ActivateUiControls(true);
            _searchListPositionLeft = -1;
            _lastSearchListLeft = new List<string>();
            _searchListPositionRight = -1;
            _lastSearchListRight = new List<string>();
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

        private void OnResizeEditor(object sender, EventArgs e)
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
            await ReadjustRows(dataGridView_examples).ConfigureAwait(false);
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
            ActivateUiControls(true);
        }

        private void TextBox_description_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (textBox_description.ReadOnly)
            {
                if (string.IsNullOrEmpty(textBox_description.Text))
                    textBox_description.Text = "Description: \r\n*Note: ";

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
                    _nodeDescriptions.TryGetValue(treeView_examples?.SelectedNode?.Name ?? "", out var descText);
                    textBox_description.Text = descText;
                    textBox_description.ReadOnly = true;
                    label_descSave.Visible = false;
                    label_edit.Visible = true;
                }
                else if (e.KeyCode == Keys.Enter && e.Control)
                {
                    try
                    {
                        _nodeDescriptions[treeView_examples?.SelectedNode?.Name ?? ""] = textBox_description.Text;
                    }
                    catch
                    {
                        _nodeDescriptions.Add(treeView_examples?.SelectedNode?.Name ?? "", textBox_description.Text);
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
            {
                var fileType = GetFileTypeFromFileName(fileName, _appConfig.ConfigStorage.FileTypes, _appConfig.ConfigStorage.DefaultContentType);
                DeserializeFile(fileName, fileType, jsonPropertiesCollection);

                if (fileNumber % 10 == 0)
                    Invoke((MethodInvoker)delegate
                   {
                       var fnum = fileNumber.ToString();
                       toolStripStatusLabel1.Text = "Files parsed " + fnum + "/" + filesList.Length;
                   });

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
            catch
            {
                return;
            }

            if (string.IsNullOrEmpty(jsonStr))
                return;

            var parser = new JsonPathParser
            {
                TrimComplexValues = false,
                SaveComplexValues = true,
                RootName = "",
                JsonPathDivider = _appConfig.ConfigStorage.JsonPathDiv
            };

            var jsonProperties = parser.ParseJsonToPathList(jsonStr, out var endPos, out var errorFound)?
                .Where(item => item.JsonPropertyType != JsonPropertyType.Comment
                               && item.JsonPropertyType != JsonPropertyType.EndOfArray
                               && item.JsonPropertyType != JsonPropertyType.EndOfObject
                               && item.JsonPropertyType != JsonPropertyType.Error
                               && item.JsonPropertyType != JsonPropertyType.Unknown)
                .ToList();

            if (jsonProperties == null || !jsonProperties.Any())
                return;

            var version = jsonProperties
                .FirstOrDefault(n => n.Path == _appConfig.ConfigStorage.VersionTagName)?
                .Value ?? "";

            var rootObject = jsonProperties
                .FirstOrDefault(n => n.JsonPropertyType == JsonPropertyType.Object
                                     && string.IsNullOrEmpty(n.Name)
                                     && string.IsNullOrEmpty(n.Path));

            jsonProperties.Remove(rootObject);

            Parallel.ForEach(jsonProperties, item =>
            //foreach (var item in jsonProperties)
            {
                item.Path = item.Path.TrimStart(_appConfig.ConfigStorage.JsonPathDiv);

                foreach (var fType in _appConfig.ConfigStorage.FileTypes.Where(n => !string.IsNullOrEmpty(n.PropertyTypeName)))
                {
                    if (item.Path.StartsWith(fType.PropertyTypeName))
                    {
                        fileType = fType.ContentType;
                        break;
                    }
                }

                var newItem = new JsonProperty
                {
                    PathDelimiter = _appConfig.ConfigStorage.JsonPathDiv,
                    Name = item.Name,
                    Value = item.Value,
                    ContentType = fileType,
                    FullFileName = longFileName,
                    Version = version,
                    JsonPath = item.Path,
                    VariableType = item.ValueType,
                    ObjectType = item.JsonPropertyType
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
                parentName[i] = parentName[i].ToLower();

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
                    .Where(n => n.FullFileName == actionItem.Key)
                    .ToArray();

                // iterate through single file item after item
                foreach (var actionProperty in actionItem)
                {
                    var moveToPathTmp = new StringBuilder();

                    if (string.IsNullOrEmpty(moveToPath))
                        moveToPathTmp.Append(actionProperty.ParentPath);
                    else
                        moveToPathTmp.Append(moveToPath);

                    moveToPathTmp.Append(
                        $".<{actionProperty.Value.Replace(_appConfig.ConfigStorage.JsonPathDiv, '_')}>");

                    //get clildren in the file for single item
                    var actionMembers = fileProperties
                        .Where(n => n.JsonPath.Contains(actionProperty.ParentPath))
                        .ToArray();

                    foreach (var actionMember in actionMembers)
                    {
                        var flattenedPath =
                            actionMember.JsonPath.Replace(actionProperty.ParentPath, moveToPathTmp.ToString());
                        actionMember.FlattenedJsonPath = flattenedPath;
                    }
                }

                if (processedItemsNumber % 50 == 0)
                {
                    Invoke((MethodInvoker)delegate
                   {
                       toolStripStatusLabel1.Text =
                           contentType + " converted " + processedItemsNumber + "/" + totalItemsNumber;
                   });
                }

                processedItemsNumber++;
            }
        }

        private TreeNode GenerateTreeFromList(IEnumerable<JsonProperty> rootCollection)
        {
            var node = new TreeNode(_appConfig.ConfigStorage.RootNodeName);

            if (rootCollection == null || !rootCollection.Any())
                return node;

            var itemNumber = 0;
            var totalItemNumber = rootCollection.Count();

            foreach (var propertyItem in rootCollection)
            {
                var itemName =
                    $"<{propertyItem.ContentType}>{_appConfig.ConfigStorage.JsonPathDiv}{propertyItem.UnifiedFlattenedJsonPath}"
                        .TrimEnd(_appConfig.ConfigStorage.JsonPathDiv);

                if (propertyItem.ObjectType != JsonPropertyType.Array)
                {
                    if (!_exampleLinkCollection.ContainsKey(itemName))
                        _exampleLinkCollection.Add(itemName, new List<JsonProperty> { propertyItem });
                    else
                        _exampleLinkCollection[itemName].Add(propertyItem);
                }

                var tmpNode = node;
                var tmpPath = new StringBuilder();

                foreach (var token in itemName.Split(new[] { _appConfig.ConfigStorage.JsonPathDiv },
                    StringSplitOptions.RemoveEmptyEntries))
                {
                    if (tmpPath.Length > 0)
                        tmpPath.Append(_appConfig.ConfigStorage.JsonPathDiv + token);
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
                                    .Replace($"<{propertyItem.ContentType}>{_appConfig.ConfigStorage.JsonPathDiv}", ""));

                        if (tagItem == null)
                            tagItem = new JsonProperty
                            {
                                VariableType = JsonValueType.Object,
                                FullFileName = propertyItem.FullFileName,
                                ObjectType = JsonPropertyType.Object,
                                Name = $"<{propertyItem.ContentType}>{_appConfig.ConfigStorage.JsonPathDiv}",
                                JsonPath = "",
                                Value = "",
                                Version = propertyItem.Version,
                                ContentType = propertyItem.ContentType,
                                PathDelimiter = _appConfig.ConfigStorage.JsonPathDiv
                            };

                        if (tagItem.ObjectType == JsonPropertyType.Array)
                            nodeName += "[]";
                        else if (tagItem.ObjectType == JsonPropertyType.Object)
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
            var treeFile = Path.ChangeExtension(longFileName, _appConfig.ConfigStorage.DefaultTreeFileExtension);
            var examplesFile = Path.ChangeExtension(longFileName, _appConfig.ConfigStorage.DefaultExamplesFileExtension);

            FormCaption = _appConfig.ConfigStorage.DefaultEditorFormCaption;

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
                MessageBox.Show($"File read exception [{longFileName}]: {ex.Message}");
                toolStripStatusLabel1.Text = "Failed to load database";
            }

            if (rootNodeExamples != null)
                _rootNodeExamples = rootNodeExamples;

            tabControl1.TabPages[1].Enabled = true;
            FormCaption = $"{_appConfig.ConfigStorage.DefaultEditorFormCaption} {GetShortFileName(longFileName)}";
            treeView_examples.Nodes.Clear();
            treeView_examples.Nodes.Add(_rootNodeExamples);
            treeView_examples.Nodes[0].Expand();
            tabControl1.SelectedTab = tabControl1.TabPages[1];
            ActivateUiControls(true, false);

            if (_rootNodeExamples.Nodes.Count > 0)
            {
                try
                {
                    var t = Task.Run(() =>
                        {
                            exampleLinkCollection = LoadBinary<Dictionary<string, List<JsonProperty>>>(examplesFile);
                        }
                    );
                    await Task.WhenAll(t).ConfigureAwait(true);
                    toolStripStatusLabel1.Text = "";
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"File read exception [{longFileName}]: {ex.Message}");
                    toolStripStatusLabel1.Text = "Failed to load database";
                }

                if (exampleLinkCollection != null)
                    _exampleLinkCollection = exampleLinkCollection;
            }

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
            toolStripStatusLabel1.Text = "Displaying " + records?.Count + " records";

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
            comboBox_ExVersions.Items.Add(_appConfig.ConfigStorage.DefaultVersionCaption);
            comboBox_ExVersions.SelectedIndex = 0;

            var versionCollection = new List<string>();
            var groupedByVersionRecords = records?.GroupBy(n => n.Version);

            if (groupedByVersionRecords == null) return false;

            foreach (var versionGroup in groupedByVersionRecords)
            {
                var currentVersion = versionGroup.Key ?? "";
                versionCollection.Add(currentVersion);

                var groupedByValueRecords = versionGroup.GroupBy(n => n.Value);

                if (!groupedByValueRecords.Any())
                    continue;

                foreach (var valueGroup in groupedByValueRecords)
                {
                    var pathList = new StringBuilder();
                    var fileNameList = new StringBuilder();
                    var recordValue = "";

                    try
                    {
                        recordValue = _appConfig.ConfigStorage.BeautifyJson
                            ? BeautifyJson(valueGroup.Key, _appConfig.ConfigStorage.ReformatJson)
                            : valueGroup.Key;
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
                                            _appConfig.ConfigStorage.TableListDelimiter);
                        pathList.Append(record.JsonPath + _appConfig.ConfigStorage.TableListDelimiter);
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
                searchParam = new SearchItem(_appConfig.ConfigStorage.DefaultVersionCaption);

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
            toolStripStatusLabel1.Text = $"Filtering {rowsNumber} rows";

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
                _lastSearchList.Add(new SearchItem(_appConfig.ConfigStorage.DefaultVersionCaption));

            var lastSearch = _lastSearchList.Last();
            if (lastSearch.Version != _appConfig.ConfigStorage.DefaultVersionCaption)
                FillExamplesGrid(exampleLinkCollection, _lastSelectedExamplesNode, searchParam);

            if (comboBox_ExVersions.Items.Contains(searchParam.Version))
            {
                comboBox_ExVersions.SelectedItem = searchParam.Version;
            }
            else
            {
                comboBox_ExVersions.SelectedItem = _appConfig.ConfigStorage.DefaultVersionCaption;
                return;
            }

            lastSearch.Version = searchParam.Version;
            var rows = _examplesTable.Rows;
            var rowsNumber = rows.Count;
            toolStripStatusLabel1.Text = $"Filtering {rowsNumber} rows";

            await Task.Run(() =>
            {
                for (var i = 0; i < rows.Count; i++)
                {
                    var cellValue = rows[i].ItemArray[0];

                    if (cellValue == null
                        || string.IsNullOrEmpty(cellValue.ToString())
                        || cellValue.ToString() != searchParam.Version)
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
            toolStripStatusLabel1.Text = $"Adjusting height for {rowsNumber} rows";

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
                           newHeight <= currentHeight * _appConfig.ConfigStorage.CellSizeAdjust)
                           return;

                       if (newHeight > currentHeight * _appConfig.ConfigStorage.CellSizeAdjust)
                           newHeight = (ushort)(currentHeight * _appConfig.ConfigStorage.CellSizeAdjust);
                       row.Height = newHeight;
                   }

                   for (var columnNumber = 0; columnNumber < dgView.ColumnCount; columnNumber++)
                   {
                       var column = dgView.Columns[columnNumber];
                       var newWidth = column.GetPreferredWidth(DataGridViewAutoSizeColumnMode.AllCells, true);
                       var currentWidth = dgView.Width;
                       if (newWidth == column.Width &&
                           newWidth <= currentWidth * _appConfig.ConfigStorage.CellSizeAdjust)
                           return;

                       if (newWidth > currentWidth * _appConfig.ConfigStorage.CellSizeAdjust)
                           newWidth = (ushort)(currentWidth * _appConfig.ConfigStorage.CellSizeAdjust);
                       column.Width = newWidth;
                   }
               });
            }).ContinueWith(t => { toolStripStatusLabel1.Text = ""; });
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
            if (newHeight == row.Height && newHeight <= currentHeight * _appConfig.ConfigStorage.CellSizeAdjust)
                return;

            if (newHeight > currentHeight * _appConfig.ConfigStorage.CellSizeAdjust)
                newHeight = (ushort)(currentHeight * _appConfig.ConfigStorage.CellSizeAdjust);
            row.Height = newHeight;
        }

        private void ActivateUiControls(bool active, bool processTable = true)
        {
            if (!active)
                comboBox_ExVersions.SelectedIndexChanged -= ComboBox_ExVersions_SelectedIndexChanged;
            else
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
                comboBox_ExVersions.SelectedIndexChanged += ComboBox_ExVersions_SelectedIndexChanged;

            Refresh();
        }

        private void OpenFile(bool standAloneEditor = false)
        {
            if (dataGridView_examples?.SelectedCells.Count <= 0 || listBox_fileList?.SelectedItems.Count <= 0)
                return;

            var jsonPaths = dataGridView_examples?.Rows[dataGridView_examples.SelectedCells[0].RowIndex]
                .Cells[3]?
                .Value?
                .ToString()?
                .Split(new[] { _appConfig.ConfigStorage.TableListDelimiter },
                    StringSplitOptions.RemoveEmptyEntries);

            var jsonSample = dataGridView_examples?.Rows[dataGridView_examples.SelectedCells[0].RowIndex]
                .Cells[3]?
                .Value?
                .ToString();

            if (listBox_fileList == null)
                return;

            var fileNumber = listBox_fileList.SelectedIndex;
            var fileName = listBox_fileList.Items[fileNumber].ToString();

            if (jsonPaths != null && jsonPaths.Length >= fileNumber)
            {
                var jsonPath = jsonPaths[fileNumber];
                ShowPreviewEditor(fileName, jsonPath, jsonSample, standAloneEditor);
            }
        }

        private void ShowPreviewEditor(string longFileName,
            string jsonPath,
            string sampleText,
            bool standAloneEditor = false)
        {
            if (_appConfig.ConfigStorage.UseVsCode)
            {
                var lineNumber = GetLineNumberForPath(longFileName, jsonPath) + 1;
                var execParams = "-r -g " + longFileName + ":" + lineNumber;
                VsCodeOpenFile(execParams);

                return;
            }

            var textEditor = _sideViewer;
            if (standAloneEditor) textEditor = null;

            var fileLoaded = false;
            var newWindow = false;
            if (textEditor != null && !textEditor.IsDisposed)
            {
                if (textEditor.SingleLineBrackets != _appConfig.ConfigStorage.ReformatJson ||
                    textEditor.Text != _appConfig.ConfigStorage.PreViewCaption + longFileName)
                {
                    textEditor.SingleLineBrackets = _appConfig.ConfigStorage.ReformatJson;
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
                    SingleLineBrackets = _appConfig.ConfigStorage.ReformatJson
                };

                newWindow = true;
                fileLoaded = textEditor.LoadJsonFromFile(longFileName);
            }

            if (!standAloneEditor)
                _sideViewer = textEditor;

            textEditor.AlwaysOnTop = _appConfig.ConfigStorage.AlwaysOnTop;
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
                textEditor.ResizeEnd += OnResizeEditor;
            }

            if (!fileLoaded)
            {
                textEditor.Text = "Failed to load " + longFileName;
                return;
            }

            if (!standAloneEditor)
                textEditor.Text = _appConfig.ConfigStorage.PreViewCaption + longFileName;
            else
                textEditor.Text = longFileName;

            if (!textEditor.HighlightPathJson(jsonPath))
                textEditor.HighlightText(sampleText);
        }

        #endregion

        #region Utilities

        private static string GetFileTypeFromFileName(string longFileName, IEnumerable<ContentTypeItem> fileTypes, string defaultContentType)
        {
            if (string.IsNullOrEmpty(longFileName) || fileTypes == null)
                return defaultContentType;

            if (fileTypes.Any(n => n.FileTypeSign.EndsWith('\\') || n.FileTypeSign.EndsWith('/')))
            {
                var dirName = GetDirectoryName(longFileName);

                if (!string.IsNullOrEmpty(dirName))
                {
                    var dirFileType = fileTypes?
                            .FirstOrDefault(n => dirName.EndsWith(n.FileTypeSign.TrimEnd('\\', '/')))?
                            .ContentType;

                    if (!string.IsNullOrEmpty(dirFileType))
                        return dirFileType;
                }
            }

            var shortFileName = GetShortFileName(longFileName);

            return fileTypes
                .Where(n => shortFileName.EndsWith(n.FileTypeSign))
                .Select(n => n.ContentType)
                .FirstOrDefault() ?? defaultContentType;
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
            return file?.Directory?.Name;
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
                return 0;

            var parser = new JsonPathParser
            {
                TrimComplexValues = false,
                SaveComplexValues = false,
                RootName = "",
                JsonPathDivider = _appConfig.ConfigStorage.JsonPathDiv,
                SearchStartOnly = true
            };

            var startLine = 0;
            var property = parser.SearchJsonPath(jsonStr, _appConfig.ConfigStorage.JsonPathDiv + jsonPath);

            if (property != null)
            {
                JsonPathParser.GetLinesNumber(jsonStr, property.StartPosition, property.EndPosition, out startLine,
                    out var _);
            }

            return startLine;
        }

        private static TreeNode FindTreeNodeByPath(List<string> pathItems, TreeNode rootNode)
        {
            if (pathItems == null || pathItems.Count <= 0) return null;

            TreeNode result = null;

            if (rootNode.Text.TrimEnd(']', '}').TrimEnd('[', '{') == pathItems.First())
            {
                if (pathItems.Count == 1)
                    return rootNode;

                pathItems.RemoveAt(0);
                foreach (TreeNode node in rootNode.Nodes)
                {
                    result = FindTreeNodeByPath(pathItems, node);

                    if (result != null)
                        return result;
                }
            }

            return result;
        }

        private static void UnfoldNode(TreeView tree)
        {
            if (tree.SelectedNode == null)
                return;

            tree.SelectedNode.ExpandAll();
        }

        private static void FoldNode(TreeView tree)
        {
            if (tree.SelectedNode == null)
                return;

            tree.SelectedNode.Collapse(false);
        }

        private static void CopyNodeText(TreeView tree)
        {
            if (tree.SelectedNode == null)
                return;

            Clipboard.SetText(tree.SelectedNode.Text);
        }

        #endregion

        #region Schema generation

        private static ISchemaBase JsonPropertyListToSchemaObject(
            ISchemaBase parent,
            IEnumerable<ParsedProperty> rootCollection,
            string startPath,
            string propertyName, char jsonPathDiv)
        {
            if (rootCollection == null)
                return null;

            // select properties describing current node from complete collection
            var properties = rootCollection.Where(n => n.ParentPath == startPath);

            if (!properties.Any())
                return null;

            var propertyNames = new SchemaPropertyNames();

            // get "schema"
            var nodeSchemaVersion = properties.FirstOrDefault(n => n.Name == propertyNames.Schema)?.Value;

            if (!string.IsNullOrEmpty(nodeSchemaVersion))
            {
                if (nodeSchemaVersion.Equals("http://json-schema.org/draft-04/schema#",
                    StringComparison.OrdinalIgnoreCase))
                    propertyNames.Id = "id";
            }

            //get "type" of the object
            var nodeTypes = new List<string>();
            var typePropertyPathSample = startPath + jsonPathDiv + propertyNames.Type;
            var currentNodeType = properties
                                      .FirstOrDefault(n => n.Path == typePropertyPathSample) ?? new ParsedProperty();

            if (currentNodeType.JsonPropertyType == JsonPropertyType.Array)
            {
                var childNodesTypes = rootCollection
                    .Where(n => n.ParentPath == typePropertyPathSample)
                    .Select(n => n.Value);

                nodeTypes.AddRange(childNodesTypes);
            }
            else
            {
                nodeTypes.Add(currentNodeType.Value);
            }

            nodeTypes.Sort();

            // get "id"
            var nodeId = properties.FirstOrDefault(n => n.Name == propertyNames.Id)?.Value;

            //get "title"
            var nodeDescription = properties.FirstOrDefault(n => n.Name == propertyNames.Title)?.Value;
            var reference = properties.FirstOrDefault(n => n.Name == propertyNames.Ref)?.Value;

            var nodeExamples = rootCollection
                .Where(n => n.ParentPath == startPath + jsonPathDiv + propertyNames.Examples && !string.IsNullOrEmpty(n.Value))
                .Select(n => n.Value)
                .OrderBy(n => n)
                .ToList();

            // to do - get all available properties even if there is a mix of object/array/property
            if (nodeTypes.Any(n => n == ISchemaBase.JsonSchemaTypes.Array))
            {
                var arrayNode = new SchemaArray(propertyName)
                {
                    Parent = parent,
                    Type = nodeTypes,
                    Id = nodeId,
                    Description = nodeDescription,
                    Reference = reference,
                    Examples = nodeExamples
                };

                if (bool.TryParse(properties
                        .FirstOrDefault(n => n.Name == propertyNames.UniqueItems)?
                        .Value,
                    out var ap))
                    arrayNode.UniqueItemsOnly = ap;
                else
                    arrayNode.UniqueItemsOnly = null;

                var newItem = JsonPropertyListToSchemaObject(
                    arrayNode,
                    rootCollection,
                    startPath + jsonPathDiv + propertyNames.Items,
                    propertyNames.Items,
                    jsonPathDiv);
                arrayNode.Items = newItem;

                return arrayNode;
            }

            if (nodeTypes.Any(n => n == ISchemaBase.JsonSchemaTypes.Object))
            {
                var objectNode = new SchemaObject(propertyName)
                {
                    Parent = parent,
                    Name = propertyName,
                    Type = nodeTypes,
                    Id = nodeId,
                    Description = nodeDescription,
                    Reference = reference,
                    Examples = nodeExamples,
                    Required = rootCollection
                        .Where(n => n.ParentPath == startPath + jsonPathDiv + propertyNames.Required)
                        .Select(n => n.Value)
                        .OrderBy(n => n)
                        .ToList()
                };

                if (bool.TryParse(properties
                        .FirstOrDefault(n => n.Name == propertyNames.AdditionalProperties)?
                        .Value,
                    out var ap))
                    objectNode.AdditionalProperties = ap;
                else
                    objectNode.AdditionalProperties = null;

                var childProperties = rootCollection
                    .Where(n => n.ParentPath == startPath + jsonPathDiv + propertyNames.Properties)
                    .OrderBy(n => n.Name);

                foreach (var item in childProperties)
                {
                    var newProperty =
                        JsonPropertyListToSchemaObject(objectNode, rootCollection, item.Path, item.Name, jsonPathDiv);
                    objectNode.Properties.Add(newProperty);
                }

                if (startPath == "#")
                {
                    objectNode.SchemaName = properties.FirstOrDefault(n => n.Name == propertyNames.Schema)?.Value;

                    var childDefs = rootCollection
                        .Where(n => n.ParentPath == startPath + jsonPathDiv + propertyNames.Definitions)
                        .OrderBy(n => n.Name);

                    foreach (var item in childDefs)
                    {
                        var newProperty =
                            JsonPropertyListToSchemaObject(objectNode, rootCollection, item.Path, item.Name, jsonPathDiv);
                        objectNode.Definitions.Add(newProperty);
                    }
                }

                return objectNode;
            }

            var propertyNode = new SchemaProperty(propertyName)
            {
                Parent = parent,
                Type = nodeTypes,
                Id = nodeId,
                Description = nodeDescription,
                Reference = reference,
                Examples = nodeExamples,
                Default = properties.FirstOrDefault(n => n.Name == propertyNames.Default)?.Value,
                Pattern = properties.FirstOrDefault(n => n.Name == propertyNames.Pattern)?.Value,
                Enum = rootCollection
                    .Where(n => n.ParentPath == startPath + jsonPathDiv + propertyNames.Enum && !string.IsNullOrEmpty(n.Value))
                    .Select(n => n.Value)
                    .OrderBy(n => n)
                    .ToList()
            };

            return propertyNode;
        }

        private ISchemaBase GenerateSchemaFromSamplesTree(TreeNode treeRoot,
            Dictionary<string, List<JsonProperty>> examples, string nodeName, ISchemaBase parent)
        {
            if (treeRoot == null)
                return null;

            var descText = "";
            _nodeDescriptions?.TryGetValue(treeRoot.Name, out descText);

            var newSchemaRoot = new SchemaObject(nodeName)
            {
                Parent = parent,
                Description = descText,
                Id = nodeName,
                Type = new List<string> { ISchemaBase.JsonSchemaTypes.Object },
                Name = nodeName,
                SchemaName = "http://json-schema.org/draft-07/schema#"
            };

            foreach (TreeNode node in treeRoot.Nodes)
            {
                var result = ConvertSamplesTreeNodeToSchemaObjectRecursive(node, newSchemaRoot, examples);

                if (result != null)
                    newSchemaRoot.Properties.Add(result);
            }

            return newSchemaRoot;
        }

        private ISchemaBase ConvertSamplesTreeNodeToSchemaObjectRecursive(TreeNode node,
            ISchemaBase parent,
            Dictionary<string, List<JsonProperty>> examples)
        {
            if (node == null)
                return null;

            var propertyNames = new SchemaPropertyNames();

            var propertyName = node.Text.TrimEnd(']', '}').TrimEnd('[', '{');

            var nodeDescription = "";

            var nodeType = JsonPropertyType.Unknown;
            if (node.Tag is JsonProperty p)
            {
                nodeType = p.ObjectType;
                _nodeDescriptions?.TryGetValue(node.Name, out nodeDescription);
            }

            nodeDescription = ConvertStringForJson(nodeDescription);

            List<JsonProperty> nodeExamples = null;
            examples?.TryGetValue(node.Name, out nodeExamples);

            if (nodeType == JsonPropertyType.Array)
            {
                var arrayNode = new SchemaArray(propertyName)
                {
                    Parent = parent,
                    Type = new List<string> { nodeType.ToString().ToLower() },
                    Description = nodeDescription,
                    Examples = nodeExamples?
                        .Where(n => (n.ObjectType == JsonPropertyType.Property
                                    || n.ObjectType == JsonPropertyType.ArrayValue
                                    || n.VariableType == JsonValueType.String)
                                    && !string.IsNullOrEmpty(n.Value))
                        .Select(n => n.Value)
                        .Distinct()
                        .Select(n => ConvertStringForJson(n))
                        .OrderBy(n => n)
                        .ToList(),
                    UniqueItemsOnly = true
                };
                arrayNode.Id = arrayNode.GetReferencePath();

                // check all items in array and create some average object
                var objList = new List<ISchemaBase>();
                foreach (TreeNode item in node.Nodes)
                {
                    var newItemsNode = ConvertSamplesTreeNodeToSchemaObjectRecursive(
                        item,
                        arrayNode,
                        examples);

                    if (newItemsNode != null)
                        objList.Add(newItemsNode);
                }

                if (objList.Any())
                {
                    arrayNode.Items = new SchemaObject
                    {
                        //Id = nodePath + propertyNames.Divider + propertyNames.Items,
                        Parent = arrayNode,
                        Name = propertyNames.Items,
                        Type = new List<string> { ISchemaBase.JsonSchemaTypes.Object },
                        Properties = objList
                    };
                    arrayNode.Items.Id = arrayNode.Items.GetReferencePath();

                    return arrayNode;
                }

                var arrayExamples = examples?
                        .Where(n => n.Key.StartsWith(node.Name))
                        .ToArray();

                var enumList = arrayExamples?
                    .FirstOrDefault()
                    .Value?
                    .Where(n => !string.IsNullOrEmpty(n.Value))
                    .Select(n => n.Value)
                    .Distinct()
                    .Select(n => ConvertStringForJson(n))
                    .OrderBy(n => n)
                    .ToList();

                var enumListTypes = arrayExamples?
                    .FirstOrDefault()
                    .Value?
                    .Select(n => n.VariableType)
                    .Distinct();

                var typesList = enumListTypes?
                    .Select(n => n.ToString().ToLower())
                    .OrderBy(n => n)
                    .ToList();

                arrayNode.Items = new SchemaProperty(propertyNames.Items)
                {
                    //Id = nodePath + propertyNames.Divider + propertyNames.Items,
                    Parent = arrayNode,
                    Type = typesList,
                    Enum = enumList,
                    Description = nodeDescription,
                    Examples = nodeExamples?
                        .Where(n => !string.IsNullOrEmpty(n.Value))
                        .Select(n => n.Value)
                        .Distinct()
                        .Select(n => ConvertStringForJson(n))
                        .OrderBy(n => n)
                        .ToList()
                };
                arrayNode.Items.Id = arrayNode.Items.GetReferencePath();

                return arrayNode;
            }

            var nodeVariableTypes = nodeExamples?
                .Select(n => n.VariableType)
                .Distinct()
                .Where(n => n == JsonValueType.String
                            || n == JsonValueType.Number
                            || n == JsonValueType.Null
                            || n == JsonValueType.Boolean)
                .Select(n => n.ToString().ToLower())
                .ToList() ?? new List<string>();

            var nodeObjectTypes = nodeExamples?
                .Select(n => n.ObjectType)
                .Distinct()
                .Where(n => n == JsonPropertyType.Object
                            || n == JsonPropertyType.Array
                            || n == JsonPropertyType.ArrayValue)
                .Select(n => n.ToString()
                    .ToLower()
                    .Replace("arrayvalue", ISchemaBase.JsonSchemaTypes.Array)) ?? new List<string>();

            nodeVariableTypes.AddRange(nodeObjectTypes);
            nodeVariableTypes = nodeVariableTypes.Distinct().OrderBy(n => n).ToList();

            if (nodeType == JsonPropertyType.Object)
            {
                var objectNode = new SchemaObject
                {
                    Parent = parent,
                    Name = propertyName,
                    Type = nodeVariableTypes,
                    //Id = nodePath,
                    Description = nodeDescription,
                    Examples = nodeExamples?
                        .Where(n => (n.ObjectType == JsonPropertyType.Property
                                    || n.ObjectType == JsonPropertyType.ArrayValue
                                    || n.VariableType == JsonValueType.String)
                                    && !string.IsNullOrEmpty(n.Value))
                        .Select(n => n.Value)
                        .Distinct()
                        .Select(n => ConvertStringForJson(n))
                        .OrderBy(n => n)
                        .ToList(),
                    AdditionalProperties = false
                };
                objectNode.Id = objectNode.GetReferencePath();

                foreach (TreeNode item in node.Nodes)
                {
                    var newProperty = ConvertSamplesTreeNodeToSchemaObjectRecursive(item, objectNode, examples);
                    if (newProperty != null) objectNode.Properties.Add(newProperty);
                }

                return objectNode;
            }

            var propertyNode = new SchemaProperty(propertyName)
            {
                Parent = parent,
                Type = nodeVariableTypes,
                //Id = nodePath,
                Description = nodeDescription,
                Examples = nodeExamples?
                    .Where(n => (n.ObjectType == JsonPropertyType.Property
                                || n.ObjectType == JsonPropertyType.ArrayValue
                                || n.VariableType == JsonValueType.String)
                                && !string.IsNullOrEmpty(n.Value))
                    .Select(n => n.Value)
                    .Distinct()
                    .Select(n => ConvertStringForJson(n))
                    .OrderBy(n => n)
                    .ToList()
            };
            propertyNode.Id = propertyNode.GetReferencePath();

            // if all property types are "string" create Enum
            if (nodeVariableTypes.Any(n => n == ISchemaBase.JsonSchemaTypes.String))
            {
                propertyNode.Enum = nodeExamples?
                    .Where(n => n.VariableType == JsonValueType.String && !string.IsNullOrEmpty(n.Value))
                    .Select(n => n.Value)
                    .Distinct()
                    .Select(n => ConvertStringForJson(n))
                    .OrderBy(n => n)
                    .ToList() ?? new List<string>();

                if (propertyNode.Type.Contains(ISchemaBase.JsonSchemaTypes.Boolean))
                {
                    propertyNode.Enum.Add("true");
                    propertyNode.Enum.Add("false");
                }

                if (propertyNode.Type.Contains(ISchemaBase.JsonSchemaTypes.Null))
                    propertyNode.Enum.Add(ISchemaBase.JsonSchemaTypes.Null);
            }

            return propertyNode;
        }

        private static TreeNode ConvertSchemaObjectToTreeNodeRecursive(ISchemaBase schema, string parentPath = "")
        {
            if (schema == null)
                return null;

            if (parentPath == null)
                parentPath = "";

            var propertyNames = new SchemaPropertyNames();
            if (!string.IsNullOrEmpty(parentPath) && parentPath.EndsWith(propertyNames.Divider))
                parentPath = parentPath.TrimEnd(propertyNames.Divider);

            var currentNodePath = string.IsNullOrEmpty(parentPath) ? schema.Name : parentPath + propertyNames.Divider + schema.Name;
            var node = new TreeNode(schema.Name)
            {
                Text = schema.Name,
                Name = currentNodePath,
                Tag = schema
            };

            if (schema is SchemaObject schemaObject)
            {
                node.Text += "{}";

                var newNodeProperties = new TreeNode(propertyNames.Properties + "{}")
                {
                    Text = propertyNames.Properties + "{}",
                    Name = node.Name + propertyNames.Divider + propertyNames.Properties
                };

                if (schemaObject.Properties != null)
                {
                    foreach (var property in schemaObject.Properties)
                    {
                        var newNodes = ConvertSchemaObjectToTreeNodeRecursive(property, newNodeProperties.Name);

                        if (newNodes != null)
                            newNodeProperties.Nodes.Add(newNodes);
                    }
                }
                node.Nodes.Add(newNodeProperties);

                if (schemaObject.Definitions != null && schemaObject.Definitions.Count > 0)
                {
                    var newNode = new TreeNode(propertyNames.Definitions + "{}")
                    {
                        Text = propertyNames.Definitions + "{}",
                        Name = node.Name + propertyNames.Divider + propertyNames.Definitions
                    };

                    foreach (var definition in schemaObject.Definitions)
                    {
                        var newNodes = ConvertSchemaObjectToTreeNodeRecursive(definition, newNode.Name);
                        if (newNodes != null)
                            newNode.Nodes.Add(newNodes);
                    }

                    node.Nodes.Add(newNode);
                }
            }
            else if (schema is SchemaArray schemaArray)
            {
                node.Text += "[]";
                var newNodes = ConvertSchemaObjectToTreeNodeRecursive(schemaArray.Items, node.Name);

                if (newNodes != null)
                    node.Nodes.Add(newNodes);
            }

            return node;
        }

        #endregion
    }
}