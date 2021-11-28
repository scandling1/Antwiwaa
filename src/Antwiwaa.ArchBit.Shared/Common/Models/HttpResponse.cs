using Antwiwaa.ArchBit.Shared.Interfaces;

namespace Antwiwaa.ArchBit.Shared.Common.Models
{
    public class HttpResponse<T> : HttpResponse where T : IPayLoadObject
    {
        public T Data { get; set; }
    }

    public class HttpResponse
    {
        public ExceptionDetails ExceptionDetails { get; set; }
        public bool IsFailure { get; set; }
        public bool IsSuccess { get; set; }
    }
}