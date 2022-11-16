using lab2_src.Model;

namespace lab2_src.Controller;

internal interface IPortController
{
    IPort[] GetPorts();
    void AddPort(IPort port);
    void RemovePort(IPort port);
    IPort GetPortByName(string name);
    void Clean();

}
