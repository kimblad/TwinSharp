namespace TwinSharpControls
{
    partial class NcAxis
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            ncOnline1 = new NcOnline();
            tabPage2 = new TabPage();
            ncFunctions1 = new NcFunctions();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            tabPage2.SuspendLayout();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Location = new Point(3, 3);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(509, 532);
            tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(ncOnline1);
            tabPage1.Location = new Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(501, 504);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Online";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // ncOnline1
            // 
            ncOnline1.Axis = null;
            ncOnline1.Location = new Point(6, 6);
            ncOnline1.Name = "ncOnline1";
            ncOnline1.Size = new Size(498, 427);
            ncOnline1.TabIndex = 0;
            ncOnline1.UpdateInterval = 100;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(ncFunctions1);
            tabPage2.Location = new Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(501, 504);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Functions";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // ncFunctions1
            // 
            ncFunctions1.Axis = null;
            ncFunctions1.Location = new Point(6, 6);
            ncFunctions1.Name = "ncFunctions1";
            ncFunctions1.Size = new Size(498, 513);
            ncFunctions1.TabIndex = 0;
            ncFunctions1.UpdateInterval = 100;
            // 
            // NcAxis
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tabControl1);
            Name = "NcAxis";
            Size = new Size(516, 539);
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private NcOnline ncOnline1;
        private NcFunctions ncFunctions1;
    }
}
