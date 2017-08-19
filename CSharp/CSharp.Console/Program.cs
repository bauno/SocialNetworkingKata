using System.Reflection;
using Autofac;
using CSharp.Core.Services;
using CSharp.Core.Services.Interfaces;
using CSharpFunctionalExtensions;
using static System.Console;

namespace CSharp.Console
{
    internal class Program
    {
        private static void RunSocialNetwork(IConsoleSocialNetwork socialNetwork)
        {
            Write("Enter command (or 'q' to quit): ");
            var cmdStr = ReadLine();
            if (cmdStr == "q") return;
            socialNetwork.Enter(cmdStr)
                .OnFailure(msg => WriteLine(msg));
            RunSocialNetwork(socialNetwork);
        }
        
        
        public static void Main(string[] args)
        {            

            RunSocialNetwork(InitSocialNetwork());
        }

        private static IConsoleSocialNetwork InitSocialNetwork()
        {
            var builder = new ContainerBuilder();
            builder.RegisterAssemblyTypes(Assembly.GetAssembly(typeof(SocialEngine)))
                .AsImplementedInterfaces();
            return builder
                .Build().Resolve<IConsoleSocialNetwork>();
        }
    }
}