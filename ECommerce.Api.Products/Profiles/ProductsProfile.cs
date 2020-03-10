using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Products.Profiles
{
  public class ProductsProfile : AutoMapper.Profile
  {
    public ProductsProfile()
    {
      CreateMap<Db.Product, Models.Product>();

    }
  }
}
