﻿using AnticiPay.Exception.Exceptions.Base;
using AnticiPay.Exception.Resources;
using System.Net;

namespace AnticiPay.Exception.Exceptions;
public class InvalidLoginException : AnticiPayException
{
    public InvalidLoginException() : base(ResourceErrorMessages.EMAIL_OR_PASSWORD_INVALID)
    {
    }

    public override int StatusCode => (int)HttpStatusCode.Unauthorized;

    public override List<string> GetErrors()
    {
        return [Message];
    }
}
