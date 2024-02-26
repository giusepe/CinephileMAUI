using Cinephile.Views;

namespace Cinephile
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("movies/detail", typeof(MovieDetailView));
        }
    }
}
