using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using TennisGame.Api.Models;
using TennisGame.Shared;
using Mapster;
using TennisGame.Api.Persistence;

namespace TennisGame.Api.Presentation;

internal class GetPlayer
{
    private readonly IPlayerRepository _playerRepository;

    public GetPlayer(IPlayerRepository playerRepository)
    {
        _playerRepository = playerRepository;
    }

    [Function("GetPlayer")]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "players/{id:int}")]
        HttpRequestData req,
        int id)
    {
        Player? player = await _playerRepository.FindAsync(id);
        if (player == null)
        {
            return req.CreateResponse(HttpStatusCode.NotFound);
        }

        var response = req.CreateResponse(HttpStatusCode.OK);
        response.Headers.Add("Content-Type", "application/json; charset=utf-8");
        await response.WriteStringAsync(JsonConvert.SerializeObject(
            player.Adapt<PlayerDto>(),
            new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() }
        ));

        return response;
    }
}
