using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer;
public class Product
{
    public int Id { get; set; } = 0;
    public string Name { get; set; } = null;
    public double UnitPrice { get; set; } = 0.0;
    public int UnitsInStock { get; set; } = 0;
    public string QuantityPerUnit { get; set; } = null;
    public int CategoryId { get; set; }
    public Category Category { get; set; }
}

public class ProductDTO
{
    public int Id { get; set; }
    public string ProductName { get; set; }
    public string Name { get; set; }
    public double UnitPrice { get; set; }
    public int UnitsInStock { get; set; }
    public string QuantityPerUnit { get; set; }
    public int CategoryId { get; set; }
    public string CategoryName { get; set; }  // New field
}