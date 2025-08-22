namespace TwinSharpControls
{
    partial class NcAxisHeader
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
            barberPole1 = new BarberPole();
            label1 = new Label();
            txtBoxSetPointPosition = new TextBox();
            txtBoxActualPosition = new TextBox();
            SuspendLayout();
            // 
            // barberPole1
            // 
            barberPole1.Location = new Point(0, 3);
            barberPole1.Margin = new Padding(0, 0, 3, 0);
            barberPole1.Name = "barberPole1";
            barberPole1.Size = new Size(36, 36);
            barberPole1.Speed = 2.0000000298023224D;
            barberPole1.TabIndex = 34;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(312, -2);
            label1.Name = "label1";
            label1.Size = new Size(100, 15);
            label1.TabIndex = 33;
            label1.Text = "Setpoint Position:";
            // 
            // txtBoxSetPointPosition
            // 
            txtBoxSetPointPosition.Location = new Point(312, 16);
            txtBoxSetPointPosition.Name = "txtBoxSetPointPosition";
            txtBoxSetPointPosition.ReadOnly = true;
            txtBoxSetPointPosition.Size = new Size(150, 23);
            txtBoxSetPointPosition.TabIndex = 32;
            txtBoxSetPointPosition.TabStop = false;
            txtBoxSetPointPosition.TextAlign = HorizontalAlignment.Right;
            // 
            // txtBoxActualPosition
            // 
            txtBoxActualPosition.Font = new Font("Segoe UI", 16F);
            txtBoxActualPosition.Location = new Point(42, 3);
            txtBoxActualPosition.Name = "txtBoxActualPosition";
            txtBoxActualPosition.ReadOnly = true;
            txtBoxActualPosition.Size = new Size(264, 36);
            txtBoxActualPosition.TabIndex = 31;
            txtBoxActualPosition.TabStop = false;
            txtBoxActualPosition.TextAlign = HorizontalAlignment.Right;
            // 
            // NcAxisHeader
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(barberPole1);
            Controls.Add(label1);
            Controls.Add(txtBoxSetPointPosition);
            Controls.Add(txtBoxActualPosition);
            Name = "NcAxisHeader";
            Size = new Size(464, 48);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private BarberPole barberPole1;
        private Label label1;
        private TextBox txtBoxSetPointPosition;
        private TextBox txtBoxActualPosition;
    }
}
