using NLayerApp.Core.Base;

namespace NLayerApp.Core.Entities.BusinessEntities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public string EncrypedId { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}


/*
 Product and Category One To Many RelationShip
 Category Entity must have IColletcion Type Products.
 */