﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core
{
    public class Result<T>
    {
        public T Value { get; set; }
        public string ResultMessage { get; set; }
        public int ResultCode { get; set; }
        public static Result<T> Success(int resCode, string message, T value)
            => new Result<T>
            { Value = value, ResultMessage = message, ResultCode = resCode };
        public static Result<T> Success(int resCode, string message)
            => new Result<T> { ResultMessage = message, ResultCode = resCode };
        public static Result<T> Failure(int resCode, string message)
            => new Result<T> { ResultMessage = message, ResultCode = resCode };
    }

    public class Result
    {
        public static Result<T> Success<T>(int resCode, string message, T value)
            => new Result<T>
            { Value = value, ResultMessage = message, ResultCode = resCode };
        public static Result<Unit> Success(int resCode, string message)
            => new Result<Unit> { ResultMessage = message, ResultCode = resCode };
        public static Result<Unit> Failure(int resCode, string message)
            => new Result<Unit> { ResultMessage = message, ResultCode = resCode };
    }
    public class ResultForNullValues
    {
        public string ResultMessage { get; set; }
        public int ResultCode { get; set; }

        public static ResultForNullValues Create<T>(Result<T> result)
            => new ResultForNullValues
            { ResultMessage = result.ResultMessage, ResultCode = result.ResultCode };
    }
    public class AppException
    {
        public AppException(int statusCode, string message, string details = null)
        {
            StatusCode = statusCode;
            Message = message;
            Details = details;
        }
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string Details { get; set; }
    }
}
