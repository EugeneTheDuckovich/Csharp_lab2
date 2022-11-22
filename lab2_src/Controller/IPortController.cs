using lab2_src.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2_src.Controller;

public interface IPortController
{
    ListBox PortsListBox { get; set; }

    void UpdateListBox();

    public void AddPort(IPort port);

    public IPort? GetPortByName(string name);

    public IPort[] Ports { get; }

    public void RemovePort(IPort port);

    public void Clear();
}
