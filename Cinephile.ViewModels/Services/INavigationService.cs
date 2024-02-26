using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace Cinephile.ViewModels.Services
{
    public interface INavigationService
    {
        IObservable<Unit> GoTo(string id);
    }
}
