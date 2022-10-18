using Microsoft.AspNetCore.Identity;
using NLayerApp.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayerApp.Core.Entities
{
    public class User : IdentityUser, IMemberShipEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
