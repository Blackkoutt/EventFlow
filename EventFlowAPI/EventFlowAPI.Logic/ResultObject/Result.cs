using EventFlowAPI.Logic.DTO.Interfaces;
using EventFlowAPI.Logic.Errors;

namespace EventFlowAPI.Logic.ResultObject
{
    public class Result<T>
    {
        private readonly T? _value;  
        private Result(T value)
        {
            _value = value;
            IsSuccessful = true;
            Error = Error.None;
        }
        private Result()
        {
            IsSuccessful = true;
            Error = Error.None;
        }
        private Result(Error error)
        {
            if(error == Error.None)
            {
                throw new ArgumentException("Invalid error");
            }
            IsSuccessful = false;
            Error = error;
        }

        public T Value
        {
            get
            {
                if (IsFailed)
                {
                    throw new InvalidOperationException("No value for failure");
                }
                return _value!;
            }
            private init => _value = value;
        }

        public bool IsSuccessful { get; }
        public bool IsFailed => !IsSuccessful;
        public Error Error { get; }

        public static Result<T> Success(T value) => new Result<T>(value);
        public static Result<T> Success() => new Result<T>();
        public static Result<T> Failure(Error error) => new Result<T>(error);
    }
}
