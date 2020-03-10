using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Orders.Profiles
{
  public class OrderItemProfile : AutoMapper.Profile
  {
    public OrderItemProfile()
    {
      CreateMap<Db.OrderItem, Models.OrderItem>();
    }
  }
}
