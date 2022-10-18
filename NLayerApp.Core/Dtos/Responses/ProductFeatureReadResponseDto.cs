using NLayerApp.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayerApp.Core.Dtos.Responses
{
    public class ProductFeatureReadResponseDto : IDto
    {
        public string Color { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
    }
}
