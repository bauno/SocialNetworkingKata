﻿using System;
using System.Reflection;
using Autofac;
using CSharp.Core.Factories.Interfaces;
using CSharp.Core.Services;
using CSharp.Core.Services.Interfaces;
using LanguageExt;
using static System.Console;

namespace CSharp.Console
{
    internal class Program
    {
        private static void RunSocialNetwork(IConsoleSocialNetwork socialNetwork)
        {
            while (true)
            {
                Write("Enter command (or 'q' to quit): ");
                var cmdStr = ReadLine();
                if (cmdStr == "q") return;                
                socialNetwork.Enter(cmdStr)
                    .IfSome(err => PrintError(err));
            }
        }

        private static void PrintError(string error)
        {
            var currentColor = ForegroundColor;
            ForegroundColor = ConsoleColor.Red;
            WriteLine("Error: {0}", error);
            ForegroundColor = currentColor;
        }
        
        
        public static void Main(string[] args)
        {                       
            RunSocialNetwork(InitSocialNetwork());
        }

        private static IConsoleSocialNetwork InitSocialNetwork()
        {
            var builder = new ContainerBuilder();
            builder.RegisterAssemblyTypes(Assembly.GetAssembly(typeof(CommandFactory)))
                .AsImplementedInterfaces();            
            
            return builder
                .Build().Resolve<IConsoleSocialNetwork>();
        }
    }
}