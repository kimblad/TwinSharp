using System.Globalization;
using System.Reflection;
using TwinCAT.Ads;
using TwinSharp;
using TwinSharp.EtherCAT;

namespace TwinSharpControls
{
    public partial class EtherCatHealthDashboard : UserControl
    {
        private TcSystem? tcSystem;
        private EtherCatMaster? currentMaster;
        private EtherCatMaster[]? allMasters;
        private EtherCatSlave? selectedSlave;

        // Slave details UI components - created dynamically
        private GroupBox? groupBoxSlaveDetails;
        private Label? lblSelectedSlaveName, lblSelectedSlaveState, lblSelectedSlaveLink;
        private Label? lblSelectedVendorId, lblSelectedProductCode, lblSelectedRevision, lblSelectedSerial;
        private GroupBox? groupBoxSelectedCrc;
        private Label? lblSelectedPortA, lblSelectedPortB, lblSelectedPortC, lblSelectedPortD;
        private GroupBox? groupBoxSelectedCoe;
        private TextBox? txtSelectedCoeIndex, txtSelectedCoeSubIndex, txtSelectedCoeValue;
        private Button? btnSelectedCoeRead, btnSelectedCoeWrite, btnSelectedRequestState;

        public EtherCatHealthDashboard()
        {
            InitializeComponent();

            // Enable double buffering to reduce flickering
            typeof(DataGridView).InvokeMember("DoubleBuffered",
                BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty,
                null, dataGridViewSlaves, new object[] { true });

            CreateSlaveDetailsPanel();
            dataGridViewSlaves.SelectionChanged += DataGridViewSlaves_SelectionChanged;
        }

        /// <summary>
        /// Set the TcSystem to monitor EtherCAT network.
        /// </summary>
        public TcSystem? System
        {
            get => tcSystem;
            set
            {
                tcSystem = value;
                LoadMasters();
            }
        }

        /// <summary>
        /// Set the target AmsNetId directly. Creates a TcSystem internally.
        /// </summary>
        public AmsNetId? Target
        {
            set
            {
                if (value == null)
                {
                    System = null;
                }
                else
                {
                    System = new TcSystem(value);
                }
            }
        }

        /// <summary>
        /// Update interval in milliseconds for refreshing data.
        /// </summary>
        public int UpdateInterval
        {
            get => timerUpdate.Interval;
            set => timerUpdate.Interval = value;
        }

        /// <summary>
        /// Enable or disable auto-refresh.
        /// </summary>
        public bool AutoRefresh
        {
            get => timerUpdate.Enabled;
            set => timerUpdate.Enabled = value;
        }

        private void LoadMasters()
        {
            try
            {
                if (tcSystem == null)
                {
                    comboBoxMasters.Items.Clear();
                    comboBoxMasters.Enabled = false;
                    ClearMasterInfo();
                    return;
                }

                allMasters = tcSystem.ListEtherCatMasters();

                comboBoxMasters.Items.Clear();
                foreach (var master in allMasters)
                {
                    comboBoxMasters.Items.Add(master.ToString());
                }

                if (allMasters.Length > 0)
                {
                    comboBoxMasters.SelectedIndex = 0;
                    comboBoxMasters.Enabled = true;
                }
                else
                {
                    comboBoxMasters.Enabled = false;
                    ClearMasterInfo();
                    MessageBox.Show("No EtherCAT masters found on the target system.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading EtherCAT masters: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void comboBoxMasters_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxMasters.SelectedIndex >= 0 && allMasters != null)
            {
                currentMaster = allMasters[comboBoxMasters.SelectedIndex];
                RefreshData();
            }
        }

        private void RefreshData()
        {
            if (currentMaster == null)
                return;

            try
            {
                UpdateMasterInfo();
                UpdateSlaveGrid();
            }
            catch (Exception ex)
            {
                lblError.Text = $"Error: {ex.Message}";
                lblError.Visible = true;
            }
        }

        private void UpdateMasterInfo()
        {
            if (currentMaster == null)
                return;

            lblMasterName.Text = $"Master: {currentMaster.Name}";
            lblMasterNetId.Text = $"NetId: {currentMaster.AmsNetId}";

            var state = currentMaster.State;
            lblMasterState.Text = $"State: {state}";
            lblMasterState.BackColor = GetStateColor(state);

            var devState = currentMaster.DevState;
            lblMasterDevState.Text = $"Device State: {currentMaster.DevStateToString(devState)}";
            lblMasterDevState.BackColor = devState == 0 ? Color.LightGreen : Color.Orange;

            uint slaveCount = currentMaster.SlaveCount;
            ushort slaveCountConfigured = currentMaster.SlaveCountConfigured;
            lblSlaveCount.Text = $"Slaves: {slaveCount} connected / {slaveCountConfigured} configured";

            lblFrameCount.Text = $"Frames: {currentMaster.FrameCount}";

            lblError.Visible = false;
        }

        private void UpdateSlaveGrid()
        {
            if (currentMaster == null)
                return;

            // Get all slave data
            var states = currentMaster.GetAllSlaveStates();
            var addresses = currentMaster.GetAllSlaveAddr();
            var abnormalChanges = currentMaster.GetAllSlaveAbnormalStateChanges();
            var configuredSlaves = currentMaster.GetConfiguredSlaves();
            var topology = currentMaster.GetSlaveTopologyInfo();

            // Suspend layout to reduce flickering
            dataGridViewSlaves.SuspendLayout();

            try
            {
                // If row count doesn't match, we need to rebuild
                if (dataGridViewSlaves.Rows.Count != states.Length)
                {
                    // Save selection
                    ushort? selectedAddress = null;
                    if (dataGridViewSlaves.SelectedRows.Count > 0)
                    {
                        selectedAddress = Convert.ToUInt16(dataGridViewSlaves.SelectedRows[0].Cells[0].Value);
                    }

                    dataGridViewSlaves.Rows.Clear();

                    // Populate grid
                    for (int i = 0; i < states.Length; i++)
                    {
                        var address = addresses[i];
                        string slaveName = i < configuredSlaves.Length ? configuredSlaves[i].Name : "Unknown";

                        dataGridViewSlaves.Rows.Add(address, slaveName, "", "", 0, "");
                    }

                    // Restore selection
                    if (selectedAddress.HasValue)
                    {
                        foreach (DataGridViewRow row in dataGridViewSlaves.Rows)
                        {
                            if (Convert.ToUInt16(row.Cells[0].Value) == selectedAddress.Value)
                            {
                                row.Selected = true;
                                break;
                            }
                        }
                    }
                }

                // Update existing rows in place (no clearing)
                for (int i = 0; i < states.Length && i < dataGridViewSlaves.Rows.Count; i++)
                {
                    var state = states[i];
                    var address = addresses[i];
                    var abnormalCount = abnormalChanges[i];
                    var row = dataGridViewSlaves.Rows[i];

                    string slaveName = i < configuredSlaves.Length ? configuredSlaves[i].Name : "Unknown";
                    string ecState = state.DeviceState.ToString();
                    string linkState = state.LinkState == EcLinkState.Ok ? "OK" : state.LinkState.ToString();

                    // Topology info
                    string topoInfo = "";
                    if (i < topology.Length)
                    {
                        var topo = topology[i];
                        topoInfo = $"A:{topo.stPhysicalAddr.PortA:X} B:{topo.stPhysicalAddr.PortB:X} C:{topo.stPhysicalAddr.PortC:X} D:{topo.stPhysicalAddr.PortD:X}";
                    }

                    // Only update if changed
                    if (row.Cells[0].Value?.ToString() != address.ToString())
                        row.Cells[0].Value = address;
                    if (row.Cells[1].Value?.ToString() != slaveName)
                        row.Cells[1].Value = slaveName;
                    if (row.Cells[2].Value?.ToString() != ecState)
                        row.Cells[2].Value = ecState;
                    if (row.Cells[3].Value?.ToString() != linkState)
                        row.Cells[3].Value = linkState;
                    if (row.Cells[4].Value?.ToString() != abnormalCount.ToString())
                        row.Cells[4].Value = abnormalCount;
                    if (row.Cells[5].Value?.ToString() != topoInfo)
                        row.Cells[5].Value = topoInfo;

                    // Color-code the state cell
                    var stateCell = row.Cells[2];
                    var newStateColor = GetStateColor(state.DeviceState);
                    if (stateCell.Style.BackColor != newStateColor)
                    {
                        stateCell.Style.BackColor = newStateColor;
                        stateCell.Style.ForeColor = Color.Black;
                    }

                    // Color-code the link state cell
                    var linkCell = row.Cells[3];
                    var newLinkColor = state.LinkState == EcLinkState.Ok ? Color.LightGreen : Color.LightCoral;
                    if (linkCell.Style.BackColor != newLinkColor)
                    {
                        linkCell.Style.BackColor = newLinkColor;
                    }

                    // Highlight abnormal changes
                    var newAbnormalColor = abnormalCount > 0 ? Color.Yellow : Color.White;
                    if (row.Cells[4].Style.BackColor != newAbnormalColor)
                    {
                        row.Cells[4].Style.BackColor = newAbnormalColor;
                    }
                }
            }
            finally
            {
                dataGridViewSlaves.ResumeLayout();
            }
        }

        private Color GetStateColor(EcDeviceState state)
        {
            // Check for base state without flags
            var baseState = state & ~(EcDeviceState.Error | EcDeviceState.InvalidVprs | EcDeviceState.InitCmdError | EcDeviceState.Disabled);

            return baseState switch
            {
                EcDeviceState.Op => Color.LightGreen,
                EcDeviceState.SafeOp => Color.LightYellow,
                EcDeviceState.PreOp => Color.LightBlue,
                EcDeviceState.Init => Color.LightGray,
                EcDeviceState.BootStrap => Color.Lavender,
                _ => Color.White
            };
        }

        private void ClearMasterInfo()
        {
            lblMasterName.Text = "Master: -";
            lblMasterNetId.Text = "NetId: -";
            lblMasterState.Text = "State: -";
            lblMasterState.BackColor = SystemColors.Control;
            lblMasterDevState.Text = "Device State: -";
            lblMasterDevState.BackColor = SystemColors.Control;
            lblSlaveCount.Text = "Slaves: -";
            lblFrameCount.Text = "Frames: -";
            dataGridViewSlaves.Rows.Clear();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void btnClearCRC_Click(object sender, EventArgs e)
        {
            try
            {
                currentMaster?.FrameStatisticClearCRC();
                MessageBox.Show("CRC error counters cleared.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error clearing CRC counters: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClearFrames_Click(object sender, EventArgs e)
        {
            try
            {
                currentMaster?.FrameStatisticClearFrames();
                MessageBox.Show("Lost frame counters cleared.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error clearing frame counters: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRequestState_Click(object sender, EventArgs e)
        {
            if (currentMaster == null)
                return;

            using var form = CreateStateRequestForm();
            if (form.ShowDialog() != DialogResult.OK)
                return;

            if (form.Tag is EcDeviceState requestedState)
            {
                try
                {
                    currentMaster.RequestState(requestedState);
                    MessageBox.Show($"State change to {requestedState} requested.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Wait a moment and refresh
                    Thread.Sleep(500);
                    RefreshData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error requesting state: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private Form CreateStateRequestForm()
        {
            var form = new Form
            {
                Size = new Size(250, 200),
                Text = "Request Master State",
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MinimizeBox = false,
                MaximizeBox = false
            };

            var label = new Label
            {
                Text = "Select target state:",
                Location = new Point(10, 10),
                AutoSize = true
            };

            var listBox = new ListBox
            {
                Location = new Point(10, 35),
                Size = new Size(210, 80)
            };

            listBox.Items.Add(EcDeviceState.Init);
            listBox.Items.Add(EcDeviceState.PreOp);
            listBox.Items.Add(EcDeviceState.SafeOp);
            listBox.Items.Add(EcDeviceState.Op);
            listBox.SelectedIndex = 0;

            var btnOk = new Button
            {
                Text = "OK",
                DialogResult = DialogResult.OK,
                Location = new Point(60, 120),
                Size = new Size(75, 23)
            };

            var btnCancel = new Button
            {
                Text = "Cancel",
                DialogResult = DialogResult.Cancel,
                Location = new Point(145, 120),
                Size = new Size(75, 23)
            };

            btnOk.Click += (s, e) =>
            {
                if (listBox.SelectedItem != null)
                {
                    form.Tag = listBox.SelectedItem;
                }
            };

            form.Controls.AddRange(new Control[] { label, listBox, btnOk, btnCancel });
            form.AcceptButton = btnOk;
            form.CancelButton = btnCancel;

            return form;
        }

        private void timerUpdate_Tick(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void EtherCatHealthDashboard_Load(object sender, EventArgs e)
        {
            // Initialize grid columns if not already done
            if (dataGridViewSlaves.Columns.Count == 0)
            {
                dataGridViewSlaves.Columns.Add("Address", "Address");
                dataGridViewSlaves.Columns.Add("Name", "Name");
                dataGridViewSlaves.Columns.Add("State", "State");
                dataGridViewSlaves.Columns.Add("Link", "Link Status");
                dataGridViewSlaves.Columns.Add("Abnormal", "Abnormal Changes");
                dataGridViewSlaves.Columns.Add("Topology", "Topology");

                dataGridViewSlaves.Columns[0].Width = 70;
                dataGridViewSlaves.Columns[1].Width = 150;
                dataGridViewSlaves.Columns[2].Width = 100;
                dataGridViewSlaves.Columns[3].Width = 100;
                dataGridViewSlaves.Columns[4].Width = 120;
                dataGridViewSlaves.Columns[5].Width = 120;
            }
        }

        private void CreateSlaveDetailsPanel()
        {
            // Create the main group box for slave details
            groupBoxSlaveDetails = new GroupBox
            {
                Text = "Slave Details (Select a slave to view details)",
                Dock = DockStyle.Bottom,
                Height = 250,
                Visible = false
            };

            // Slave info labels (left side)
            lblSelectedSlaveName = new Label { Location = new Point(15, 25), AutoSize = true, Font = new Font("Segoe UI", 10F, FontStyle.Bold) };
            lblSelectedSlaveState = new Label { Location = new Point(15, 50), AutoSize = true };
            lblSelectedSlaveLink = new Label { Location = new Point(15, 70), AutoSize = true };
            lblSelectedVendorId = new Label { Location = new Point(15, 95), AutoSize = true };
            lblSelectedProductCode = new Label { Location = new Point(15, 115), AutoSize = true };
            lblSelectedRevision = new Label { Location = new Point(15, 135), AutoSize = true };
            lblSelectedSerial = new Label { Location = new Point(15, 155), AutoSize = true };

            // Request State button
            btnSelectedRequestState = new Button
            {
                Text = "Request State",
                Location = new Point(15, 185),
                Size = new Size(120, 30)
            };
            btnSelectedRequestState.Click += BtnSelectedRequestState_Click;

            // CRC Error group (middle)
            groupBoxSelectedCrc = new GroupBox
            {
                Text = "CRC Errors",
                Location = new Point(300, 20),
                Size = new Size(150, 150)
            };
            lblSelectedPortA = new Label { Location = new Point(10, 25), AutoSize = true };
            lblSelectedPortB = new Label { Location = new Point(10, 50), AutoSize = true };
            lblSelectedPortC = new Label { Location = new Point(10, 75), AutoSize = true };
            lblSelectedPortD = new Label { Location = new Point(10, 100), AutoSize = true };
            groupBoxSelectedCrc.Controls.AddRange(new Control[] { lblSelectedPortA, lblSelectedPortB, lblSelectedPortC, lblSelectedPortD });

            // CoE group (right side)
            groupBoxSelectedCoe = new GroupBox
            {
                Text = "CoE (CAN over EtherCAT)",
                Location = new Point(460, 20),
                Size = new Size(320, 210)
            };

            var lblCoeIndex = new Label { Text = "Index (hex):", Location = new Point(10, 25), AutoSize = true };
            txtSelectedCoeIndex = new TextBox { Location = new Point(100, 22), Size = new Size(80, 23), Text = "1000" };

            var lblCoeSubIndex = new Label { Text = "SubIndex (hex):", Location = new Point(10, 55), AutoSize = true };
            txtSelectedCoeSubIndex = new TextBox { Location = new Point(100, 52), Size = new Size(80, 23), Text = "0" };

            var lblCoeValue = new Label { Text = "Value:", Location = new Point(10, 85), AutoSize = true };
            txtSelectedCoeValue = new TextBox { Location = new Point(100, 82), Size = new Size(200, 23) };

            btnSelectedCoeRead = new Button
            {
                Text = "Read",
                Location = new Point(100, 120),
                Size = new Size(90, 30)
            };
            btnSelectedCoeRead.Click += BtnSelectedCoeRead_Click;

            btnSelectedCoeWrite = new Button
            {
                Text = "Write",
                Location = new Point(200, 120),
                Size = new Size(90, 30)
            };
            btnSelectedCoeWrite.Click += BtnSelectedCoeWrite_Click;

            groupBoxSelectedCoe.Controls.AddRange(new Control[] {
                lblCoeIndex, txtSelectedCoeIndex,
                lblCoeSubIndex, txtSelectedCoeSubIndex,
                lblCoeValue, txtSelectedCoeValue,
                btnSelectedCoeRead, btnSelectedCoeWrite
            });

            // Add all controls to the main group box
            groupBoxSlaveDetails.Controls.AddRange(new Control[] {
                lblSelectedSlaveName, lblSelectedSlaveState, lblSelectedSlaveLink,
                lblSelectedVendorId, lblSelectedProductCode, lblSelectedRevision, lblSelectedSerial,
                btnSelectedRequestState,
                groupBoxSelectedCrc,
                groupBoxSelectedCoe
            });

            // Add the group box to the control
            this.Controls.Add(groupBoxSlaveDetails);
            groupBoxSlaveDetails.BringToFront();

            // Adjust dataGridViewSlaves to make room
            dataGridViewSlaves.Height -= 250;
        }

        private ushort selectedSlaveAddress;

        private void DataGridViewSlaves_SelectionChanged(object? sender, EventArgs e)
        {
            if (dataGridViewSlaves.SelectedRows.Count == 0 || currentMaster == null)
            {
                if (groupBoxSlaveDetails != null && groupBoxSlaveDetails.Visible)
                    groupBoxSlaveDetails.Visible = false;
                selectedSlave = null;
                return;
            }

            try
            {
                var selectedRow = dataGridViewSlaves.SelectedRows[0];
                selectedSlaveAddress = Convert.ToUInt16(selectedRow.Cells[0].Value);

                selectedSlave = currentMaster.GetSlave(selectedSlaveAddress);
                UpdateSlaveDetails();

                if (groupBoxSlaveDetails != null && !groupBoxSlaveDetails.Visible)
                    groupBoxSlaveDetails.Visible = true;
            }
            catch (Exception ex)
            {
                lblError.Text = $"Error loading slave details: {ex.Message}";
                lblError.Visible = true;
            }
        }

        private void UpdateSlaveDetails()
        {
            if (selectedSlave == null || lblSelectedSlaveName == null)
                return;

            try
            {
                // Get slave data
                var configuredSlaves = currentMaster?.GetConfiguredSlaves();
                var slaveConfig = configuredSlaves?.FirstOrDefault(s => s.Addr == selectedSlaveAddress);
                string slaveName = slaveConfig?.Name ?? "Unknown";

                var identity = selectedSlave.Identity;
                var state = selectedSlave.State;

                // Update labels
                lblSelectedSlaveName!.Text = $"{selectedSlaveAddress}: {slaveName}";
                lblSelectedSlaveState!.Text = $"State: {state.DeviceState}";
                lblSelectedSlaveState.BackColor = GetStateColor(state.DeviceState);
                lblSelectedSlaveLink!.Text = $"Link: {(state.LinkState == EcLinkState.Ok ? "OK" : state.LinkState.ToString())}";
                lblSelectedSlaveLink.BackColor = state.LinkState == EcLinkState.Ok ? Color.LightGreen : Color.LightCoral;

                lblSelectedVendorId!.Text = $"Vendor ID: 0x{identity.VendorId:X8}";
                lblSelectedProductCode!.Text = $"Product Code: 0x{identity.ProductCode:X8}";
                lblSelectedRevision!.Text = $"Revision: 0x{identity.RevisionNo:X8}";
                lblSelectedSerial!.Text = $"Serial: 0x{identity.SerialNo:X8}";

                // Update CRC errors
                try
                {
                    var crc = selectedSlave.CrcError;
                    lblSelectedPortA!.Text = $"Port A: {crc.PortACount}";
                    lblSelectedPortB!.Text = $"Port B: {crc.PortBCount}";
                    lblSelectedPortC!.Text = $"Port C: {crc.PortCCount}";
                    lblSelectedPortD!.Text = $"Port D: {crc.PortDCount}";
                    if (groupBoxSelectedCrc != null)
                        groupBoxSelectedCrc.Visible = true;
                }
                catch
                {
                    if (groupBoxSelectedCrc != null)
                        groupBoxSelectedCrc.Visible = false;
                }
            }
            catch (Exception ex)
            {
                lblError.Text = $"Error updating slave details: {ex.Message}";
                lblError.Visible = true;
            }
        }

        private void BtnSelectedRequestState_Click(object? sender, EventArgs e)
        {
            if (selectedSlave == null)
                return;

            using var form = CreateSlaveStateRequestForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                if (form.Tag is EcDeviceState requestedState)
                {
                    try
                    {
                        selectedSlave.RequestState(requestedState);
                        MessageBox.Show($"State change to {requestedState} requested for slave {selectedSlaveAddress}.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        Thread.Sleep(500);
                        UpdateSlaveDetails();
                        RefreshData();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error requesting state: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private Form CreateSlaveStateRequestForm()
        {
            var form = new Form
            {
                Size = new Size(250, 200),
                Text = "Request Slave State",
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MinimizeBox = false,
                MaximizeBox = false
            };

            var label = new Label
            {
                Text = "Select target state:",
                Location = new Point(10, 10),
                AutoSize = true
            };

            var listBox = new ListBox
            {
                Location = new Point(10, 35),
                Size = new Size(210, 80)
            };

            listBox.Items.Add(EcDeviceState.Init);
            listBox.Items.Add(EcDeviceState.PreOp);
            listBox.Items.Add(EcDeviceState.SafeOp);
            listBox.Items.Add(EcDeviceState.Op);
            listBox.SelectedIndex = 0;

            var btnOk = new Button
            {
                Text = "OK",
                DialogResult = DialogResult.OK,
                Location = new Point(60, 120),
                Size = new Size(75, 23)
            };

            var btnCancel = new Button
            {
                Text = "Cancel",
                DialogResult = DialogResult.Cancel,
                Location = new Point(145, 120),
                Size = new Size(75, 23)
            };

            btnOk.Click += (s, e) =>
            {
                if (listBox.SelectedItem != null)
                {
                    form.Tag = listBox.SelectedItem;
                }
            };

            form.Controls.AddRange(new Control[] { label, listBox, btnOk, btnCancel });
            form.AcceptButton = btnOk;
            form.CancelButton = btnCancel;

            return form;
        }

        private void BtnSelectedCoeRead_Click(object? sender, EventArgs e)
        {
            if (selectedSlave == null || txtSelectedCoeIndex == null || txtSelectedCoeSubIndex == null || txtSelectedCoeValue == null)
            {
                MessageBox.Show("No slave selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                var index = Convert.ToUInt32(txtSelectedCoeIndex.Text, 16);
                var subIndex = Convert.ToUInt32(txtSelectedCoeSubIndex.Text, 16);

                // Try reading as different types
                try
                {
                    var value = selectedSlave.CoeReadAny<uint>(index, subIndex);
                    txtSelectedCoeValue.Text = $"0x{value:X} ({value})";
                }
                catch
                {
                    try
                    {
                        var value = selectedSlave.CoeReadAny<ushort>(index, subIndex);
                        txtSelectedCoeValue.Text = $"0x{value:X} ({value})";
                    }
                    catch
                    {
                        var value = selectedSlave.CoeReadAny<byte>(index, subIndex);
                        txtSelectedCoeValue.Text = $"0x{value:X} ({value})";
                    }
                }

                lblError.Visible = false;
            }
            catch (Exception ex)
            {
                lblError.Text = $"CoE Read error: {ex.Message}";
                lblError.Visible = true;
            }
        }

        private void BtnSelectedCoeWrite_Click(object? sender, EventArgs e)
        {
            if (selectedSlave == null || txtSelectedCoeIndex == null || txtSelectedCoeSubIndex == null || txtSelectedCoeValue == null)
            {
                MessageBox.Show("No slave selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                var index = Convert.ToUInt32(txtSelectedCoeIndex.Text, 16);
                var subIndex = Convert.ToUInt32(txtSelectedCoeSubIndex.Text, 16);
                var value = Convert.ToUInt32(txtSelectedCoeValue.Text, 0);

                selectedSlave.CoeWriteAny(index, subIndex, value);

                MessageBox.Show("CoE write successful.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                lblError.Visible = false;
            }
            catch (Exception ex)
            {
                lblError.Text = $"CoE Write error: {ex.Message}";
                lblError.Visible = true;
            }
        }
    }
}
