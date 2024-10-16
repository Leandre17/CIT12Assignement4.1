using Microsoft.EntityFrameworkCore;
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

        int id = db.Categories.Max(x => x.Id) + 1;
        var category = new Category
        {
            Id = id,
            Name = name,
            Description = description
        };

        db.Categories.Add(category);

        db.SaveChanges();

        return category;

    }

    public bool DeleteCategory(int id)
    {
        var db = new NorthwindContext();

        var category = db.Categories.Find(id);

        if (category == null)
        {
            return false;
        }

        db.Categories.Remove(category);

        return db.SaveChanges() > 0;

    }
    public IList<Category> GetCategories()
    {
        var db = new NorthwindContext();
        return db.Categories.ToList();
    }
    public Category GetCategory(int categoryId)
    {
        var db = new NorthwindContext();
        return db.Categories.Where(x => x.Id == categoryId).SingleOrDefault();;
    }
    public IList<Product> GetProducts()
    {
        var db = new NorthwindContext();
        return db.Products.ToList();
    }
    public IList<Product> GetProducts(int categoryId)
    {
        var db = new NorthwindContext();
        return db.Products.Where(x => x.CategoryId == categoryId).ToList();
    }

    public bool UpdateCategory(int id, string name, string description)
    {
        var db = new NorthwindContext();

        var category = db.Categories.Find(id);

        if (category == null)
        {
            return false;
        }
        category.Name = name;
        category.Description = description;
        db.Categories.Update(category);

        return db.SaveChanges() > 0;

    }
}