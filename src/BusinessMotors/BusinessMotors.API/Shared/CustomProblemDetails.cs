using System.Net;

namespace BusinessMotors.API.Shared
{
    public class CustomProblemDetails : ProblemDetails
    {
        public List<string> Errors { get; private set; }

        public CustomProblemDetails(HttpStatusCode status, string? detail = null, IEnumerable<string>? errors = null) : this()
        {
            Title = status switch
            {
                HttpStatusCode.BadRequest => "One or more validation errors occurred.",
                HttpStatusCode.InternalServerError => "Internal server error.",
                HttpStatusCode.NotFound => "Object not found",
                _ => "An error has occurred."
            };
            
            Status = (int)status;
            Detail = detail;

            if (errors is not null)
            {
                if (errors.Count() == 1)
                    Detail = errors.First();
                else if (errors.Count() > 1)
                    Detail = "Multiple problems have occurred.";                

                Errors.AddRange(errors);
            }
        }

        public CustomProblemDetails(HttpStatusCode status, HttpRequest request, string? detail = null, IEnumerable<string>? errors = null) : this(status, detail, errors) =>
            Instance = request.Path;

        private CustomProblemDetails() =>
            Errors = new List<string>();
    }
}