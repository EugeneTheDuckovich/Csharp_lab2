using lab2_src.Controller;
using lab2_src.Forms;


namespace lab2_src;

public partial class MainMenuForm : Form
{
    private IPortController _controller;
    private MainMenuForm()
    {
        InitializeComponent();
        this.Closed += CommonEvents.OnClosing;
        this.CenterToScreen();

        if (_controller == null) _controller = new PortController();

        var createNewPortButton = new Button();
        createNewPortButton.Text = "Create new port";
        createNewPortButton.Location = new Point(this.Size.Width/2 - 100, this.Height/2 - 100);
        createNewPortButton.Size = new Size(200, 30);
        createNewPortButton.TabIndex = 11;
        createNewPortButton.Click += CreateNewPortButton_Click;

        var viewAllPortsButton = new Button();
        viewAllPortsButton.Text = "View all ports";
        viewAllPortsButton.Location = 
            new Point(createNewPortButton.Location.X, createNewPortButton.Bottom + Padding.Top);
        viewAllPortsButton.Size = createNewPortButton.Size;
        viewAllPortsButton.TabIndex = 11;
        viewAllPortsButton.Click += ViewAllPortsButton_Click;

        var exitButton = new Button();
        exitButton.Text = "Exit";
        exitButton.Location =
            new Point(viewAllPortsButton.Location.X, viewAllPortsButton.Bottom + Padding.Top);
        exitButton.Size = createNewPortButton.Size;
        exitButton.TabIndex = 11;
        exitButton.Click += CommonEvents.OnClosing;

        Controls.Add(createNewPortButton);
        Controls.Add(viewAllPortsButton);
        Controls.Add(exitButton);
    }

    public MainMenuForm(IPortController controller) : this()
    {
        _controller = controller;
    }

    private void CreateNewPortButton_Click(object? sender, EventArgs e)
    {
        CommonEvents.ChangeForm(this, FormType.PortCreation, _controller);
    }

    private void ViewAllPortsButton_Click(object? sender, EventArgs e)
    {
        CommonEvents.ChangeForm(this, FormType.PortView, _controller);
    }
}