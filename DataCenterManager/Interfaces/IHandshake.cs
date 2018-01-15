using DataCenterManager.Data;

namespace DataCenterManager.Interfaces
{
    interface IHandshake
    {
        void PerformHandshake(IPAddress IPAddress);
    }
}
