using System.Reflection;
using Autofac;
using CSharp.Core.Exceptions;
using CSharp.Core.Factories;
using CSharp.Core.Factories.Interfaces;
using CSharp.Core.Repositories;
using CSharp.Core.Services;
using CSharp.Core.Services.Interfaces;
using CSharpFunctionalExtensions;

namespace CSharp.Console
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var socialNetwork = InitMain();

            while (true)
            {
                System.Console.Write("Enter command (or 'q' to quit): ");
                var cmdStr = System.Console.ReadLine();
                if (cmdStr == "q") return;
                socialNetwork.Enter(cmdStr)
                    .OnFailure(msg => System.Console.WriteLine(msg));
            }
        }

        private static IConsoleSocialNetwork InitMain()
        {
            var builder = new ContainerBuilder();
            builder.RegisterAssemblyTypes(Assembly.GetAssembly(typeof(SocialEngine)))
                .AsImplementedInterfaces();
            return builder.Build().Resolve<IConsoleSocialNetwork>();

        }
    }
}