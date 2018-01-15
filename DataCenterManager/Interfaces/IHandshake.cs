using DataCenterManager.Data;

namespace DataCenterManager.Interfaces
{
    public interface IHandshake
    {
        int PerformHandshake(IPAddress IPAddress);
    }
}
