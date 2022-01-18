//
// Meditu - A way to track Meditation Sessions.
// Copyright (C) 2017-2022 Meditation Enthusiasts.
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU Affero General Public License as published
// by the Free Software Foundation, either version 3 of the License, or
// any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Affero General Public License for more details.
// 
// You should have received a copy of the GNU Affero General Public License
// along with this program.  If not, see <https://www.gnu.org/licenses/>.
//

using Cake.ArgumentBinder;
using Cake.Core.IO;
using Cake.Frosting;
using Seth.CakeLib.TestRunner;

namespace DevOps.UnitTests
{
    [TaskName( "unit_test" )]
    [TaskDescription( "Runs all the unit tests.  Pass in --code_coverage=true to run with coverage." )]
    public sealed class RunUnitTestTask : DefaultTask
    {
        // ----------------- Functions -----------------

        public override void Run( MeditationLogContext context )
        {
            TestConfig testConfig = new TestConfig
            {
                ResultsFolder = context.RepoRoot.Combine( new DirectoryPath( "TestResults" ) ),
                TestCsProject = context.SrcPath.CombineWithFilePath( new FilePath( "Meditu.UnitTests/Meditu.UnitTests.csproj" ) )
            };
            UnitTestRunner runner = new UnitTestRunner( context, testConfig );

            TestArguments args = context.CreateFromArguments<TestArguments>();
            if( args.RunWithCodeCoverage )
            {
                runner.RunCodeCoverage( TestArguments.CoverageFilter );
            }
            else
            {
                runner.RunTests();
            }
        }
    }
}
