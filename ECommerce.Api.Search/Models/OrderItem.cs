﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Search.Models
{
  public class OrderItem
  {
    public int Id { get; set; }
    public int ProductId { get; set; }
    public string ProductName { get; set;}
    public int Quantity { get; set; }
    public Decimal UnitPrice { get; set; }
  }
}
