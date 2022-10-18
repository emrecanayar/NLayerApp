using NLayerApp.Core.Base;

namespace NLayerApp.Core.Entities.BusinessEntities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public int Stock { get; set; }
        public int Amount { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public ProductFeature ProductFeature { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}

/*
 Product(Child) and Category(Parant) One To Many RelationShip
 Product Entity must have CategoryId and Category navigation property.
 */


/*
 Product(Parent) and ProductFeature(Child) One To One RelationShip
 Product Entity must have ProductFeature navigation property.
 */