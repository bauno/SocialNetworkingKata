#!/bin/sh
nuget restore CSharp.sln
xbuild /p:Configuration=Release CSharp.sln
mono ../testrunner/NUnit.ConsoleRunner.3.6.1/tools/nunit3-console.exe ./CSharp.Tests/bin/Release/CSharp.Tests.dll
mono ../testrunner/NUnit.ConsoleRunner.3.6.1/tools/nunit3-console.exe ./CSharp.Tests.Acceptance/bin/Release/CSharp.Tests.Acceptance.dll
