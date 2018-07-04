using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Quartz;
using Quartz.Spi;

namespace PluginTester
{
    public class StopableJobListner:IJobListener
    {
        private static int runJobs = 0;

        public async Task JobToBeExecuted(IJobExecutionContext context, CancellationToken cancellationToken = new CancellationToken())
        {
            Console.WriteLine($"Start execute {context.JobDetail.JobType.Name}");

        }

        public async Task JobExecutionVetoed(IJobExecutionContext context, CancellationToken cancellationToken = new CancellationToken())
        {
        }

        public async Task JobWasExecuted(IJobExecutionContext context, JobExecutionException jobException,
            CancellationToken cancellationToken = new CancellationToken())
        {
            runJobs++;

        }

        public string Name => "Stop all jobs";
    }

    public class MyJobFactory:IJobFactory
    {
        ConcurrentDictionary<Type,int> _jobs=new ConcurrentDictionary<Type, int>();
        public MyJobFactory()
        {

        }
        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            if (!_jobs.ContainsKey(bundle.JobDetail.JobType))
                _jobs.TryAdd(bundle.JobDetail.JobType,0);
            _jobs[bundle.JobDetail.JobType]++;
            ShowStatus();
            return Activator.CreateInstance(bundle.JobDetail.JobType) as IJob;
        }

        public void ReturnJob(IJob job)
        {
            _jobs[job.GetType()]--;
            ShowStatus();
        }


        public void ShowStatus()
        {
            Console.Clear();
            Console.WriteLine("==============================");
            foreach (var job in _jobs.AsEnumerable())
            {
                Console.WriteLine($"{job.Key} has instances: {job.Value}");

            }
            Console.WriteLine("==============================");
        }
    }
}
