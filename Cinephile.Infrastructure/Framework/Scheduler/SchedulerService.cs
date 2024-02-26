using Rx = System.Reactive.Concurrency;
using System.Reactive.Concurrency;
using System.Threading;

namespace Cinephile.Infrastructure.Framework.Scheduler
{
    public sealed class SchedulerService : ISchedulerService
    {
        readonly IScheduler m_mainScheduler;

        public SchedulerService()
        {
            var current = SynchronizationContext.Current ?? new SynchronizationContext();
            m_mainScheduler = new SynchronizationContextScheduler(current);
        }

        public IScheduler DefaultScheduler => Rx.DefaultScheduler.Instance;

        public IScheduler CurrentThreadScheduler => Rx.CurrentThreadScheduler.Instance;

        public IScheduler ImmediateScheduler => Rx.ImmediateScheduler.Instance;

        public IScheduler MainThreadScheduler => m_mainScheduler;

        public IScheduler TaskPoolScheduler => Rx.TaskPoolScheduler.Default;
    }
}