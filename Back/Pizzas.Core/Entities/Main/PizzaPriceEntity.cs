using System.ComponentModel.DataAnnotations.Schema;
using MongoDB.Bson;
using Pizzas.Core.Enums;

namespace Pizzas.Core.Entities.Main;

public class PizzaPriceEntity
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public string Id { get; set; } = ObjectId.GenerateNewId().ToString();
    public PizzaSize Size { get; set; }
    public decimal OriginalPrice { get; set; }
    public decimal DiscountPrice { get; set; }
    public string PizzaId { get; set; } = string.Empty;
    public virtual PizzaEntity Pizza { get; set; } = null!;
}