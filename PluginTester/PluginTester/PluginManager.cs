using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using PluginCore;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;

namespace PluginTester
{
    public class Tmp
    {
        public IPlugin Plugin { get; set; }
        public IJobDetail Job { get; set; }
        public ITrigger Trigger { get; set; }
        public IScheduler Scheduler { get; set; }
    }
    public class PluginManager
    {
        private IScheduler _scheduler;
        private IJobFactory _jobFactory;
        List<Tmp> _plugins = new List<Tmp>();


        public async Task StopAll()
        {
            await _scheduler.Shutdown(true);
        }
        public IEnumerable<IPlugin> GetPlugins(IEnumerable<Assembly> assemblies)
        {
            List<IPlugin> plugins = new List<IPlugin>();
            foreach (var assembly in assemblies)
            {
                foreach (var type in assembly.GetExportedTypes().Where(c => typeof(IPlugin).IsAssignableFrom(c) && c.IsClass))
                {
                    plugins.Add((IPlugin)Activator.CreateInstance(type, new object[] { }));
                }
            }

            return plugins;
        }

        public async Task RunAll(IEnumerable<Assembly> assemblies)
        {
            var plugins = GetPlugins(assemblies);
            StdSchedulerFactory factory = new StdSchedulerFactory();
            _jobFactory = new MyJobFactory();
            _scheduler = await factory.GetScheduler();
            _scheduler.JobFactory = _jobFactory;
            await _scheduler.Start();
            foreach (var plugin in plugins)
            {
                var job = plugin.GetJob();
                var trigger = plugin.GetTrigger();
                _plugins.Add(new Tmp
                {
                    Scheduler = _scheduler,
                    Job = job,
                    Trigger = trigger,
                    Plugin = plugin
                });
                await _scheduler.ScheduleJob(job, trigger);
            }
        }
    }
}
