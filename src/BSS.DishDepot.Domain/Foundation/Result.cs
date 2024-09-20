using System.Runtime.Serialization;

namespace BSS.DishDepot.Domain.Foundation;

public enum ResultKind
{
    Success = 0,
    Invalid,
    NotFound,
    Unauthorized,
    Unexpected
}

[DataContract]
public sealed class Result
{
    [DataMember]
    public string? Message { get; set; }

    [DataMember]
    public ResultKind? Kind { get; set; }

    [DataMember]
    public Exception? Exception { get; set; }

    [DataMember]
    public bool IsSuccessful => Kind == ResultKind.Success;

    public Result WithException(Exception ex)
    {
        Exception = ex;
        return this;
    }

    public static Result Success()
    {
        return new Result
        {
            Kind = ResultKind.Success
        };
    }

    public static Result Success(string message)
    {
        return new Result
        {
            Kind = ResultKind.Success,
            Message = message
        };
    }

    public static Result Invalid()
    {
        return new Result
        {
            Kind = ResultKind.Invalid
        };
    }

    public static Result Invalid(string message)
    {
        return new Result
        {
            Kind = ResultKind.Invalid,
            Message = message
        };
    }

    public static Result NotFound()
    {
        return new Result
        {
            Kind = ResultKind.NotFound
        };
    }

    public static Result NotFound(string message)
    {
        return new Result
        {
            Kind = ResultKind.NotFound,
            Message = message
        };
    }

    public static Result Unauthorized()
    {
        return new Result
        {
            Kind = ResultKind.Unauthorized
        };
    }

    public static Result Unauthorized(string message)
    {
        return new Result
        {
            Kind = ResultKind.Unauthorized,
            Message = message
        };
    }

    public static Result Unexpected()
    {
        return new Result
        {
            Kind = ResultKind.Unexpected
        };
    }

    public static Result Unexpected(string message)
    {
        return new Result
        {
            Kind = ResultKind.Unexpected,
            Message = message
        };
    }

    public static Result Unexpected(Exception ex)
    {
        return new Result
        {
            Kind = ResultKind.Unexpected,
            Exception = ex
        };
    }

    public static Result Unexpected(string message, Exception ex)
    {
        return new Result
        {
            Kind = ResultKind.Unexpected,
            Message = message,
            Exception = ex
        };
    }

    public static Result Clone<T>(Result<T> result)
    {
        return new Result
        {
            Kind = result.Kind,
            Message = result.Message,
            Exception = result.Exception
        };
    }

    public static Result Clone(Result result)
    {
        return new Result
        {
            Kind = result.Kind,
            Message = result.Message,
            Exception = result.Exception
        };
    }
}

[DataContract]
public sealed class Result<T>
{
    [DataMember]
    public T? Data { get; set; }

    [DataMember]
    public string? Message { get; set; }

    [DataMember]
    public ResultKind? Kind { get; set; }

    [DataMember]
    public Exception? Exception { get; set; }

    [DataMember]
    public bool IsSuccessful => Kind == ResultKind.Success;

    public Result<T> WithException(Exception ex)
    {
        Exception = ex;
        return this;
    }

    public static Result<T> Success()
    {
        return new Result<T>
        {
            Kind = ResultKind.Success
        };
    }

    public static Result<T> Success(T data)
    {
        return new Result<T>
        {
            Kind = ResultKind.Success,
            Data = data
        };
    }

    public static Result<T> Success(T data, string message)
    {
        return new Result<T>
        {
            Kind = ResultKind.Success,
            Data = data,
            Message = message
        };
    }

    public static Result<T> Invalid()
    {
        return new Result<T>
        {
            Kind = ResultKind.Invalid
        };
    }

    public static Result<T> Invalid(string message)
    {
        return new Result<T>
        {
            Kind = ResultKind.Invalid,
            Message = message
        };
    }

    public static Result<T> Invalid(T data)
    {
        return new Result<T>
        {
            Kind = ResultKind.Invalid,
            Data = data
        };
    }

    public static Result<T> Invalid(T data, string message)
    {
        return new Result<T>
        {
            Kind = ResultKind.Invalid,
            Data = data,
            Message = message
        };
    }

    public static Result<T> NotFound()
    {
        return new Result<T>
        {
            Kind = ResultKind.NotFound
        };
    }

    public static Result<T> NotFound(string message)
    {
        return new Result<T>
        {
            Kind = ResultKind.NotFound,
            Message = message
        };
    }

    public static Result<T> Unauthorized()
    {
        return new Result<T>
        {
            Kind = ResultKind.Unauthorized
        };
    }

    public static Result<T> Unauthorized(string message)
    {
        return new Result<T>()
        {
            Kind = ResultKind.Unauthorized,
            Message = message
        };
    }

    public static Result<T> Unauthorized(T data, string message)
    {
        return new Result<T>
        {
            Kind = ResultKind.Unauthorized,
            Data = data,
            Message = message
        };
    }

    public static Result<T> Unexpected()
    {
        return new Result<T>
        {
            Kind = ResultKind.Unexpected
        };
    }

    public static Result<T> Unexpected(T data)
    {
        return new Result<T>
        {
            Kind = ResultKind.Unexpected,
            Data = data
        };
    }

    public static Result<T> Unexpected(string message)
    {
        return new Result<T>
        {
            Kind = ResultKind.Unexpected,
            Message = message
        };
    }

    public static Result<T> Unexpected(Exception ex)
    {
        return new Result<T>
        {
            Kind = ResultKind.Unexpected,
            Exception = ex
        };
    }

    public static Result<T> Unexpected(string message, Exception ex)
    {
        return new Result<T>
        {
            Kind = ResultKind.Unexpected,
            Message = message,
            Exception = ex
        };
    }

    public static Result<T> Clone(Result result)
    {
        return new Result<T>
        {
            Kind = result.Kind,
            Message = result.Message,
            Exception = result.Exception
        };
    }

    public static Result<T> Clone(Result<T> result)
    {
        return new Result<T>
        {
            Kind = result.Kind,
            Message = result.Message,
            Exception = result.Exception
        };
    }

    public static Result<T> Clone<TU>(Result<TU> result)
    {
        return new Result<T>
        {
            Kind = result.Kind,
            Message = result.Message,
            Exception = result.Exception
        };
    }
}