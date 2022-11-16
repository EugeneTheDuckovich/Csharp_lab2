

namespace lab2_src.Model;

internal class Port : IPort
{
    public string Name { get; }
    public string Adress { get; }

    private int _workersAmount;
    private int _vehiclesAmount;
    private int _docksAmount;
    public int WorkersAmount { get => _workersAmount; }

    public int VehiclesAmount { get => _vehiclesAmount; }
    public int VehiclePrice { get; }

    public int DocksAmount { get => _docksAmount; }

    public int ShipServiceTime { get; }
    public int ShipServicePrice { get; }

    public Port(string name, string adress, int shipServiseTime, int shipServicePrice, int docksAmount)
    {
        Name = name;
        Adress = adress;
        ShipServiceTime = shipServiseTime;
        ShipServicePrice = shipServicePrice;
        _docksAmount = docksAmount;
        _vehiclesAmount = docksAmount * 5;
        _workersAmount = docksAmount * 15;
    }

    public Port(Port other)
    {
        Name = other.Name;
        Adress = other.Adress;
        ShipServicePrice = other.ShipServicePrice;
        ShipServiceTime = other.ShipServiceTime;
        _docksAmount = other._docksAmount;
        _vehiclesAmount = other._vehiclesAmount;
        _workersAmount = other._workersAmount;
    }

    public static Port operator ++(Port port)
    {
        port._docksAmount++;
        port._vehiclesAmount += 5;
        return port;
    }

    private int GetFunctioningDocksAmount()
    {
        var min = Math.Min(_docksAmount, _workersAmount / 15);
        return Math.Min(min, _vehiclesAmount/5);
    }

    public static bool operator >=(Port first, Port second)
    {
        return first.GetFunctioningDocksAmount() >= second.GetFunctioningDocksAmount();
    }

    public static bool operator <=(Port first, Port second)
    {
        return second >= first;
    }

    public void HireWorker()
    {
        _workersAmount++;
    }

    public void HireSeveralWorkers(int hiredWorkersAmount)
    {
        _workersAmount += hiredWorkersAmount;
    }
    
    public void FireWorker()
    {
        if (_workersAmount > 0) _workersAmount--;
    }

    public void FireSeveralWorkers(int firedWorkersAmount)
    {
        if (_workersAmount <= firedWorkersAmount) _workersAmount = 0;
        else _workersAmount -= firedWorkersAmount;
    }

    public int GetIncomeAfterService(int shipsAmount)
    {
        return (ShipServicePrice * shipsAmount) / (VehiclePrice * 5 * shipsAmount);
    }

    public override bool Equals(object? obj)
    {
        if(obj == null) return false;
        if(obj.GetType() != this.GetType()) return false;
        var portObj = (Port)obj;
        return this.Name == portObj.Name && this.Adress == portObj.Adress;
    }

    public override int GetHashCode()
    {
        return Name.GetHashCode() ^ Adress.GetHashCode();
    }

}