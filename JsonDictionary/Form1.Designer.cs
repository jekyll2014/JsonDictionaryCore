namespace JsonDictionaryCore
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            tabControl1 = new System.Windows.Forms.TabControl();
            tabPage_DataCollection = new System.Windows.Forms.TabPage();
            splitContainer_buttons = new System.Windows.Forms.SplitContainer();
            checkBox_vsCode = new System.Windows.Forms.CheckBox();
            checkBox_loadDbOnStart = new System.Windows.Forms.CheckBox();
            checkBox_alwaysOnTop = new System.Windows.Forms.CheckBox();
            checkBox_showPreview = new System.Windows.Forms.CheckBox();
            checkBox_beautifyJson = new System.Windows.Forms.CheckBox();
            checkBox_reformatJsonBrackets = new System.Windows.Forms.CheckBox();
            button_saveDb = new System.Windows.Forms.Button();
            button_loadDb = new System.Windows.Forms.Button();
            button_collectDatabase = new System.Windows.Forms.Button();
            textBox_logText = new System.Windows.Forms.TextBox();
            tabPage_SamplesTree = new System.Windows.Forms.TabPage();
            splitContainer_tree = new System.Windows.Forms.SplitContainer();
            treeView_examples = new System.Windows.Forms.TreeView();
            contextMenuStrip_tree = new System.Windows.Forms.ContextMenuStrip(components);
            toolStripMenuItem_treeShow = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItem_unfoldAll = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItem_foldAll = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItem_treeCopy = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItem_treeDelete = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItem_generateSchema = new System.Windows.Forms.ToolStripMenuItem();
            splitContainer_description = new System.Windows.Forms.SplitContainer();
            splitContainer_fileList = new System.Windows.Forms.SplitContainer();
            dataGridView_examples = new System.Windows.Forms.DataGridView();
            contextMenuStrip_samples = new System.Windows.Forms.ContextMenuStrip(components);
            toolStripMenuItem_SampleOpen = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItem_SampleDelete = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItem_SampleCopy = new System.Windows.Forms.ToolStripMenuItem();
            listBox_fileList = new System.Windows.Forms.ListBox();
            contextMenuStrip_fileList = new System.Windows.Forms.ContextMenuStrip(components);
            toolStripMenuItem_openFile = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItem_openFolder = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItem_FileDelete = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItem_copy = new System.Windows.Forms.ToolStripMenuItem();
            textBox_ExSearchHistory = new System.Windows.Forms.TextBox();
            comboBox_ExVersions = new System.Windows.Forms.ComboBox();
            button_ExAdjustRows = new System.Windows.Forms.Button();
            comboBox_ExCondition = new System.Windows.Forms.ComboBox();
            textBox_ExSearchString = new System.Windows.Forms.TextBox();
            checkBox_ExCaseSensitive = new System.Windows.Forms.CheckBox();
            button_ExClearSearch = new System.Windows.Forms.Button();
            label_edit = new System.Windows.Forms.Label();
            label_descSave = new System.Windows.Forms.Label();
            textBox_description = new System.Windows.Forms.TextBox();
            tabPage_Schema = new System.Windows.Forms.TabPage();
            button_compareNode = new System.Windows.Forms.Button();
            button_clearCompare = new System.Windows.Forms.Button();
            checkBox_selectedSchema = new System.Windows.Forms.CheckBox();
            checkBox_deepCompare = new System.Windows.Forms.CheckBox();
            button_compare = new System.Windows.Forms.Button();
            textBox_find = new System.Windows.Forms.TextBox();
            button_findPrev = new System.Windows.Forms.Button();
            button_findNext = new System.Windows.Forms.Button();
            checkBox_schemaSelectionSync = new System.Windows.Forms.CheckBox();
            splitContainer_schemaMain = new System.Windows.Forms.SplitContainer();
            splitContainer_schemaLeft = new System.Windows.Forms.SplitContainer();
            button_saveLeftSchema = new System.Windows.Forms.Button();
            button_generateSchema = new System.Windows.Forms.Button();
            treeView_leftSchema = new System.Windows.Forms.TreeView();
            contextMenuStrip_leftSchema = new System.Windows.Forms.ContextMenuStrip(components);
            toolStripMenuItem_unfoldLS = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItem_foldLS = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItem_copyLS = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItem_addPropertyLS = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItem_addObjectLS = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItem_addArrayLS = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItem_renameLS = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItem_deleteLS = new System.Windows.Forms.ToolStripMenuItem();
            splitContainer_schemaRight = new System.Windows.Forms.SplitContainer();
            button_saveRightSchema = new System.Windows.Forms.Button();
            button_loadSchema = new System.Windows.Forms.Button();
            treeView_rightSchema = new System.Windows.Forms.TreeView();
            contextMenuStrip_rightSchema = new System.Windows.Forms.ContextMenuStrip(components);
            toolStripMenuItem_unfoldRS = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItem_foldRS = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItem_copyRS = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItem_addPropertyRS = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItem_addObjectRS = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItem_addArrayRS = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItem_renameRS = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItem_deleteRS = new System.Windows.Forms.ToolStripMenuItem();
            openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            statusStrip1 = new System.Windows.Forms.StatusStrip();
            toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            tabControl1.SuspendLayout();
            tabPage_DataCollection.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer_buttons).BeginInit();
            splitContainer_buttons.Panel1.SuspendLayout();
            splitContainer_buttons.Panel2.SuspendLayout();
            splitContainer_buttons.SuspendLayout();
            tabPage_SamplesTree.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer_tree).BeginInit();
            splitContainer_tree.Panel1.SuspendLayout();
            splitContainer_tree.Panel2.SuspendLayout();
            splitContainer_tree.SuspendLayout();
            contextMenuStrip_tree.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer_description).BeginInit();
            splitContainer_description.Panel1.SuspendLayout();
            splitContainer_description.Panel2.SuspendLayout();
            splitContainer_description.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer_fileList).BeginInit();
            splitContainer_fileList.Panel1.SuspendLayout();
            splitContainer_fileList.Panel2.SuspendLayout();
            splitContainer_fileList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView_examples).BeginInit();
            contextMenuStrip_samples.SuspendLayout();
            contextMenuStrip_fileList.SuspendLayout();
            tabPage_Schema.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer_schemaMain).BeginInit();
            splitContainer_schemaMain.Panel1.SuspendLayout();
            splitContainer_schemaMain.Panel2.SuspendLayout();
            splitContainer_schemaMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer_schemaLeft).BeginInit();
            splitContainer_schemaLeft.Panel1.SuspendLayout();
            splitContainer_schemaLeft.SuspendLayout();
            contextMenuStrip_leftSchema.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer_schemaRight).BeginInit();
            splitContainer_schemaRight.Panel1.SuspendLayout();
            splitContainer_schemaRight.SuspendLayout();
            contextMenuStrip_rightSchema.SuspendLayout();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            tabControl1.Controls.Add(tabPage_DataCollection);
            tabControl1.Controls.Add(tabPage_SamplesTree);
            tabControl1.Controls.Add(tabPage_Schema);
            tabControl1.Location = new System.Drawing.Point(0, 0);
            tabControl1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new System.Drawing.Size(915, 621);
            tabControl1.TabIndex = 0;
            // 
            // tabPage_DataCollection
            // 
            tabPage_DataCollection.Controls.Add(splitContainer_buttons);
            tabPage_DataCollection.Location = new System.Drawing.Point(4, 24);
            tabPage_DataCollection.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            tabPage_DataCollection.Name = "tabPage_DataCollection";
            tabPage_DataCollection.Size = new System.Drawing.Size(907, 593);
            tabPage_DataCollection.TabIndex = 0;
            tabPage_DataCollection.Text = "Data collection";
            tabPage_DataCollection.UseVisualStyleBackColor = true;
            // 
            // splitContainer_buttons
            // 
            splitContainer_buttons.Dock = System.Windows.Forms.DockStyle.Fill;
            splitContainer_buttons.Location = new System.Drawing.Point(0, 0);
            splitContainer_buttons.Margin = new System.Windows.Forms.Padding(0);
            splitContainer_buttons.Name = "splitContainer_buttons";
            // 
            // splitContainer_buttons.Panel1
            // 
            splitContainer_buttons.Panel1.Controls.Add(checkBox_vsCode);
            splitContainer_buttons.Panel1.Controls.Add(checkBox_loadDbOnStart);
            splitContainer_buttons.Panel1.Controls.Add(checkBox_alwaysOnTop);
            splitContainer_buttons.Panel1.Controls.Add(checkBox_showPreview);
            splitContainer_buttons.Panel1.Controls.Add(checkBox_beautifyJson);
            splitContainer_buttons.Panel1.Controls.Add(checkBox_reformatJsonBrackets);
            splitContainer_buttons.Panel1.Controls.Add(button_saveDb);
            splitContainer_buttons.Panel1.Controls.Add(button_loadDb);
            splitContainer_buttons.Panel1.Controls.Add(button_collectDatabase);
            splitContainer_buttons.Panel1MinSize = 120;
            // 
            // splitContainer_buttons.Panel2
            // 
            splitContainer_buttons.Panel2.Controls.Add(textBox_logText);
            splitContainer_buttons.Size = new System.Drawing.Size(907, 593);
            splitContainer_buttons.SplitterDistance = 155;
            splitContainer_buttons.SplitterWidth = 5;
            splitContainer_buttons.TabIndex = 18;
            // 
            // checkBox_vsCode
            // 
            checkBox_vsCode.AutoSize = true;
            checkBox_vsCode.Location = new System.Drawing.Point(6, 204);
            checkBox_vsCode.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            checkBox_vsCode.Name = "checkBox_vsCode";
            checkBox_vsCode.Size = new System.Drawing.Size(89, 19);
            checkBox_vsCode.TabIndex = 11;
            checkBox_vsCode.Text = "Use VSCode";
            checkBox_vsCode.UseVisualStyleBackColor = true;
            checkBox_vsCode.CheckedChanged += CheckBox_vsCode_CheckedChanged;
            // 
            // checkBox_loadDbOnStart
            // 
            checkBox_loadDbOnStart.AutoSize = true;
            checkBox_loadDbOnStart.Location = new System.Drawing.Point(6, 104);
            checkBox_loadDbOnStart.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            checkBox_loadDbOnStart.Name = "checkBox_loadDbOnStart";
            checkBox_loadDbOnStart.Size = new System.Drawing.Size(132, 19);
            checkBox_loadDbOnStart.TabIndex = 5;
            checkBox_loadDbOnStart.Text = "Load DB on start-up";
            checkBox_loadDbOnStart.UseVisualStyleBackColor = true;
            checkBox_loadDbOnStart.CheckedChanged += CheckBox_loadDbOnStart_CheckedChanged;
            // 
            // checkBox_alwaysOnTop
            // 
            checkBox_alwaysOnTop.AutoSize = true;
            checkBox_alwaysOnTop.Location = new System.Drawing.Point(6, 229);
            checkBox_alwaysOnTop.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            checkBox_alwaysOnTop.Name = "checkBox_alwaysOnTop";
            checkBox_alwaysOnTop.Size = new System.Drawing.Size(101, 19);
            checkBox_alwaysOnTop.TabIndex = 10;
            checkBox_alwaysOnTop.Text = "Always on top";
            checkBox_alwaysOnTop.UseVisualStyleBackColor = true;
            checkBox_alwaysOnTop.CheckedChanged += CheckBox_alwaysOnTop_CheckedChanged;
            // 
            // checkBox_showPreview
            // 
            checkBox_showPreview.AutoSize = true;
            checkBox_showPreview.Location = new System.Drawing.Point(6, 179);
            checkBox_showPreview.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            checkBox_showPreview.Name = "checkBox_showPreview";
            checkBox_showPreview.Size = new System.Drawing.Size(99, 19);
            checkBox_showPreview.TabIndex = 9;
            checkBox_showPreview.Text = "Show preview";
            checkBox_showPreview.UseVisualStyleBackColor = true;
            checkBox_showPreview.CheckedChanged += CheckBox_showPreview_CheckedChanged;
            // 
            // checkBox_beautifyJson
            // 
            checkBox_beautifyJson.AutoSize = true;
            checkBox_beautifyJson.Location = new System.Drawing.Point(6, 129);
            checkBox_beautifyJson.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            checkBox_beautifyJson.Name = "checkBox_beautifyJson";
            checkBox_beautifyJson.Size = new System.Drawing.Size(100, 19);
            checkBox_beautifyJson.TabIndex = 7;
            checkBox_beautifyJson.Text = "Beautify JSON";
            checkBox_beautifyJson.UseVisualStyleBackColor = true;
            checkBox_beautifyJson.CheckedChanged += CheckBox_beautifyJson_CheckedChanged;
            // 
            // checkBox_reformatJsonBrackets
            // 
            checkBox_reformatJsonBrackets.AutoSize = true;
            checkBox_reformatJsonBrackets.Enabled = false;
            checkBox_reformatJsonBrackets.Location = new System.Drawing.Point(6, 154);
            checkBox_reformatJsonBrackets.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            checkBox_reformatJsonBrackets.Name = "checkBox_reformatJsonBrackets";
            checkBox_reformatJsonBrackets.Size = new System.Drawing.Size(153, 19);
            checkBox_reformatJsonBrackets.TabIndex = 7;
            checkBox_reformatJsonBrackets.Text = "Reformat JSON brackets";
            checkBox_reformatJsonBrackets.UseVisualStyleBackColor = true;
            checkBox_reformatJsonBrackets.CheckedChanged += CheckBox_reformatJsonBrackets_CheckedChanged;
            checkBox_reformatJsonBrackets.EnabledChanged += CheckBox_reformatJsonBrackets_EnabledChanged;
            // 
            // button_saveDb
            // 
            button_saveDb.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            button_saveDb.Location = new System.Drawing.Point(0, 70);
            button_saveDb.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            button_saveDb.Name = "button_saveDb";
            button_saveDb.Size = new System.Drawing.Size(153, 27);
            button_saveDb.TabIndex = 4;
            button_saveDb.Text = "Save database";
            button_saveDb.UseVisualStyleBackColor = true;
            button_saveDb.Click += Button_saveDb_Click;
            // 
            // button_loadDb
            // 
            button_loadDb.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            button_loadDb.Location = new System.Drawing.Point(0, 37);
            button_loadDb.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            button_loadDb.Name = "button_loadDb";
            button_loadDb.Size = new System.Drawing.Size(153, 27);
            button_loadDb.TabIndex = 3;
            button_loadDb.Text = "Load database";
            button_loadDb.UseVisualStyleBackColor = true;
            button_loadDb.Click += Button_loadDb_Click;
            // 
            // button_collectDatabase
            // 
            button_collectDatabase.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            button_collectDatabase.Location = new System.Drawing.Point(0, 3);
            button_collectDatabase.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            button_collectDatabase.Name = "button_collectDatabase";
            button_collectDatabase.Size = new System.Drawing.Size(153, 27);
            button_collectDatabase.TabIndex = 2;
            button_collectDatabase.Text = "Collect database";
            button_collectDatabase.UseVisualStyleBackColor = true;
            button_collectDatabase.Click += Button_collectDatabase_Click;
            // 
            // textBox_logText
            // 
            textBox_logText.Dock = System.Windows.Forms.DockStyle.Fill;
            textBox_logText.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            textBox_logText.Location = new System.Drawing.Point(0, 0);
            textBox_logText.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            textBox_logText.Multiline = true;
            textBox_logText.Name = "textBox_logText";
            textBox_logText.ReadOnly = true;
            textBox_logText.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            textBox_logText.Size = new System.Drawing.Size(747, 593);
            textBox_logText.TabIndex = 0;
            // 
            // tabPage_SamplesTree
            // 
            tabPage_SamplesTree.Controls.Add(splitContainer_tree);
            tabPage_SamplesTree.Location = new System.Drawing.Point(4, 24);
            tabPage_SamplesTree.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            tabPage_SamplesTree.Name = "tabPage_SamplesTree";
            tabPage_SamplesTree.Size = new System.Drawing.Size(907, 593);
            tabPage_SamplesTree.TabIndex = 1;
            tabPage_SamplesTree.Text = "Samples tree";
            tabPage_SamplesTree.UseVisualStyleBackColor = true;
            // 
            // splitContainer_tree
            // 
            splitContainer_tree.Dock = System.Windows.Forms.DockStyle.Fill;
            splitContainer_tree.Location = new System.Drawing.Point(0, 0);
            splitContainer_tree.Margin = new System.Windows.Forms.Padding(0);
            splitContainer_tree.Name = "splitContainer_tree";
            // 
            // splitContainer_tree.Panel1
            // 
            splitContainer_tree.Panel1.Controls.Add(treeView_examples);
            splitContainer_tree.Panel1MinSize = 100;
            // 
            // splitContainer_tree.Panel2
            // 
            splitContainer_tree.Panel2.Controls.Add(splitContainer_description);
            splitContainer_tree.Panel2MinSize = 350;
            splitContainer_tree.Size = new System.Drawing.Size(907, 593);
            splitContainer_tree.SplitterDistance = 227;
            splitContainer_tree.SplitterWidth = 5;
            splitContainer_tree.TabIndex = 1;
            // 
            // treeView_examples
            // 
            treeView_examples.ContextMenuStrip = contextMenuStrip_tree;
            treeView_examples.Dock = System.Windows.Forms.DockStyle.Fill;
            treeView_examples.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            treeView_examples.HideSelection = false;
            treeView_examples.Location = new System.Drawing.Point(0, 0);
            treeView_examples.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            treeView_examples.Name = "treeView_examples";
            treeView_examples.Size = new System.Drawing.Size(227, 593);
            treeView_examples.TabIndex = 0;
            treeView_examples.BeforeCollapse += TreeView_No_Expand_Collapse;
            treeView_examples.BeforeExpand += TreeView_No_Expand_Collapse;
            treeView_examples.AfterSelect += TreeView_examples_AfterSelect;
            treeView_examples.NodeMouseClick += TreeView_NodeMouseClick;
            treeView_examples.NodeMouseDoubleClick += TreeView_examples_NodeMouseDoubleClick;
            treeView_examples.KeyDown += TreeView_examples_KeyDown;
            treeView_examples.MouseDown += TreeView_MouseDown;
            // 
            // contextMenuStrip_tree
            // 
            contextMenuStrip_tree.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { toolStripMenuItem_treeShow, toolStripMenuItem_unfoldAll, toolStripMenuItem_foldAll, toolStripMenuItem_treeCopy, toolStripMenuItem_treeDelete, toolStripMenuItem_generateSchema });
            contextMenuStrip_tree.Name = "contextMenuStrip_tree";
            contextMenuStrip_tree.Size = new System.Drawing.Size(190, 136);
            // 
            // toolStripMenuItem_treeShow
            // 
            toolStripMenuItem_treeShow.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            toolStripMenuItem_treeShow.Name = "toolStripMenuItem_treeShow";
            toolStripMenuItem_treeShow.ShortcutKeyDisplayString = "Enter";
            toolStripMenuItem_treeShow.Size = new System.Drawing.Size(189, 22);
            toolStripMenuItem_treeShow.Text = "Show samples";
            toolStripMenuItem_treeShow.Click += ToolStripMenuItem_treeShow_Click;
            // 
            // toolStripMenuItem_unfoldAll
            // 
            toolStripMenuItem_unfoldAll.Name = "toolStripMenuItem_unfoldAll";
            toolStripMenuItem_unfoldAll.ShortcutKeyDisplayString = "+";
            toolStripMenuItem_unfoldAll.Size = new System.Drawing.Size(189, 22);
            toolStripMenuItem_unfoldAll.Text = "Unfold all children";
            toolStripMenuItem_unfoldAll.Click += ToolStripMenuItem_unfoldAll_Click;
            // 
            // toolStripMenuItem_foldAll
            // 
            toolStripMenuItem_foldAll.Name = "toolStripMenuItem_foldAll";
            toolStripMenuItem_foldAll.ShortcutKeyDisplayString = "-";
            toolStripMenuItem_foldAll.Size = new System.Drawing.Size(189, 22);
            toolStripMenuItem_foldAll.Text = "Fold all children";
            toolStripMenuItem_foldAll.Click += ToolStripMenuItem_foldAll_Click;
            // 
            // toolStripMenuItem_treeCopy
            // 
            toolStripMenuItem_treeCopy.Name = "toolStripMenuItem_treeCopy";
            toolStripMenuItem_treeCopy.ShortcutKeyDisplayString = "Ctrl-C";
            toolStripMenuItem_treeCopy.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C;
            toolStripMenuItem_treeCopy.Size = new System.Drawing.Size(189, 22);
            toolStripMenuItem_treeCopy.Text = "Copy name";
            toolStripMenuItem_treeCopy.Click += ToolStripMenuItem_treeCopy_Click;
            // 
            // toolStripMenuItem_treeDelete
            // 
            toolStripMenuItem_treeDelete.Name = "toolStripMenuItem_treeDelete";
            toolStripMenuItem_treeDelete.ShortcutKeyDisplayString = "Del";
            toolStripMenuItem_treeDelete.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            toolStripMenuItem_treeDelete.Size = new System.Drawing.Size(189, 22);
            toolStripMenuItem_treeDelete.Text = "Delete";
            toolStripMenuItem_treeDelete.Click += ToolStripMenuItem_treeDelete_Click;
            // 
            // toolStripMenuItem_generateSchema
            // 
            toolStripMenuItem_generateSchema.Name = "toolStripMenuItem_generateSchema";
            toolStripMenuItem_generateSchema.Size = new System.Drawing.Size(189, 22);
            toolStripMenuItem_generateSchema.Text = "Generate schema";
            toolStripMenuItem_generateSchema.Click += ToolStripMenuItem_generateSchema_Click;
            // 
            // splitContainer_description
            // 
            splitContainer_description.Dock = System.Windows.Forms.DockStyle.Fill;
            splitContainer_description.Location = new System.Drawing.Point(0, 0);
            splitContainer_description.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            splitContainer_description.Name = "splitContainer_description";
            splitContainer_description.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer_description.Panel1
            // 
            splitContainer_description.Panel1.Controls.Add(splitContainer_fileList);
            splitContainer_description.Panel1.Controls.Add(textBox_ExSearchHistory);
            splitContainer_description.Panel1.Controls.Add(comboBox_ExVersions);
            splitContainer_description.Panel1.Controls.Add(button_ExAdjustRows);
            splitContainer_description.Panel1.Controls.Add(comboBox_ExCondition);
            splitContainer_description.Panel1.Controls.Add(textBox_ExSearchString);
            splitContainer_description.Panel1.Controls.Add(checkBox_ExCaseSensitive);
            splitContainer_description.Panel1.Controls.Add(button_ExClearSearch);
            // 
            // splitContainer_description.Panel2
            // 
            splitContainer_description.Panel2.Controls.Add(label_edit);
            splitContainer_description.Panel2.Controls.Add(label_descSave);
            splitContainer_description.Panel2.Controls.Add(textBox_description);
            splitContainer_description.Size = new System.Drawing.Size(675, 593);
            splitContainer_description.SplitterDistance = 498;
            splitContainer_description.SplitterWidth = 5;
            splitContainer_description.TabIndex = 9;
            // 
            // splitContainer_fileList
            // 
            splitContainer_fileList.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            splitContainer_fileList.Location = new System.Drawing.Point(0, 37);
            splitContainer_fileList.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            splitContainer_fileList.Name = "splitContainer_fileList";
            // 
            // splitContainer_fileList.Panel1
            // 
            splitContainer_fileList.Panel1.Controls.Add(dataGridView_examples);
            // 
            // splitContainer_fileList.Panel2
            // 
            splitContainer_fileList.Panel2.Controls.Add(listBox_fileList);
            splitContainer_fileList.Size = new System.Drawing.Size(675, 427);
            splitContainer_fileList.SplitterDistance = 458;
            splitContainer_fileList.SplitterWidth = 5;
            splitContainer_fileList.TabIndex = 8;
            // 
            // dataGridView_examples
            // 
            dataGridView_examples.AllowUserToAddRows = false;
            dataGridView_examples.AllowUserToDeleteRows = false;
            dataGridView_examples.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dataGridView_examples.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            dataGridView_examples.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView_examples.ContextMenuStrip = contextMenuStrip_samples;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            dataGridView_examples.DefaultCellStyle = dataGridViewCellStyle1;
            dataGridView_examples.Dock = System.Windows.Forms.DockStyle.Fill;
            dataGridView_examples.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            dataGridView_examples.Location = new System.Drawing.Point(0, 0);
            dataGridView_examples.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            dataGridView_examples.Name = "dataGridView_examples";
            dataGridView_examples.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders;
            dataGridViewCellStyle2.NullValue = "Adjust";
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            dataGridView_examples.RowsDefaultCellStyle = dataGridViewCellStyle2;
            dataGridView_examples.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            dataGridView_examples.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            dataGridView_examples.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            dataGridView_examples.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            dataGridView_examples.Size = new System.Drawing.Size(458, 427);
            dataGridView_examples.TabIndex = 5;
            dataGridView_examples.CellDoubleClick += DataGridView_examples_CellDoubleClick;
            dataGridView_examples.CellMouseDown += DataGridView_CellMouseDown;
            dataGridView_examples.RowEnter += DataGridView_RowEnter;
            dataGridView_examples.RowHeaderMouseDoubleClick += DataGridView_RowHeaderMouseDoubleClick;
            dataGridView_examples.KeyDown += DataGridView_examples_KeyDown;
            // 
            // contextMenuStrip_samples
            // 
            contextMenuStrip_samples.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { toolStripMenuItem_SampleOpen, toolStripMenuItem_SampleDelete, toolStripMenuItem_SampleCopy });
            contextMenuStrip_samples.Name = "contextMenuStrip_samples";
            contextMenuStrip_samples.Size = new System.Drawing.Size(175, 70);
            // 
            // toolStripMenuItem_SampleOpen
            // 
            toolStripMenuItem_SampleOpen.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            toolStripMenuItem_SampleOpen.Name = "toolStripMenuItem_SampleOpen";
            toolStripMenuItem_SampleOpen.ShortcutKeyDisplayString = "Enter";
            toolStripMenuItem_SampleOpen.Size = new System.Drawing.Size(174, 22);
            toolStripMenuItem_SampleOpen.Text = "Open file";
            toolStripMenuItem_SampleOpen.Click += ToolStripMenuItem_SampleOpen_Click;
            // 
            // toolStripMenuItem_SampleDelete
            // 
            toolStripMenuItem_SampleDelete.Name = "toolStripMenuItem_SampleDelete";
            toolStripMenuItem_SampleDelete.ShortcutKeyDisplayString = "Del";
            toolStripMenuItem_SampleDelete.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            toolStripMenuItem_SampleDelete.Size = new System.Drawing.Size(174, 22);
            toolStripMenuItem_SampleDelete.Text = "Delete";
            toolStripMenuItem_SampleDelete.Visible = false;
            toolStripMenuItem_SampleDelete.Click += ToolStripMenuItem_SampleDelete_Click;
            // 
            // toolStripMenuItem_SampleCopy
            // 
            toolStripMenuItem_SampleCopy.Name = "toolStripMenuItem_SampleCopy";
            toolStripMenuItem_SampleCopy.ShortcutKeyDisplayString = "Ctrl-C";
            toolStripMenuItem_SampleCopy.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C;
            toolStripMenuItem_SampleCopy.Size = new System.Drawing.Size(174, 22);
            toolStripMenuItem_SampleCopy.Text = "Copy name";
            toolStripMenuItem_SampleCopy.Click += ToolStripMenuItem_SampleCopy_Click;
            // 
            // listBox_fileList
            // 
            listBox_fileList.ContextMenuStrip = contextMenuStrip_fileList;
            listBox_fileList.Dock = System.Windows.Forms.DockStyle.Fill;
            listBox_fileList.FormattingEnabled = true;
            listBox_fileList.HorizontalScrollbar = true;
            listBox_fileList.ItemHeight = 15;
            listBox_fileList.Location = new System.Drawing.Point(0, 0);
            listBox_fileList.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            listBox_fileList.Name = "listBox_fileList";
            listBox_fileList.Size = new System.Drawing.Size(212, 427);
            listBox_fileList.TabIndex = 0;
            listBox_fileList.SelectedValueChanged += ListBox_fileList_SelectedValueChanged;
            listBox_fileList.KeyDown += ListBox_fileList_KeyDown;
            listBox_fileList.MouseDoubleClick += ListBox_fileList_MouseDoubleClick;
            // 
            // contextMenuStrip_fileList
            // 
            contextMenuStrip_fileList.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { toolStripMenuItem_openFile, toolStripMenuItem_openFolder, toolStripMenuItem_FileDelete, toolStripMenuItem_copy });
            contextMenuStrip_fileList.Name = "contextMenuStrip_fileList";
            contextMenuStrip_fileList.Size = new System.Drawing.Size(175, 92);
            // 
            // toolStripMenuItem_openFile
            // 
            toolStripMenuItem_openFile.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            toolStripMenuItem_openFile.Name = "toolStripMenuItem_openFile";
            toolStripMenuItem_openFile.ShortcutKeyDisplayString = "Enter";
            toolStripMenuItem_openFile.Size = new System.Drawing.Size(174, 22);
            toolStripMenuItem_openFile.Text = "Open file";
            toolStripMenuItem_openFile.Click += ToolStripMenuItem_openFile_Click;
            // 
            // toolStripMenuItem_openFolder
            // 
            toolStripMenuItem_openFolder.Name = "toolStripMenuItem_openFolder";
            toolStripMenuItem_openFolder.Size = new System.Drawing.Size(174, 22);
            toolStripMenuItem_openFolder.Text = "Open folder";
            toolStripMenuItem_openFolder.Click += ToolStripMenuItem_openFolder_Click;
            // 
            // toolStripMenuItem_FileDelete
            // 
            toolStripMenuItem_FileDelete.Name = "toolStripMenuItem_FileDelete";
            toolStripMenuItem_FileDelete.ShortcutKeyDisplayString = "Del";
            toolStripMenuItem_FileDelete.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            toolStripMenuItem_FileDelete.Size = new System.Drawing.Size(174, 22);
            toolStripMenuItem_FileDelete.Text = "Delete sample";
            toolStripMenuItem_FileDelete.Visible = false;
            toolStripMenuItem_FileDelete.Click += ToolStripMenuItem_FileDelete_Click;
            // 
            // toolStripMenuItem_copy
            // 
            toolStripMenuItem_copy.Name = "toolStripMenuItem_copy";
            toolStripMenuItem_copy.ShortcutKeyDisplayString = "Ctrl-C";
            toolStripMenuItem_copy.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C;
            toolStripMenuItem_copy.Size = new System.Drawing.Size(174, 22);
            toolStripMenuItem_copy.Text = "Copy name";
            toolStripMenuItem_copy.Click += ToolStripMenuItem_copy_Click;
            // 
            // textBox_ExSearchHistory
            // 
            textBox_ExSearchHistory.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            textBox_ExSearchHistory.Location = new System.Drawing.Point(4, 470);
            textBox_ExSearchHistory.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            textBox_ExSearchHistory.Name = "textBox_ExSearchHistory";
            textBox_ExSearchHistory.ReadOnly = true;
            textBox_ExSearchHistory.Size = new System.Drawing.Size(616, 23);
            textBox_ExSearchHistory.TabIndex = 6;
            // 
            // comboBox_ExVersions
            // 
            comboBox_ExVersions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBox_ExVersions.FormattingEnabled = true;
            comboBox_ExVersions.Items.AddRange(new object[] { "Any" });
            comboBox_ExVersions.Location = new System.Drawing.Point(4, 6);
            comboBox_ExVersions.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            comboBox_ExVersions.Name = "comboBox_ExVersions";
            comboBox_ExVersions.Size = new System.Drawing.Size(70, 23);
            comboBox_ExVersions.TabIndex = 0;
            comboBox_ExVersions.SelectedIndexChanged += ComboBox_ExVersions_SelectedIndexChanged;
            // 
            // button_ExAdjustRows
            // 
            button_ExAdjustRows.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            button_ExAdjustRows.AutoSize = true;
            button_ExAdjustRows.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            button_ExAdjustRows.Location = new System.Drawing.Point(593, 3);
            button_ExAdjustRows.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            button_ExAdjustRows.Name = "button_ExAdjustRows";
            button_ExAdjustRows.Size = new System.Drawing.Size(79, 25);
            button_ExAdjustRows.TabIndex = 4;
            button_ExAdjustRows.Text = "Adjust rows";
            button_ExAdjustRows.UseVisualStyleBackColor = true;
            button_ExAdjustRows.Click += Button_ExAdjustRows_Click;
            // 
            // comboBox_ExCondition
            // 
            comboBox_ExCondition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBox_ExCondition.FormattingEnabled = true;
            comboBox_ExCondition.Location = new System.Drawing.Point(82, 6);
            comboBox_ExCondition.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            comboBox_ExCondition.Name = "comboBox_ExCondition";
            comboBox_ExCondition.Size = new System.Drawing.Size(81, 23);
            comboBox_ExCondition.TabIndex = 1;
            // 
            // textBox_ExSearchString
            // 
            textBox_ExSearchString.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            textBox_ExSearchString.Location = new System.Drawing.Point(170, 6);
            textBox_ExSearchString.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            textBox_ExSearchString.Name = "textBox_ExSearchString";
            textBox_ExSearchString.Size = new System.Drawing.Size(290, 23);
            textBox_ExSearchString.TabIndex = 2;
            textBox_ExSearchString.KeyDown += TextBox_ExSearchString_KeyDown;
            // 
            // checkBox_ExCaseSensitive
            // 
            checkBox_ExCaseSensitive.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            checkBox_ExCaseSensitive.AutoSize = true;
            checkBox_ExCaseSensitive.Location = new System.Drawing.Point(486, 8);
            checkBox_ExCaseSensitive.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            checkBox_ExCaseSensitive.Name = "checkBox_ExCaseSensitive";
            checkBox_ExCaseSensitive.Size = new System.Drawing.Size(99, 19);
            checkBox_ExCaseSensitive.TabIndex = 3;
            checkBox_ExCaseSensitive.Text = "Case sensitive";
            checkBox_ExCaseSensitive.UseVisualStyleBackColor = true;
            // 
            // button_ExClearSearch
            // 
            button_ExClearSearch.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            button_ExClearSearch.AutoSize = true;
            button_ExClearSearch.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            button_ExClearSearch.Location = new System.Drawing.Point(628, 470);
            button_ExClearSearch.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            button_ExClearSearch.Name = "button_ExClearSearch";
            button_ExClearSearch.Size = new System.Drawing.Size(44, 25);
            button_ExClearSearch.TabIndex = 7;
            button_ExClearSearch.Text = "Clear";
            button_ExClearSearch.UseVisualStyleBackColor = true;
            button_ExClearSearch.Click += Button_ExClearSearch_Click;
            // 
            // label_edit
            // 
            label_edit.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            label_edit.AutoSize = true;
            label_edit.Location = new System.Drawing.Point(555, 73);
            label_edit.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label_edit.Name = "label_edit";
            label_edit.Size = new System.Drawing.Size(111, 15);
            label_edit.TabIndex = 9;
            label_edit.Text = "Double-click to edit";
            // 
            // label_descSave
            // 
            label_descSave.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            label_descSave.AutoSize = true;
            label_descSave.Location = new System.Drawing.Point(491, 73);
            label_descSave.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label_descSave.Name = "label_descSave";
            label_descSave.Size = new System.Drawing.Size(175, 15);
            label_descSave.TabIndex = 9;
            label_descSave.Text = "Ctrl-Enter to save, ESC to cancel";
            label_descSave.Visible = false;
            // 
            // textBox_description
            // 
            textBox_description.Dock = System.Windows.Forms.DockStyle.Fill;
            textBox_description.Location = new System.Drawing.Point(0, 0);
            textBox_description.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            textBox_description.Multiline = true;
            textBox_description.Name = "textBox_description";
            textBox_description.ReadOnly = true;
            textBox_description.Size = new System.Drawing.Size(675, 90);
            textBox_description.TabIndex = 8;
            textBox_description.KeyDown += TextBox_description_KeyDown;
            textBox_description.MouseDoubleClick += TextBox_description_MouseDoubleClick;
            // 
            // tabPage_Schema
            // 
            tabPage_Schema.AutoScroll = true;
            tabPage_Schema.Controls.Add(button_compareNode);
            tabPage_Schema.Controls.Add(button_clearCompare);
            tabPage_Schema.Controls.Add(checkBox_selectedSchema);
            tabPage_Schema.Controls.Add(checkBox_deepCompare);
            tabPage_Schema.Controls.Add(button_compare);
            tabPage_Schema.Controls.Add(textBox_find);
            tabPage_Schema.Controls.Add(button_findPrev);
            tabPage_Schema.Controls.Add(button_findNext);
            tabPage_Schema.Controls.Add(checkBox_schemaSelectionSync);
            tabPage_Schema.Controls.Add(splitContainer_schemaMain);
            tabPage_Schema.Location = new System.Drawing.Point(4, 24);
            tabPage_Schema.Name = "tabPage_Schema";
            tabPage_Schema.Size = new System.Drawing.Size(907, 593);
            tabPage_Schema.TabIndex = 2;
            tabPage_Schema.Text = "Schema";
            tabPage_Schema.UseVisualStyleBackColor = true;
            // 
            // button_compareNode
            // 
            button_compareNode.AutoSize = true;
            button_compareNode.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            button_compareNode.Location = new System.Drawing.Point(299, 4);
            button_compareNode.Name = "button_compareNode";
            button_compareNode.Size = new System.Drawing.Size(96, 25);
            button_compareNode.TabIndex = 17;
            button_compareNode.Text = "Node compare";
            button_compareNode.UseVisualStyleBackColor = true;
            button_compareNode.Click += Button_compareNode_Click;
            // 
            // button_clearCompare
            // 
            button_clearCompare.AutoSize = true;
            button_clearCompare.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            button_clearCompare.Location = new System.Drawing.Point(183, 4);
            button_clearCompare.Name = "button_clearCompare";
            button_clearCompare.Size = new System.Drawing.Size(110, 25);
            button_clearCompare.TabIndex = 16;
            button_clearCompare.Text = "Clear comparison";
            button_clearCompare.UseVisualStyleBackColor = true;
            button_clearCompare.Click += Button_clearCompare_Click;
            // 
            // checkBox_selectedSchema
            // 
            checkBox_selectedSchema.AutoSize = true;
            checkBox_selectedSchema.Location = new System.Drawing.Point(75, 15);
            checkBox_selectedSchema.Name = "checkBox_selectedSchema";
            checkBox_selectedSchema.Size = new System.Drawing.Size(69, 19);
            checkBox_selectedSchema.TabIndex = 15;
            checkBox_selectedSchema.Text = "selected";
            checkBox_selectedSchema.UseVisualStyleBackColor = true;
            // 
            // checkBox_deepCompare
            // 
            checkBox_deepCompare.AutoSize = true;
            checkBox_deepCompare.Location = new System.Drawing.Point(75, 0);
            checkBox_deepCompare.Name = "checkBox_deepCompare";
            checkBox_deepCompare.Size = new System.Drawing.Size(102, 19);
            checkBox_deepCompare.TabIndex = 15;
            checkBox_deepCompare.Text = "deep compare";
            checkBox_deepCompare.UseVisualStyleBackColor = true;
            // 
            // button_compare
            // 
            button_compare.AutoSize = true;
            button_compare.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            button_compare.Location = new System.Drawing.Point(3, 3);
            button_compare.Name = "button_compare";
            button_compare.Size = new System.Drawing.Size(66, 25);
            button_compare.TabIndex = 14;
            button_compare.Text = "Compare";
            button_compare.UseVisualStyleBackColor = true;
            button_compare.Click += Button_compare_Click;
            // 
            // textBox_find
            // 
            textBox_find.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            textBox_find.Location = new System.Drawing.Point(401, 5);
            textBox_find.Name = "textBox_find";
            textBox_find.Size = new System.Drawing.Size(293, 23);
            textBox_find.TabIndex = 13;
            textBox_find.Leave += TextBox_find_Leave;
            // 
            // button_findPrev
            // 
            button_findPrev.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            button_findPrev.Location = new System.Drawing.Point(700, 4);
            button_findPrev.Name = "button_findPrev";
            button_findPrev.Size = new System.Drawing.Size(23, 23);
            button_findPrev.TabIndex = 12;
            button_findPrev.Text = "<";
            button_findPrev.UseVisualStyleBackColor = true;
            button_findPrev.Click += Button_findPrev_Click;
            // 
            // button_findNext
            // 
            button_findNext.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            button_findNext.Location = new System.Drawing.Point(729, 4);
            button_findNext.Name = "button_findNext";
            button_findNext.Size = new System.Drawing.Size(23, 23);
            button_findNext.TabIndex = 12;
            button_findNext.Text = ">";
            button_findNext.UseVisualStyleBackColor = true;
            button_findNext.Click += Button_findNext_Click;
            // 
            // checkBox_schemaSelectionSync
            // 
            checkBox_schemaSelectionSync.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            checkBox_schemaSelectionSync.AutoSize = true;
            checkBox_schemaSelectionSync.Location = new System.Drawing.Point(759, 7);
            checkBox_schemaSelectionSync.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            checkBox_schemaSelectionSync.Name = "checkBox_schemaSelectionSync";
            checkBox_schemaSelectionSync.Size = new System.Drawing.Size(145, 19);
            checkBox_schemaSelectionSync.TabIndex = 11;
            checkBox_schemaSelectionSync.Text = "Schema selection sync";
            checkBox_schemaSelectionSync.UseVisualStyleBackColor = true;
            checkBox_schemaSelectionSync.CheckedChanged += CheckBox_schemaSelectionSync_CheckedChanged;
            // 
            // splitContainer_schemaMain
            // 
            splitContainer_schemaMain.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            splitContainer_schemaMain.Location = new System.Drawing.Point(0, 35);
            splitContainer_schemaMain.Name = "splitContainer_schemaMain";
            // 
            // splitContainer_schemaMain.Panel1
            // 
            splitContainer_schemaMain.Panel1.Controls.Add(splitContainer_schemaLeft);
            // 
            // splitContainer_schemaMain.Panel2
            // 
            splitContainer_schemaMain.Panel2.Controls.Add(splitContainer_schemaRight);
            splitContainer_schemaMain.Size = new System.Drawing.Size(907, 558);
            splitContainer_schemaMain.SplitterDistance = 442;
            splitContainer_schemaMain.TabIndex = 0;
            // 
            // splitContainer_schemaLeft
            // 
            splitContainer_schemaLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            splitContainer_schemaLeft.Location = new System.Drawing.Point(0, 0);
            splitContainer_schemaLeft.Name = "splitContainer_schemaLeft";
            splitContainer_schemaLeft.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer_schemaLeft.Panel1
            // 
            splitContainer_schemaLeft.Panel1.Controls.Add(button_saveLeftSchema);
            splitContainer_schemaLeft.Panel1.Controls.Add(button_generateSchema);
            splitContainer_schemaLeft.Panel1.Controls.Add(treeView_leftSchema);
            splitContainer_schemaLeft.Size = new System.Drawing.Size(442, 558);
            splitContainer_schemaLeft.SplitterDistance = 216;
            splitContainer_schemaLeft.TabIndex = 0;
            splitContainer_schemaLeft.SplitterMoved += SplitContainer_schemaLeft_SplitterMoved;
            // 
            // button_saveLeftSchema
            // 
            button_saveLeftSchema.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            button_saveLeftSchema.AutoSize = true;
            button_saveLeftSchema.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            button_saveLeftSchema.Location = new System.Drawing.Point(334, 32);
            button_saveLeftSchema.Name = "button_saveLeftSchema";
            button_saveLeftSchema.Size = new System.Drawing.Size(85, 25);
            button_saveLeftSchema.TabIndex = 1;
            button_saveLeftSchema.Text = "Save schema";
            button_saveLeftSchema.UseVisualStyleBackColor = true;
            button_saveLeftSchema.Click += Button_saveLeftSchema_Click;
            // 
            // button_generateSchema
            // 
            button_generateSchema.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            button_generateSchema.AutoSize = true;
            button_generateSchema.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            button_generateSchema.Location = new System.Drawing.Point(311, 3);
            button_generateSchema.Name = "button_generateSchema";
            button_generateSchema.Size = new System.Drawing.Size(108, 25);
            button_generateSchema.TabIndex = 1;
            button_generateSchema.Text = "Generate schema";
            button_generateSchema.UseVisualStyleBackColor = true;
            button_generateSchema.Click += Button_generateSchema_Click;
            // 
            // treeView_leftSchema
            // 
            treeView_leftSchema.ContextMenuStrip = contextMenuStrip_leftSchema;
            treeView_leftSchema.Dock = System.Windows.Forms.DockStyle.Fill;
            treeView_leftSchema.LabelEdit = true;
            treeView_leftSchema.Location = new System.Drawing.Point(0, 0);
            treeView_leftSchema.Name = "treeView_leftSchema";
            treeView_leftSchema.Size = new System.Drawing.Size(442, 216);
            treeView_leftSchema.TabIndex = 0;
            treeView_leftSchema.BeforeLabelEdit += TreeView_BeforeLabelEdit;
            treeView_leftSchema.AfterLabelEdit += TreeView_AfterLabelEdit;
            treeView_leftSchema.AfterSelect += TreeView_leftSchema_AfterSelect;
            treeView_leftSchema.NodeMouseClick += TreeView_NodeMouseClick;
            treeView_leftSchema.Enter += TreeView_leftSchema_Enter;
            // 
            // contextMenuStrip_leftSchema
            // 
            contextMenuStrip_leftSchema.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { toolStripMenuItem_unfoldLS, toolStripMenuItem_foldLS, toolStripMenuItem_copyLS, toolStripMenuItem_addPropertyLS, toolStripMenuItem_addObjectLS, toolStripMenuItem_addArrayLS, toolStripMenuItem_renameLS, toolStripMenuItem_deleteLS });
            contextMenuStrip_leftSchema.Name = "contextMenuStrip_schema";
            contextMenuStrip_leftSchema.Size = new System.Drawing.Size(220, 180);
            contextMenuStrip_leftSchema.Opening += ContextMenuStrip_leftSchema_Opening;
            // 
            // toolStripMenuItem_unfoldLS
            // 
            toolStripMenuItem_unfoldLS.Name = "toolStripMenuItem_unfoldLS";
            toolStripMenuItem_unfoldLS.ShortcutKeyDisplayString = "Ctrl+'+'";
            toolStripMenuItem_unfoldLS.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Oemplus;
            toolStripMenuItem_unfoldLS.Size = new System.Drawing.Size(219, 22);
            toolStripMenuItem_unfoldLS.Text = "Unfold all children";
            toolStripMenuItem_unfoldLS.Click += ToolStripMenuItem_unfoldLS_Click;
            // 
            // toolStripMenuItem_foldLS
            // 
            toolStripMenuItem_foldLS.Name = "toolStripMenuItem_foldLS";
            toolStripMenuItem_foldLS.ShortcutKeyDisplayString = "Ctrl+'-'";
            toolStripMenuItem_foldLS.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.OemMinus;
            toolStripMenuItem_foldLS.Size = new System.Drawing.Size(219, 22);
            toolStripMenuItem_foldLS.Text = "Fold all children";
            toolStripMenuItem_foldLS.Click += ToolStripMenuItem_foldLS_Click;
            // 
            // toolStripMenuItem_copyLS
            // 
            toolStripMenuItem_copyLS.Name = "toolStripMenuItem_copyLS";
            toolStripMenuItem_copyLS.ShortcutKeyDisplayString = "Ctrl+C";
            toolStripMenuItem_copyLS.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C;
            toolStripMenuItem_copyLS.Size = new System.Drawing.Size(219, 22);
            toolStripMenuItem_copyLS.Text = "Copy name";
            toolStripMenuItem_copyLS.Click += ToolStripMenuItem_copyLS_Click;
            // 
            // toolStripMenuItem_addPropertyLS
            // 
            toolStripMenuItem_addPropertyLS.Name = "toolStripMenuItem_addPropertyLS";
            toolStripMenuItem_addPropertyLS.Size = new System.Drawing.Size(219, 22);
            toolStripMenuItem_addPropertyLS.Text = "Add property";
            toolStripMenuItem_addPropertyLS.Click += ToolStripMenuItem_addPropertyLS_Click;
            // 
            // toolStripMenuItem_addObjectLS
            // 
            toolStripMenuItem_addObjectLS.Name = "toolStripMenuItem_addObjectLS";
            toolStripMenuItem_addObjectLS.Size = new System.Drawing.Size(219, 22);
            toolStripMenuItem_addObjectLS.Text = "Add object";
            toolStripMenuItem_addObjectLS.Click += ToolStripMenuItem_addObjectLS_Click;
            // 
            // toolStripMenuItem_addArrayLS
            // 
            toolStripMenuItem_addArrayLS.Name = "toolStripMenuItem_addArrayLS";
            toolStripMenuItem_addArrayLS.Size = new System.Drawing.Size(219, 22);
            toolStripMenuItem_addArrayLS.Text = "Add array";
            toolStripMenuItem_addArrayLS.Click += ToolStripMenuItem_addArrayLS_Click;
            // 
            // toolStripMenuItem_renameLS
            // 
            toolStripMenuItem_renameLS.Name = "toolStripMenuItem_renameLS";
            toolStripMenuItem_renameLS.Size = new System.Drawing.Size(219, 22);
            toolStripMenuItem_renameLS.Text = "Rename";
            toolStripMenuItem_renameLS.Click += ToolStripMenuItem_renameLS_Click;
            // 
            // toolStripMenuItem_deleteLS
            // 
            toolStripMenuItem_deleteLS.Name = "toolStripMenuItem_deleteLS";
            toolStripMenuItem_deleteLS.ShortcutKeyDisplayString = "Del";
            toolStripMenuItem_deleteLS.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            toolStripMenuItem_deleteLS.Size = new System.Drawing.Size(219, 22);
            toolStripMenuItem_deleteLS.Text = "Delete";
            toolStripMenuItem_deleteLS.Click += ToolStripMenuItem_deleteLS_Click;
            // 
            // splitContainer_schemaRight
            // 
            splitContainer_schemaRight.Dock = System.Windows.Forms.DockStyle.Fill;
            splitContainer_schemaRight.Location = new System.Drawing.Point(0, 0);
            splitContainer_schemaRight.Name = "splitContainer_schemaRight";
            splitContainer_schemaRight.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer_schemaRight.Panel1
            // 
            splitContainer_schemaRight.Panel1.AutoScroll = true;
            splitContainer_schemaRight.Panel1.Controls.Add(button_saveRightSchema);
            splitContainer_schemaRight.Panel1.Controls.Add(button_loadSchema);
            splitContainer_schemaRight.Panel1.Controls.Add(treeView_rightSchema);
            // 
            // splitContainer_schemaRight.Panel2
            // 
            splitContainer_schemaRight.Panel2.AutoScroll = true;
            splitContainer_schemaRight.Size = new System.Drawing.Size(461, 558);
            splitContainer_schemaRight.SplitterDistance = 222;
            splitContainer_schemaRight.TabIndex = 0;
            splitContainer_schemaRight.SplitterMoved += SplitContainer_schemaRight_SplitterMoved;
            // 
            // button_saveRightSchema
            // 
            button_saveRightSchema.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            button_saveRightSchema.Location = new System.Drawing.Point(342, 32);
            button_saveRightSchema.Name = "button_saveRightSchema";
            button_saveRightSchema.Size = new System.Drawing.Size(96, 23);
            button_saveRightSchema.TabIndex = 1;
            button_saveRightSchema.Text = "Save schema";
            button_saveRightSchema.UseVisualStyleBackColor = true;
            button_saveRightSchema.Click += Button_saveRightSchema_Click;
            // 
            // button_loadSchema
            // 
            button_loadSchema.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            button_loadSchema.AutoSize = true;
            button_loadSchema.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            button_loadSchema.Location = new System.Drawing.Point(351, 3);
            button_loadSchema.Name = "button_loadSchema";
            button_loadSchema.Size = new System.Drawing.Size(87, 25);
            button_loadSchema.TabIndex = 7;
            button_loadSchema.Text = "Load schema";
            button_loadSchema.UseVisualStyleBackColor = true;
            button_loadSchema.Click += Button_loadSchema_Click;
            // 
            // treeView_rightSchema
            // 
            treeView_rightSchema.ContextMenuStrip = contextMenuStrip_rightSchema;
            treeView_rightSchema.Dock = System.Windows.Forms.DockStyle.Fill;
            treeView_rightSchema.LabelEdit = true;
            treeView_rightSchema.Location = new System.Drawing.Point(0, 0);
            treeView_rightSchema.Name = "treeView_rightSchema";
            treeView_rightSchema.Size = new System.Drawing.Size(461, 222);
            treeView_rightSchema.TabIndex = 0;
            treeView_rightSchema.BeforeLabelEdit += TreeView_BeforeLabelEdit;
            treeView_rightSchema.AfterLabelEdit += TreeView_AfterLabelEdit;
            treeView_rightSchema.AfterSelect += TreeView_rightSchema_AfterSelect;
            treeView_rightSchema.NodeMouseClick += TreeView_NodeMouseClick;
            treeView_rightSchema.Enter += TreeView_rightSchema_Enter;
            // 
            // contextMenuStrip_rightSchema
            // 
            contextMenuStrip_rightSchema.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { toolStripMenuItem_unfoldRS, toolStripMenuItem_foldRS, toolStripMenuItem_copyRS, toolStripMenuItem_addPropertyRS, toolStripMenuItem_addObjectRS, toolStripMenuItem_addArrayRS, toolStripMenuItem_renameRS, toolStripMenuItem_deleteRS });
            contextMenuStrip_rightSchema.Name = "contextMenuStrip_schema";
            contextMenuStrip_rightSchema.Size = new System.Drawing.Size(226, 180);
            contextMenuStrip_rightSchema.Opening += ContextMenuStrip_rightSchema_Opening;
            // 
            // toolStripMenuItem_unfoldRS
            // 
            toolStripMenuItem_unfoldRS.Name = "toolStripMenuItem_unfoldRS";
            toolStripMenuItem_unfoldRS.ShortcutKeyDisplayString = "Ctrel+'+'";
            toolStripMenuItem_unfoldRS.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Oemplus;
            toolStripMenuItem_unfoldRS.Size = new System.Drawing.Size(225, 22);
            toolStripMenuItem_unfoldRS.Text = "Unfold all children";
            toolStripMenuItem_unfoldRS.Click += ToolStripMenuItem_unfoldRS_Click;
            // 
            // toolStripMenuItem_foldRS
            // 
            toolStripMenuItem_foldRS.Name = "toolStripMenuItem_foldRS";
            toolStripMenuItem_foldRS.ShortcutKeyDisplayString = "Ctrl+'-'";
            toolStripMenuItem_foldRS.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.OemMinus;
            toolStripMenuItem_foldRS.Size = new System.Drawing.Size(225, 22);
            toolStripMenuItem_foldRS.Text = "Fold all children";
            toolStripMenuItem_foldRS.Click += ToolStripMenuItem_foldRS_Click;
            // 
            // toolStripMenuItem_copyRS
            // 
            toolStripMenuItem_copyRS.Name = "toolStripMenuItem_copyRS";
            toolStripMenuItem_copyRS.ShortcutKeyDisplayString = "Ctrl+C";
            toolStripMenuItem_copyRS.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C;
            toolStripMenuItem_copyRS.Size = new System.Drawing.Size(225, 22);
            toolStripMenuItem_copyRS.Text = "Copy name";
            toolStripMenuItem_copyRS.Click += ToolStripMenuItem_copyRS_Click;
            // 
            // toolStripMenuItem_addPropertyRS
            // 
            toolStripMenuItem_addPropertyRS.Name = "toolStripMenuItem_addPropertyRS";
            toolStripMenuItem_addPropertyRS.Size = new System.Drawing.Size(225, 22);
            toolStripMenuItem_addPropertyRS.Text = "Add property";
            toolStripMenuItem_addPropertyRS.Click += ToolStripMenuItem_addPropertyRS_Click;
            // 
            // toolStripMenuItem_addObjectRS
            // 
            toolStripMenuItem_addObjectRS.Name = "toolStripMenuItem_addObjectRS";
            toolStripMenuItem_addObjectRS.Size = new System.Drawing.Size(225, 22);
            toolStripMenuItem_addObjectRS.Text = "Add object";
            toolStripMenuItem_addObjectRS.Click += ToolStripMenuItem_addObjectRS_Click;
            // 
            // toolStripMenuItem_addArrayRS
            // 
            toolStripMenuItem_addArrayRS.Name = "toolStripMenuItem_addArrayRS";
            toolStripMenuItem_addArrayRS.Size = new System.Drawing.Size(225, 22);
            toolStripMenuItem_addArrayRS.Text = "Add array";
            toolStripMenuItem_addArrayRS.Click += ToolStripMenuItem_addArrayRS_Click;
            // 
            // toolStripMenuItem_renameRS
            // 
            toolStripMenuItem_renameRS.Name = "toolStripMenuItem_renameRS";
            toolStripMenuItem_renameRS.Size = new System.Drawing.Size(225, 22);
            toolStripMenuItem_renameRS.Text = "Rename";
            toolStripMenuItem_renameRS.Click += ToolStripMenuItem_renameRS_Click;
            // 
            // toolStripMenuItem_deleteRS
            // 
            toolStripMenuItem_deleteRS.Name = "toolStripMenuItem_deleteRS";
            toolStripMenuItem_deleteRS.ShortcutKeyDisplayString = "Del";
            toolStripMenuItem_deleteRS.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            toolStripMenuItem_deleteRS.Size = new System.Drawing.Size(225, 22);
            toolStripMenuItem_deleteRS.Text = "Delete";
            toolStripMenuItem_deleteRS.Click += ToolStripMenuItem_deleteRS_Click;
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            openFileDialog1.FileOk += OpenFileDialog1_FileOk;
            // 
            // folderBrowserDialog1
            // 
            folderBrowserDialog1.Description = "Select root folder";
            folderBrowserDialog1.RootFolder = System.Environment.SpecialFolder.MyComputer;
            folderBrowserDialog1.ShowNewFolderButton = false;
            // 
            // saveFileDialog1
            // 
            saveFileDialog1.FileOk += SaveFileDialog1_FileOk;
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { toolStripStatusLabel1 });
            statusStrip1.Location = new System.Drawing.Point(0, 625);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
            statusStrip1.Size = new System.Drawing.Size(915, 22);
            statusStrip1.TabIndex = 2;
            statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // Form1
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(915, 647);
            Controls.Add(statusStrip1);
            Controls.Add(tabControl1);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            MinimumSize = new System.Drawing.Size(697, 456);
            Name = "Form1";
            StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            Text = "JsonDictionary";
            FormClosing += Form1_FormClosing;
            Shown += Form1_Shown;
            tabControl1.ResumeLayout(false);
            tabPage_DataCollection.ResumeLayout(false);
            splitContainer_buttons.Panel1.ResumeLayout(false);
            splitContainer_buttons.Panel1.PerformLayout();
            splitContainer_buttons.Panel2.ResumeLayout(false);
            splitContainer_buttons.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer_buttons).EndInit();
            splitContainer_buttons.ResumeLayout(false);
            tabPage_SamplesTree.ResumeLayout(false);
            splitContainer_tree.Panel1.ResumeLayout(false);
            splitContainer_tree.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer_tree).EndInit();
            splitContainer_tree.ResumeLayout(false);
            contextMenuStrip_tree.ResumeLayout(false);
            splitContainer_description.Panel1.ResumeLayout(false);
            splitContainer_description.Panel1.PerformLayout();
            splitContainer_description.Panel2.ResumeLayout(false);
            splitContainer_description.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer_description).EndInit();
            splitContainer_description.ResumeLayout(false);
            splitContainer_fileList.Panel1.ResumeLayout(false);
            splitContainer_fileList.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer_fileList).EndInit();
            splitContainer_fileList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView_examples).EndInit();
            contextMenuStrip_samples.ResumeLayout(false);
            contextMenuStrip_fileList.ResumeLayout(false);
            tabPage_Schema.ResumeLayout(false);
            tabPage_Schema.PerformLayout();
            splitContainer_schemaMain.Panel1.ResumeLayout(false);
            splitContainer_schemaMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer_schemaMain).EndInit();
            splitContainer_schemaMain.ResumeLayout(false);
            splitContainer_schemaLeft.Panel1.ResumeLayout(false);
            splitContainer_schemaLeft.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer_schemaLeft).EndInit();
            splitContainer_schemaLeft.ResumeLayout(false);
            contextMenuStrip_leftSchema.ResumeLayout(false);
            splitContainer_schemaRight.Panel1.ResumeLayout(false);
            splitContainer_schemaRight.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer_schemaRight).EndInit();
            splitContainer_schemaRight.ResumeLayout(false);
            contextMenuStrip_rightSchema.ResumeLayout(false);
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage_DataCollection;
        private System.Windows.Forms.TabPage tabPage_SamplesTree;
        private System.Windows.Forms.TreeView treeView_examples;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TextBox textBox_logText;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button button_collectDatabase;
        private System.Windows.Forms.Button button_saveDb;
        private System.Windows.Forms.Button button_loadDb;
        private System.Windows.Forms.SplitContainer splitContainer_tree;
        private System.Windows.Forms.SplitContainer splitContainer_buttons;
        private System.Windows.Forms.DataGridView dataGridView_examples;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.TextBox textBox_ExSearchString;
        private System.Windows.Forms.ComboBox comboBox_ExCondition;
        private System.Windows.Forms.CheckBox checkBox_ExCaseSensitive;
        private System.Windows.Forms.TextBox textBox_ExSearchHistory;
        private System.Windows.Forms.ComboBox comboBox_ExVersions;
        private System.Windows.Forms.Button button_ExAdjustRows;
        private System.Windows.Forms.CheckBox checkBox_alwaysOnTop;
        private System.Windows.Forms.CheckBox checkBox_showPreview;
        private System.Windows.Forms.CheckBox checkBox_reformatJsonBrackets;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Button button_ExClearSearch;
        private System.Windows.Forms.CheckBox checkBox_loadDbOnStart;
        private System.Windows.Forms.CheckBox checkBox_vsCode;
        private System.Windows.Forms.SplitContainer splitContainer_description;
        private System.Windows.Forms.TextBox textBox_description;
        private System.Windows.Forms.SplitContainer splitContainer_fileList;
        private System.Windows.Forms.ListBox listBox_fileList;
        private System.Windows.Forms.Label label_descSave;
        private System.Windows.Forms.Label label_edit;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_fileList;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_openFolder;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_copy;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_openFile;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_samples;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_tree;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_SampleCopy;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_SampleOpen;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_SampleDelete;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_FileDelete;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_treeShow;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_unfoldAll;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_foldAll;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_treeCopy;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_treeDelete;
        private System.Windows.Forms.CheckBox checkBox_beautifyJson;
        private System.Windows.Forms.TabPage tabPage_Schema;
        private System.Windows.Forms.SplitContainer splitContainer_schemaMain;
        private System.Windows.Forms.SplitContainer splitContainer_schemaLeft;
        private System.Windows.Forms.SplitContainer splitContainer_schemaRight;
        private System.Windows.Forms.TreeView treeView_leftSchema;
        private System.Windows.Forms.TreeView treeView_rightSchema;
        private System.Windows.Forms.Button button_loadSchema;
        private System.Windows.Forms.Button button_generateSchema;
        private System.Windows.Forms.Button button_saveLeftSchema;
        private System.Windows.Forms.Button button_saveRightSchema;
        private System.Windows.Forms.Button button_compare;
        private System.Windows.Forms.TextBox textBox_find;
        private System.Windows.Forms.Button button_findPrev;
        private System.Windows.Forms.Button button_findNext;
        private System.Windows.Forms.CheckBox checkBox_schemaSelectionSync;
        private System.Windows.Forms.CheckBox checkBox_deepCompare;
        private System.Windows.Forms.Button button_clearCompare;
        private System.Windows.Forms.Button button_compareNode;
        private System.Windows.Forms.CheckBox checkBox_selectedSchema;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_leftSchema;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_addPropertyLS;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_renameLS;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_deleteLS;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_copyLS;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_foldLS;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_unfoldLS;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_rightSchema;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_addPropertyRS;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_renameRS;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_deleteRS;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_copyRS;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_foldRS;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_unfoldRS;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_addObjectLS;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_addArrayLS;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_addObjectRS;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_addArrayRS;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_generateSchema;
    }
}

