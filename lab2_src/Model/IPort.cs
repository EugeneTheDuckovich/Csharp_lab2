using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2_src.Model;

public interface IPort : IEquatable<IPort>
{
    string Name { get; }
    string Adress { get; }
    int WorkersAmount { get; }
    int VehiclesAmount { get; }
    int VehiclePrice { get; }
    int DocksAmount { get; }
    int FunctionigDocks { get; }
    int ShipServiceTime { get; }
    int ShipServicePrice { get; }
    void HireWorker();
    void HireSeveralWorkers(int hiredWorkersAmount);
    void FireWorker();
    void FireSeveralWorkers(int firedWorkersAmount);
    int GetIncomeAfterService(int shipsAmount);
}