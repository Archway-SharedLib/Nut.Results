﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Nut.Results
{
    public interface IError
    {
        string Message { get; }

        Exception ToException()
        {
            return Message is null ? new Exception() : new Exception(Message);
        }
    }
}