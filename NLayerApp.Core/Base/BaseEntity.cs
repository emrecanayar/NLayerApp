using NLayerApp.Core.ComplexTypes;
using NLayerApp.Core.Interfaces;

namespace NLayerApp.Core.Base
{
    public abstract class BaseEntity : IEntity
    {
        public int Id { get; set; }
        public RecordStatu Status { get; set; }
        public string CreatedBy { get; set; } = "Admin";
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; }
    }
}
