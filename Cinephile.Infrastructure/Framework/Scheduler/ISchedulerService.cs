using System.Reactive.Concurrency;

namespace Cinephile.Infrastructure.Framework.Scheduler
{
    public interface ISchedulerService
    {
        IScheduler DefaultScheduler
        {
            get;
        }

        IScheduler CurrentThreadScheduler
        {
            get;
        }

        IScheduler ImmediateScheduler
        {
            get;
        }

        IScheduler MainThreadScheduler
        {
            get;
        }

        IScheduler TaskPoolScheduler
        {
            get;
        }
    }
}
