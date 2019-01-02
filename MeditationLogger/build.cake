string target = Argument( "target", "build" );

const string version = "0.4.0"; // This is the version of Meditation Logger.  Update before releasing.
const string makeRelaseTarget = "make_release";
const string armBuildTarget = "arm_linux";
const string allTarget = "all";

bool isRelease = ( target == makeRelaseTarget ) || ( target == armBuildTarget ) || ( target == allTarget );

DotNetCoreMSBuildSettings msBuildSettings = new DotNetCoreMSBuildSettings();

// Sets Meditation Loggers's assembly version.
msBuildSettings.WithProperty( "Version", version )
    .WithProperty( "AssemblyVersion", version )
    .SetMaxCpuCount( System.Environment.ProcessorCount )
    .WithProperty( "FileVersion", version );

const string distFolder = "./dist";
string configuration;
if ( isRelease )
{
    configuration = "Release";
    msBuildSettings.WithProperty( "TrimUnusedDependencies", "true" );
}
else
{
    configuration = "Debug";
}

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
        string outputDir = System.IO.Path.Combine( distFolder, configuration + "-desktop" );
        CleanDirectories( outputDir );

        DoRelease( "win" );
        DoRelease( "osx" );
        DoRelease( "linux" );

        Information( "Copying to Dist folder" );
        CopyDirectory( "./MeditationLogger.Gui/bin/desktop", outputDir );
    }
)
.IsDependentOn( "unit_test" )
.Description( "Makes the Electron App." );

Task( armBuildTarget )
.Does(
    () =>
    {
        string output = System.IO.Path.Combine(
            distFolder,
            configuration + "-arm_Linux"
        );

        CleanDirectories( output );

        DotNetCorePublishSettings winSettings = new DotNetCorePublishSettings
        {
            OutputDirectory = output,
            Configuration = "Release",
            Runtime = "linux-arm",
            MSBuildSettings = msBuildSettings,
            NoBuild = false,
            NoRestore = false
        };

        DotNetCorePublish( "./MeditationLogger.Gui/MeditationLogger.Gui.csproj", winSettings );
    }
).IsDependentOn( "unit_test" )
.Description( "Builds and publishes for Linux Arm (e.g. a Raspberry Pi)" );

Task( allTarget )
.IsDependentOn( makeRelaseTarget )
.IsDependentOn( armBuildTarget )
.Description( "Builds ALL targets for release" );

RunTarget( target );
