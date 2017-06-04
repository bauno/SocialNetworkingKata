﻿using System.Text.RegularExpressions;
using CSharp.Core.Commands;
using CSharp.Core.Commands.Interfaces;
using CSharp.Core.Factories.Interfaces;

namespace CSharp.Core.Factories
{
    public class ReadCommandFactory : CommandFactory
    {
        private readonly Regex _readCmd = new Regex(@"^(\w+)$");
        public Command Parse(string cmdString)
        {
            if (_readCmd.IsMatch(cmdString))
            {
                var matches = _readCmd.Matches(cmdString);
                var user = matches[0].Groups[1].Value;
                return new ReadCommand(user);
            }
            return null;
        }
    }
}