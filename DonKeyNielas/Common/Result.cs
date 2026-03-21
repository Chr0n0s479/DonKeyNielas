namespace DonKeyNielas.Common;

public class Result<T>
{
    public bool Success { get; private set; }

    public string Message { get; private set; } = string.Empty;

    public T? Data { get; private set; }

    private Result(bool success, T? data, string message)
    {
        Success = success;
        Data = data;
        Message = message;
    }

    public static Result<T> Ok(T data)
    {
        return new Result<T>(true, data, string.Empty);
    }

    public static Result<T> Fail(string message)
    {
        return new Result<T>(false, default, message);
    }
}