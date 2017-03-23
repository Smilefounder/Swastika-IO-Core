REM Usage: dotnet ef dbcontext scaffold [arguments] [options]

REM Arguments:
REM   <CONNECTION>  The connection string to the database.
REM   <PROVIDER>    The provider to use. (E.g. Microsoft.EntityFrameworkCore.SqlServer)

REM Options:
REM   -d|--data-annotations                  Use attributes to configure the model (where possible). If omitted, only the fluent API is used.
REM   -c|--context <NAME>                    The name of the DbContext.
REM   -f|--force                             Overwrite existing files.
REM   -o|--output-dir <PATH>                 The directory to put files in. Paths are relative to the project directory.
REM   --schema <SCHEMA_NAME>...              The schemas of tables to generate entity types for.
REM   -t|--table <TABLE_NAME>...             The tables to generate entity types for.
REM   --json                                 Show JSON output.
REM   -p|--project <PROJECT>                 The project to use.
REM   -s|--startup-project <PROJECT>         The startup project to use.
REM   --framework <FRAMEWORK>                The target framework.
REM   --configuration <CONFIGURATION>        The configuration to use.
REM   --msbuildprojectextensionspath <PATH>  The MSBuild project extensions path. Defaults to "obj".
REM   -e|--environment <NAME>                The environment to use. Defaults to "Development".
REM   -h|--help                              Show help information
REM   -v|--verbose                           Show verbose output.
REM   --no-color                             Don't colorize output.
REM   --prefix-output                        Prefix output with level.
cd Swastika
dotnet ef dbcontext scaffold "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\_Workspace\SW\Github\Swastika\Swastika\wwwroot\database\SwastikaDatabase.mdf;Integrated Security=True;Connect Timeout=30"  Microsoft.EntityFrameworkCore.SqlServer

pause