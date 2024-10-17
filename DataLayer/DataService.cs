﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer;
public class DataService : IDataService
{
    public Category CreateCategory(string name, string description)
    {
        var db = new NorthwindContext();
        int id = (db.Categories?.Any() ?? false) ? db.Categories.Max(x => x.Id) + 1 : 1;
        var category = new Category
        {
            Id = id,
            Name = name,
            Description = description
        };

        db.Categories?.Add(category);

        db.SaveChanges();

        return category;

    }

    public bool DeleteCategory(int id)
    {
        var db = new NorthwindContext();

        var category = db.Categories?.Find(id);

        if (category == null)
        {
            return false;
        }

        db.Categories?.Remove(category);

        return db.SaveChanges() > 0;

    }
    public IList<Category>? GetCategories()
    {
        var db = new NorthwindContext();
        return db.Categories?.ToList();
    }
    public Category? GetCategory(int categoryId)
    {
        var db = new NorthwindContext();
        return db.Categories?.Where(x => x.Id == categoryId).SingleOrDefault(); ;
    }
    public IList<Product>? GetProducts()
    {
        var db = new NorthwindContext();
        return db.Products?.ToList();
    }
    public IList<Product>? GetProducts(int categoryId)
    {
        var db = new NorthwindContext();
        return db.Products?.Where(x => x.CategoryId == categoryId).ToList();
    }

    public bool UpdateCategory(int id, string name, string description)
    {
        var db = new NorthwindContext();

        var category = db.Categories?.Find(id);

        if (category == null)
        {
            return false;
        }
        category.Name = name;
        category.Description = description;
        db.Categories?.Update(category);

        return db.SaveChanges() > 0;

    }
    public ProductDTO? GetProduct(int productId)
    {
        var db = new NorthwindContext();
        var product = db.Products?
                   .Include(p => p.Category)
                   .Where(p => p.Id == productId)
                   .Select(p => new ProductDTO
                   {
                       Id = p.Id,
                       ProductName = p.Name,
                       Name = p.Name,
                       UnitPrice = p.UnitPrice,
                       UnitsInStock = p.UnitsInStock,
                       QuantityPerUnit = p.QuantityPerUnit,
                       CategoryId = p.CategoryId,
                       CategoryName = p.Category != null ? p.Category.Name : null
                   })
                   .FirstOrDefault();

        return product;
    }

    public IList<ProductDTO>? GetProductByCategory(int categoryId)
    {
        var db = new NorthwindContext();

        return db.Products?.Include(p => p.Category).Where(x => x.CategoryId == categoryId).Select(p => new ProductDTO
        {
            Id = p.Id,
            ProductName = p.Name,
            Name = p.Name,
            UnitPrice = p.UnitPrice,
            UnitsInStock = p.UnitsInStock,
            QuantityPerUnit = p.QuantityPerUnit,
            CategoryId = p.CategoryId,
            CategoryName = p.Category != null ? p.Category.Name : null
        }).ToList();
    }
    public IList<ProductDTO>? GetProductByName(string name)
    {
        var db = new NorthwindContext();
        return db.Products?
    .Include(p => p.Category)
    .Where(x => x.Name != null && x.Name.Contains(name))
    .Select(p => new ProductDTO
    {
        Id = p.Id,
        ProductName = p.Name,
        Name = p.Name,
        UnitPrice = p.UnitPrice,
        UnitsInStock = p.UnitsInStock,
        QuantityPerUnit = p.QuantityPerUnit,
        CategoryId = p.CategoryId,
        CategoryName = p.Category != null ? p.Category.Name : null
    }).ToList();
    }
    public Order? GetOrder(int orderId)
    {
        var db = new NorthwindContext();
        var order = db.Orders?.Include(o => o.OrderDetails).ThenInclude(od => od.Product)
            .ThenInclude(p => p.Category)
            .FirstOrDefault(o => o.Id == orderId);
        return order;
    }
    public IList<OrderDTO>? GetOrders()
    {
        var db = new NorthwindContext();
        return db.Orders?.Include(o => o.OrderDetails)
            .ThenInclude(od => od.Product)
            .Select(o => new OrderDTO
            {
                Id = o.Id,
                Required = o.Required,
                ShipName = o.ShipName,
                ShipAddress = o.ShipAddress,
                ShipCity = o.ShipCity,
                Freight = o.Freight,
                ProductName = o.OrderDetails.Select(od => od.Product.Name).ToList()
            }).ToList();
    }
    public IList<OrderDetails>? GetOrderDetailsByOrderId(int orderId)
    {
        var db = new NorthwindContext();
        return db.OrderDetails?.Include(od => od.Product).Where(x => x.OrderId == orderId).ToList();
    }
    public IList<OrderDetails>? GetOrderDetailsByProductId(int productId)
    {
        var db = new NorthwindContext();
        return db.OrderDetails
            .Include(od => od.Product) // Include Product
            .Include(od => od.Order)   // Include Order directly, not with ThenInclude
            .Where(x => x.ProductId == productId)
            .OrderBy(x => x.OrderId)
            .ToList();
    }
}

