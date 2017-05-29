using Microsoft.Practices.Unity;
using PeopleManager.Persistence;
using System;
using System.ServiceModel;
using Unity.Wcf;

namespace PeopleManager.WcfService.Host
{
    public class Program
    {
        private static void Main(string[] args)
        {
            IUnityContainer container = new UnityContainer();
            container.RegisterType<IPersonRepository, PersonRepository>();
            container.RegisterType<IPersonService, PersonService>();

            using (ServiceHost host = new UnityServiceHost(container, typeof(PersonService)))
            {
                host.Open();

                Console.WriteLine("Press <Enter> to stop the service.");
                Console.ReadLine();

                host.Close();
            }
        }
    }
}