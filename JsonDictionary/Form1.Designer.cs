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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage_DataCollection = new System.Windows.Forms.TabPage();
            this.splitContainer_buttons = new System.Windows.Forms.SplitContainer();
            this.checkBox_vsCode = new System.Windows.Forms.CheckBox();
            this.checkBox_loadDbOnStart = new System.Windows.Forms.CheckBox();
            this.checkBox_schemaSelectionSync = new System.Windows.Forms.CheckBox();
            this.checkBox_alwaysOnTop = new System.Windows.Forms.CheckBox();
            this.checkBox_showPreview = new System.Windows.Forms.CheckBox();
            this.checkBox_beautifyJson = new System.Windows.Forms.CheckBox();
            this.checkBox_reformatJsonBrackets = new System.Windows.Forms.CheckBox();
            this.button_saveDb = new System.Windows.Forms.Button();
            this.button_loadDb = new System.Windows.Forms.Button();
            this.button_collectDatabase = new System.Windows.Forms.Button();
            this.textBox_logText = new System.Windows.Forms.TextBox();
            this.tabPage_SamplesTree = new System.Windows.Forms.TabPage();
            this.splitContainer_tree = new System.Windows.Forms.SplitContainer();
            this.treeView_examples = new System.Windows.Forms.TreeView();
            this.contextMenuStrip_tree = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem_treeShow = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_unfoldAll = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_foldAll = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_treeCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_treeDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer_description = new System.Windows.Forms.SplitContainer();
            this.splitContainer_fileList = new System.Windows.Forms.SplitContainer();
            this.dataGridView_examples = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip_samples = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem_SampleOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_SampleDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_SampleCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.listBox_fileList = new System.Windows.Forms.ListBox();
            this.contextMenuStrip_fileList = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem_openFile = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_openFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_FileDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_copy = new System.Windows.Forms.ToolStripMenuItem();
            this.textBox_ExSearchHistory = new System.Windows.Forms.TextBox();
            this.comboBox_ExVersions = new System.Windows.Forms.ComboBox();
            this.button_ExAdjustRows = new System.Windows.Forms.Button();
            this.comboBox_ExCondition = new System.Windows.Forms.ComboBox();
            this.textBox_ExSearchString = new System.Windows.Forms.TextBox();
            this.checkBox_ExCaseSensitive = new System.Windows.Forms.CheckBox();
            this.button_ExClearSearch = new System.Windows.Forms.Button();
            this.label_edit = new System.Windows.Forms.Label();
            this.label_descSave = new System.Windows.Forms.Label();
            this.textBox_description = new System.Windows.Forms.TextBox();
            this.tabPage_Schema = new System.Windows.Forms.TabPage();
            this.textBox_schemaUrl = new System.Windows.Forms.TextBox();
            this.button_regenerateSchema = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox_contentVersion = new System.Windows.Forms.ComboBox();
            this.comboBox_fileType = new System.Windows.Forms.ComboBox();
            this.splitContainer_schemaMain = new System.Windows.Forms.SplitContainer();
            this.splitContainer_schemaLeft = new System.Windows.Forms.SplitContainer();
            this.button_generateSchema = new System.Windows.Forms.Button();
            this.treeView_leftSchema = new System.Windows.Forms.TreeView();
            this.splitContainer_schemaRight = new System.Windows.Forms.SplitContainer();
            this.button_loadSchema = new System.Windows.Forms.Button();
            this.treeView_rightSchema = new System.Windows.Forms.TreeView();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.button_saveLeftSchema = new System.Windows.Forms.Button();
            this.button_saveRightSchema = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage_DataCollection.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_buttons)).BeginInit();
            this.splitContainer_buttons.Panel1.SuspendLayout();
            this.splitContainer_buttons.Panel2.SuspendLayout();
            this.splitContainer_buttons.SuspendLayout();
            this.tabPage_SamplesTree.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_tree)).BeginInit();
            this.splitContainer_tree.Panel1.SuspendLayout();
            this.splitContainer_tree.Panel2.SuspendLayout();
            this.splitContainer_tree.SuspendLayout();
            this.contextMenuStrip_tree.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_description)).BeginInit();
            this.splitContainer_description.Panel1.SuspendLayout();
            this.splitContainer_description.Panel2.SuspendLayout();
            this.splitContainer_description.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_fileList)).BeginInit();
            this.splitContainer_fileList.Panel1.SuspendLayout();
            this.splitContainer_fileList.Panel2.SuspendLayout();
            this.splitContainer_fileList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_examples)).BeginInit();
            this.contextMenuStrip_samples.SuspendLayout();
            this.contextMenuStrip_fileList.SuspendLayout();
            this.tabPage_Schema.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_schemaMain)).BeginInit();
            this.splitContainer_schemaMain.Panel1.SuspendLayout();
            this.splitContainer_schemaMain.Panel2.SuspendLayout();
            this.splitContainer_schemaMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_schemaLeft)).BeginInit();
            this.splitContainer_schemaLeft.Panel1.SuspendLayout();
            this.splitContainer_schemaLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_schemaRight)).BeginInit();
            this.splitContainer_schemaRight.Panel1.SuspendLayout();
            this.splitContainer_schemaRight.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage_DataCollection);
            this.tabControl1.Controls.Add(this.tabPage_SamplesTree);
            this.tabControl1.Controls.Add(this.tabPage_Schema);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(915, 621);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage_DataCollection
            // 
            this.tabPage_DataCollection.Controls.Add(this.splitContainer_buttons);
            this.tabPage_DataCollection.Location = new System.Drawing.Point(4, 24);
            this.tabPage_DataCollection.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tabPage_DataCollection.Name = "tabPage_DataCollection";
            this.tabPage_DataCollection.Size = new System.Drawing.Size(907, 593);
            this.tabPage_DataCollection.TabIndex = 0;
            this.tabPage_DataCollection.Text = "Data collection";
            this.tabPage_DataCollection.UseVisualStyleBackColor = true;
            // 
            // splitContainer_buttons
            // 
            this.splitContainer_buttons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer_buttons.Location = new System.Drawing.Point(0, 0);
            this.splitContainer_buttons.Margin = new System.Windows.Forms.Padding(0);
            this.splitContainer_buttons.Name = "splitContainer_buttons";
            // 
            // splitContainer_buttons.Panel1
            // 
            this.splitContainer_buttons.Panel1.Controls.Add(this.checkBox_vsCode);
            this.splitContainer_buttons.Panel1.Controls.Add(this.checkBox_loadDbOnStart);
            this.splitContainer_buttons.Panel1.Controls.Add(this.checkBox_schemaSelectionSync);
            this.splitContainer_buttons.Panel1.Controls.Add(this.checkBox_alwaysOnTop);
            this.splitContainer_buttons.Panel1.Controls.Add(this.checkBox_showPreview);
            this.splitContainer_buttons.Panel1.Controls.Add(this.checkBox_beautifyJson);
            this.splitContainer_buttons.Panel1.Controls.Add(this.checkBox_reformatJsonBrackets);
            this.splitContainer_buttons.Panel1.Controls.Add(this.button_saveDb);
            this.splitContainer_buttons.Panel1.Controls.Add(this.button_loadDb);
            this.splitContainer_buttons.Panel1.Controls.Add(this.button_collectDatabase);
            this.splitContainer_buttons.Panel1MinSize = 120;
            // 
            // splitContainer_buttons.Panel2
            // 
            this.splitContainer_buttons.Panel2.Controls.Add(this.textBox_logText);
            this.splitContainer_buttons.Size = new System.Drawing.Size(907, 593);
            this.splitContainer_buttons.SplitterDistance = 155;
            this.splitContainer_buttons.SplitterWidth = 5;
            this.splitContainer_buttons.TabIndex = 18;
            // 
            // checkBox_vsCode
            // 
            this.checkBox_vsCode.AutoSize = true;
            this.checkBox_vsCode.Location = new System.Drawing.Point(6, 204);
            this.checkBox_vsCode.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.checkBox_vsCode.Name = "checkBox_vsCode";
            this.checkBox_vsCode.Size = new System.Drawing.Size(89, 19);
            this.checkBox_vsCode.TabIndex = 11;
            this.checkBox_vsCode.Text = "Use VSCode";
            this.checkBox_vsCode.UseVisualStyleBackColor = true;
            this.checkBox_vsCode.CheckedChanged += new System.EventHandler(this.CheckBox_vsCode_CheckedChanged);
            // 
            // checkBox_loadDbOnStart
            // 
            this.checkBox_loadDbOnStart.AutoSize = true;
            this.checkBox_loadDbOnStart.Location = new System.Drawing.Point(6, 104);
            this.checkBox_loadDbOnStart.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.checkBox_loadDbOnStart.Name = "checkBox_loadDbOnStart";
            this.checkBox_loadDbOnStart.Size = new System.Drawing.Size(132, 19);
            this.checkBox_loadDbOnStart.TabIndex = 5;
            this.checkBox_loadDbOnStart.Text = "Load DB on start-up";
            this.checkBox_loadDbOnStart.UseVisualStyleBackColor = true;
            this.checkBox_loadDbOnStart.CheckedChanged += new System.EventHandler(this.CheckBox_loadDbOnStart_CheckedChanged);
            // 
            // checkBox_schemaSelectionSync
            // 
            this.checkBox_schemaSelectionSync.AutoSize = true;
            this.checkBox_schemaSelectionSync.Location = new System.Drawing.Point(6, 254);
            this.checkBox_schemaSelectionSync.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.checkBox_schemaSelectionSync.Name = "checkBox_schemaSelectionSync";
            this.checkBox_schemaSelectionSync.Size = new System.Drawing.Size(145, 19);
            this.checkBox_schemaSelectionSync.TabIndex = 10;
            this.checkBox_schemaSelectionSync.Text = "Schema selection sync";
            this.checkBox_schemaSelectionSync.UseVisualStyleBackColor = true;
            this.checkBox_schemaSelectionSync.CheckedChanged += new System.EventHandler(this.CheckBox_schemaSelectionSync_CheckedChanged);
            // 
            // checkBox_alwaysOnTop
            // 
            this.checkBox_alwaysOnTop.AutoSize = true;
            this.checkBox_alwaysOnTop.Location = new System.Drawing.Point(6, 229);
            this.checkBox_alwaysOnTop.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.checkBox_alwaysOnTop.Name = "checkBox_alwaysOnTop";
            this.checkBox_alwaysOnTop.Size = new System.Drawing.Size(101, 19);
            this.checkBox_alwaysOnTop.TabIndex = 10;
            this.checkBox_alwaysOnTop.Text = "Always on top";
            this.checkBox_alwaysOnTop.UseVisualStyleBackColor = true;
            this.checkBox_alwaysOnTop.CheckedChanged += new System.EventHandler(this.CheckBox_alwaysOnTop_CheckedChanged);
            // 
            // checkBox_showPreview
            // 
            this.checkBox_showPreview.AutoSize = true;
            this.checkBox_showPreview.Location = new System.Drawing.Point(6, 179);
            this.checkBox_showPreview.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.checkBox_showPreview.Name = "checkBox_showPreview";
            this.checkBox_showPreview.Size = new System.Drawing.Size(99, 19);
            this.checkBox_showPreview.TabIndex = 9;
            this.checkBox_showPreview.Text = "Show preview";
            this.checkBox_showPreview.UseVisualStyleBackColor = true;
            this.checkBox_showPreview.CheckedChanged += new System.EventHandler(this.CheckBox_showPreview_CheckedChanged);
            // 
            // checkBox_beautifyJson
            // 
            this.checkBox_beautifyJson.AutoSize = true;
            this.checkBox_beautifyJson.Location = new System.Drawing.Point(6, 129);
            this.checkBox_beautifyJson.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.checkBox_beautifyJson.Name = "checkBox_beautifyJson";
            this.checkBox_beautifyJson.Size = new System.Drawing.Size(100, 19);
            this.checkBox_beautifyJson.TabIndex = 7;
            this.checkBox_beautifyJson.Text = "Beautify JSON";
            this.checkBox_beautifyJson.UseVisualStyleBackColor = true;
            this.checkBox_beautifyJson.CheckedChanged += new System.EventHandler(this.CheckBox_beautifyJson_CheckedChanged);
            // 
            // checkBox_reformatJsonBrackets
            // 
            this.checkBox_reformatJsonBrackets.AutoSize = true;
            this.checkBox_reformatJsonBrackets.Enabled = false;
            this.checkBox_reformatJsonBrackets.Location = new System.Drawing.Point(6, 154);
            this.checkBox_reformatJsonBrackets.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.checkBox_reformatJsonBrackets.Name = "checkBox_reformatJsonBrackets";
            this.checkBox_reformatJsonBrackets.Size = new System.Drawing.Size(153, 19);
            this.checkBox_reformatJsonBrackets.TabIndex = 7;
            this.checkBox_reformatJsonBrackets.Text = "Reformat JSON brackets";
            this.checkBox_reformatJsonBrackets.UseVisualStyleBackColor = true;
            this.checkBox_reformatJsonBrackets.CheckedChanged += new System.EventHandler(this.CheckBox_reformatJsonBrackets_CheckedChanged);
            this.checkBox_reformatJsonBrackets.EnabledChanged += new System.EventHandler(this.CheckBox_reformatJsonBrackets_EnabledChanged);
            // 
            // button_saveDb
            // 
            this.button_saveDb.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button_saveDb.Location = new System.Drawing.Point(0, 70);
            this.button_saveDb.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.button_saveDb.Name = "button_saveDb";
            this.button_saveDb.Size = new System.Drawing.Size(153, 27);
            this.button_saveDb.TabIndex = 4;
            this.button_saveDb.Text = "Save database";
            this.button_saveDb.UseVisualStyleBackColor = true;
            this.button_saveDb.Click += new System.EventHandler(this.Button_saveDb_Click);
            // 
            // button_loadDb
            // 
            this.button_loadDb.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button_loadDb.Location = new System.Drawing.Point(0, 37);
            this.button_loadDb.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.button_loadDb.Name = "button_loadDb";
            this.button_loadDb.Size = new System.Drawing.Size(153, 27);
            this.button_loadDb.TabIndex = 3;
            this.button_loadDb.Text = "Load database";
            this.button_loadDb.UseVisualStyleBackColor = true;
            this.button_loadDb.Click += new System.EventHandler(this.Button_loadDb_Click);
            // 
            // button_collectDatabase
            // 
            this.button_collectDatabase.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button_collectDatabase.Location = new System.Drawing.Point(0, 3);
            this.button_collectDatabase.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.button_collectDatabase.Name = "button_collectDatabase";
            this.button_collectDatabase.Size = new System.Drawing.Size(153, 27);
            this.button_collectDatabase.TabIndex = 2;
            this.button_collectDatabase.Text = "Collect database";
            this.button_collectDatabase.UseVisualStyleBackColor = true;
            this.button_collectDatabase.Click += new System.EventHandler(this.Button_collectDatabase_Click);
            // 
            // textBox_logText
            // 
            this.textBox_logText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox_logText.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBox_logText.Location = new System.Drawing.Point(0, 0);
            this.textBox_logText.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.textBox_logText.Multiline = true;
            this.textBox_logText.Name = "textBox_logText";
            this.textBox_logText.ReadOnly = true;
            this.textBox_logText.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox_logText.Size = new System.Drawing.Size(747, 593);
            this.textBox_logText.TabIndex = 0;
            // 
            // tabPage_SamplesTree
            // 
            this.tabPage_SamplesTree.Controls.Add(this.splitContainer_tree);
            this.tabPage_SamplesTree.Location = new System.Drawing.Point(4, 24);
            this.tabPage_SamplesTree.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tabPage_SamplesTree.Name = "tabPage_SamplesTree";
            this.tabPage_SamplesTree.Size = new System.Drawing.Size(907, 593);
            this.tabPage_SamplesTree.TabIndex = 1;
            this.tabPage_SamplesTree.Text = "Samples tree";
            this.tabPage_SamplesTree.UseVisualStyleBackColor = true;
            // 
            // splitContainer_tree
            // 
            this.splitContainer_tree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer_tree.Location = new System.Drawing.Point(0, 0);
            this.splitContainer_tree.Margin = new System.Windows.Forms.Padding(0);
            this.splitContainer_tree.Name = "splitContainer_tree";
            // 
            // splitContainer_tree.Panel1
            // 
            this.splitContainer_tree.Panel1.Controls.Add(this.treeView_examples);
            this.splitContainer_tree.Panel1MinSize = 100;
            // 
            // splitContainer_tree.Panel2
            // 
            this.splitContainer_tree.Panel2.Controls.Add(this.splitContainer_description);
            this.splitContainer_tree.Panel2MinSize = 350;
            this.splitContainer_tree.Size = new System.Drawing.Size(907, 593);
            this.splitContainer_tree.SplitterDistance = 227;
            this.splitContainer_tree.SplitterWidth = 5;
            this.splitContainer_tree.TabIndex = 1;
            // 
            // treeView_examples
            // 
            this.treeView_examples.ContextMenuStrip = this.contextMenuStrip_tree;
            this.treeView_examples.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView_examples.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.treeView_examples.HideSelection = false;
            this.treeView_examples.Location = new System.Drawing.Point(0, 0);
            this.treeView_examples.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.treeView_examples.Name = "treeView_examples";
            this.treeView_examples.Size = new System.Drawing.Size(227, 593);
            this.treeView_examples.TabIndex = 0;
            this.treeView_examples.BeforeCollapse += new System.Windows.Forms.TreeViewCancelEventHandler(this.TreeView_No_Expand_Collapse);
            this.treeView_examples.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.TreeView_No_Expand_Collapse);
            this.treeView_examples.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TreeView_examples_AfterSelect);
            this.treeView_examples.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.TreeView_NodeMouseClick);
            this.treeView_examples.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.TreeView_examples_NodeMouseDoubleClick);
            this.treeView_examples.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TreeView_examples_KeyDown);
            this.treeView_examples.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TreeView_MouseDown);
            // 
            // contextMenuStrip_tree
            // 
            this.contextMenuStrip_tree.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_treeShow,
            this.toolStripMenuItem_unfoldAll,
            this.toolStripMenuItem_foldAll,
            this.toolStripMenuItem_treeCopy,
            this.toolStripMenuItem_treeDelete});
            this.contextMenuStrip_tree.Name = "contextMenuStrip_tree";
            this.contextMenuStrip_tree.Size = new System.Drawing.Size(190, 114);
            // 
            // toolStripMenuItem_treeShow
            // 
            this.toolStripMenuItem_treeShow.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.toolStripMenuItem_treeShow.Name = "toolStripMenuItem_treeShow";
            this.toolStripMenuItem_treeShow.ShortcutKeyDisplayString = "Enter";
            this.toolStripMenuItem_treeShow.Size = new System.Drawing.Size(189, 22);
            this.toolStripMenuItem_treeShow.Text = "Show samples";
            this.toolStripMenuItem_treeShow.Click += new System.EventHandler(this.ToolStripMenuItem_treeShow_Click);
            // 
            // toolStripMenuItem_unfoldAll
            // 
            this.toolStripMenuItem_unfoldAll.Name = "toolStripMenuItem_unfoldAll";
            this.toolStripMenuItem_unfoldAll.ShortcutKeyDisplayString = "+";
            this.toolStripMenuItem_unfoldAll.Size = new System.Drawing.Size(189, 22);
            this.toolStripMenuItem_unfoldAll.Text = "Unfold all children";
            this.toolStripMenuItem_unfoldAll.Click += new System.EventHandler(this.ToolStripMenuItem_unfoldAll_Click);
            // 
            // toolStripMenuItem_foldAll
            // 
            this.toolStripMenuItem_foldAll.Name = "toolStripMenuItem_foldAll";
            this.toolStripMenuItem_foldAll.ShortcutKeyDisplayString = "-";
            this.toolStripMenuItem_foldAll.Size = new System.Drawing.Size(189, 22);
            this.toolStripMenuItem_foldAll.Text = "Fold all children";
            this.toolStripMenuItem_foldAll.Click += new System.EventHandler(this.ToolStripMenuItem_foldAll_Click);
            // 
            // toolStripMenuItem_treeCopy
            // 
            this.toolStripMenuItem_treeCopy.Name = "toolStripMenuItem_treeCopy";
            this.toolStripMenuItem_treeCopy.ShortcutKeyDisplayString = "Ctrl-C";
            this.toolStripMenuItem_treeCopy.Size = new System.Drawing.Size(189, 22);
            this.toolStripMenuItem_treeCopy.Text = "Copy";
            this.toolStripMenuItem_treeCopy.Click += new System.EventHandler(this.ToolStripMenuItem_treeCopy_Click);
            // 
            // toolStripMenuItem_treeDelete
            // 
            this.toolStripMenuItem_treeDelete.Name = "toolStripMenuItem_treeDelete";
            this.toolStripMenuItem_treeDelete.ShortcutKeyDisplayString = "Del";
            this.toolStripMenuItem_treeDelete.Size = new System.Drawing.Size(189, 22);
            this.toolStripMenuItem_treeDelete.Text = "Delete";
            this.toolStripMenuItem_treeDelete.Click += new System.EventHandler(this.ToolStripMenuItem_treeDelete_Click);
            // 
            // splitContainer_description
            // 
            this.splitContainer_description.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer_description.Location = new System.Drawing.Point(0, 0);
            this.splitContainer_description.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.splitContainer_description.Name = "splitContainer_description";
            this.splitContainer_description.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer_description.Panel1
            // 
            this.splitContainer_description.Panel1.Controls.Add(this.splitContainer_fileList);
            this.splitContainer_description.Panel1.Controls.Add(this.textBox_ExSearchHistory);
            this.splitContainer_description.Panel1.Controls.Add(this.comboBox_ExVersions);
            this.splitContainer_description.Panel1.Controls.Add(this.button_ExAdjustRows);
            this.splitContainer_description.Panel1.Controls.Add(this.comboBox_ExCondition);
            this.splitContainer_description.Panel1.Controls.Add(this.textBox_ExSearchString);
            this.splitContainer_description.Panel1.Controls.Add(this.checkBox_ExCaseSensitive);
            this.splitContainer_description.Panel1.Controls.Add(this.button_ExClearSearch);
            // 
            // splitContainer_description.Panel2
            // 
            this.splitContainer_description.Panel2.Controls.Add(this.label_edit);
            this.splitContainer_description.Panel2.Controls.Add(this.label_descSave);
            this.splitContainer_description.Panel2.Controls.Add(this.textBox_description);
            this.splitContainer_description.Size = new System.Drawing.Size(675, 593);
            this.splitContainer_description.SplitterDistance = 498;
            this.splitContainer_description.SplitterWidth = 5;
            this.splitContainer_description.TabIndex = 9;
            // 
            // splitContainer_fileList
            // 
            this.splitContainer_fileList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer_fileList.Location = new System.Drawing.Point(0, 37);
            this.splitContainer_fileList.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.splitContainer_fileList.Name = "splitContainer_fileList";
            // 
            // splitContainer_fileList.Panel1
            // 
            this.splitContainer_fileList.Panel1.Controls.Add(this.dataGridView_examples);
            // 
            // splitContainer_fileList.Panel2
            // 
            this.splitContainer_fileList.Panel2.Controls.Add(this.listBox_fileList);
            this.splitContainer_fileList.Size = new System.Drawing.Size(675, 427);
            this.splitContainer_fileList.SplitterDistance = 458;
            this.splitContainer_fileList.SplitterWidth = 5;
            this.splitContainer_fileList.TabIndex = 8;
            // 
            // dataGridView_examples
            // 
            this.dataGridView_examples.AllowUserToAddRows = false;
            this.dataGridView_examples.AllowUserToDeleteRows = false;
            this.dataGridView_examples.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dataGridView_examples.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dataGridView_examples.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_examples.ContextMenuStrip = this.contextMenuStrip_samples;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView_examples.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView_examples.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_examples.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridView_examples.Location = new System.Drawing.Point(0, 0);
            this.dataGridView_examples.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.dataGridView_examples.Name = "dataGridView_examples";
            this.dataGridView_examples.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders;
            dataGridViewCellStyle2.NullValue = "Adjust";
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView_examples.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView_examples.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            this.dataGridView_examples.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView_examples.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView_examples.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridView_examples.Size = new System.Drawing.Size(458, 427);
            this.dataGridView_examples.TabIndex = 5;
            this.dataGridView_examples.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView_examples_CellDoubleClick);
            this.dataGridView_examples.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DataGridView_CellMouseDown);
            this.dataGridView_examples.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView_RowEnter);
            this.dataGridView_examples.RowHeaderMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DataGridView_RowHeaderMouseDoubleClick);
            this.dataGridView_examples.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DataGridView_examples_KeyDown);
            // 
            // contextMenuStrip_samples
            // 
            this.contextMenuStrip_samples.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_SampleOpen,
            this.toolStripMenuItem_SampleDelete,
            this.toolStripMenuItem_SampleCopy});
            this.contextMenuStrip_samples.Name = "contextMenuStrip_samples";
            this.contextMenuStrip_samples.Size = new System.Drawing.Size(163, 70);
            // 
            // toolStripMenuItem_SampleOpen
            // 
            this.toolStripMenuItem_SampleOpen.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.toolStripMenuItem_SampleOpen.Name = "toolStripMenuItem_SampleOpen";
            this.toolStripMenuItem_SampleOpen.ShortcutKeyDisplayString = "Enter";
            this.toolStripMenuItem_SampleOpen.Size = new System.Drawing.Size(162, 22);
            this.toolStripMenuItem_SampleOpen.Text = "Open file";
            this.toolStripMenuItem_SampleOpen.Click += new System.EventHandler(this.ToolStripMenuItem_SampleOpen_Click);
            // 
            // toolStripMenuItem_SampleDelete
            // 
            this.toolStripMenuItem_SampleDelete.Name = "toolStripMenuItem_SampleDelete";
            this.toolStripMenuItem_SampleDelete.ShortcutKeyDisplayString = "Del";
            this.toolStripMenuItem_SampleDelete.Size = new System.Drawing.Size(162, 22);
            this.toolStripMenuItem_SampleDelete.Text = "Delete";
            this.toolStripMenuItem_SampleDelete.Visible = false;
            this.toolStripMenuItem_SampleDelete.Click += new System.EventHandler(this.ToolStripMenuItem_SampleDelete_Click);
            // 
            // toolStripMenuItem_SampleCopy
            // 
            this.toolStripMenuItem_SampleCopy.Name = "toolStripMenuItem_SampleCopy";
            this.toolStripMenuItem_SampleCopy.ShortcutKeyDisplayString = "Ctrl-C";
            this.toolStripMenuItem_SampleCopy.Size = new System.Drawing.Size(162, 22);
            this.toolStripMenuItem_SampleCopy.Text = "Copy";
            this.toolStripMenuItem_SampleCopy.Click += new System.EventHandler(this.ToolStripMenuItem_SampleCopy_Click);
            // 
            // listBox_fileList
            // 
            this.listBox_fileList.ContextMenuStrip = this.contextMenuStrip_fileList;
            this.listBox_fileList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox_fileList.FormattingEnabled = true;
            this.listBox_fileList.HorizontalScrollbar = true;
            this.listBox_fileList.ItemHeight = 15;
            this.listBox_fileList.Location = new System.Drawing.Point(0, 0);
            this.listBox_fileList.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.listBox_fileList.Name = "listBox_fileList";
            this.listBox_fileList.Size = new System.Drawing.Size(212, 427);
            this.listBox_fileList.TabIndex = 0;
            this.listBox_fileList.SelectedValueChanged += new System.EventHandler(this.ListBox_fileList_SelectedValueChanged);
            this.listBox_fileList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ListBox_fileList_KeyDown);
            this.listBox_fileList.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ListBox_fileList_MouseDoubleClick);
            // 
            // contextMenuStrip_fileList
            // 
            this.contextMenuStrip_fileList.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_openFile,
            this.toolStripMenuItem_openFolder,
            this.toolStripMenuItem_FileDelete,
            this.toolStripMenuItem_copy});
            this.contextMenuStrip_fileList.Name = "contextMenuStrip_fileList";
            this.contextMenuStrip_fileList.Size = new System.Drawing.Size(173, 92);
            // 
            // toolStripMenuItem_openFile
            // 
            this.toolStripMenuItem_openFile.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.toolStripMenuItem_openFile.Name = "toolStripMenuItem_openFile";
            this.toolStripMenuItem_openFile.ShortcutKeyDisplayString = "Enter";
            this.toolStripMenuItem_openFile.Size = new System.Drawing.Size(172, 22);
            this.toolStripMenuItem_openFile.Text = "Open file";
            this.toolStripMenuItem_openFile.Click += new System.EventHandler(this.ToolStripMenuItem_openFile_Click);
            // 
            // toolStripMenuItem_openFolder
            // 
            this.toolStripMenuItem_openFolder.Name = "toolStripMenuItem_openFolder";
            this.toolStripMenuItem_openFolder.Size = new System.Drawing.Size(172, 22);
            this.toolStripMenuItem_openFolder.Text = "Open folder";
            this.toolStripMenuItem_openFolder.Click += new System.EventHandler(this.ToolStripMenuItem_openFolder_Click);
            // 
            // toolStripMenuItem_FileDelete
            // 
            this.toolStripMenuItem_FileDelete.Name = "toolStripMenuItem_FileDelete";
            this.toolStripMenuItem_FileDelete.ShortcutKeyDisplayString = "Del";
            this.toolStripMenuItem_FileDelete.Size = new System.Drawing.Size(172, 22);
            this.toolStripMenuItem_FileDelete.Text = "Delete sample";
            this.toolStripMenuItem_FileDelete.Visible = false;
            this.toolStripMenuItem_FileDelete.Click += new System.EventHandler(this.ToolStripMenuItem_FileDelete_Click);
            // 
            // toolStripMenuItem_copy
            // 
            this.toolStripMenuItem_copy.Name = "toolStripMenuItem_copy";
            this.toolStripMenuItem_copy.ShortcutKeyDisplayString = "Ctrl-C";
            this.toolStripMenuItem_copy.Size = new System.Drawing.Size(172, 22);
            this.toolStripMenuItem_copy.Text = "Copy";
            this.toolStripMenuItem_copy.Click += new System.EventHandler(this.ToolStripMenuItem_copy_Click);
            // 
            // textBox_ExSearchHistory
            // 
            this.textBox_ExSearchHistory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_ExSearchHistory.Location = new System.Drawing.Point(4, 470);
            this.textBox_ExSearchHistory.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.textBox_ExSearchHistory.Name = "textBox_ExSearchHistory";
            this.textBox_ExSearchHistory.ReadOnly = true;
            this.textBox_ExSearchHistory.Size = new System.Drawing.Size(573, 23);
            this.textBox_ExSearchHistory.TabIndex = 6;
            // 
            // comboBox_ExVersions
            // 
            this.comboBox_ExVersions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_ExVersions.FormattingEnabled = true;
            this.comboBox_ExVersions.Items.AddRange(new object[] {
            "Any"});
            this.comboBox_ExVersions.Location = new System.Drawing.Point(4, 6);
            this.comboBox_ExVersions.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.comboBox_ExVersions.Name = "comboBox_ExVersions";
            this.comboBox_ExVersions.Size = new System.Drawing.Size(70, 23);
            this.comboBox_ExVersions.TabIndex = 0;
            this.comboBox_ExVersions.SelectedIndexChanged += new System.EventHandler(this.ComboBox_ExVersions_SelectedIndexChanged);
            // 
            // button_ExAdjustRows
            // 
            this.button_ExAdjustRows.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_ExAdjustRows.Location = new System.Drawing.Point(584, 3);
            this.button_ExAdjustRows.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.button_ExAdjustRows.Name = "button_ExAdjustRows";
            this.button_ExAdjustRows.Size = new System.Drawing.Size(88, 27);
            this.button_ExAdjustRows.TabIndex = 4;
            this.button_ExAdjustRows.Text = "Adjust rows";
            this.button_ExAdjustRows.UseVisualStyleBackColor = true;
            this.button_ExAdjustRows.Click += new System.EventHandler(this.Button_ExAdjustRows_Click);
            // 
            // comboBox_ExCondition
            // 
            this.comboBox_ExCondition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_ExCondition.FormattingEnabled = true;
            this.comboBox_ExCondition.Location = new System.Drawing.Point(82, 6);
            this.comboBox_ExCondition.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.comboBox_ExCondition.Name = "comboBox_ExCondition";
            this.comboBox_ExCondition.Size = new System.Drawing.Size(81, 23);
            this.comboBox_ExCondition.TabIndex = 1;
            // 
            // textBox_ExSearchString
            // 
            this.textBox_ExSearchString.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_ExSearchString.Location = new System.Drawing.Point(170, 6);
            this.textBox_ExSearchString.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.textBox_ExSearchString.Name = "textBox_ExSearchString";
            this.textBox_ExSearchString.Size = new System.Drawing.Size(290, 23);
            this.textBox_ExSearchString.TabIndex = 2;
            this.textBox_ExSearchString.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBox_ExSearchString_KeyDown);
            // 
            // checkBox_ExCaseSensitive
            // 
            this.checkBox_ExCaseSensitive.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox_ExCaseSensitive.AutoSize = true;
            this.checkBox_ExCaseSensitive.Location = new System.Drawing.Point(478, 8);
            this.checkBox_ExCaseSensitive.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.checkBox_ExCaseSensitive.Name = "checkBox_ExCaseSensitive";
            this.checkBox_ExCaseSensitive.Size = new System.Drawing.Size(99, 19);
            this.checkBox_ExCaseSensitive.TabIndex = 3;
            this.checkBox_ExCaseSensitive.Text = "Case sensitive";
            this.checkBox_ExCaseSensitive.UseVisualStyleBackColor = true;
            // 
            // button_ExClearSearch
            // 
            this.button_ExClearSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_ExClearSearch.Location = new System.Drawing.Point(584, 468);
            this.button_ExClearSearch.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.button_ExClearSearch.Name = "button_ExClearSearch";
            this.button_ExClearSearch.Size = new System.Drawing.Size(88, 27);
            this.button_ExClearSearch.TabIndex = 7;
            this.button_ExClearSearch.Text = "Clear";
            this.button_ExClearSearch.UseVisualStyleBackColor = true;
            this.button_ExClearSearch.Click += new System.EventHandler(this.Button_ExClearSearch_Click);
            // 
            // label_edit
            // 
            this.label_edit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label_edit.AutoSize = true;
            this.label_edit.Location = new System.Drawing.Point(564, 52);
            this.label_edit.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_edit.Name = "label_edit";
            this.label_edit.Size = new System.Drawing.Size(111, 15);
            this.label_edit.TabIndex = 9;
            this.label_edit.Text = "Double-click to edit";
            // 
            // label_descSave
            // 
            this.label_descSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label_descSave.AutoSize = true;
            this.label_descSave.Location = new System.Drawing.Point(500, 52);
            this.label_descSave.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_descSave.Name = "label_descSave";
            this.label_descSave.Size = new System.Drawing.Size(175, 15);
            this.label_descSave.TabIndex = 9;
            this.label_descSave.Text = "Ctrl-Enter to save, ESC to cancel";
            this.label_descSave.Visible = false;
            // 
            // textBox_description
            // 
            this.textBox_description.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox_description.Location = new System.Drawing.Point(0, 0);
            this.textBox_description.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.textBox_description.Multiline = true;
            this.textBox_description.Name = "textBox_description";
            this.textBox_description.ReadOnly = true;
            this.textBox_description.Size = new System.Drawing.Size(675, 90);
            this.textBox_description.TabIndex = 8;
            this.textBox_description.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBox_description_KeyDown);
            this.textBox_description.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.TextBox_description_MouseDoubleClick);
            // 
            // tabPage_Schema
            // 
            this.tabPage_Schema.AutoScroll = true;
            this.tabPage_Schema.Controls.Add(this.textBox_schemaUrl);
            this.tabPage_Schema.Controls.Add(this.button_regenerateSchema);
            this.tabPage_Schema.Controls.Add(this.label2);
            this.tabPage_Schema.Controls.Add(this.label1);
            this.tabPage_Schema.Controls.Add(this.comboBox_contentVersion);
            this.tabPage_Schema.Controls.Add(this.comboBox_fileType);
            this.tabPage_Schema.Controls.Add(this.splitContainer_schemaMain);
            this.tabPage_Schema.Location = new System.Drawing.Point(4, 24);
            this.tabPage_Schema.Name = "tabPage_Schema";
            this.tabPage_Schema.Size = new System.Drawing.Size(907, 593);
            this.tabPage_Schema.TabIndex = 2;
            this.tabPage_Schema.Text = "Schema";
            this.tabPage_Schema.UseVisualStyleBackColor = true;
            // 
            // textBox_schemaUrl
            // 
            this.textBox_schemaUrl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_schemaUrl.Location = new System.Drawing.Point(411, 3);
            this.textBox_schemaUrl.Name = "textBox_schemaUrl";
            this.textBox_schemaUrl.Size = new System.Drawing.Size(358, 23);
            this.textBox_schemaUrl.TabIndex = 6;
            // 
            // button_regenerateSchema
            // 
            this.button_regenerateSchema.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_regenerateSchema.Location = new System.Drawing.Point(775, 2);
            this.button_regenerateSchema.Name = "button_regenerateSchema";
            this.button_regenerateSchema.Size = new System.Drawing.Size(129, 23);
            this.button_regenerateSchema.TabIndex = 5;
            this.button_regenerateSchema.Text = "Regenerate schema";
            this.button_regenerateSchema.UseVisualStyleBackColor = true;
            this.button_regenerateSchema.Click += new System.EventHandler(this.Button_regenerateSchema_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(187, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 15);
            this.label2.TabIndex = 4;
            this.label2.Text = "Content version";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 15);
            this.label1.TabIndex = 4;
            this.label1.Text = "File type";
            // 
            // comboBox_contentVersion
            // 
            this.comboBox_contentVersion.FormattingEnabled = true;
            this.comboBox_contentVersion.Location = new System.Drawing.Point(284, 3);
            this.comboBox_contentVersion.Name = "comboBox_contentVersion";
            this.comboBox_contentVersion.Size = new System.Drawing.Size(121, 23);
            this.comboBox_contentVersion.TabIndex = 2;
            this.comboBox_contentVersion.SelectedIndexChanged += new System.EventHandler(this.ComboBox_contentVersion_SelectedIndexChanged);
            // 
            // comboBox_fileType
            // 
            this.comboBox_fileType.FormattingEnabled = true;
            this.comboBox_fileType.Location = new System.Drawing.Point(60, 3);
            this.comboBox_fileType.Name = "comboBox_fileType";
            this.comboBox_fileType.Size = new System.Drawing.Size(121, 23);
            this.comboBox_fileType.TabIndex = 1;
            this.comboBox_fileType.SelectedIndexChanged += new System.EventHandler(this.ComboBox_fileType_SelectedIndexChanged);
            // 
            // splitContainer_schemaMain
            // 
            this.splitContainer_schemaMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer_schemaMain.Location = new System.Drawing.Point(0, 32);
            this.splitContainer_schemaMain.Name = "splitContainer_schemaMain";
            // 
            // splitContainer_schemaMain.Panel1
            // 
            this.splitContainer_schemaMain.Panel1.Controls.Add(this.splitContainer_schemaLeft);
            // 
            // splitContainer_schemaMain.Panel2
            // 
            this.splitContainer_schemaMain.Panel2.Controls.Add(this.splitContainer_schemaRight);
            this.splitContainer_schemaMain.Size = new System.Drawing.Size(907, 561);
            this.splitContainer_schemaMain.SplitterDistance = 449;
            this.splitContainer_schemaMain.TabIndex = 0;
            // 
            // splitContainer_schemaLeft
            // 
            this.splitContainer_schemaLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer_schemaLeft.Location = new System.Drawing.Point(0, 0);
            this.splitContainer_schemaLeft.Name = "splitContainer_schemaLeft";
            this.splitContainer_schemaLeft.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer_schemaLeft.Panel1
            // 
            this.splitContainer_schemaLeft.Panel1.Controls.Add(this.button_saveLeftSchema);
            this.splitContainer_schemaLeft.Panel1.Controls.Add(this.button_generateSchema);
            this.splitContainer_schemaLeft.Panel1.Controls.Add(this.treeView_leftSchema);
            this.splitContainer_schemaLeft.Size = new System.Drawing.Size(449, 561);
            this.splitContainer_schemaLeft.SplitterDistance = 219;
            this.splitContainer_schemaLeft.TabIndex = 0;
            this.splitContainer_schemaLeft.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.SplitContainer_schemaLeft_SplitterMoved);
            // 
            // button_generateSchema
            // 
            this.button_generateSchema.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_generateSchema.Location = new System.Drawing.Point(338, 3);
            this.button_generateSchema.Name = "button_generateSchema";
            this.button_generateSchema.Size = new System.Drawing.Size(108, 23);
            this.button_generateSchema.TabIndex = 1;
            this.button_generateSchema.Text = "Generate schema";
            this.button_generateSchema.UseVisualStyleBackColor = true;
            this.button_generateSchema.Click += new System.EventHandler(this.Button_generateSchema_Click);
            // 
            // treeView_leftSchema
            // 
            this.treeView_leftSchema.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView_leftSchema.Location = new System.Drawing.Point(0, 0);
            this.treeView_leftSchema.Name = "treeView_leftSchema";
            this.treeView_leftSchema.Size = new System.Drawing.Size(449, 219);
            this.treeView_leftSchema.TabIndex = 0;
            this.treeView_leftSchema.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TreeView_leftSchema_AfterSelect);
            // 
            // splitContainer_schemaRight
            // 
            this.splitContainer_schemaRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer_schemaRight.Location = new System.Drawing.Point(0, 0);
            this.splitContainer_schemaRight.Name = "splitContainer_schemaRight";
            this.splitContainer_schemaRight.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer_schemaRight.Panel1
            // 
            this.splitContainer_schemaRight.Panel1.AutoScroll = true;
            this.splitContainer_schemaRight.Panel1.Controls.Add(this.button_saveRightSchema);
            this.splitContainer_schemaRight.Panel1.Controls.Add(this.button_loadSchema);
            this.splitContainer_schemaRight.Panel1.Controls.Add(this.treeView_rightSchema);
            // 
            // splitContainer_schemaRight.Panel2
            // 
            this.splitContainer_schemaRight.Panel2.AutoScroll = true;
            this.splitContainer_schemaRight.Size = new System.Drawing.Size(454, 561);
            this.splitContainer_schemaRight.SplitterDistance = 224;
            this.splitContainer_schemaRight.TabIndex = 0;
            this.splitContainer_schemaRight.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.SplitContainer_schemaRight_SplitterMoved);
            // 
            // button_loadSchema
            // 
            this.button_loadSchema.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_loadSchema.Location = new System.Drawing.Point(355, 3);
            this.button_loadSchema.Name = "button_loadSchema";
            this.button_loadSchema.Size = new System.Drawing.Size(96, 23);
            this.button_loadSchema.TabIndex = 7;
            this.button_loadSchema.Text = "Load schema";
            this.button_loadSchema.UseVisualStyleBackColor = true;
            this.button_loadSchema.Click += new System.EventHandler(this.Button_loadSchema_Click);
            // 
            // treeView_rightSchema
            // 
            this.treeView_rightSchema.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView_rightSchema.Location = new System.Drawing.Point(0, 0);
            this.treeView_rightSchema.Name = "treeView_rightSchema";
            this.treeView_rightSchema.Size = new System.Drawing.Size(454, 224);
            this.treeView_rightSchema.TabIndex = 0;
            this.treeView_rightSchema.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TreeView_RightSchema_AfterSelect);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.OpenFileDialog1_FileOk);
            // 
            // folderBrowserDialog1
            // 
            this.folderBrowserDialog1.Description = "Select root folder";
            this.folderBrowserDialog1.RootFolder = System.Environment.SpecialFolder.MyComputer;
            this.folderBrowserDialog1.ShowNewFolderButton = false;
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.SaveFileDialog1_FileOk);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 625);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
            this.statusStrip1.Size = new System.Drawing.Size(915, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // button_saveLeftSchema
            // 
            this.button_saveLeftSchema.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_saveLeftSchema.Location = new System.Drawing.Point(338, 32);
            this.button_saveLeftSchema.Name = "button_saveLeftSchema";
            this.button_saveLeftSchema.Size = new System.Drawing.Size(108, 23);
            this.button_saveLeftSchema.TabIndex = 1;
            this.button_saveLeftSchema.Text = "Save schema";
            this.button_saveLeftSchema.UseVisualStyleBackColor = true;
            this.button_saveLeftSchema.Click += new System.EventHandler(this.Button_saveLeftSchema_Click);
            // 
            // button_saveRightSchema
            // 
            this.button_saveRightSchema.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_saveRightSchema.Location = new System.Drawing.Point(355, 32);
            this.button_saveRightSchema.Name = "button_saveRightSchema";
            this.button_saveRightSchema.Size = new System.Drawing.Size(96, 23);
            this.button_saveRightSchema.TabIndex = 1;
            this.button_saveRightSchema.Text = "Save schema";
            this.button_saveRightSchema.UseVisualStyleBackColor = true;
            this.button_saveRightSchema.Click += new System.EventHandler(this.Button_saveRightSchema_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(915, 647);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MinimumSize = new System.Drawing.Size(697, 456);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "JsonDictionary";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.tabControl1.ResumeLayout(false);
            this.tabPage_DataCollection.ResumeLayout(false);
            this.splitContainer_buttons.Panel1.ResumeLayout(false);
            this.splitContainer_buttons.Panel1.PerformLayout();
            this.splitContainer_buttons.Panel2.ResumeLayout(false);
            this.splitContainer_buttons.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_buttons)).EndInit();
            this.splitContainer_buttons.ResumeLayout(false);
            this.tabPage_SamplesTree.ResumeLayout(false);
            this.splitContainer_tree.Panel1.ResumeLayout(false);
            this.splitContainer_tree.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_tree)).EndInit();
            this.splitContainer_tree.ResumeLayout(false);
            this.contextMenuStrip_tree.ResumeLayout(false);
            this.splitContainer_description.Panel1.ResumeLayout(false);
            this.splitContainer_description.Panel1.PerformLayout();
            this.splitContainer_description.Panel2.ResumeLayout(false);
            this.splitContainer_description.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_description)).EndInit();
            this.splitContainer_description.ResumeLayout(false);
            this.splitContainer_fileList.Panel1.ResumeLayout(false);
            this.splitContainer_fileList.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_fileList)).EndInit();
            this.splitContainer_fileList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_examples)).EndInit();
            this.contextMenuStrip_samples.ResumeLayout(false);
            this.contextMenuStrip_fileList.ResumeLayout(false);
            this.tabPage_Schema.ResumeLayout(false);
            this.tabPage_Schema.PerformLayout();
            this.splitContainer_schemaMain.Panel1.ResumeLayout(false);
            this.splitContainer_schemaMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_schemaMain)).EndInit();
            this.splitContainer_schemaMain.ResumeLayout(false);
            this.splitContainer_schemaLeft.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_schemaLeft)).EndInit();
            this.splitContainer_schemaLeft.ResumeLayout(false);
            this.splitContainer_schemaRight.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_schemaRight)).EndInit();
            this.splitContainer_schemaRight.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

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
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox_contentVersion;
        private System.Windows.Forms.ComboBox comboBox_fileType;
        private System.Windows.Forms.Button button_regenerateSchema;
        private System.Windows.Forms.TextBox textBox_schemaUrl;
        private System.Windows.Forms.Button button_loadSchema;
        private System.Windows.Forms.Button button_generateSchema;
        private System.Windows.Forms.CheckBox checkBox_schemaSelectionSync;
        private System.Windows.Forms.Button button_saveLeftSchema;
        private System.Windows.Forms.Button button_saveRightSchema;
    }
}

