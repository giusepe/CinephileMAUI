using System.Reactive.Concurrency;
using Rx = System.Reactive.Concurrency;

namespace Cinephile.Infrastructure.Framework.Scheduler
{
    public class FunctionalTestsSchedulerService : ISchedulerService
    {
        private bool m_allowParallel;
        public FunctionalTestsSchedulerService(bool allowParallel = false)
        {
            m_allowParallel = allowParallel;
        }

        public IScheduler DefaultScheduler => Rx.ImmediateScheduler.Instance;

        public IScheduler CurrentThreadScheduler => Rx.ImmediateScheduler.Instance;

        public IScheduler ImmediateScheduler => Rx.ImmediateScheduler.Instance;

        public IScheduler MainThreadScheduler => Rx.ImmediateScheduler.Instance;

        public IScheduler TaskPoolScheduler
        {
            get
            {
                if (m_allowParallel)
                {
                    return Rx.TaskPoolScheduler.Default;
                }
                return Rx.ImmediateScheduler.Instance;
            }
        }
    }
}