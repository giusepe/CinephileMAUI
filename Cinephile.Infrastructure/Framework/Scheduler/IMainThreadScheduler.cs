using System.Reactive.Concurrency;

namespace Cinephile.Infrastructure.Framework.Scheduler
{
    public interface IMainThreadScheduler : IScheduler
    {
        IScheduler Current { get; }
    }
}
