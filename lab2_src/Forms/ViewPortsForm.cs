using lab2_src.Controller;
using lab2_src.Forms;
using lab2_src.Model;
using lab2_src.PortExtentions;

namespace lab2_src;

public partial class PortsViewForm : Form
{
    private IPortController _controller;
    private TextBox[] _dataTextBoxes;
    private TextBox[] _shipServiceTextBoxes;
    private TextBox[] _portComparisonTextBoxes;
    private IPort? _selectedPort;

    public PortsViewForm(IPortController controller)
    {
        InitializeComponent();
        this.Text = "Ports view page";
        this.Closed += CommonEvents.OnClosing;
        this.CenterToScreen();

        var toolBar = new ToolStrip();
        toolBar.Text = "menu";

        var item = new ToolStripMenuItem();
        item.Text = "go to main menu";
        item.Click += GoToMainMenuToolBoxItem_Click;
        toolBar.Items.Add(item);

        item = new ToolStripMenuItem();
        item.Text = "go to port creation page";
        item.Click += GoToPortCreationToolBoxitem_Click;
        toolBar.Items.Add(item);

        _controller = controller;

        _controller.PortsListBox = new ListBox();
        _controller.PortsListBox.Location = new Point(10, toolBar.Bottom + Padding.Top);
        _controller.PortsListBox.Size = new Size(200, 300);
        _controller.PortsListBox.SelectedValueChanged += PortsListBox_SelectedValueChanged;
        _controller.UpdateListBox();

        #region output data
        _dataTextBoxes = new TextBox[9];
        var outputLabels = new Label[9];
        var textBoxSize = new Size(200, 30);
        var labelSize = new Size(200, 20);

        var outputPanel = new Panel();
        outputPanel.Location =
            new Point(_controller.PortsListBox.Bounds.Right + 10, _controller.PortsListBox.Location.Y);
        outputPanel.BackColor = Color.White;
        outputPanel.AutoSize = true;
        outputPanel.BorderStyle = BorderStyle.FixedSingle;

        for (int i = 0; i < _dataTextBoxes.Length; i++)
        {
            outputLabels[i] = new Label();
            outputLabels[i].Location = i == 0? new Point(0, 0) : 
                new Point(_dataTextBoxes[i-1].Location.X, _dataTextBoxes[i-1].Bounds.Bottom + Padding.Top);
            outputLabels[i].Size = new Size(200, 20);

            _dataTextBoxes[i] = new TextBox();
            _dataTextBoxes[i].ReadOnly = true;
            _dataTextBoxes[i].BackColor = Color.White;
            _dataTextBoxes[i].Location = new Point(outputLabels[i].Location.X, outputLabels[i].Bounds.Bottom);
            _dataTextBoxes[i].Size = textBoxSize;
        }

        outputLabels[0].Text = "Name:";
        outputLabels[1].Text = "Adress:";
        outputLabels[2].Text = "Count of docks:";
        outputLabels[3].Text = "Count of functioning docks:";
        outputLabels[4].Text = "One ship service time(hours):";
        outputLabels[5].Text = "One ship service price :";
        outputLabels[6].Text = "Count of vehicles:";
        outputLabels[7].Text = "Vehicle price:";
        outputLabels[8].Text = "count of workers:";

        outputPanel.Controls.AddRange(outputLabels);
        outputPanel.Controls.AddRange(_dataTextBoxes);
#endregion

        var functionalityPanel = new Panel();
        functionalityPanel.Location =
            new Point(outputPanel.Bounds.Right + 10, outputPanel.Location.Y);
        functionalityPanel.BackColor = Color.White;
        functionalityPanel.AutoSize = true;
        functionalityPanel.BorderStyle = BorderStyle.FixedSingle;

        #region ship service

        var shipsServiceLabels = new Label[4];
        _shipServiceTextBoxes = new TextBox[3];

        for(int i = 0; i < shipsServiceLabels.Length; i++)
        {
            shipsServiceLabels[i] = new Label();
            shipsServiceLabels[i].Location = i==0? new Point(0,0) :
                new Point(shipsServiceLabels[i-1].Location.X, shipsServiceLabels[i-1].Bottom + Padding.Top);
            shipsServiceLabels[i].Size = i == 0 ? labelSize : new Size(100,30);
        }
        for(int i = 0; i < _shipServiceTextBoxes.Length; i++)
        {
            _shipServiceTextBoxes[i] = new TextBox();
            _shipServiceTextBoxes[i].Location = 
                new Point(shipsServiceLabels[i+1].Right + Padding.Left, shipsServiceLabels[i+1].Location.Y);
            _shipServiceTextBoxes[i].Size = new Size(100,30);
            _shipServiceTextBoxes[i].ReadOnly = i != 0;
            _shipServiceTextBoxes[i].BackColor = Color.White;
        }

        shipsServiceLabels[0].Text = "Ships servise";
        shipsServiceLabels[0].TextAlign = ContentAlignment.MiddleCenter;
        shipsServiceLabels[1].Text = "ships count:";
        shipsServiceLabels[2].Text = "service time:";
        shipsServiceLabels[3].Text = "income:";

        var shipServiceButton = new Button();
        shipServiceButton.Text = "Calculate";
        shipServiceButton.Location = new Point(shipsServiceLabels[3].Left, shipsServiceLabels[3].Bottom + Padding.Top);
        shipServiceButton.Size = new Size(200,30);
        shipServiceButton.Click += ShipServiceButton_Click;

        functionalityPanel.Controls.AddRange(shipsServiceLabels);
        functionalityPanel.Controls.AddRange(_shipServiceTextBoxes);
        functionalityPanel.Controls.Add(shipServiceButton);

        #endregion

        #region workers manipulations

        var workersLabel = new Label();
        workersLabel.Text = "Manipulations with workers";
        workersLabel.TextAlign = ContentAlignment.MiddleCenter;
        workersLabel.Location = new Point(shipServiceButton.Left, shipServiceButton.Bottom + 10);
        workersLabel.Size = labelSize;

        var hireButton = new Button();
        hireButton.Text = "Hire";
        hireButton.Location = new Point(workersLabel.Left, workersLabel.Bottom + Padding.Top);
        hireButton.Size = new Size(100, 30);
        hireButton.Click += HireButton_Click;

        var fireButton = new Button();
        fireButton.Text = "Fire";
        fireButton.Location = new Point(hireButton.Right + Padding.Left, hireButton.Location.Y);
        fireButton.Size = hireButton.Size;
        fireButton.Click += FireButton_Click;

        functionalityPanel.Controls.Add(workersLabel);
        functionalityPanel.Controls.Add(hireButton);
        functionalityPanel.Controls.Add(fireButton);

        #endregion

        #region docks incrementation

        var docksIncrementLabel = new Label();
        docksIncrementLabel.Text = "Docks incrementation";
        docksIncrementLabel.TextAlign = ContentAlignment.MiddleCenter;
        docksIncrementLabel.Location = new Point(hireButton.Left, hireButton.Bottom + 10);
        docksIncrementLabel.Size = labelSize;

        var docksIncrementButton = new Button();
        docksIncrementButton.Text = "Increment number of docks";
        docksIncrementButton.TextAlign = ContentAlignment.MiddleCenter;
        docksIncrementButton.Location = new Point(docksIncrementLabel.Left, docksIncrementLabel.Bottom + Padding.Top);
        docksIncrementButton.Size = new Size(200, 30);
        docksIncrementButton.Click += DocksIncrementButton_Click;

        functionalityPanel.Controls.Add(docksIncrementLabel);
        functionalityPanel.Controls.Add(docksIncrementButton);

        #endregion

        #region docks comparison

        var portComparisonLabels = new Label[4];
        _portComparisonTextBoxes = new TextBox[3];

        for (int i = 0; i < portComparisonLabels.Length; i++)
        {
            portComparisonLabels[i] = new Label();
            portComparisonLabels[i].Text = "input amount of ships";
            portComparisonLabels[i].Location = i == 0 ? 
                new Point(docksIncrementButton.Location.X, docksIncrementButton.Bottom + 10) :
                new Point(portComparisonLabels[i - 1].Location.X, portComparisonLabels[i - 1].Bottom + Padding.Top);
            portComparisonLabels[i].Size = i == 0 ? labelSize : new Size(100, 30);
        }
        for (int i = 0; i < _shipServiceTextBoxes.Length; i++)
        {
            _portComparisonTextBoxes[i] = new TextBox();
            _portComparisonTextBoxes[i].Location =
                new Point(shipsServiceLabels[i + 1].Right + Padding.Left, portComparisonLabels[i + 1].Location.Y);
            _portComparisonTextBoxes[i].Size = new Size(100, 30);
            _portComparisonTextBoxes[i].ReadOnly = i == _shipServiceTextBoxes.Length - 1;
            _portComparisonTextBoxes[i].BackColor = Color.White;
        }

        portComparisonLabels[0].Text = "Ports comp (func docks)";
        portComparisonLabels[0].TextAlign = ContentAlignment.MiddleCenter;
        portComparisonLabels[1].Text = "input first port:";
        portComparisonLabels[2].Text = "unput second port:";
        portComparisonLabels[3].Text = "result:";

        var portComparisonButton = new Button();
        portComparisonButton.Text = "compare";
        portComparisonButton.TextAlign = ContentAlignment.MiddleCenter;
        portComparisonButton.Location = 
            new Point(portComparisonLabels[3].Left, portComparisonLabels[3].Bottom + Padding.Top);
        portComparisonButton.Size = new Size(200, 30);
        portComparisonButton.Click += PortComparisonButton_Click;


        functionalityPanel.Controls.AddRange(portComparisonLabels);
        functionalityPanel.Controls.AddRange(_portComparisonTextBoxes);
        functionalityPanel.Controls.Add(portComparisonButton);

        #endregion


        Controls.AddRange( new Control[] { 
            _controller.PortsListBox,
            outputPanel,
            functionalityPanel,
            toolBar
        });

    }

    private void PortComparisonButton_Click(object? sender, EventArgs e)
    {
        var first = _controller.FindPortByName(_portComparisonTextBoxes[0].Text);
        var second = _controller.FindPortByName(_portComparisonTextBoxes[1].Text);
        if (first == null || second == null) return;

        if (first.Equals(second)) _portComparisonTextBoxes[2].Text = $"{first.Name} = {second.Name}";
        else if(first >= second) _portComparisonTextBoxes[2].Text = $"{first.Name} > {second.Name}";
        else _portComparisonTextBoxes[2].Text = $"{first.Name} < {second.Name}";
    }

    private void DocksIncrementButton_Click(object? sender, EventArgs e)
    {
        if (_selectedPort == null) return;

        var asPort = _selectedPort as Port;
        asPort++;

        PortsListBox_SelectedValueChanged(new object(), new EventArgs());
    }

    private void FireButton_Click(object? sender, EventArgs e)
    {
        if (_selectedPort == null) return;

        _selectedPort.FireWorker();
        PortsListBox_SelectedValueChanged(new object(), new EventArgs());
    }

    private void HireButton_Click(object? sender, EventArgs e)
    {
        if (_selectedPort == null) return;

        _selectedPort.HireWorker();
        PortsListBox_SelectedValueChanged(new object(), new EventArgs());
    }

    private void ShipServiceButton_Click(object? sender, EventArgs e)
    {
        if (_selectedPort == null) return;

        if (string.IsNullOrEmpty(_shipServiceTextBoxes[0].Text)) return;

        int parsedShipsAmount = 0;

        if (!int.TryParse(_shipServiceTextBoxes[0].Text, out parsedShipsAmount)) return;

        _shipServiceTextBoxes[1].Text = _selectedPort.GetServiceTime(parsedShipsAmount).ToString();
        _shipServiceTextBoxes[2].Text = _selectedPort.GetIncomeAfterService(parsedShipsAmount).ToString();
    }

    private void PortsListBox_SelectedValueChanged(object? sender, EventArgs e)
    {
        var name = _controller.PortsListBox.SelectedItem.ToString();
        if (string.IsNullOrEmpty(name)) return;

        _selectedPort = _controller.FindPortByName(name);
        if(_selectedPort == null) return;

        _dataTextBoxes[0].Text = _selectedPort.Name;
        _dataTextBoxes[1].Text = _selectedPort.Adress;
        _dataTextBoxes[2].Text = _selectedPort.DocksAmount.ToString();
        _dataTextBoxes[3].Text = _selectedPort.FunctionigDocks.ToString();
        _dataTextBoxes[4].Text = _selectedPort.ShipServiceTime.ToString();
        _dataTextBoxes[5].Text = _selectedPort.ShipServicePrice.ToString();
        _dataTextBoxes[6].Text = _selectedPort.VehiclesAmount.ToString();
        _dataTextBoxes[7].Text = _selectedPort.VehiclePrice.ToString();
        _dataTextBoxes[8].Text = _selectedPort.WorkersAmount.ToString();
    }

    private void GoToMainMenuToolBoxItem_Click(object? sender, EventArgs e)
    {
        CommonEvents.ChangeForm(this, FormType.MainMenu, _controller);
    }

    private void GoToPortCreationToolBoxitem_Click(object? sender, EventArgs e)
    {
        CommonEvents.ChangeForm(this, FormType.PortCreation, _controller);
    }

}