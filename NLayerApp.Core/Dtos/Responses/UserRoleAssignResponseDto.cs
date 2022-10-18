using NLayerApp.Core.Dtos.Requests;
using NLayerApp.Core.Entities;
using NLayerApp.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayerApp.Core.Dtos.Responses
{
    public class UserRoleAssignResponseDto : IDto
    {
        public UserRoleAssignResponseDto()
        {
            UserRoles = new List<string>();
        }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public IList<string> UserRoles { get; set; }
    }
}
