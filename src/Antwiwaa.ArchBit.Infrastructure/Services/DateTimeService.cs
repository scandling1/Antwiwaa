using System;
using Antwiwaa.ArchBit.Application.Common.Interfaces;

namespace Antwiwaa.ArchBit.Infrastructure.Services
{
    public class DateTimeService : IDateTimeService
    {
        public DateTime Now => DateTime.UtcNow;
    }
}