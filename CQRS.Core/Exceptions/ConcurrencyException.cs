﻿using System;
namespace CQRS.Core.Exceptions
{
    public class ConcurrencyException : Exception
    {
        public ConcurrencyException(string message) : base(message) 
        {
        }

        public ConcurrencyException()
        {

        }
    }
}

