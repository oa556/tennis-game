using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using TennisGame.Api.Models;
using TennisGame.Shared;
using Mapster;

namespace TennisGame.Api.Presentation;

public class GetPlayer
{
    [Function("GetPlayer")]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "players/{id:int}")]
        HttpRequestData req,
        int id)
    {
        var player = new Player { Id = id, Name = "Player 1", Skill = 7 };

        var response = req.CreateResponse(HttpStatusCode.OK);
        response.Headers.Add("Content-Type", "application/json; charset=utf-8");
        await response.WriteStringAsync(JsonConvert.SerializeObject(
            player.Adapt<PlayerDto>(),
            new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() }
        ));

        return response;
    }
}
