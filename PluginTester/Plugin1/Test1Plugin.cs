using System;
using System.Collections.Generic;
using System.Text;
using PluginCore;
using Quartz;

namespace Plugin1
{
    public class Test1:IPlugin
    {
        public IJobDetail GetJob()
        {
            return JobBuilder.Create<Test1Job>()
                .WithIdentity("Test1","Plugin1")
                .Build();
        }

        public ITrigger GetTrigger()
        {
            return TriggerBuilder.Create()
                .WithIdentity("Test1Trigger", "Plugin1")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(3)
                    .RepeatForever())
                .Build();
        }
    }
    public class Test2 : IPlugin
    {
        public IJobDetail GetJob()
        {
            return JobBuilder.Create<Test2Job>()
                .WithIdentity("Test2", "Plugin1")
                .Build();
        }

        public ITrigger GetTrigger()
        {
            return TriggerBuilder.Create()
                .WithIdentity("Test2Trigger", "Plugin1")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(2)
                    .RepeatForever())
                .Build();
        }
    }
}
