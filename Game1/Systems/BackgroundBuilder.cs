using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Xna.Framework;

using Game1.Comps;

namespace Game1.Systems
{
    /// <summary>
    /// A threaded builder that works in the background, used by BuilderSystem.
    /// </summary>
    public class BackgroundBuilder
    {
        Queue<BuilderComp> jobQ = new Queue<BuilderComp>();
        Thread thread = null;
        bool isRunning = true;
        int checkIntervalMs = 0;
        BuilderSystem builderSystem = null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="checkIntervalMs">number of milliseconds to wait in between queue checks in case queue was found empty.</param>
        public BackgroundBuilder(BuilderSystem parentSystem, int checkIntervalMs = 100)
        {
            this.builderSystem = parentSystem;
            this.checkIntervalMs = checkIntervalMs;
            StartSystemThread();
        }

        protected void StartSystemThread()
        {
            thread = new Thread(new ThreadStart(RunSystemMainloop));
            thread.Priority = ThreadPriority.BelowNormal;
            thread.Start();
        }

        public void Stop()
        {
            isRunning = false;
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
        public void AddJob(BuilderComp job)
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
            while (isRunning)
            {
                BuilderComp job = null;
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
                    job.BuildScript(/*builderSystem.EntityWorld*/);
                }
                else
                {
                    Thread.Sleep(checkIntervalMs);
                }
            }
        }
    }
}
