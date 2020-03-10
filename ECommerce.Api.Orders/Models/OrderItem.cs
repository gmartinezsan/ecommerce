using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Orders.Models
{
  public class OrderItem
  {
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public Decimal UnitPrice { get; set; }
  }
}
