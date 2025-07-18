﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
namespace TicketManagement.BLL.Results
{
    public class Result
    {
        public bool IsSuccess { get; }
        public Error Error { get; }
        public Result(bool isSuccess, Error? error)
        {
            if (isSuccess && error != Error.None || !isSuccess && error == Error.None)
                throw new InvalidOperationException();

            IsSuccess = isSuccess;
            Error = error;
        }       
        public static Result<TValue> Success<TValue>(TValue value) => new(value, true, Error.None);
        public static Result<TValue> Failure<TValue>(Error? error) => new(default, false, error);

    }

    public class Result<TValue> : Result
    {
        private readonly TValue _value;

        public Result(TValue value, bool isSuccess, Error? error) : base(isSuccess, error)
        {
            _value = value;
        } 

        public TValue Value => IsSuccess ? _value! : throw new InvalidOperationException("Failure results cannot have value");
    }
}
