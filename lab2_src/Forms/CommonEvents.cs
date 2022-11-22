using lab2_src.Controller;


namespace lab2_src.Forms;

public static class CommonEvents
{
    public static void OnClosing(object? sender, EventArgs e)
    {
        Application.Exit();
    }

    public static void ChangeForm(Form currentForm, FormType type, IPortController controller)
    {
        Form form;
        switch (type) 
        {
            case FormType.PortView:
                form = new PortsViewForm(controller);
                break;
            case FormType.PortCreation:
                form = new PortCreationForm(controller);
                break;
            default:
                form = new MainMenuForm(controller);
                break;
        }

        form.Show();
        currentForm.Hide();
    }

}
