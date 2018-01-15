using DataCenterManager.Data;

namespace DataCenterManager.Interfaces
{
    public interface IHandshake
    {
        void PerformHandshake(IPAddress IPAddress);
    }
}
