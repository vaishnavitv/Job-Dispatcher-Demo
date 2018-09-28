#region MS Directives
using System;
using System.Collections.Generic;
#endregion

namespace Job_Dispatcher_Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> jobNames = new List<string> { "A", "b", "C", "d", "e", "F", "g", "H" };
            int maxDelayInterval = 8; //Should be 1 at least.

            JobExecutor jobExecutor = new JobExecutor(jobNames, maxDelayInterval);
            jobExecutor.Execute();
            jobExecutor.PrintResults();

            Console.WriteLine("Press ENTER to quit...");
            Console.ReadLine();
        }
    }
}
