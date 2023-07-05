using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using OfferApp.Infrastructure;

namespace OfferApp.Api.Controllers
{
    [ApiController]
    [Route("/api")]
    public class HealthCheckController : ControllerBase
    {
        private readonly AppOptions _appOptions;

        public HealthCheckController(IOptionsMonitor<AppOptions> appOptions)
        {
            _appOptions = appOptions.CurrentValue;
        }

        [HttpGet]
        public string Get()
        {
            return $"Welcome to {_appOptions.Name}";
        }
    }
}