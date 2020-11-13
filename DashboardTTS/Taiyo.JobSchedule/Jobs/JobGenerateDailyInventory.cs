using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz; 

namespace Taiyo.JobSchedule.Jobs
{
    public class JobGenerateDailyInventory : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            await Task.Run(() =>
            {
                DBHelp.Reports.LogFile.Log("JobGenerateDailyInventory", "********************Start Job********************");



                DBHelp.Reports.LogFile.Log("JobGenerateDailyInventory", "do job....");




                DBHelp.Reports.LogFile.Log("JobGenerateDailyInventory", "********************End Job********************");
            });
        }
    }
}
