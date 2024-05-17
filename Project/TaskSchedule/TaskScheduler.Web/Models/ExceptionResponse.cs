using System.Net;

namespace TaskScheduler.Web.Models
{
    public record ExceptionResponse(HttpStatusCode StatusCode, string Description);

}
