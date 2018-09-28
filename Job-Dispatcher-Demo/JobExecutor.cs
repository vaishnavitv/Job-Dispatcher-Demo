#region MS Directives
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
#endregion

namespace Job_Dispatcher_Demo
{
    class JobExecutor
    {
        List<Job> jobs;
        ConcurrentDictionary<Job, Tuple<object, Exception, TimeSpan>> jobResults;

        public JobExecutor(IEnumerable<string> jobNames, int maxDelayInterval)
        {
            if (maxDelayInterval < 1)
            {
                throw new ArgumentException("maxDelayInterval must at least be 1 second.");
            }

            jobs = new List<Job>();
            jobResults = new ConcurrentDictionary<Job, Tuple<object, Exception, TimeSpan>>();

            Random random = new Random();
            foreach (var jobName in jobNames)
            {
                int randomDelayInterval = random.Next(0, maxDelayInterval);
                jobs.Add(new Job(jobName, randomDelayInterval));
            }

        }

        public void Execute()
        {
            Parallel.ForEach(jobs, job =>
                {
                    var jobName = job.GetJobName();
                    object result;
                    Exception exceptionIncurred = null;
                    var watch = System.Diagnostics.Stopwatch.StartNew();
                    try
                    {
                        result = job.Execute();
                    }
                    catch (Exception e)
                    {
                        result = null;
                        exceptionIncurred = e;
                    }
                    finally
                    {
                        watch.Stop();
                    }
                    jobResults[job] = Tuple.Create(result, exceptionIncurred, watch.Elapsed);
                }
             );
        }

        public void PrintResults()
        {
            foreach (KeyValuePair<Job, Tuple<object, Exception, TimeSpan>> jobResultPair in jobResults)
            {
                var jobName = jobResultPair.Key.GetJobName();
                var result = (jobResultPair.Value.Item2 == null) ? jobResultPair.Value.Item1: "[None]";
                var exceptionMessage = (jobResultPair.Value.Item2 == null) ? "" : $"Exception: {jobResultPair.Value.Item2.Message}";
                var elapsed = $"{jobResultPair.Value.Item3}";
                Console.WriteLine($"Job: {jobName}, Result: {result}{exceptionMessage}, Elapsed: {elapsed}.");
            }
        }
    }
}
