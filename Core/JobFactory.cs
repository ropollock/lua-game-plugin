using System;
using Quartz;
using Quartz.Spi;

namespace Lou {
    public class JobFactory : IJobFactory {
        
        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler) {
            return DIManager.Container.Resolve(bundle.JobDetail.JobType) as IJob;
        }

        public void ReturnJob(IJob job) {
            var disposable = job as IDisposable;  
            disposable?.Dispose();  
        }
    }
}