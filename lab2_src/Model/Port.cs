

namespace lab2_src.Model;

public class Port : IPort
{
    public string Name { get; }
    public string Adress { get; }

    public int WorkersAmount { get; private set; }

    public int VehiclesAmount { get; private set; }
    public int VehiclePrice { get; }

    public int DocksAmount { get; private set; }

    public int ShipServiceTime { get; }
    public int ShipServicePrice { get; }

    public int FunctionigDocks 
    { 
        get => Math.Min(Math.Min(DocksAmount, WorkersAmount / 15), VehiclesAmount / 5);
    }

    public Port(string name, string adress, int docksAmount, int shipServiseTime, int shipServicePrice, int vehiclePrice)
    {
        Name = name;
        Adress = adress;
        DocksAmount = docksAmount;
        ShipServiceTime = shipServiseTime;
        ShipServicePrice = shipServicePrice;
        VehiclePrice = vehiclePrice;
        VehiclesAmount = docksAmount * 5;
        WorkersAmount = docksAmount * 15;
    }

    public Port(Port other)
    {
        Name = other.Name;
        Adress = other.Adress;
        ShipServicePrice = other.ShipServicePrice;
        ShipServiceTime = other.ShipServiceTime;
        DocksAmount = other.DocksAmount;
        VehiclesAmount = other.VehiclesAmount;
        WorkersAmount = other.WorkersAmount;
    }

    public static Port operator ++(Port port)
    {
        port.DocksAmount++;
        port.VehiclesAmount += 5;
        return port;
    }

    public void HireWorker()
    {
        WorkersAmount++;
    }

    public void HireSeveralWorkers(int hiredWorkersAmount)
    {
        WorkersAmount += hiredWorkersAmount;
    }
    
    public void FireWorker()
    {
        if (WorkersAmount > 0) WorkersAmount--;
    }

    public void FireSeveralWorkers(int firedWorkersAmount)
    {
        if (WorkersAmount <= firedWorkersAmount) WorkersAmount = 0;
        else WorkersAmount -= firedWorkersAmount;
    }

    public int GetIncomeAfterService(int shipsAmount)
    {
        return (ShipServicePrice - VehiclePrice * 5) * shipsAmount;
    }

    public override int GetHashCode()
    {
        return Name.GetHashCode() ^ Adress.GetHashCode();
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as Port);
    }

    public bool Equals(IPort? other)
    {
        if(other == null) return false;
        if(ReferenceEquals(this, other)) return true;

        return this.GetHashCode() == other.GetHashCode();
    }

}