using System.Diagnostics;

namespace pulumi_resource_one_password_native_unoffical;

public static class DebugHelper
{
    public static void WaitForDebugger()
    {
        if (Environment.GetCommandLineArgs().Any(z => z == "--debugger" || z == "--debug"))
        {
            var count = 0;
            while (!Debugger.IsAttached)
            {
                Thread.Sleep(1000);
                if (count++ > 30)
                {
                    break;
                }
            }
        }
    }
}