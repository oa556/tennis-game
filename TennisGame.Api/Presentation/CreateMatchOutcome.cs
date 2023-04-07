using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using TennisGame.Shared;

namespace TennisGame.Api.Presentation;

public class CreateMatchOutcome
{
    private ILogger<CreateMatchOutcome> _logger;

    public CreateMatchOutcome(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<CreateMatchOutcome>();
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

        _logger.LogInformation(request.ToString());

        var response = req.CreateResponse(HttpStatusCode.Created);
        return response;
    }
}
