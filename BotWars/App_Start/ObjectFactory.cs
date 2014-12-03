using System;
using System.Threading;
using BotWars.Services;
using StructureMap;
using StructureMap.Graph;
using log4net;

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
            log4net.Config.XmlConfigurator.Configure();

            return new Container(x =>
                {
                    x.For<ILog>().Use(LogManager.GetLogger(typeof(WebApiApplication)));

                    x.Scan(scan =>
                        {
                            scan.TheCallingAssembly();
                            scan.WithDefaultConventions();
                            scan.AssemblyContainingType<IBotManager>();
                        });

                });
        }
    }
}