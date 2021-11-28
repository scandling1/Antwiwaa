using System;
using System.Threading.Tasks;
using Quartz;

namespace Antwiwaa.ArchBit.Application.StudentManagement.IndexNumbers.Commands
{
    public class HelloJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            await Console.Out.WriteLineAsync("Greetings from HelloJob!");
        }
    }
}