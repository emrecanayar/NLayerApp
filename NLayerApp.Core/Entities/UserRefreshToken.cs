using NLayerApp.Core.Base;
using NLayerApp.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayerApp.Core.Entities
{
    public class UserRefreshToken : BaseEntity, IMemberShipEntity
    {
        public string UserId { get; set; }
        public User User { get; set; }
        public string Token { get; set; }
        public DateTime TokenExpireDate { get; set; }
        public string CreatorIp { get; set; }
        public string RevokerIp { get; set; }
        public DateTime? RevokeDate { get; set; }
        public string ReplacedByToken { get; set; }
    }
}
