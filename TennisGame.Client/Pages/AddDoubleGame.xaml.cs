using System.Collections.ObjectModel;
using TennisGame.Client.Services;
using TennisGame.Shared;

namespace TennisGame.Client.Pages;

public partial class AddDoubleGame : ContentPage
{
    private readonly IDataService _dataService;
    private readonly IAuthenticationService _authenticationService;

    private ObservableCollection<PlayerDto> Players { get; set; }

    public AddDoubleGame(IDataService dataService,
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
        PartnerPicker.ItemsSource = Players;
        Opponent1Picker.ItemsSource = Players;
        Opponent2Picker.ItemsSource = Players;
    }

    private async void OnSubmitClicked(object sender, EventArgs e)
    {
        var partner = PartnerPicker.SelectedItem as PlayerDto;
        var opponent1 = Opponent1Picker.SelectedItem as PlayerDto;
        var opponent2 = Opponent2Picker.SelectedItem as PlayerDto;
        var result = WinResultOption.IsChecked ? MatchResult.Team1Wins : MatchResult.Team2Wins;

        await _dataService.CreateMatchOutcomeAsync(new CreateMatchOutcomeRequest(
            new int[] { _authenticationService.CurrentUserId, partner.Id },
            new int[] { opponent1.Id, opponent2.Id },
            result
        ));

        await Shell.Current.GoToAsync("..");
    }
}