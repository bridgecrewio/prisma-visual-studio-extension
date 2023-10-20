using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace PrismaCloudVSExtension.Utils.Commands
{
    public static class AsyncExec
    {

        public static async Task<string> run(string command, string options) {

            Debug.WriteLine(command + " " + options, "[DEBUG] Command execution start");

            var processInfo = new ProcessStartInfo(command, options);

            processInfo.CreateNoWindow = true;
            processInfo.UseShellExecute = false;
            processInfo.RedirectStandardOutput = true;
            processInfo.RedirectStandardError = true;
            int timeout = 120000;

            using (var process = new Process())
            {
                process.StartInfo = processInfo;
                process.Start();
                process.WaitForExit(timeout);
                if (!process.HasExited)
                {
                    process.Kill();
                }

                string result = process.StandardOutput.ReadToEnd();
                string stderr = process.StandardError.ReadToEnd();

                process.Close();

                Debug.WriteLine(result, "[DEBUG] Command execution result");

                if (stderr.Length > 0) {
                    Debug.WriteLine(stderr, "[DEBUG] Command execution fail");
                    throw new Exception(stderr);
                }

                return result;
            }
        }
        
    }
}
