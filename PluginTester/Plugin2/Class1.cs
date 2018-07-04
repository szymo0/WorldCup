using System;
using System.Threading.Tasks;
using PluginCore;
using Quartz;

namespace Plugin2
{
    public class Test1:IPlugin
    {
        public IJobDetail GetJob()
        {
            return JobBuilder.Create<IterateJob>()
                .WithIdentity("IterateJob", "Plugin2")
                .Build();
        }

        public ITrigger GetTrigger()
        {
            return TriggerBuilder.Create()
                .WithIdentity("IterateJobTrigger", "Plugin2")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(3)
                    .RepeatForever())
                .Build();
        }
    }

    public class IterateJob:IJob
    {
        private static int _count = 0;
        public Task Execute(IJobExecutionContext context)
        {
            Console.WriteLine($"{this.GetType().FullName} {_count++} ssss");
            return Task.CompletedTask;
        }
    }
}
