
namespace TicketManagement.BLL.Results
{
    public class Error
    {
        public static readonly Error None = new("OK", string.Empty, 200);
        public int StatusCode { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public Error()
        {
            Title = string.Empty;
            StatusCode = 400;
        }
        public Error(string message, string title, int statusCode)
        {
            StatusCode = statusCode;
            Title = title ?? GetDefaultMessageForStatusCode(statusCode);
            Message = message;
        } 
        private string GetDefaultMessageForStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "A bad request, you have made!",
                401 => "Authorized, you are not!",
                404 => "Resource was not found!",
                500 => "Server Error",
                _ => "Invalid request"
            };
        }
    }

}
