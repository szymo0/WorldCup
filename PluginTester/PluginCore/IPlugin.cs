using System;
using System.Collections.Generic;
using System.Text;
using Quartz;

namespace PluginCore
{
    public interface IPlugin
    {

        IJobDetail GetJob();
        ITrigger GetTrigger();
    }
}
