using System.Reactive.Concurrency;
using ReactiveUI;

namespace Cine.ViewModels;

public class ViewModelBase : ReactiveObject, IActivatableViewModel
{
    public ViewModelBase(IScheduler? mainThreadScheduler, IScheduler? taskPoolScheduler)
    {
        // Set the schedulers like this so we can inject the test scheduler later on when doing VM unit tests
        MainThreadScheduler = mainThreadScheduler ?? RxApp.MainThreadScheduler;
        TaskPoolScheduler = taskPoolScheduler ?? RxApp.TaskpoolScheduler;

    }
    public ViewModelActivator Activator { get; } = new ViewModelActivator();

    /// <summary>
    /// Gets the scheduler for scheduling operations on the main thread.
    /// </summary>
    protected IScheduler MainThreadScheduler { get; }

    /// <summary>
    /// Gets the scheduler for scheduling operations on the task pool.
    /// </summary>
    protected IScheduler TaskPoolScheduler { get; }
}
