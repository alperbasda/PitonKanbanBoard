namespace Core.Utilities.Results
{
    public interface IResult
    {
        object Data { get; set; }
        bool Success { get; set; }
        string Message { get; set; }
    }
}
