string target = Argument( "target", "build" );

const string version = "0.4.0"; // This is the version of Meditation Logger.  Update before releasing.
const string makeRelaseTarget = "make_release";

bool isRelease = ( target == makeRelaseTarget );

DotNetCoreMSBuildSettings msBuildSettings = new DotNetCoreMSBuildSettings();

// Sets Meditation Loggers's assembly version.
msBuildSettings.WithProperty( "Version", version )
    .WithProperty( "AssemblyVersion", version )
    .SetMaxCpuCount( System.Environment.ProcessorCount )
    .WithProperty( "FileVersion", version );

string configuration;
string packageOutput;
if ( isRelease )
{
    configuration = "Release";
    packageOutput = "./dist/Release";
    msBuildSettings.WithProperty( "TrimUnusedDependencies", "true" );
}
else
{
    configuration = "Debug";
    packageOutput = "./dist/Debug";
}

packageOutput = MakeAbsolute( new FilePath( packageOutput ) ).FullPath;
msBuildSettings.SetConfiguration( configuration );

Task( "build" )
.Does(
    () => 
    {
        DotNetCoreBuildSettings settings = new DotNetCoreBuildSettings
        {
            MSBuildSettings = msBuildSettings
        };
        DotNetCoreBuild( "./MeditationLogger.sln", settings );
    }
)
.Description( "Compiles Meditation Logger." );

Task( "unit_test" )
.Does(
    () =>
    {
		DotNetCoreTestSettings settings = new DotNetCoreTestSettings
		{
			NoBuild = true,
			NoRestore = true,
            Configuration = configuration
		};
        DotNetCoreTest( "./MeditationLogger.UnitTests/MeditationLogger.UnitTests.csproj", settings );
    }
)
.IsDependentOn( "build" )
.Description( "Runs Meditation Loggers's Tests." );

void DoRelease( string target )
{
    const string workingDirectory = "./MeditationLogger.Gui";

    ProcessSettings settings = new ProcessSettings
    {
        WorkingDirectory = workingDirectory,
        Arguments = "version"
    };

    int exitCode = StartProcess( "electronize", settings );
    if ( exitCode != 0 )
    {
        throw new Exception( "Could not get Electron's Version.  Exit Code: " + exitCode );
    }

    settings.Arguments = "build /target " + target;
    exitCode = StartProcess( "electronize", settings );
    if ( exitCode != 0 )
    {
        throw new Exception( "Could build target '" + target + "'. Exit Code: " + exitCode );
    }
}

Task( makeRelaseTarget )
.Does(
    () =>
    {
        DoRelease( "win" );
        DoRelease( "osx" );
        DoRelease( "linux" );
    }
)
.IsDependentOn( "unit_test" )
.Description( "Makes the Electron App." );

RunTarget( target );