using NLayerApp.Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayerApp.Core.Entities.BusinessEntities
{
    public class Teacher : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<Student> Students { get; set; }
    }
}
//Teacher Many-To-Many Student relationship 
//public ICollection<Student> Students { get; set; }