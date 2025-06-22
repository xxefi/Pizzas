using System.ComponentModel.DataAnnotations.Schema;
using MongoDB.Bson;
using NanoidDotNet;
using Pizzas.Core.Enums;

namespace Pizzas.Core.Entities.Main;

public class PizzaPriceEntity
{
    public string Id { get; set; } = Nanoid.Generate(size: 24);
    public PizzaSize Size { get; set; }
    public decimal OriginalPrice { get; set; }
    public decimal DiscountPrice { get; set; }
    public string PizzaId { get; set; } = string.Empty;
    public virtual PizzaEntity Pizza { get; set; } = null!;
}