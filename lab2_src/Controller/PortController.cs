using lab2_src.Model;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

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

    public IPort? FindPortByName([DisallowNull]string name)
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
        var pair = _ports.FirstOrDefault(p => p.Value == port).Key;
        if(string.IsNullOrEmpty(pair)) return;

        _ports.Remove(pair);
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