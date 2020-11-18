using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;
using Quartz.Impl;

namespace Taiyo.JobSchedule
{
   public  class DemoSchedule
    {

        //调度器
       public static IScheduler scheduler;

        //调度器工厂
        ISchedulerFactory factory;


        public async void StartJob()
        {
            DBHelp.Reports.LogFile.Log("JobSchedule", "In Func");


            //创建一个调度器
            factory = new StdSchedulerFactory();
            scheduler = await factory.GetScheduler();

            DBHelp.Reports.LogFile.Log("JobSchedule", "Create Scheduler");


           



            //创建一个任务
            IJobDetail job = JobBuilder.Create<Jobs.DemoJob>().Build();

            DBHelp.Reports.LogFile.Log("JobSchedule", "Create Job");



            //创建一个触发器
            ITrigger trigger = TriggerBuilder.Create().WithCronSchedule("0/10 * * * * ?").Build();

            DBHelp.Reports.LogFile.Log("JobSchedule", "Create trigger");




            //将任务与触发器添加到调度器中
            await scheduler.ScheduleJob(job, trigger);

            DBHelp.Reports.LogFile.Log("JobSchedule", "Add job trigger in schedule");





            //开始作业
            await scheduler.Start();

            DBHelp.Reports.LogFile.Log("JobSchedule", "schedule start");
        }



        public void CloseJob()
        {
            if (scheduler != null)
            {
                scheduler.Shutdown(true);
                DBHelp.Reports.LogFile.Log("JobSchedule", "Close Job");
            }
        }

    }
}
