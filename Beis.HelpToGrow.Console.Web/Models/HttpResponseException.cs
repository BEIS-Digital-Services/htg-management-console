﻿namespace Beis.HelpToGrow.Console.Web.Models
{
    using System;
    
    public class HttpResponseException : Exception
    {
        public int Status { get; set; } = 500;

        public object Value { get; set; }
    }
}