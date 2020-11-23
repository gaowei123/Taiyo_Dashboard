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
            Taiyo.Tool.LogHelper.Log("********************In Func StartJob********************");


            //创建一个调度器
            factory = new StdSchedulerFactory();
            scheduler = await factory.GetScheduler();
            

            //创建一个任务
            IJobDetail job = JobBuilder.Create<Jobs.JobGenerateDailyInventory>()
                .WithIdentity("job1", "group1")
                .Build();
            


            //创建一个触发器
            ITrigger trigger = TriggerBuilder.Create()
               .WithIdentity("trigger1", "group1")
               .WithCronSchedule(" 0 0 15 * * ? ")//每天下午3点触发
               .Build();
            



            //将任务与触发器添加到调度器中
            await scheduler.ScheduleJob(job, trigger);
            
            



            //开始作业
            await scheduler.Start();
            Taiyo.Tool.LogHelper.Log("********************Start Schdule********************");
        }



        public void CloseJob()
        {
            if (scheduler != null)
            {
                scheduler.Shutdown(true);
                Taiyo.Tool.LogHelper.Log("********************Shutdown Schdule********************");
            }
        }


    }
}
