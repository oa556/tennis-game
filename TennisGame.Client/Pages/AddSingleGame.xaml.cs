using System.Collections.ObjectModel;
using TennisGame.Client.Services;
using TennisGame.Shared;

namespace TennisGame.Client.Pages;

public partial class AddSingleGame : ContentPage
{
    private readonly IDataService _dataService;
    private readonly IAuthenticationService _authenticationService;

    private ObservableCollection<PlayerDto> Players { get; set; }

    public AddSingleGame(IDataService dataService,
                         IAuthenticationService authentificationService)
	{
		InitializeComponent();
        _dataService = dataService;
        _authenticationService = authentificationService;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        Players = new ObservableCollection<PlayerDto>(await _dataService.GetPlayersAsync());
        OpponentPicker.ItemsSource = Players;
    }

    private async void OnSubmitClicked(object sender, EventArgs e)
    {
        var opponent = OpponentPicker.SelectedItem as PlayerDto;
        var result = WinResultOption.IsChecked ? MatchResult.Team1Wins : MatchResult.Team2Wins;

        await _dataService.CreateMatchOutcomeAsync(new CreateMatchOutcomeRequest(
            new int[] { _authenticationService.CurrentUserId },
            new int[] { opponent.Id },
            result
        ));

        await Shell.Current.GoToAsync("..");
    }
}
