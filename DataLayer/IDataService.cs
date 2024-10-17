using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer;
public interface IDataService
{
    IList<Category> GetCategories();
    Category GetCategory(int categoryId);

    Category CreateCategory(string name, string description);

    bool DeleteCategory(int id);
    bool UpdateCategory(int id, string name, string description);
    IList<Product> GetProducts();
    ProductDTO GetProduct(int productId);
    IList<ProductDTO> GetProductByCategory(int categoryId);
    IList<ProductDTO> GetProductByName(string name);

    Order GetOrder(int orderId);
    IList<OrderDTO> GetOrders();
}
