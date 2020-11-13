using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;

namespace Taiyo.JobSchedule.Jobs
{
    public class DemoJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            await Task.Run(() =>
            {
                DBHelp.Reports.LogFile.Log("JobSchedule", "this is a testing");
            });
        }
    }
}
