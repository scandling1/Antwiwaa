using System.Net.Http;

namespace Antwiwaa.ArchBit.Shared.Interfaces
{
    public interface IPayLoadObject
    {
        HttpContent GetHttpContent();
    }
}