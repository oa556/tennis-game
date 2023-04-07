using System.Net;
using Mapster;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using TennisGame.Api.Models;
using TennisGame.Shared;

namespace TennisGame.Api.Presentation;

public class GetAllPlayers
{
    [Function("GetAllPlayers")]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "players")]
        HttpRequestData req)
    {
        var players = new Player[]
        {
            new Player { Id = 1, Name = "Player 1", Skill = 2 },
            new Player { Id = 2, Name = "Player 2", Skill = 7 },
            new Player { Id = 3, Name = "Player 3", Skill = 5 }
        };

        var response = req.CreateResponse(HttpStatusCode.OK);
        response.Headers.Add("Content-Type", "application/json; charset=utf-8");
        await response.WriteStringAsync(JsonConvert.SerializeObject(
            players.Adapt<PlayerDto[]>(),
            new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() }
        ));

        return response;
    }
}
