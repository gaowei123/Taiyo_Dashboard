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
            //创建一个调度器
            factory = new StdSchedulerFactory();
            scheduler = await factory.GetScheduler();
            

            //创建一个任务
            IJobDetail job = JobBuilder.Create<Jobs.DemoJob>().Build();
            


            //创建一个触发器
            ITrigger trigger = TriggerBuilder.Create().WithCronSchedule("0 0 0/1 * * ?").Build();
            


            //将任务与触发器添加到调度器中
            await scheduler.ScheduleJob(job, trigger);

            

            //开始作业
            await scheduler.Start();            
        }



        public void CloseJob()
        {
            if (scheduler != null)
            {
                scheduler.Shutdown(true);
            }
        }

    }
}
