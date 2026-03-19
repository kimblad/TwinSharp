using TwinCAT.Ads;

namespace TwinSharpControls
{
    public partial class SymbolBrowserDialog : Form
    {
        private PlcSymbolBrowser symbolBrowser = null!;
        private Button btnOk = null!;
        private Button btnCancel = null!;

        public string? SelectedSymbolPath { get; private set; }

        public SymbolBrowserDialog()
        {
            InitializeComponent();
        }

        public SymbolBrowserDialog(AmsNetId target, int port = 851) : this()
        {
            symbolBrowser.Target = target;
            symbolBrowser.Port = port;
        }

        private void InitializeComponent()
        {
            symbolBrowser = new PlcSymbolBrowser();
            btnOk = new Button();
            btnCancel = new Button();
            SuspendLayout();

            //
            // symbolBrowser
            //
            symbolBrowser.Dock = DockStyle.Fill;
            symbolBrowser.Location = new Point(0, 0);
            symbolBrowser.Name = "symbolBrowser";
            symbolBrowser.Size = new Size(984, 611);
            symbolBrowser.TabIndex = 0;
            symbolBrowser.SymbolSelected += SymbolBrowser_SymbolSelected;

            //
            // btnOk
            //
            btnOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnOk.DialogResult = DialogResult.OK;
            btnOk.Enabled = false;
            btnOk.Location = new Point(816, 626);
            btnOk.Name = "btnOk";
            btnOk.Size = new Size(75, 23);
            btnOk.TabIndex = 1;
            btnOk.Text = "OK";
            btnOk.UseVisualStyleBackColor = true;

            //
            // btnCancel
            //
            btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.Location = new Point(897, 626);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(75, 23);
            btnCancel.TabIndex = 2;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;

            //
            // SymbolBrowserDialog
            //
            AcceptButton = btnOk;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancel;
            ClientSize = new Size(984, 661);
            Controls.Add(btnCancel);
            Controls.Add(btnOk);
            Controls.Add(symbolBrowser);
            MinimizeBox = false;
            MaximizeBox = true;
            Name = "SymbolBrowserDialog";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Select PLC Symbol";
            ResumeLayout(false);
        }

        private void SymbolBrowser_SymbolSelected(object? sender, string symbolPath)
        {
            SelectedSymbolPath = symbolPath;
            btnOk.Enabled = !string.IsNullOrEmpty(symbolPath);
        }
    }
}
