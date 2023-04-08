using System.Net;
using Mapster;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using TennisGame.Api.Models;
using TennisGame.Api.Persistence;
using TennisGame.Shared;

namespace TennisGame.Api.Presentation;

internal class GetAllPlayers
{
    private readonly IPlayerRepository _playerRepository;

    public GetAllPlayers(IPlayerRepository playerRepository)
    {
        _playerRepository = playerRepository;
    }

    [Function("GetAllPlayers")]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "players")]
        HttpRequestData req)
    {
        Player[] players = await _playerRepository.GetAllAsync();

        var response = req.CreateResponse(HttpStatusCode.OK);
        response.Headers.Add("Content-Type", "application/json; charset=utf-8");
        await response.WriteStringAsync(JsonConvert.SerializeObject(
            players.Adapt<PlayerDto[]>(),
            new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() }
        ));

        return response;
    }
}
