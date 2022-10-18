using NLayerApp.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayerApp.Core.Dtos.Responses
{
    public class CategoryReadResponseDto : IDto
    {
        public string EncrypedId { get; set; }
        public string Name { get; set; }
    }
}
