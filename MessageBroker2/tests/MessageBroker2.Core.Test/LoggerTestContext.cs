using System;
using System.Collections.Generic;
using System.Text;
using Serilog;

namespace MessageBroker2.Core.Test
{
    public class LoggerTestContext
    {
        protected ILogger GetLogger()
        {
            return NSubstitute.Substitute.For<ILogger>();
        }
    }
}
