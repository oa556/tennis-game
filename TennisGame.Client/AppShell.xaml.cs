using TennisGame.Client.Pages;

namespace TennisGame.Client;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute(nameof(AddSingleGame), typeof(AddSingleGame));
        Routing.RegisterRoute(nameof(AddDoubleGame), typeof(AddDoubleGame));
    }
}
