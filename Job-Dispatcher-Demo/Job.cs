#region MS Directives
using System;
using System.Threading;
using System.Linq;
#endregion

namespace Job_Dispatcher_Demo
{
    class Job
    {
        String jobName;
        int delayInterval;

        public Job(string jobName, int delayInterval)
        {
            this.jobName = jobName;
            this.delayInterval = delayInterval;
        }

        public string GetJobName()
        {
            return jobName;
        }

        public object Execute()
        {
            if (jobName.Any(char.IsLower))
            {
                throw new ArgumentException($"Job Name: {jobName} must be in UpperCase.");
            }
            Thread.Sleep(delayInterval * 1000);
            Console.WriteLine($"Job Name: {jobName} waited for: {delayInterval}s.");
            return $"{jobName}={jobName.GetHashCode()}";
        }

    }
}
