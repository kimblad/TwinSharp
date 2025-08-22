namespace TwinSharpControls
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
            comboBoxTargets = new ComboBox();
            comboBoxAxes = new ComboBox();
            lblTarget = new Label();
            lblAxis = new Label();
            ncAxis1 = new NcAxis();
            SuspendLayout();
            // 
            // comboBoxTargets
            // 
            comboBoxTargets.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxTargets.FormattingEnabled = true;
            comboBoxTargets.Location = new Point(12, 27);
            comboBoxTargets.Name = "comboBoxTargets";
            comboBoxTargets.Size = new Size(200, 23);
            comboBoxTargets.TabIndex = 1;
            comboBoxTargets.SelectedIndexChanged += comboBoxTargets_SelectedIndexChanged;
            // 
            // comboBoxAxes
            // 
            comboBoxAxes.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxAxes.FormattingEnabled = true;
            comboBoxAxes.Location = new Point(225, 27);
            comboBoxAxes.Name = "comboBoxAxes";
            comboBoxAxes.Size = new Size(150, 23);
            comboBoxAxes.TabIndex = 2;
            comboBoxAxes.SelectedIndexChanged += comboBoxAxes_SelectedIndexChanged;
            // 
            // lblTarget
            // 
            lblTarget.AutoSize = true;
            lblTarget.Location = new Point(12, 9);
            lblTarget.Name = "lblTarget";
            lblTarget.Size = new Size(42, 15);
            lblTarget.TabIndex = 4;
            lblTarget.Text = "Target:";
            // 
            // lblAxis
            // 
            lblAxis.AutoSize = true;
            lblAxis.Location = new Point(225, 9);
            lblAxis.Name = "lblAxis";
            lblAxis.Size = new Size(32, 15);
            lblAxis.TabIndex = 5;
            lblAxis.Text = "Axis:";
            // 
            // ncAxis1
            // 
            ncAxis1.Axis = null;
            ncAxis1.Location = new Point(12, 56);
            ncAxis1.Name = "ncAxis1";
            ncAxis1.Size = new Size(516, 539);
            ncAxis1.TabIndex = 6;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(534, 599);
            Controls.Add(ncAxis1);
            Controls.Add(lblAxis);
            Controls.Add(lblTarget);
            Controls.Add(comboBoxAxes);
            Controls.Add(comboBoxTargets);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Name = "Form1";
            Text = "TwinSharp Demo";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox comboBoxTargets;
        private ComboBox comboBoxAxes;
        private Label lblTarget;
        private Label lblAxis;
        private NcAxis ncAxis1;
    }
}