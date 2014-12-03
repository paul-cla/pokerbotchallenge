using System;
using System.Threading;
using System.Web;
using BotWars.Controllers;
using BotWars.Services;
using StructureMap;
using StructureMap.Graph;

namespace BotWars.App_Start
{
    public static class ObjectFactory
    {
        private static readonly Lazy<Container> ContainerBuilder =
            new Lazy<Container>(DefaultContainer, LazyThreadSafetyMode.ExecutionAndPublication);

        public static IContainer Container
        {
            get { return ContainerBuilder.Value; }
        }

        private static Container DefaultContainer()
        {
            return new Container(x => x.Scan(scan =>
                {
                    scan.TheCallingAssembly();
                    scan.WithDefaultConventions();
                    scan.AssemblyContainingType<IBotManager>();
                }));
        }
    }
}