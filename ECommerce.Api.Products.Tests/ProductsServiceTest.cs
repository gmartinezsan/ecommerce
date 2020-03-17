using System;
using Xunit;
using Microsoft.EntityFrameworkCore;
using ECommerce.Api.Products.Db;
using ECommerce.Api.Products.Profiles;
using ECommerce.Api.Products.Providers;
using AutoMapper;
using System.Threading.Tasks;
using System.Linq;

namespace ECommerce.Api.Products.Tests
{
  public class ProductsServiceTest
  {
    [Fact]
    public async Task GetProductsReturnsAllProducts()
    {
      var options = new DbContextOptionsBuilder<ProductsDbContext>() 
      .UseInMemoryDatabase(nameof(GetProductsReturnsAllProducts))
      .EnableSensitiveDataLogging()
      .Options;

       var dbContext = new ProductsDbContext(options);
       CreateProducts(dbContext);

       var productProfile = new ProductsProfile();  
       var configuration = new MapperConfiguration(cfg => cfg.AddProfile(productProfile)); 
       var mapper = new Mapper(configuration);

       var productsProvider = new ProductsProvider(dbContext, null, mapper);
       var products = await productsProvider.GetProductsAsync();

       Assert.True(products.IsSuccess);
       Assert.True(products.Products.Any());
       Assert.Null(products.ErrorMessage);
    }

    [Fact]
    public async Task GetProductsReturnsProductUsingInvalidId()
    {
      var options = new DbContextOptionsBuilder<ProductsDbContext>()
      .UseInMemoryDatabase(nameof(GetProductsReturnsAllProducts))
      .EnableSensitiveDataLogging()
      .Options;

      var dbContext = new ProductsDbContext(options);
      CreateProducts(dbContext);

      var productProfile = new ProductsProfile();
      var configuration = new MapperConfiguration(cfg => cfg.AddProfile(productProfile));
      var mapper = new Mapper(configuration);

      var productsProvider = new ProductsProvider(dbContext, null, mapper);
      var product = await productsProvider.GetProductAsync(-1);

      Assert.False(product.IsSuccess);
      Assert.NotNull(product.ErrorMessage);
      Assert.Null(product.Product);
    }

    private void CreateProducts(ProductsDbContext dbContext)
    {
      if (dbContext.Products.Any())
        return;

       for (int i = 1; i <= 10; i++)
       {
        dbContext.Products.Add(
          new Product()
          { 
              Id = i,
              Name = Guid.NewGuid().ToString(),
              Inventory= i + 10,
              Price = (decimal)(i * 5.2) 
          }
        );
       }
       dbContext.SaveChanges();
    }
  }
}
