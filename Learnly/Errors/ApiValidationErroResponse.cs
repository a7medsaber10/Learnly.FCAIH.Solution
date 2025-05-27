namespace Learnly.APIs.Errors
{
    public class ApiValidationErroResponse : ApiResponse
    {
        public IEnumerable<string> Errors { get; set; }

        // validation Error is a type of Bad Request so status code = 400
        public ApiValidationErroResponse() : base(400)
        {
            Errors = new List<string>();

        }
    }
}
