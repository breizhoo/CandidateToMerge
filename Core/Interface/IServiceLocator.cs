namespace BootStrapper
{
    public interface IServiceLocator
    {
        System.Collections.Generic.IEnumerable<TService> GetAllInstances<TService>();
        System.Collections.Generic.IEnumerable<object> GetAllInstances(System.Type serviceType);
        TService GetInstance<TService>(string key);
        TService GetInstance<TService>();
        object GetInstance(System.Type serviceType, string key);
        object GetInstance(System.Type serviceType);
        object GetService(System.Type serviceType);
    }
}