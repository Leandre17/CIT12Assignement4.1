using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer;
public class Order
{
    public int Id { get; set; }
    public DateTime Date { get; set; } = DateTime.MinValue;
    public string? CustomerId { get; set; }
    public int EmployeeId { get; set; }
    public DateTime Required { get; set; }
    public DateTime? ShippedDate { get; set; }
    public double Freight { get; set; }
    public string? ShipName { get; set; }
    public string? ShipAddress { get; set; }
    public string? ShipCity { get; set; }
    public List<OrderDetails>? OrderDetails { get; set; } 
}

public class OrderDTO
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public DateTime ShippedDate { get; set; }
    public DateTime Required { get; set; }
    public string? ShipName { get; set; }
    public string? ShipAddress { get; set; }
    public string? ShipCity { get; set; }
    public double Freight { get; set; }
    public List<string>? ProductName { get; set; }
    public List<OrderDetails>? OrderDetails { get; set; } 
}