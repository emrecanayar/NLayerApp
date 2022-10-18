using NLayerApp.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayerApp.Core.Dtos.Requests
{
    public class LoginRequestDto : IDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
