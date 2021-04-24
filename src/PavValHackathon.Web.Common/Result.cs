using System;
using System.Threading.Tasks;

namespace PavValHackathon.Web.Common
{
    public record Result
    {
        protected Result()
        {
            IsFailed = false;
        }

        protected Result(int errorCode, string errorMessage)
        {
            IsFailed = true;
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
            TraceId = Guid.NewGuid().ToString();
        }
        
        public bool IsFailed { get; }
        
        public int? ErrorCode { get; }
        
        public string? ErrorMessage { get; }
        
        public string? TraceId { get; }

        public static Result Ok() => new();
        public static Task<Result> OkAsync() => Task.FromResult(Ok());

        public static Result Failed(int errorCode, string errorMessage) => new(errorCode, errorMessage);
        public static Task<Result> FailedAsync(int errorCode, string errorMessage) => Task.FromResult(Failed(errorCode, errorMessage));
        
        public static Result<TValue> Ok<TValue>(TValue value) => new(value);
        public static Task<Result<TValue>> OkAsync<TValue>(TValue value) => Task.FromResult(Ok(value));
        
        public static Result<TValue> Failed<TValue>(int errorCode, string errorMessage) => new(errorCode, errorMessage);
        public static Task<Result<TValue>> FailedAsync<TValue>(int errorCode, string errorMessage) => Task.FromResult(Failed<TValue>(errorCode, errorMessage));
    }

    public record Result<TValue> : Result
    {
        protected internal Result(TValue value)
        {
            Value = value;
        }

        protected internal Result(int errorCode, string errorMessage)
            : base(errorCode, errorMessage)
        {
        }
        
        public TValue? Value { get; }
    }
}