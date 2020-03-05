using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
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
                Process currentProcess = Process.GetCurrentProcess();
                string[] counters = new string[] {
                    "Working Set",
                    "Working Set - Private",
                    "Private Bytes",
                    "Virtual Bytes"
                };
                List<long> results = new List<long>();
/*
                pc.CategoryName = "Process";
                pc.CounterName = "Working Set - Private";
                pc.InstanceName = currentProcess.ProcessName;

                int memsize = Convert.ToInt32(pc.NextValue()) / (int)1024;

                pc.Close();
                pc.Dispose();
*/
                foreach(string counter in counters)
                {
                    results.Add(GetLongValuedPerformanceCounterForProcess(
                                     counter, 
                                     currentProcess));
                }

                processStats.ProcessName = currentProcess.ProcessName;
                processStats.WorkingSet = results[0];
                processStats.WorkingSetPrivate = results[1];
                processStats.PrivateBytes = results[2];
                processStats.VirtualBytes = results[3];

                return processStats;
            }
            catch (Exception exception)
            {
                string message = "Failure accessing process resources.";

                logger.LogError(message, exception);

                throw new ResourceFindException(message);
            }
        }

        private long GetLongValuedPerformanceCounterForProcess(string name, Process currentProcess)
        {
            long result = 0;
            PerformanceCounter pc = new PerformanceCounter();

            pc.CategoryName = "Process";
            pc.CounterName = name;
            pc.InstanceName = currentProcess.ProcessName;

            result = Convert.ToInt64(pc.NextValue()) / (long)1024;

            pc.Close();
            pc.Dispose();

            return result;
        }
    }
}
