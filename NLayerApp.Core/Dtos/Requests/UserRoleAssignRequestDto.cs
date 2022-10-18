using NLayerApp.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayerApp.Core.Dtos.Requests
{
    public class UserRoleAssignRequestDto : IDto
    {
        public string UserId { get; set; }
        public string RoleId { get; set; }

    }
}
