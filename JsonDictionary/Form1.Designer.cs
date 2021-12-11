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
            this.button_compareNode = new System.Windows.Forms.Button();
            this.button_clearCompare = new System.Windows.Forms.Button();
            this.checkBox_selectedSchema = new System.Windows.Forms.CheckBox();
            this.checkBox_deepCompare = new System.Windows.Forms.CheckBox();
            this.button_compare = new System.Windows.Forms.Button();
            this.textBox_find = new System.Windows.Forms.TextBox();
            this.button_findPrev = new System.Windows.Forms.Button();
            this.button_findNext = new System.Windows.Forms.Button();
            this.checkBox_schemaSelectionSync = new System.Windows.Forms.CheckBox();
            this.splitContainer_schemaMain = new System.Windows.Forms.SplitContainer();
            this.splitContainer_schemaLeft = new System.Windows.Forms.SplitContainer();
            this.button_saveLeftSchema = new System.Windows.Forms.Button();
            this.button_generateSchema = new System.Windows.Forms.Button();
            this.treeView_leftSchema = new System.Windows.Forms.TreeView();
            this.contextMenuStrip_leftSchema = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem_unfoldLS = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_foldLS = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_copyLS = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_addLS = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_renameLS = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_deleteLS = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer_schemaRight = new System.Windows.Forms.SplitContainer();
            this.button_saveRightSchema = new System.Windows.Forms.Button();
            this.button_loadSchema = new System.Windows.Forms.Button();
            this.treeView_rightSchema = new System.Windows.Forms.TreeView();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.contextMenuStrip_rightSchema = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem_unfoldRS = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_foldRS = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_copyRS = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_addRS = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_renameRS = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_deleteRS = new System.Windows.Forms.ToolStripMenuItem();
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
            this.contextMenuStrip_leftSchema.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_schemaRight)).BeginInit();
            this.splitContainer_schemaRight.Panel1.SuspendLayout();
            this.splitContainer_schemaRight.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.contextMenuStrip_rightSchema.SuspendLayout();
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
            this.toolStripMenuItem_treeCopy.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.toolStripMenuItem_treeCopy.Size = new System.Drawing.Size(189, 22);
            this.toolStripMenuItem_treeCopy.Text = "Copy name";
            this.toolStripMenuItem_treeCopy.Click += new System.EventHandler(this.ToolStripMenuItem_treeCopy_Click);
            // 
            // toolStripMenuItem_treeDelete
            // 
            this.toolStripMenuItem_treeDelete.Name = "toolStripMenuItem_treeDelete";
            this.toolStripMenuItem_treeDelete.ShortcutKeyDisplayString = "Del";
            this.toolStripMenuItem_treeDelete.ShortcutKeys = System.Windows.Forms.Keys.Delete;
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
            this.contextMenuStrip_samples.Size = new System.Drawing.Size(175, 70);
            // 
            // toolStripMenuItem_SampleOpen
            // 
            this.toolStripMenuItem_SampleOpen.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.toolStripMenuItem_SampleOpen.Name = "toolStripMenuItem_SampleOpen";
            this.toolStripMenuItem_SampleOpen.ShortcutKeyDisplayString = "Enter";
            this.toolStripMenuItem_SampleOpen.Size = new System.Drawing.Size(174, 22);
            this.toolStripMenuItem_SampleOpen.Text = "Open file";
            this.toolStripMenuItem_SampleOpen.Click += new System.EventHandler(this.ToolStripMenuItem_SampleOpen_Click);
            // 
            // toolStripMenuItem_SampleDelete
            // 
            this.toolStripMenuItem_SampleDelete.Name = "toolStripMenuItem_SampleDelete";
            this.toolStripMenuItem_SampleDelete.ShortcutKeyDisplayString = "Del";
            this.toolStripMenuItem_SampleDelete.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.toolStripMenuItem_SampleDelete.Size = new System.Drawing.Size(174, 22);
            this.toolStripMenuItem_SampleDelete.Text = "Delete";
            this.toolStripMenuItem_SampleDelete.Visible = false;
            this.toolStripMenuItem_SampleDelete.Click += new System.EventHandler(this.ToolStripMenuItem_SampleDelete_Click);
            // 
            // toolStripMenuItem_SampleCopy
            // 
            this.toolStripMenuItem_SampleCopy.Name = "toolStripMenuItem_SampleCopy";
            this.toolStripMenuItem_SampleCopy.ShortcutKeyDisplayString = "Ctrl-C";
            this.toolStripMenuItem_SampleCopy.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.toolStripMenuItem_SampleCopy.Size = new System.Drawing.Size(174, 22);
            this.toolStripMenuItem_SampleCopy.Text = "Copy name";
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
            this.contextMenuStrip_fileList.Size = new System.Drawing.Size(175, 92);
            // 
            // toolStripMenuItem_openFile
            // 
            this.toolStripMenuItem_openFile.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.toolStripMenuItem_openFile.Name = "toolStripMenuItem_openFile";
            this.toolStripMenuItem_openFile.ShortcutKeyDisplayString = "Enter";
            this.toolStripMenuItem_openFile.Size = new System.Drawing.Size(174, 22);
            this.toolStripMenuItem_openFile.Text = "Open file";
            this.toolStripMenuItem_openFile.Click += new System.EventHandler(this.ToolStripMenuItem_openFile_Click);
            // 
            // toolStripMenuItem_openFolder
            // 
            this.toolStripMenuItem_openFolder.Name = "toolStripMenuItem_openFolder";
            this.toolStripMenuItem_openFolder.Size = new System.Drawing.Size(174, 22);
            this.toolStripMenuItem_openFolder.Text = "Open folder";
            this.toolStripMenuItem_openFolder.Click += new System.EventHandler(this.ToolStripMenuItem_openFolder_Click);
            // 
            // toolStripMenuItem_FileDelete
            // 
            this.toolStripMenuItem_FileDelete.Name = "toolStripMenuItem_FileDelete";
            this.toolStripMenuItem_FileDelete.ShortcutKeyDisplayString = "Del";
            this.toolStripMenuItem_FileDelete.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.toolStripMenuItem_FileDelete.Size = new System.Drawing.Size(174, 22);
            this.toolStripMenuItem_FileDelete.Text = "Delete sample";
            this.toolStripMenuItem_FileDelete.Visible = false;
            this.toolStripMenuItem_FileDelete.Click += new System.EventHandler(this.ToolStripMenuItem_FileDelete_Click);
            // 
            // toolStripMenuItem_copy
            // 
            this.toolStripMenuItem_copy.Name = "toolStripMenuItem_copy";
            this.toolStripMenuItem_copy.ShortcutKeyDisplayString = "Ctrl-C";
            this.toolStripMenuItem_copy.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.toolStripMenuItem_copy.Size = new System.Drawing.Size(174, 22);
            this.toolStripMenuItem_copy.Text = "Copy name";
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
            this.textBox_ExSearchHistory.Size = new System.Drawing.Size(616, 23);
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
            this.button_ExAdjustRows.AutoSize = true;
            this.button_ExAdjustRows.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.button_ExAdjustRows.Location = new System.Drawing.Point(593, 3);
            this.button_ExAdjustRows.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.button_ExAdjustRows.Name = "button_ExAdjustRows";
            this.button_ExAdjustRows.Size = new System.Drawing.Size(79, 25);
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
            this.checkBox_ExCaseSensitive.Location = new System.Drawing.Point(486, 8);
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
            this.button_ExClearSearch.AutoSize = true;
            this.button_ExClearSearch.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.button_ExClearSearch.Location = new System.Drawing.Point(628, 470);
            this.button_ExClearSearch.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.button_ExClearSearch.Name = "button_ExClearSearch";
            this.button_ExClearSearch.Size = new System.Drawing.Size(44, 25);
            this.button_ExClearSearch.TabIndex = 7;
            this.button_ExClearSearch.Text = "Clear";
            this.button_ExClearSearch.UseVisualStyleBackColor = true;
            this.button_ExClearSearch.Click += new System.EventHandler(this.Button_ExClearSearch_Click);
            // 
            // label_edit
            // 
            this.label_edit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label_edit.AutoSize = true;
            this.label_edit.Location = new System.Drawing.Point(542, 50);
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
            this.label_descSave.Location = new System.Drawing.Point(478, 50);
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
            this.tabPage_Schema.Controls.Add(this.button_compareNode);
            this.tabPage_Schema.Controls.Add(this.button_clearCompare);
            this.tabPage_Schema.Controls.Add(this.checkBox_selectedSchema);
            this.tabPage_Schema.Controls.Add(this.checkBox_deepCompare);
            this.tabPage_Schema.Controls.Add(this.button_compare);
            this.tabPage_Schema.Controls.Add(this.textBox_find);
            this.tabPage_Schema.Controls.Add(this.button_findPrev);
            this.tabPage_Schema.Controls.Add(this.button_findNext);
            this.tabPage_Schema.Controls.Add(this.checkBox_schemaSelectionSync);
            this.tabPage_Schema.Controls.Add(this.splitContainer_schemaMain);
            this.tabPage_Schema.Location = new System.Drawing.Point(4, 24);
            this.tabPage_Schema.Name = "tabPage_Schema";
            this.tabPage_Schema.Size = new System.Drawing.Size(907, 593);
            this.tabPage_Schema.TabIndex = 2;
            this.tabPage_Schema.Text = "Schema";
            this.tabPage_Schema.UseVisualStyleBackColor = true;
            // 
            // button_compareNode
            // 
            this.button_compareNode.AutoSize = true;
            this.button_compareNode.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.button_compareNode.Location = new System.Drawing.Point(299, 4);
            this.button_compareNode.Name = "button_compareNode";
            this.button_compareNode.Size = new System.Drawing.Size(96, 25);
            this.button_compareNode.TabIndex = 17;
            this.button_compareNode.Text = "Node compare";
            this.button_compareNode.UseVisualStyleBackColor = true;
            this.button_compareNode.Click += new System.EventHandler(this.Button_compareNode_Click);
            // 
            // button_clearCompare
            // 
            this.button_clearCompare.AutoSize = true;
            this.button_clearCompare.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.button_clearCompare.Location = new System.Drawing.Point(183, 4);
            this.button_clearCompare.Name = "button_clearCompare";
            this.button_clearCompare.Size = new System.Drawing.Size(110, 25);
            this.button_clearCompare.TabIndex = 16;
            this.button_clearCompare.Text = "Clear comparison";
            this.button_clearCompare.UseVisualStyleBackColor = true;
            this.button_clearCompare.Click += new System.EventHandler(this.Button_clearCompare_Click);
            // 
            // checkBox_selectedSchema
            // 
            this.checkBox_selectedSchema.AutoSize = true;
            this.checkBox_selectedSchema.Location = new System.Drawing.Point(75, 15);
            this.checkBox_selectedSchema.Name = "checkBox_selectedSchema";
            this.checkBox_selectedSchema.Size = new System.Drawing.Size(69, 19);
            this.checkBox_selectedSchema.TabIndex = 15;
            this.checkBox_selectedSchema.Text = "selected";
            this.checkBox_selectedSchema.UseVisualStyleBackColor = true;
            // 
            // checkBox_deepCompare
            // 
            this.checkBox_deepCompare.AutoSize = true;
            this.checkBox_deepCompare.Location = new System.Drawing.Point(75, 0);
            this.checkBox_deepCompare.Name = "checkBox_deepCompare";
            this.checkBox_deepCompare.Size = new System.Drawing.Size(102, 19);
            this.checkBox_deepCompare.TabIndex = 15;
            this.checkBox_deepCompare.Text = "deep compare";
            this.checkBox_deepCompare.UseVisualStyleBackColor = true;
            // 
            // button_compare
            // 
            this.button_compare.AutoSize = true;
            this.button_compare.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.button_compare.Location = new System.Drawing.Point(3, 3);
            this.button_compare.Name = "button_compare";
            this.button_compare.Size = new System.Drawing.Size(66, 25);
            this.button_compare.TabIndex = 14;
            this.button_compare.Text = "Compare";
            this.button_compare.UseVisualStyleBackColor = true;
            this.button_compare.Click += new System.EventHandler(this.Button_compare_Click);
            // 
            // textBox_find
            // 
            this.textBox_find.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_find.Location = new System.Drawing.Point(401, 5);
            this.textBox_find.Name = "textBox_find";
            this.textBox_find.Size = new System.Drawing.Size(293, 23);
            this.textBox_find.TabIndex = 13;
            this.textBox_find.Leave += new System.EventHandler(this.TextBox_find_Leave);
            // 
            // button_findPrev
            // 
            this.button_findPrev.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_findPrev.Location = new System.Drawing.Point(700, 4);
            this.button_findPrev.Name = "button_findPrev";
            this.button_findPrev.Size = new System.Drawing.Size(23, 23);
            this.button_findPrev.TabIndex = 12;
            this.button_findPrev.Text = "<";
            this.button_findPrev.UseVisualStyleBackColor = true;
            this.button_findPrev.Click += new System.EventHandler(this.Button_findPrev_Click);
            // 
            // button_findNext
            // 
            this.button_findNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_findNext.Location = new System.Drawing.Point(729, 4);
            this.button_findNext.Name = "button_findNext";
            this.button_findNext.Size = new System.Drawing.Size(23, 23);
            this.button_findNext.TabIndex = 12;
            this.button_findNext.Text = ">";
            this.button_findNext.UseVisualStyleBackColor = true;
            this.button_findNext.Click += new System.EventHandler(this.Button_findNext_Click);
            // 
            // checkBox_schemaSelectionSync
            // 
            this.checkBox_schemaSelectionSync.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox_schemaSelectionSync.AutoSize = true;
            this.checkBox_schemaSelectionSync.Location = new System.Drawing.Point(759, 7);
            this.checkBox_schemaSelectionSync.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.checkBox_schemaSelectionSync.Name = "checkBox_schemaSelectionSync";
            this.checkBox_schemaSelectionSync.Size = new System.Drawing.Size(145, 19);
            this.checkBox_schemaSelectionSync.TabIndex = 11;
            this.checkBox_schemaSelectionSync.Text = "Schema selection sync";
            this.checkBox_schemaSelectionSync.UseVisualStyleBackColor = true;
            this.checkBox_schemaSelectionSync.CheckedChanged += new System.EventHandler(this.CheckBox_schemaSelectionSync_CheckedChanged);
            // 
            // splitContainer_schemaMain
            // 
            this.splitContainer_schemaMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer_schemaMain.Cursor = System.Windows.Forms.Cursors.Default;
            this.splitContainer_schemaMain.Location = new System.Drawing.Point(0, 35);
            this.splitContainer_schemaMain.Name = "splitContainer_schemaMain";
            // 
            // splitContainer_schemaMain.Panel1
            // 
            this.splitContainer_schemaMain.Panel1.Controls.Add(this.splitContainer_schemaLeft);
            // 
            // splitContainer_schemaMain.Panel2
            // 
            this.splitContainer_schemaMain.Panel2.Controls.Add(this.splitContainer_schemaRight);
            this.splitContainer_schemaMain.Size = new System.Drawing.Size(907, 558);
            this.splitContainer_schemaMain.SplitterDistance = 442;
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
            this.splitContainer_schemaLeft.Size = new System.Drawing.Size(442, 558);
            this.splitContainer_schemaLeft.SplitterDistance = 216;
            this.splitContainer_schemaLeft.TabIndex = 0;
            this.splitContainer_schemaLeft.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.SplitContainer_schemaLeft_SplitterMoved);
            // 
            // button_saveLeftSchema
            // 
            this.button_saveLeftSchema.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_saveLeftSchema.AutoSize = true;
            this.button_saveLeftSchema.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.button_saveLeftSchema.Location = new System.Drawing.Point(334, 32);
            this.button_saveLeftSchema.Name = "button_saveLeftSchema";
            this.button_saveLeftSchema.Size = new System.Drawing.Size(85, 25);
            this.button_saveLeftSchema.TabIndex = 1;
            this.button_saveLeftSchema.Text = "Save schema";
            this.button_saveLeftSchema.UseVisualStyleBackColor = true;
            this.button_saveLeftSchema.Click += new System.EventHandler(this.Button_saveLeftSchema_Click);
            // 
            // button_generateSchema
            // 
            this.button_generateSchema.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_generateSchema.AutoSize = true;
            this.button_generateSchema.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.button_generateSchema.Location = new System.Drawing.Point(311, 3);
            this.button_generateSchema.Name = "button_generateSchema";
            this.button_generateSchema.Size = new System.Drawing.Size(108, 25);
            this.button_generateSchema.TabIndex = 1;
            this.button_generateSchema.Text = "Generate schema";
            this.button_generateSchema.UseVisualStyleBackColor = true;
            this.button_generateSchema.Click += new System.EventHandler(this.Button_generateSchema_Click);
            // 
            // treeView_leftSchema
            // 
            this.treeView_leftSchema.ContextMenuStrip = this.contextMenuStrip_leftSchema;
            this.treeView_leftSchema.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView_leftSchema.LabelEdit = true;
            this.treeView_leftSchema.Location = new System.Drawing.Point(0, 0);
            this.treeView_leftSchema.Name = "treeView_leftSchema";
            this.treeView_leftSchema.Size = new System.Drawing.Size(442, 216);
            this.treeView_leftSchema.TabIndex = 0;
            this.treeView_leftSchema.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.TreeView_leftSchema_AfterLabelEdit);
            this.treeView_leftSchema.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TreeView_leftSchema_AfterSelect);
            this.treeView_leftSchema.Enter += new System.EventHandler(this.TreeView_leftSchema_Enter);
            // 
            // contextMenuStrip_leftSchema
            // 
            this.contextMenuStrip_leftSchema.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_unfoldLS,
            this.toolStripMenuItem_foldLS,
            this.toolStripMenuItem_copyLS,
            this.toolStripMenuItem_addLS,
            this.toolStripMenuItem_renameLS,
            this.toolStripMenuItem_deleteLS});
            this.contextMenuStrip_leftSchema.Name = "contextMenuStrip_schema";
            this.contextMenuStrip_leftSchema.Size = new System.Drawing.Size(220, 136);
            // 
            // toolStripMenuItem_unfoldLS
            // 
            this.toolStripMenuItem_unfoldLS.Name = "toolStripMenuItem_unfoldLS";
            this.toolStripMenuItem_unfoldLS.ShortcutKeyDisplayString = "Ctrl+\'+\'";
            this.toolStripMenuItem_unfoldLS.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Oemplus)));
            this.toolStripMenuItem_unfoldLS.Size = new System.Drawing.Size(219, 22);
            this.toolStripMenuItem_unfoldLS.Text = "Unfold all children";
            this.toolStripMenuItem_unfoldLS.Click += new System.EventHandler(this.ToolStripMenuItem_unfoldLS_Click);
            // 
            // toolStripMenuItem_foldLS
            // 
            this.toolStripMenuItem_foldLS.Name = "toolStripMenuItem_foldLS";
            this.toolStripMenuItem_foldLS.ShortcutKeyDisplayString = "Ctrl+\'-\'";
            this.toolStripMenuItem_foldLS.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.OemMinus)));
            this.toolStripMenuItem_foldLS.Size = new System.Drawing.Size(219, 22);
            this.toolStripMenuItem_foldLS.Text = "Fold all children";
            this.toolStripMenuItem_foldLS.Click += new System.EventHandler(this.ToolStripMenuItem_foldLS_Click);
            // 
            // toolStripMenuItem_copyLS
            // 
            this.toolStripMenuItem_copyLS.Name = "toolStripMenuItem_copyLS";
            this.toolStripMenuItem_copyLS.ShortcutKeyDisplayString = "Ctrl+C";
            this.toolStripMenuItem_copyLS.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.toolStripMenuItem_copyLS.Size = new System.Drawing.Size(219, 22);
            this.toolStripMenuItem_copyLS.Text = "Copy name";
            this.toolStripMenuItem_copyLS.Click += new System.EventHandler(this.ToolStripMenuItem_copyLS_Click);
            // 
            // toolStripMenuItem_addLS
            // 
            this.toolStripMenuItem_addLS.Name = "toolStripMenuItem_addLS";
            this.toolStripMenuItem_addLS.Size = new System.Drawing.Size(219, 22);
            this.toolStripMenuItem_addLS.Text = "Add";
            this.toolStripMenuItem_addLS.Click += new System.EventHandler(this.ToolStripMenuItem_addLS_Click);
            // 
            // toolStripMenuItem_renameLS
            // 
            this.toolStripMenuItem_renameLS.Name = "toolStripMenuItem_renameLS";
            this.toolStripMenuItem_renameLS.Size = new System.Drawing.Size(219, 22);
            this.toolStripMenuItem_renameLS.Text = "Rename";
            this.toolStripMenuItem_renameLS.Click += new System.EventHandler(this.ToolStripMenuItem_renameLS_Click);
            // 
            // toolStripMenuItem_deleteLS
            // 
            this.toolStripMenuItem_deleteLS.Name = "toolStripMenuItem_deleteLS";
            this.toolStripMenuItem_deleteLS.ShortcutKeyDisplayString = "Del";
            this.toolStripMenuItem_deleteLS.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.toolStripMenuItem_deleteLS.Size = new System.Drawing.Size(219, 22);
            this.toolStripMenuItem_deleteLS.Text = "Delete";
            this.toolStripMenuItem_deleteLS.Click += new System.EventHandler(this.ToolStripMenuItem_deleteLS_Click);
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
            this.splitContainer_schemaRight.Size = new System.Drawing.Size(461, 558);
            this.splitContainer_schemaRight.SplitterDistance = 222;
            this.splitContainer_schemaRight.TabIndex = 0;
            this.splitContainer_schemaRight.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.SplitContainer_schemaRight_SplitterMoved);
            // 
            // button_saveRightSchema
            // 
            this.button_saveRightSchema.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_saveRightSchema.Location = new System.Drawing.Point(342, 32);
            this.button_saveRightSchema.Name = "button_saveRightSchema";
            this.button_saveRightSchema.Size = new System.Drawing.Size(96, 23);
            this.button_saveRightSchema.TabIndex = 1;
            this.button_saveRightSchema.Text = "Save schema";
            this.button_saveRightSchema.UseVisualStyleBackColor = true;
            this.button_saveRightSchema.Click += new System.EventHandler(this.Button_saveRightSchema_Click);
            // 
            // button_loadSchema
            // 
            this.button_loadSchema.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_loadSchema.AutoSize = true;
            this.button_loadSchema.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.button_loadSchema.Location = new System.Drawing.Point(351, 3);
            this.button_loadSchema.Name = "button_loadSchema";
            this.button_loadSchema.Size = new System.Drawing.Size(87, 25);
            this.button_loadSchema.TabIndex = 7;
            this.button_loadSchema.Text = "Load schema";
            this.button_loadSchema.UseVisualStyleBackColor = true;
            this.button_loadSchema.Click += new System.EventHandler(this.Button_loadSchema_Click);
            // 
            // treeView_rightSchema
            // 
            this.treeView_rightSchema.ContextMenuStrip = this.contextMenuStrip_leftSchema;
            this.treeView_rightSchema.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView_rightSchema.LabelEdit = true;
            this.treeView_rightSchema.Location = new System.Drawing.Point(0, 0);
            this.treeView_rightSchema.Name = "treeView_rightSchema";
            this.treeView_rightSchema.Size = new System.Drawing.Size(461, 222);
            this.treeView_rightSchema.TabIndex = 0;
            this.treeView_rightSchema.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.TreeView_rightSchema_AfterLabelEdit);
            this.treeView_rightSchema.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TreeView_rightSchema_AfterSelect);
            this.treeView_rightSchema.Enter += new System.EventHandler(this.TreeView_rightSchema_Enter);
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
            // contextMenuStrip_rightSchema
            // 
            this.contextMenuStrip_rightSchema.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_unfoldRS,
            this.toolStripMenuItem_foldRS,
            this.toolStripMenuItem_copyRS,
            this.toolStripMenuItem_addRS,
            this.toolStripMenuItem_renameRS,
            this.toolStripMenuItem_deleteRS});
            this.contextMenuStrip_rightSchema.Name = "contextMenuStrip_schema";
            this.contextMenuStrip_rightSchema.Size = new System.Drawing.Size(226, 136);
            // 
            // toolStripMenuItem_unfoldRS
            // 
            this.toolStripMenuItem_unfoldRS.Name = "toolStripMenuItem_unfoldRS";
            this.toolStripMenuItem_unfoldRS.ShortcutKeyDisplayString = "Ctrel+\'+\'";
            this.toolStripMenuItem_unfoldRS.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Oemplus)));
            this.toolStripMenuItem_unfoldRS.Size = new System.Drawing.Size(225, 22);
            this.toolStripMenuItem_unfoldRS.Text = "Unfold all children";
            this.toolStripMenuItem_unfoldRS.Click += new System.EventHandler(this.ToolStripMenuItem_unfoldRS_Click);
            // 
            // toolStripMenuItem_foldRS
            // 
            this.toolStripMenuItem_foldRS.Name = "toolStripMenuItem_foldRS";
            this.toolStripMenuItem_foldRS.ShortcutKeyDisplayString = "Ctrl+\'-\'";
            this.toolStripMenuItem_foldRS.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.OemMinus)));
            this.toolStripMenuItem_foldRS.Size = new System.Drawing.Size(225, 22);
            this.toolStripMenuItem_foldRS.Text = "Fold all children";
            this.toolStripMenuItem_foldRS.Click += new System.EventHandler(this.ToolStripMenuItem_foldRS_Click);
            // 
            // toolStripMenuItem_copyRS
            // 
            this.toolStripMenuItem_copyRS.Name = "toolStripMenuItem_copyRS";
            this.toolStripMenuItem_copyRS.ShortcutKeyDisplayString = "Ctrl+C";
            this.toolStripMenuItem_copyRS.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.toolStripMenuItem_copyRS.Size = new System.Drawing.Size(225, 22);
            this.toolStripMenuItem_copyRS.Text = "Copy name";
            this.toolStripMenuItem_copyRS.Click += new System.EventHandler(this.ToolStripMenuItem_copyRS_Click);
            // 
            // toolStripMenuItem_addRS
            // 
            this.toolStripMenuItem_addRS.Name = "toolStripMenuItem_addRS";
            this.toolStripMenuItem_addRS.Size = new System.Drawing.Size(225, 22);
            this.toolStripMenuItem_addRS.Text = "Add";
            this.toolStripMenuItem_addRS.Click += new System.EventHandler(this.ToolStripMenuItem_addRS_Click);
            // 
            // toolStripMenuItem_renameRS
            // 
            this.toolStripMenuItem_renameRS.Name = "toolStripMenuItem_renameRS";
            this.toolStripMenuItem_renameRS.Size = new System.Drawing.Size(225, 22);
            this.toolStripMenuItem_renameRS.Text = "Rename";
            this.toolStripMenuItem_renameRS.Click += new System.EventHandler(this.ToolStripMenuItem_renameRS_Click);
            // 
            // toolStripMenuItem_deleteRS
            // 
            this.toolStripMenuItem_deleteRS.Name = "toolStripMenuItem_deleteRS";
            this.toolStripMenuItem_deleteRS.ShortcutKeyDisplayString = "Del";
            this.toolStripMenuItem_deleteRS.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.toolStripMenuItem_deleteRS.Size = new System.Drawing.Size(225, 22);
            this.toolStripMenuItem_deleteRS.Text = "Delete";
            this.toolStripMenuItem_deleteRS.Click += new System.EventHandler(this.ToolStripMenuItem_deleteRS_Click);
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
            this.splitContainer_schemaLeft.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_schemaLeft)).EndInit();
            this.splitContainer_schemaLeft.ResumeLayout(false);
            this.contextMenuStrip_leftSchema.ResumeLayout(false);
            this.splitContainer_schemaRight.Panel1.ResumeLayout(false);
            this.splitContainer_schemaRight.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_schemaRight)).EndInit();
            this.splitContainer_schemaRight.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.contextMenuStrip_rightSchema.ResumeLayout(false);
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
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_addLS;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_renameLS;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_deleteLS;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_copyLS;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_foldLS;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_unfoldLS;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_rightSchema;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_addRS;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_renameRS;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_deleteRS;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_copyRS;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_foldRS;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_unfoldRS;
    }
}

