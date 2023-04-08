using TennisGame.Client.Pages;
using TennisGame.Client.Services;

namespace TennisGame.Client;

public partial class MainPage : ContentPage
{
    private readonly IDataService _dataService;
    private readonly IAuthenticationService _authenticationService;

    public MainPage(IDataService dataService,
                    IAuthenticationService authentificationService)
    {
        InitializeComponent();
        _dataService = dataService;
        _authenticationService = authentificationService;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        var currentPlayer = await _dataService.GetPlayerAsync(_authenticationService.CurrentUserId);
        SkillLabel.Text = currentPlayer?.Skill.ToString() ?? "?";
        NameLabel.Text = $"Hi, {currentPlayer?.Name ?? "Anonymous"}";
    }

    public async void OnAddSingleGameClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(AddSingleGame));
    }

    public async void OnAddDoubleGameClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(AddDoubleGame));
    }
}
