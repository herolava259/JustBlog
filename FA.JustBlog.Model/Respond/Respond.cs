namespace FA.JustBlog.Model.Respond
{
    public class Response<TData>
    {
        /// <summary>
        /// Create a response with sucscess result
        /// </summary>
        public Response()
        {
            this.IsSuccessed = true;
        }
        /// <summary>
        /// Create a response with faile result with errror message
        /// </summary>
        /// <param name="errorMessage"></param>
        public Response(string errorMessage)
        {
            this.IsSuccessed = false;
            this.ErrorMessage = errorMessage;
        }
        /// <summary>
        /// State of response
        /// </summary>
        public bool IsSuccessed { get; set; }

        /// <summary>
        /// Error Message
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Status code
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// Data
        /// </summary>
        public TData Data { get; set; }
    }
}
