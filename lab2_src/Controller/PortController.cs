using lab2_src.Model;
using System.Collections.Generic;

namespace lab2_src.Controller;

public class PortController : IPortController
{
    private Dictionary<string,IPort> _ports;
    public ListBox PortsListBox { get; set; }

    public PortController()
    {
        _ports = new Dictionary<string, IPort>();
        PortsListBox = new ListBox();
    }

    public void AddPort(IPort port)
    {
        try 
        {
            _ports.Add(port.Name, port);
        }
        catch(ArgumentException)
        {
            MessageBox.Show("this port already exists!", "warning");
        }
    }

    public void Clear()
    {
        _ports.Clear();
    }

    public IPort? GetPortByName(string name)
    {
        try
        {
            return _ports[name];
        }
        catch (ArgumentException)
        {
            return null;
        }
    }

    public IPort[] Ports
    {
        get=> _ports.Values.ToArray();
    }

    public void RemovePort(IPort port)
    {
        var pair = _ports.FirstOrDefault(p => p.Value == port);
        if(pair.Equals(default(KeyValuePair<string, Port>))) return;

        _ports.Remove(pair.Key);
    }

    public void UpdateListBox()
    {
        PortsListBox.BeginUpdate();
        PortsListBox.Items.Clear();
        foreach (var port in Ports)
        {
            PortsListBox.Items.Add(port.Name);
        }
        PortsListBox.EndUpdate();
    }

}