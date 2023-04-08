using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using TennisGame.Api.Services;
using TennisGame.Shared;

namespace TennisGame.Api.Presentation;

internal class CreateMatchOutcome
{
    private readonly IMatchOutcomeService _matchOutcomeService;

    public CreateMatchOutcome(IMatchOutcomeService matchOutcomeService)
    {
        _matchOutcomeService = matchOutcomeService;
    }

    [Function("CreateMatchOutcome")]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "matches")]
        HttpRequestData req)
    {
        string body = await new StreamReader(req.Body).ReadToEndAsync();
        var request = JsonConvert.DeserializeObject<CreateMatchOutcomeRequest>(body);
        if (request == null)
        {
            return req.CreateResponse(HttpStatusCode.BadRequest);
        }

        try
        {
            await _matchOutcomeService.CreateMatchOutcomeAsync(request);
        }
        catch (ArgumentException)
        {
            return req.CreateResponse(HttpStatusCode.BadRequest);
        }

        var response = req.CreateResponse(HttpStatusCode.Created);
        return response;
    }
}
