namespace Core.Utilities.Results
{
    public class Result : IResult
    {
        public object Data { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
