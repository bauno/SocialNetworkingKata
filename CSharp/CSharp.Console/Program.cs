using CSharp.Core.Exceptions;
using CSharp.Core.Factories;
using CSharp.Core.Factories.Interfaces;
using CSharp.Core.Repositories;
using CSharp.Core.Services;

namespace CSharp.Console
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var socialNetwork = InitMain();

            while (true)
            {
                System.Console.Write("Enter command (or 'q' to quit):");
                var cmdStr = System.Console.ReadLine();
                if (cmdStr == "q") return;
                try
                {
                    socialNetwork.Enter(cmdStr);
                }
                catch (InvalidCommandException e)
                {
                    System.Console.WriteLine(e.Message);
                }                                
                
            }
        }

        private static ConsoleSocialNetwork InitMain()
        {
            var postFac = new PostCommandFactory();
            var readFac = new ReadCommandFactory();
            var wallFac = new WallCommandFactory();
            var followFac = new FollowCommandFactory();
            var commandFactories = new CommandFactory[] {postFac, readFac, wallFac, followFac};

            var parser = new StringCommandParser(commandFactories);
            var engine = new SocialEngine(new MemoryPostRepository());
            var display = new ConsoleDisplay(new PostTsStringFormatter(), new TextConsole());

            var socialNetwork = new ConsoleSocialNetwork(parser, engine, display);
            return socialNetwork;
        }
    }
}