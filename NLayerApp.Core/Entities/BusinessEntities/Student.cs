using NLayerApp.Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayerApp.Core.Entities.BusinessEntities
{
    public class Student : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int StudentNo { get; set; }
        public ICollection<Teacher> Teachers { get; set; }
    }
}

//Student Many-To-Many Teacher relationship 
//public ICollection<Teacher> Teachers { get; set; }