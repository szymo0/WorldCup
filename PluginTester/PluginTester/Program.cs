using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace PluginTester
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            string path = @".\Plugins\";

            List<Assembly> assemblies=new List<Assembly>();

            foreach (var directory in Directory.GetDirectories(path))
            {
                foreach (var file in Directory.GetFiles(directory,"*.dll"))
                {
                    assemblies.Add(new AssemblyLoader(file).Load(file));
                }

            }

            var pluginManager =new PluginManager();
            await (pluginManager.RunAll(assemblies));


            string tmp;
            while ((tmp=Console.ReadLine()) != "exit")
            {
                if (tmp == "stop")
                {
                    await pluginManager.StopAll();
                    Console.WriteLine("Jobs ended");
                    Console.ReadLine();
                }

            }





            //convention...
            //APPFOLDER/
            //./Plugins
            //./Plugins/PluginName.Plugin
            //./Pluigns/PluginName.Plugin/plugin.dll
            //./Plugins/PluginName.Plugin/plugin.config
            //./Plugins/PluginName.Plugin/other_dlls
        }
    }
}
