namespace TwinSharpControls
{
    partial class PlcSymbolBrowser
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblTitle = new Label();
            splitContainer1 = new SplitContainer();
            treeViewSymbols = new TreeView();
            panelTreeHeader = new Panel();
            txtFilter = new TextBox();
            lblFilter = new Label();
            btnLoadSymbols = new Button();
            comboBoxPort = new ComboBox();
            lblPort = new Label();
            groupBoxDetails = new GroupBox();
            btnWrite = new Button();
            btnRead = new Button();
            txtValue = new TextBox();
            lblValue = new Label();
            txtComment = new TextBox();
            lblComment = new Label();
            txtSize = new TextBox();
            lblSize = new Label();
            txtCategory = new TextBox();
            lblCategory = new Label();
            txtTypeName = new TextBox();
            lblTypeName = new Label();
            txtSymbolPath = new TextBox();
            lblSymbolPath = new Label();
            txtSymbolName = new TextBox();
            lblSymbolName = new Label();
            lblStatus = new Label();
            lblError = new Label();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            panelTreeHeader.SuspendLayout();
            groupBoxDetails.SuspendLayout();
            SuspendLayout();
            //
            // lblTitle
            //
            lblTitle.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblTitle.Location = new Point(810, 13);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(150, 20);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "PLC Symbol Browser";
            //
            // splitContainer1
            //
            splitContainer1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            splitContainer1.Location = new Point(15, 45);
            splitContainer1.Name = "splitContainer1";
            //
            // splitContainer1.Panel1
            //
            splitContainer1.Panel1.Controls.Add(treeViewSymbols);
            splitContainer1.Panel1.Controls.Add(panelTreeHeader);
            //
            // splitContainer1.Panel2
            //
            splitContainer1.Panel2.Controls.Add(groupBoxDetails);
            splitContainer1.Size = new Size(945, 460);
            splitContainer1.SplitterDistance = 450;
            splitContainer1.TabIndex = 1;
            //
            // treeViewSymbols
            //
            treeViewSymbols.Dock = DockStyle.Fill;
            treeViewSymbols.Location = new Point(0, 70);
            treeViewSymbols.Name = "treeViewSymbols";
            treeViewSymbols.Size = new Size(450, 390);
            treeViewSymbols.TabIndex = 0;
            treeViewSymbols.AfterSelect += treeViewSymbols_AfterSelect;
            //
            // panelTreeHeader
            //
            panelTreeHeader.Controls.Add(txtFilter);
            panelTreeHeader.Controls.Add(lblFilter);
            panelTreeHeader.Controls.Add(btnLoadSymbols);
            panelTreeHeader.Controls.Add(comboBoxPort);
            panelTreeHeader.Controls.Add(lblPort);
            panelTreeHeader.Dock = DockStyle.Top;
            panelTreeHeader.Location = new Point(0, 0);
            panelTreeHeader.Name = "panelTreeHeader";
            panelTreeHeader.Size = new Size(450, 70);
            panelTreeHeader.TabIndex = 1;
            //
            // txtFilter
            //
            txtFilter.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtFilter.Location = new Point(60, 38);
            txtFilter.Name = "txtFilter";
            txtFilter.PlaceholderText = "Type to filter symbols...";
            txtFilter.Size = new Size(380, 23);
            txtFilter.TabIndex = 2;
            txtFilter.TextChanged += txtFilter_TextChanged;
            //
            // lblFilter
            //
            lblFilter.AutoSize = true;
            lblFilter.Location = new Point(10, 41);
            lblFilter.Name = "lblFilter";
            lblFilter.Size = new Size(36, 15);
            lblFilter.TabIndex = 1;
            lblFilter.Text = "Filter:";
            //
            // btnLoadSymbols
            //
            btnLoadSymbols.Location = new Point(10, 8);
            btnLoadSymbols.Name = "btnLoadSymbols";
            btnLoadSymbols.Size = new Size(120, 25);
            btnLoadSymbols.TabIndex = 0;
            btnLoadSymbols.Text = "Load Symbols";
            btnLoadSymbols.UseVisualStyleBackColor = true;
            btnLoadSymbols.Click += btnLoadSymbols_Click;
            //
            // comboBoxPort
            //
            comboBoxPort.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            comboBoxPort.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxPort.FormattingEnabled = true;
            comboBoxPort.Location = new Point(250, 9);
            comboBoxPort.Name = "comboBoxPort";
            comboBoxPort.Size = new Size(190, 23);
            comboBoxPort.TabIndex = 3;
            comboBoxPort.SelectedIndexChanged += comboBoxPort_SelectedIndexChanged;
            //
            // lblPort
            //
            lblPort.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblPort.AutoSize = true;
            lblPort.Location = new Point(210, 12);
            lblPort.Name = "lblPort";
            lblPort.Size = new Size(32, 15);
            lblPort.TabIndex = 4;
            lblPort.Text = "Port:";
            //
            // groupBoxDetails
            //
            groupBoxDetails.Controls.Add(btnWrite);
            groupBoxDetails.Controls.Add(btnRead);
            groupBoxDetails.Controls.Add(txtValue);
            groupBoxDetails.Controls.Add(lblValue);
            groupBoxDetails.Controls.Add(txtComment);
            groupBoxDetails.Controls.Add(lblComment);
            groupBoxDetails.Controls.Add(txtSize);
            groupBoxDetails.Controls.Add(lblSize);
            groupBoxDetails.Controls.Add(txtCategory);
            groupBoxDetails.Controls.Add(lblCategory);
            groupBoxDetails.Controls.Add(txtTypeName);
            groupBoxDetails.Controls.Add(lblTypeName);
            groupBoxDetails.Controls.Add(txtSymbolPath);
            groupBoxDetails.Controls.Add(lblSymbolPath);
            groupBoxDetails.Controls.Add(txtSymbolName);
            groupBoxDetails.Controls.Add(lblSymbolName);
            groupBoxDetails.Dock = DockStyle.Fill;
            groupBoxDetails.Location = new Point(0, 0);
            groupBoxDetails.Name = "groupBoxDetails";
            groupBoxDetails.Size = new Size(491, 460);
            groupBoxDetails.TabIndex = 0;
            groupBoxDetails.TabStop = false;
            groupBoxDetails.Text = "Symbol Details";
            //
            // btnWrite
            //
            btnWrite.Enabled = false;
            btnWrite.Location = new Point(100, 405);
            btnWrite.Name = "btnWrite";
            btnWrite.Size = new Size(80, 30);
            btnWrite.TabIndex = 15;
            btnWrite.Text = "Write";
            btnWrite.UseVisualStyleBackColor = true;
            btnWrite.Click += btnWrite_Click;
            //
            // btnRead
            //
            btnRead.Location = new Point(15, 405);
            btnRead.Name = "btnRead";
            btnRead.Size = new Size(80, 30);
            btnRead.TabIndex = 14;
            btnRead.Text = "Read";
            btnRead.UseVisualStyleBackColor = true;
            btnRead.Click += btnRead_Click;
            //
            // txtValue
            //
            txtValue.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtValue.Font = new Font("Consolas", 9F);
            txtValue.Location = new Point(95, 365);
            txtValue.Name = "txtValue";
            txtValue.ReadOnly = true;
            txtValue.Size = new Size(380, 22);
            txtValue.TabIndex = 13;
            //
            // lblValue
            //
            lblValue.AutoSize = true;
            lblValue.Location = new Point(15, 368);
            lblValue.Name = "lblValue";
            lblValue.Size = new Size(38, 15);
            lblValue.TabIndex = 12;
            lblValue.Text = "Value:";
            //
            // txtComment
            //
            txtComment.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtComment.Location = new Point(95, 215);
            txtComment.Multiline = true;
            txtComment.Name = "txtComment";
            txtComment.ReadOnly = true;
            txtComment.ScrollBars = ScrollBars.Vertical;
            txtComment.Size = new Size(380, 135);
            txtComment.TabIndex = 11;
            //
            // lblComment
            //
            lblComment.AutoSize = true;
            lblComment.Location = new Point(15, 218);
            lblComment.Name = "lblComment";
            lblComment.Size = new Size(64, 15);
            lblComment.TabIndex = 10;
            lblComment.Text = "Comment:";
            //
            // txtSize
            //
            txtSize.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtSize.Location = new Point(95, 175);
            txtSize.Name = "txtSize";
            txtSize.ReadOnly = true;
            txtSize.Size = new Size(380, 23);
            txtSize.TabIndex = 9;
            //
            // lblSize
            //
            lblSize.AutoSize = true;
            lblSize.Location = new Point(15, 178);
            lblSize.Name = "lblSize";
            lblSize.Size = new Size(30, 15);
            lblSize.TabIndex = 8;
            lblSize.Text = "Size:";
            //
            // txtCategory
            //
            txtCategory.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtCategory.Location = new Point(95, 145);
            txtCategory.Name = "txtCategory";
            txtCategory.ReadOnly = true;
            txtCategory.Size = new Size(380, 23);
            txtCategory.TabIndex = 7;
            //
            // lblCategory
            //
            lblCategory.AutoSize = true;
            lblCategory.Location = new Point(15, 148);
            lblCategory.Name = "lblCategory";
            lblCategory.Size = new Size(58, 15);
            lblCategory.TabIndex = 6;
            lblCategory.Text = "Category:";
            //
            // txtTypeName
            //
            txtTypeName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtTypeName.Location = new Point(95, 115);
            txtTypeName.Name = "txtTypeName";
            txtTypeName.ReadOnly = true;
            txtTypeName.Size = new Size(380, 23);
            txtTypeName.TabIndex = 5;
            //
            // lblTypeName
            //
            lblTypeName.AutoSize = true;
            lblTypeName.Location = new Point(15, 118);
            lblTypeName.Name = "lblTypeName";
            lblTypeName.Size = new Size(34, 15);
            lblTypeName.TabIndex = 4;
            lblTypeName.Text = "Type:";
            //
            // txtSymbolPath
            //
            txtSymbolPath.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtSymbolPath.Location = new Point(95, 55);
            txtSymbolPath.Multiline = true;
            txtSymbolPath.Name = "txtSymbolPath";
            txtSymbolPath.ReadOnly = true;
            txtSymbolPath.Size = new Size(380, 50);
            txtSymbolPath.TabIndex = 3;
            //
            // lblSymbolPath
            //
            lblSymbolPath.AutoSize = true;
            lblSymbolPath.Location = new Point(15, 58);
            lblSymbolPath.Name = "lblSymbolPath";
            lblSymbolPath.Size = new Size(34, 15);
            lblSymbolPath.TabIndex = 2;
            lblSymbolPath.Text = "Path:";
            //
            // txtSymbolName
            //
            txtSymbolName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtSymbolName.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            txtSymbolName.Location = new Point(95, 25);
            txtSymbolName.Name = "txtSymbolName";
            txtSymbolName.ReadOnly = true;
            txtSymbolName.Size = new Size(380, 23);
            txtSymbolName.TabIndex = 1;
            //
            // lblSymbolName
            //
            lblSymbolName.AutoSize = true;
            lblSymbolName.Location = new Point(15, 28);
            lblSymbolName.Name = "lblSymbolName";
            lblSymbolName.Size = new Size(42, 15);
            lblSymbolName.TabIndex = 0;
            lblSymbolName.Text = "Name:";
            //
            // lblStatus
            //
            lblStatus.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblStatus.AutoSize = true;
            lblStatus.ForeColor = Color.Blue;
            lblStatus.Location = new Point(15, 515);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(39, 15);
            lblStatus.TabIndex = 2;
            lblStatus.Text = "Status";
            lblStatus.Visible = false;
            //
            // lblError
            //
            lblError.AutoSize = true;
            lblError.ForeColor = Color.Red;
            lblError.Location = new Point(120, 16);
            lblError.Name = "lblError";
            lblError.Size = new Size(32, 15);
            lblError.TabIndex = 3;
            lblError.Text = "Error";
            lblError.Visible = false;
            //
            // PlcSymbolBrowser
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(lblError);
            Controls.Add(lblStatus);
            Controls.Add(splitContainer1);
            Controls.Add(lblTitle);
            Name = "PlcSymbolBrowser";
            Size = new Size(980, 545);
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            panelTreeHeader.ResumeLayout(false);
            panelTreeHeader.PerformLayout();
            groupBoxDetails.ResumeLayout(false);
            groupBoxDetails.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitle;
        private SplitContainer splitContainer1;
        private TreeView treeViewSymbols;
        private Panel panelTreeHeader;
        private Button btnLoadSymbols;
        private TextBox txtFilter;
        private Label lblFilter;
        private GroupBox groupBoxDetails;
        private Label lblSymbolName;
        private TextBox txtSymbolName;
        private TextBox txtSymbolPath;
        private Label lblSymbolPath;
        private TextBox txtTypeName;
        private Label lblTypeName;
        private TextBox txtCategory;
        private Label lblCategory;
        private TextBox txtSize;
        private Label lblSize;
        private TextBox txtComment;
        private Label lblComment;
        private TextBox txtValue;
        private Label lblValue;
        private Button btnRead;
        private Button btnWrite;
        private Label lblStatus;
        private Label lblError;
        private ComboBox comboBoxPort;
        private Label lblPort;
    }
}
