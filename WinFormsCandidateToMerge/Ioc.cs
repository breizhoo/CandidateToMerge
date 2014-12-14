using System.ComponentModel;
using CommonServiceLocator.StructureMapAdapter.Unofficial;
using Core.Implementation;
using Core.Interface;
using Microsoft.Practices.ServiceLocation;
using WinFormsCandidateToMerge;
using WinFormsCandidateToMerge.Interface;

namespace BootStrapper
{
    public class Ioc : IServiceLocator
    {
        public void Initialization()
        {
            var container = new StructureMap.Container(x =>
            {
                x.For<IChangesetVisualizer>().Use<ChangesetVisualizer>();
                x.For<IServiceLocator>().Use(() => this);
                x.For<IMainForm>().Use<CandidateToMerge>();
                x.For<IDataSetManipulator>().Use<DataSetManipulator>().Singleton();

                x.Scan(s => s.Assembly("WindowsFormsLibrarie"));
            });
            var locatorProvider = new StructureMapServiceLocator(container);


            ServiceLocator.SetLocatorProvider(() => locatorProvider);
        }

        public System.Collections.Generic.IEnumerable<TService> GetAllInstances<TService>()
        {
            return ServiceLocator.Current.GetAllInstances<TService>();
        }

        public System.Collections.Generic.IEnumerable<object> GetAllInstances(System.Type serviceType)
        {
            return ServiceLocator.Current.GetAllInstances(serviceType);
        }

        public TService GetInstance<TService>(string key)
        {
            return ServiceLocator.Current.GetInstance<TService>(key);
        }

        public TService GetInstance<TService>()
        {
            return ServiceLocator.Current.GetInstance<TService>();
        }

        public object GetInstance(System.Type serviceType, string key)
        {
            return ServiceLocator.Current.GetInstance(serviceType, key);
        }

        public object GetInstance(System.Type serviceType)
        {
            return ServiceLocator.Current.GetInstance(serviceType);
        }

        public object GetService(System.Type serviceType)
        {
            return ServiceLocator.Current.GetService(serviceType);
        }
    }
}