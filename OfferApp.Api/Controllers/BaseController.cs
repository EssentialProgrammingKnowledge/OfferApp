using Microsoft.AspNetCore.Mvc;

namespace OfferApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        public ActionResult<T?> OkOrNotFound<T>(T? model)
        {
            return model is not null ? Ok(model) : NotFound();
        }
    }
}
