using NLayerApp.Core.Base;
using NLayerApp.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayerApp.Core.Entities.BusinessEntities
{
    public class ProductFeature : IEntity
    {
        public int Id { get; set; }
        public string Color { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }

    }
}
/*
 Product(Parent) and ProductFeature(Child) One To One RelationShip
 ProductFeature Entity must have ProductId and Product navigation property.
 */