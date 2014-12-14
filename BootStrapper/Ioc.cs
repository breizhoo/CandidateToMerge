using System.ComponentModel;
using CommonServiceLocator.StructureMapAdapter.Unofficial;
using Core.Implementation;
using Core.Interface;
using Microsoft.Practices.ServiceLocation;

namespace BootStrapper
{
    public class Ioc
    {
        public static void Initialization()
        {
            var container = new StructureMap.Container(x =>
            {
                x.For<IChangesetVisualizer>().Use<ChangesetVisualizer>();
            });
            var locatorProvider = new StructureMapServiceLocator(container);


            ServiceLocator.SetLocatorProvider(() => locatorProvider);
        }

        public static System.Collections.Generic.IEnumerable<TService> GetAllInstances<TService>()
        {
            return ServiceLocator.Current.GetAllInstances<TService>();
        }

        public static System.Collections.Generic.IEnumerable<object> GetAllInstances(System.Type serviceType)
        {
            return ServiceLocator.Current.GetAllInstances(serviceType);
        }

        public static TService GetInstance<TService>(string key)
        {
            return ServiceLocator.Current.GetInstance<TService>(key);
        }

        public static TService GetInstance<TService>()
        {
            return ServiceLocator.Current.GetInstance<TService>();
        }

        public static object GetInstance(System.Type serviceType, string key)
        {
            return ServiceLocator.Current.GetInstance(serviceType, key);
        }

        public static object GetInstance(System.Type serviceType)
        {
            return ServiceLocator.Current.GetInstance(serviceType);
        }

        public static object GetService(System.Type serviceType)
        {
            return ServiceLocator.Current.GetService(serviceType);
        }
    }
}