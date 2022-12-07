using lab2_src.Controller;
using lab2_src.Forms;
using lab2_src.Model;

namespace lab2_src;

public partial class PortCreationForm : Form
{
    private IPortController _controller;
    private TextBox[] _textBoxes;

    public PortCreationForm(IPortController controller)
    {
        InitializeComponent();
        this.Closed += CommonEvents.OnClosing;
        this.Text = "Port creation page";
        this.CenterToScreen();

        var toolBar = new ToolStrip();
        toolBar.Text = "menu";
        
        var item = new ToolStripMenuItem();
        item.Text = "go to main menu";
        item.Click += GoToMainMenuToolBoxItem_Click;
        toolBar.Items.Add(item);

        item = new ToolStripMenuItem();
        item.Text = "go to ports view page";
        item.Click += GoToPortsViewToolBoxitem_Click;
        toolBar.Items.Add(item);

        _controller = controller;

        _controller.PortsListBox = new ListBox();
        _controller.PortsListBox.Size = new Size(200, 300);
        _controller.PortsListBox.Location = new Point(10, toolBar.Bottom + Padding.Top);
        _controller.UpdateListBox();


        _textBoxes = new TextBox[6];
        var labels = new Label[6];
        var textBoxSize = new Size(200, 30);
        var labelSize = new Size(200, 20);

        var inputPanel = new Panel();
        inputPanel.Location =
            new Point(_controller.PortsListBox.Bounds.Right + 10, _controller.PortsListBox.Location.Y);
        inputPanel.BackColor = Color.White;
        inputPanel.AutoSize = true;

        for (int i = 0; i < 6; i++)
        {
            labels[i] = new Label();
            labels[i].Location = i == 0 ? new Point(0, 0) :
                new Point(_textBoxes[i - 1].Location.X, _textBoxes[i - 1].Bounds.Bottom + Padding.Top);
            labels[i].Size = labelSize;

            _textBoxes[i] = new TextBox();
            _textBoxes[i].Location = new Point(labels[i].Location.X, labels[i].Bounds.Bottom);
            _textBoxes[i].Size = textBoxSize;
        }

        labels[0].Text = "Name:";
        labels[1].Text = "Adress:";
        labels[2].Text = "Count of docks:";
        labels[3].Text = "One ship service time(hours):";
        labels[4].Text = "One ship service price:";
        labels[5].Text = "Vehicle price:";

        inputPanel.Controls.AddRange(labels);
        inputPanel.Controls.AddRange(_textBoxes);
        inputPanel.BorderStyle = BorderStyle.FixedSingle;

        var removePortButton = new Button();
        removePortButton.Text = "delete port";
        removePortButton.Location = new Point(_controller.PortsListBox.Location.X, _controller.PortsListBox.Bottom);
        removePortButton.Size = new Size(200, 30);
        removePortButton.Click += RemovePortButton_Click;

        var createPortButton = new Button();
        createPortButton.Text = "create port";
        createPortButton.Location = new Point(labels[5].Location.X, _textBoxes[5].Bottom + 5);
        createPortButton.Size = new Size(200, 30);
        createPortButton.BackColor = removePortButton.BackColor;
        createPortButton.Click += CreatePortButton_Click;

        inputPanel.Controls.Add(createPortButton);



        Controls.AddRange(new Control[] {
            _controller.PortsListBox,
            removePortButton,
            inputPanel,
            toolBar
        });
    }

    private void GoToMainMenuToolBoxItem_Click(object? sender, EventArgs e)
    {
        CommonEvents.ChangeForm(this, FormType.MainMenu, _controller);
    }

    private void GoToPortsViewToolBoxitem_Click(object? sender, EventArgs e)
    {
        CommonEvents.ChangeForm(this, FormType.PortView, _controller);
    }

    private void CreatePortButton_Click(object? sender, EventArgs e)
    {
        Port? port = InputToPort();
        if (port == null) return;

        _controller.AddPort(port);
        _controller.UpdateListBox();
    }

    private Port? InputToPort()
    {
        var input = _textBoxes.Select(t => t.Text).ToArray();
        var parsedInput = new int[4];
        if(input.Any(t => String.IsNullOrEmpty(t)))
        {
            return null;
        }

        if (input[0].Any(c => char.IsDigit(c))) return null;

        for(int i = 2; i < 6; i++)
        {
            if (!int.TryParse(input[i], out parsedInput[i - 2])) return null;
        }

        return new Port(input[0], input[1], parsedInput[0], parsedInput[1], parsedInput[2], parsedInput[3]);
    }

    private void RemovePortButton_Click(object? sender, EventArgs e)
    {
        var port = _controller.Ports.FirstOrDefault(p => p.Name == _controller.PortsListBox.Text);
        
        if (port == null) return;

        _controller.RemovePort(port);

        _controller.UpdateListBox();
    }

}