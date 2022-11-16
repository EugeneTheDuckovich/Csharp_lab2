using lab2_src.Model;

namespace lab2_src.PortExtentions;

internal static class PortExtentions
{
    public static int GetServiceTime(this IPort port, int shipsAmount)
    {
        return port.ShipServiceTime * shipsAmount;
    }
}
