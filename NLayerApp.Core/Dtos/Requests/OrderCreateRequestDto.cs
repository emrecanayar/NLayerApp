﻿using NLayerApp.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayerApp.Core.Dtos.Requests
{
    public class OrderCreateRequestDto : IDto
    {
        public DateTime OrderDate { get; set; }
    }
}
