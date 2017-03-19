using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Xna.Framework;

using TTengine.Comps;

namespace TTengine.Systems
{
    /// <summary>
    /// A threaded builder that works in the background, used by BuilderSystem.
    /// </summary>
    public class BackgroundBuilder
    {
        static Queue<BuildScriptDelegate> jobQ = new Queue<BuildScriptDelegate>();
        static Thread thread = null;
        static bool isRunning = true;
        int checkIntervalMs = 0;        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="checkIntervalMs">number of milliseconds to wait in between queue checks in case queue was found empty.</param>
        public BackgroundBuilder(int checkIntervalMs = 50)
        {
            this.checkIntervalMs = checkIntervalMs;
            if (thread == null)
                StartSystemThread();
        }

        protected void StartSystemThread()
        {
            thread = new Thread(new ThreadStart(RunSystemMainloop));
            thread.Name = "BackgroundBuilder";
            thread.Priority = ThreadPriority.BelowNormal;
            thread.Start();
        }

        public void Stop()
        {
            isRunning = false;
            thread.Interrupt();
            thread.Join();
        }

        public bool IsBusy()
        {
            lock (jobQ)
            {
                return jobQ.Count > 0;
            }
        }

        /// <summary>
        /// Add a new building job to the queue of the builder
        /// </summary>
        /// <param name="job">job to enqueue</param>
        public void AddJob(BuildScriptDelegate job)
        {
            lock (jobQ)
            {
                jobQ.Enqueue(job);
            }
        }

        /// <summary>
        /// Main loop which is run in separate builder-thread.
        /// </summary>
        protected void RunSystemMainloop()
        {
            try
            {
                while (isRunning)
                {
                    BuildScriptDelegate job = null;
                    lock (jobQ)
                    {
                        if (jobQ.Count > 0)
                        {
                            job = jobQ.Dequeue();
                        }
                    }
                    if (job != null)
                    {
                        // do the job - build
                        job();
                    }
                    else
                    {
                        Thread.Sleep(checkIntervalMs);
                    }
                }
            }catch(ThreadInterruptedException)
            {
                ;
            }
        }
    }
}
