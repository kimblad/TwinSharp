using System.Text;
using TwinCAT;
using TwinCAT.Ads;
using TwinCAT.Ads.TypeSystem;
using TwinCAT.TypeSystem;
using System.Reflection;

namespace TwinSharpControls
{
    public partial class PlcSymbolBrowser : UserControl
    {
        private AdsClient? client;
        private ISymbolLoader? symbolLoader;
        private AmsNetId? targetNetId;
        private int adsPort = 851; // Default to PLC Runtime port 851

        // Event that fires when a symbol is selected
        public event EventHandler<string>? SymbolSelected;

        public PlcSymbolBrowser()
        {
            InitializeComponent();
            PopulatePortComboBox();
        }

        private void PopulatePortComboBox()
        {
            comboBoxPort.Items.Clear();

            // Add common TwinCAT 3 PLC Runtime ports
            comboBoxPort.Items.Add(new AdsPortItem("PLC Runtime 1 (TC3)", 851));
            comboBoxPort.Items.Add(new AdsPortItem("PLC Runtime 2 (TC3)", 852));
            comboBoxPort.Items.Add(new AdsPortItem("PLC Runtime 3 (TC3)", 853));
            comboBoxPort.Items.Add(new AdsPortItem("PLC Runtime 4 (TC3)", 854));

            // Add TwinCAT 2 PLC Runtime ports
            comboBoxPort.Items.Add(new AdsPortItem("PLC Runtime 1 (TC2)", 300));
            comboBoxPort.Items.Add(new AdsPortItem("PLC Runtime 2 (TC2)", 301));
            comboBoxPort.Items.Add(new AdsPortItem("PLC Runtime 3 (TC2)", 302));
            comboBoxPort.Items.Add(new AdsPortItem("PLC Runtime 4 (TC2)", 303));

            // Add other common ports
            comboBoxPort.Items.Add(new AdsPortItem("System Service", 10000));
            comboBoxPort.Items.Add(new AdsPortItem("NC (Motion)", 501));
            comboBoxPort.Items.Add(new AdsPortItem("CNC", 500));
            comboBoxPort.Items.Add(new AdsPortItem("I/O", 200));

            // Select PLC Runtime 1 (TC3) by default
            comboBoxPort.SelectedIndex = 0;
        }

        /// <summary>
        /// Set the target AmsNetId.
        /// </summary>
        public AmsNetId? Target
        {
            set
            {
                targetNetId = value;
                Reconnect();
            }
        }

        /// <summary>
        /// Set the ADS port number (default 851 for PLC Runtime).
        /// </summary>
        public int Port
        {
            get => adsPort;
            set
            {
                adsPort = value;
                // Try to find and select matching port in combo box
                for (int i = 0; i < comboBoxPort.Items.Count; i++)
                {
                    if (comboBoxPort.Items[i] is AdsPortItem item && item.Port == value)
                    {
                        comboBoxPort.SelectedIndex = i;
                        return;
                    }
                }
                Reconnect();
            }
        }

        private void Reconnect()
        {
            Disconnect();

            if (targetNetId != null)
            {
                try
                {
                    client = new AdsClient();
                    client.Connect(targetNetId, adsPort);

                    // Create symbol loader using SymbolLoaderFactory with default settings
                    var settings = SymbolLoaderSettings.Default;
                    symbolLoader = SymbolLoaderFactory.Create(client, settings);

                    lblError.Visible = false;
                }
                catch (Exception ex)
                {
                    lblError.Text = $"Error connecting to PLC: {ex.Message}";
                    lblError.Visible = true;
                }
            }
        }

        private void btnLoadSymbols_Click(object sender, EventArgs e)
        {
            LoadSymbols();
        }

        private void comboBoxPort_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxPort.SelectedItem is AdsPortItem portItem)
            {
                adsPort = portItem.Port;
                Reconnect();
            }
        }

        private void LoadSymbols()
        {
            if (symbolLoader == null)
            {
                lblError.Text = "Not connected to PLC";
                lblError.Visible = true;
                return;
            }

            try
            {
                treeViewSymbols.Nodes.Clear();
                lblStatus.Text = "Loading symbols...";
                lblStatus.Visible = true;
                lblStatus.ForeColor = Color.Blue;
                Application.DoEvents();

                // Get all symbols from the loader
                var symbols = symbolLoader.Symbols;

                // Add each root-level symbol to the tree
                foreach (ISymbol symbol in symbols)
                {
                    AddSymbolNode(null, symbol);
                }

                lblStatus.Text = $"Loaded {symbols.Count} root symbols successfully";
                lblStatus.ForeColor = Color.Green;
                lblError.Visible = false;
            }
            catch (Exception ex)
            {
                lblError.Text = $"Error loading symbols: {ex.Message}";
                lblError.Visible = true;
                lblStatus.Visible = false;
            }
        }

        private void AddSymbolNode(TreeNode? parentNode, ISymbol symbol)
        {
            var node = new TreeNode();
            node.Tag = symbol;

            // Set icon/prefix based on symbol category
            string icon = symbol.Category switch
            {
                DataTypeCategory.Struct => "📁",
                DataTypeCategory.Array => "📊",
                DataTypeCategory.Primitive => "🔢",
                DataTypeCategory.String => "📝",
                DataTypeCategory.Enum => "🔤",
                DataTypeCategory.Alias => "🔗",
                DataTypeCategory.Pointer => "👉",
                DataTypeCategory.Reference => "🔗",
                _ => "❓"
            };

            node.Text = $"{icon} {symbol.InstanceName} : {symbol.TypeName}";

            // Add to parent or root
            if (parentNode == null)
                treeViewSymbols.Nodes.Add(node);
            else
                parentNode.Nodes.Add(node);

            // Recursively add sub-symbols (for structs, arrays, etc.)
            if (symbol.SubSymbols != null && symbol.SubSymbols.Count > 0)
            {
                foreach (ISymbol subSymbol in symbol.SubSymbols)
                {
                    AddSymbolNode(node, subSymbol);
                }
            }
        }

        private void treeViewSymbols_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node?.Tag is ISymbol symbol)
            {
                DisplaySymbolDetails(symbol);
                // Raise event for symbol selection
                SymbolSelected?.Invoke(this, symbol.InstancePath);
            }
        }

        private void DisplaySymbolDetails(ISymbol symbol)
        {
            try
            {
                // Display basic symbol information
                txtSymbolName.Text = symbol.InstanceName;
                txtSymbolPath.Text = symbol.InstancePath;
                txtTypeName.Text = symbol.TypeName;
                txtCategory.Text = symbol.Category.ToString();
                txtSize.Text = $"{symbol.Size} bytes";
                txtComment.Text = symbol.Comment ?? "";

                // Try to read value for primitive types
                if (symbol.IsPrimitiveType || symbol.Category == DataTypeCategory.String || symbol.Category == DataTypeCategory.Enum)
                {
                    try
                    {
                        if (symbol is IValueSymbol valueSymbol)
                        {
                            object value = valueSymbol.ReadValue();
                            txtValue.Text = value?.ToString() ?? "null";
                            txtValue.ReadOnly = false;
                            btnWrite.Enabled = true;
                        }
                        else
                        {
                            txtValue.Text = "<cannot read - not a value symbol>";
                            txtValue.ReadOnly = true;
                            btnWrite.Enabled = false;
                        }
                    }
                    catch (Exception ex)
                    {
                        txtValue.Text = $"<read error: {ex.Message}>";
                        txtValue.ReadOnly = true;
                        btnWrite.Enabled = false;
                    }
                }
                else
                {
                    txtValue.Text = "<complex type - expand in tree to see members>";
                    txtValue.ReadOnly = true;
                    btnWrite.Enabled = false;
                }

                lblError.Visible = false;
            }
            catch (Exception ex)
            {
                lblError.Text = $"Error reading symbol: {ex.Message}";
                lblError.ForeColor = Color.Red;
                lblError.Visible = true;
            }
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            if (treeViewSymbols.SelectedNode?.Tag is ISymbol symbol)
            {
                DisplaySymbolDetails(symbol);
            }
            else
            {
                lblError.Text = "Please select a symbol from the tree";
                lblError.ForeColor = Color.Red;
                lblError.Visible = true;
            }
        }

        private void btnWrite_Click(object sender, EventArgs e)
        {
            if (treeViewSymbols.SelectedNode?.Tag is not ISymbol symbol)
            {
                lblError.Text = "Please select a symbol from the tree";
                lblError.ForeColor = Color.Red;
                lblError.Visible = true;
                return;
            }

            if (symbol is not IValueSymbol valueSymbol)
            {
                lblError.Text = "Selected symbol is not writable";
                lblError.ForeColor = Color.Red;
                lblError.Visible = true;
                return;
            }

            try
            {
                // Convert the text value to the appropriate type
                object newValue = ConvertValue(txtValue.Text, symbol);

                // Write the value
                valueSymbol.WriteValue(newValue);

                lblError.Text = "Value written successfully";
                lblError.ForeColor = Color.Green;
                lblError.Visible = true;

                // Re-read to confirm
                System.Threading.Thread.Sleep(50);
                DisplaySymbolDetails(symbol);
            }
            catch (Exception ex)
            {
                lblError.Text = $"Write error: {ex.Message}";
                lblError.ForeColor = Color.Red;
                lblError.Visible = true;
            }
        }

        private object ConvertValue(string text, ISymbol symbol)
        {
            var typeName = symbol.TypeName.ToUpper();

            // Handle common PLC types
            return typeName switch
            {
                "BOOL" => bool.Parse(text),
                "BYTE" or "USINT" => byte.Parse(text),
                "SINT" => sbyte.Parse(text),
                "WORD" or "UINT" => ushort.Parse(text),
                "INT" => short.Parse(text),
                "DWORD" or "UDINT" => uint.Parse(text),
                "DINT" => int.Parse(text),
                "LWORD" or "ULINT" => ulong.Parse(text),
                "LINT" => long.Parse(text),
                "REAL" => float.Parse(text),
                "LREAL" => double.Parse(text),
                _ when typeName.StartsWith("STRING") => text,
                _ => throw new NotSupportedException($"Type {typeName} conversion not supported. Please use a specific type.")
            };
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            FilterSymbols(txtFilter.Text);
        }

        private void FilterSymbols(string filter)
        {
            if (string.IsNullOrWhiteSpace(filter))
            {
                // Show all nodes
                ShowAllNodes(treeViewSymbols.Nodes);
                return;
            }

            // Hide nodes that don't match filter
            foreach (TreeNode node in treeViewSymbols.Nodes)
            {
                FilterNode(node, filter.ToLower());
            }
        }

        private void ShowAllNodes(TreeNodeCollection nodes)
        {
            foreach (TreeNode node in nodes)
            {
                node.BackColor = Color.Transparent;
                node.ForeColor = Color.Black;
                ShowAllNodes(node.Nodes);
            }
        }

        private bool FilterNode(TreeNode node, string filter)
        {
            bool hasMatch = node.Text.ToLower().Contains(filter);
            bool childMatch = false;

            foreach (TreeNode child in node.Nodes)
            {
                if (FilterNode(child, filter))
                    childMatch = true;
            }

            if (hasMatch || childMatch)
            {
                node.BackColor = hasMatch ? Color.Yellow : Color.Transparent;
                node.ForeColor = Color.Black;
                node.Expand();
                return true;
            }
            else
            {
                node.BackColor = Color.Transparent;
                node.ForeColor = Color.LightGray;
                return false;
            }
        }

        private void Disconnect()
        {
            symbolLoader = null;

            if (client != null)
            {
                try
                {
                    client.Disconnect();
                    client.Dispose();
                }
                catch { }
                client = null;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Disconnect();
                components?.Dispose();
            }
            base.Dispose(disposing);
        }
    }

    /// <summary>
    /// Container for ADS port information in the combo box.
    /// </summary>
    public class AdsPortItem
    {
        public AdsPortItem(string description, int port)
        {
            Description = description;
            Port = port;
        }

        public string Description { get; set; }
        public int Port { get; set; }

        public override string ToString()
        {
            return $"{Description} (Port {Port})";
        }
    }
}
