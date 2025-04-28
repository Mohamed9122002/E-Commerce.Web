﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Exceptions
{
    public sealed class BadRequestException(List<string> erros) : Exception("Validation Valid")
    {
        public List<string> Erros { get; } = erros;

    }
}
