using Microsoft.Practices.Unity;
using System;

namespace PeopleManager.WinClient
{
    public static class UnityConfig
    {
        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            IUnityContainer container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }

        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<WcfService.IPersonService, WcfService.Proxy.PersonServiceProxy>(new ContainerControlledLifetimeManager());
        }
    }
}