using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Quartz;
using Serilog;

namespace Plugin1
{
    [DisallowConcurrentExecution]
    public class Test1Job : IJob
    {
        private ILogger _logger;

        public Test1Job()
        {
            _logger = new LoggerConfiguration()
                .WriteTo.Console().MinimumLevel.Verbose()
                .CreateLogger();
        }
        public Task Execute(IJobExecutionContext context)
        {
            var log = _logger.ForContext<Test1Job>();
            log.Information("{@jobName} information {@name}", this.GetType().FullName, "Szymon");
            return Task.Delay(5000);
        }
    }
    [DisallowConcurrentExecution]
    public class Test2Job : IJob
    {
        private ILogger _logger;
        public Test2Job()
        {
            _logger = new LoggerConfiguration()
                .WriteTo.Console().MinimumLevel.Verbose()
                .CreateLogger();
        }

        public Task Execute(IJobExecutionContext context)
        {
            var log = _logger.ForContext<Test2Job>();
            log.Information("{@jobName} information {@name}",this.GetType().FullName,"michał");

            return Task.Delay(5000);
        }
    }
}
