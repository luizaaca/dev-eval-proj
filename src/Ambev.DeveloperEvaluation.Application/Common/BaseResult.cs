namespace Ambev.DeveloperEvaluation.Application.Common;

public abstract record BaseResult(bool Success, string? Message = null, Exception? Exception = null);
public record BaseResult<T> : BaseResult
{
    public T? Data { get; }

    public BaseResult(bool success, T? data = default) : base(success)
    {
        Data = data;
    }

    public BaseResult(bool success, string message) : base(success, message) { }
    public BaseResult(bool success, string message, Exception exception) : base(success, message, exception) { }

    public static BaseResult<T> Ok(T data) => new(true, data: data);
    public static BaseResult<T> Fail(string message) => new(false, message);
    public static BaseResult<T> Fail(string message, Exception exception) => new(false, message, exception);
}
