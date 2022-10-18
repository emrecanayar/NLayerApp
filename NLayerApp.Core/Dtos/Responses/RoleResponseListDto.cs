using NLayerApp.Core.Entities;
using NLayerApp.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayerApp.Core.Dtos.Responses
{
    public class RoleResponseListDto : IDto
    {
        public List<Role> Roles { get; set; }
    }
}
