using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;
using Quartz.Impl;

namespace Taiyo.JobSchedule
{
    public class InventorySchedule
    {
        //调度器
        IScheduler scheduler;

        //调度器工厂
        ISchedulerFactory factory;
        

        public async void StartJob()
        {

            DBHelp.Reports.LogFile.Log("JobGenerateDailyInventory", "StartJob");


            //创建一个调度器
            factory = new StdSchedulerFactory();
            scheduler = await factory.GetScheduler();

            DBHelp.Reports.LogFile.Log("JobGenerateDailyInventory", "GetScheduler");

            //创建一个任务
            IJobDetail job = JobBuilder.Create<Jobs.JobGenerateDailyInventory>()
                .WithIdentity("job1", "group1")
                .Build();

            DBHelp.Reports.LogFile.Log("JobGenerateDailyInventory", "CreateJob");


            //创建一个触发器
            ITrigger trigger = TriggerBuilder.Create()
               .WithIdentity("trigger1", "group1")
               .WithCronSchedule(" 0 0 19 * * ? ")//每天下午3点触发
               .Build();

            DBHelp.Reports.LogFile.Log("JobGenerateDailyInventory", "create trigger");


            //将任务与触发器添加到调度器中
            await scheduler.ScheduleJob(job, trigger);
            

            DBHelp.Reports.LogFile.Log("JobGenerateDailyInventory", "Add job and trigger in schedule");



            //开始作业
            await scheduler.Start();

            DBHelp.Reports.LogFile.Log("JobGenerateDailyInventory", "start schedule");
        }



        public void CloseJob()
        {
            if (scheduler != null)
            {
                scheduler.Shutdown(true);

                DBHelp.Reports.LogFile.Log("JobGenerateDailyInventory", "close schedule");
            }
        }


    }
}
