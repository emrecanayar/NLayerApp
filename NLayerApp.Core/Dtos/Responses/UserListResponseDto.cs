using NLayerApp.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayerApp.Core.Dtos.Responses
{
    public class UserListResponseDto : IDto
    {
        public List<UserResponseDto> Users { get; set; }
    }
}
