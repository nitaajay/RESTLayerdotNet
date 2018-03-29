using System;
using System.Configuration;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Web.Http.SelfHost;
using WebApiSelfHostApp.AppStartConfig;

namespace WebApiSelfHostApp
{
    internal class Program
    {
        private static readonly Func<Task> TaskFunction = StartApi;

        private const int SW_MINIMIZE = 6;

        [DllImport("Kernel32.dll", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        private static extern IntPtr GetConsoleWindow();

        [DllImport("User32.dll", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool ShowWindow([In] IntPtr hWnd, [In] Int32 nCmdShow);

        private static void Main()
        {
            try
            {
                Task.Factory.StartNew(async () => await Task.Run(TaskFunction)
                    .ConfigureAwait(false));
                /*
                var key = Console.ReadKey();
                var d = char.GetNumericValue(key.KeyChar);
                Console.WriteLine(d);

                if (key.Key == ConsoleKey.Spacebar)
                {
                    var path = Application.ExecutablePath;
                    var pid = Process.GetCurrentProcess().Id;
                    RestartApp(pid, path);
                }
                */

                Console.ReadLine();
            }
            catch (ObjectDisposedException objectDisposedException)
            {
                Console.WriteLine(objectDisposedException.Message);
                Console.WriteLine(Environment.NewLine + "Press Enter to quit.");
                Console.ReadLine();
            }
            catch (AggregateException aggregateException)
            {
                Console.WriteLine(aggregateException.InnerException);
                Console.WriteLine(Environment.NewLine + "Press Enter to quit.");
                Console.ReadLine();
            }
        }

        private static Task StartApi()
        {
            var apiBaseAddress = ConfigurationManager.AppSettings["ApiBaseAddress"];
            var httpSelfHostConfiguration = new HttpSelfHostConfiguration(apiBaseAddress);
            var selfHostApiTask = WebApiConfig.Register(httpSelfHostConfiguration);

            var hWndConsole = GetConsoleWindow();
            ShowWindow(hWndConsole, SW_MINIMIZE);

            return selfHostApiTask;
        }

        /*
        private static void RestartApp(int pid, string applicationName)
        {
            try
            {
                Process.Start(applicationName, "");
                var process = Process.GetProcessById(pid);
                process.Kill();
                process.WaitForExit(1000);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
            Process.Start(applicationName, "");
        }
        */
    }
}
