using Cinephile.ViewModels.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinephile.Services
{
    internal class NavigationService : INavigationService
    {
        public IObservable<Unit> GoTo(string id) => Observable.FromAsync(() => Shell.Current.GoToAsync(id));
    }
}
