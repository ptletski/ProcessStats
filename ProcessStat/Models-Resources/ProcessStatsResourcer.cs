using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;

namespace Models_Resources
{
    public class ProcessStatsResourcer
    {
        public ProcessStats Retrieve(ILogger logger)
        {
            ProcessStats processStats = new ProcessStats();

            try
            {
                PerformanceCounter pc = new PerformanceCounter();
                Process currentProcess = Process.GetCurrentProcess();

                pc.CategoryName = "Process";
                pc.CounterName = "Working Set - Private";
                pc.InstanceName = currentProcess.ProcessName;

                int memsize = Convert.ToInt32(pc.NextValue()) / (int)1024;

                pc.Close();
                pc.Dispose();

                processStats.ProcessName = currentProcess.ProcessName;
                processStats.Memsize = memsize;
            }
            catch (Exception exception)
            {
                string message = "Failure accessing process resources.";

                logger.LogError(message, exception);

                throw new ResourceFindException(message);
            }

            return processStats;
        }
    }
}
