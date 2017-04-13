using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LayerNorthwindBDO;
using LayerNorthwindDAL;


namespace LayerNorthwindLogic
{
    public class ProductLogic
    {
        ProductDAO productDAO = new ProductDAO();
        public ProductBDO GetProduct(int id) {
            // TODO: call data access layer to retrieve product
            return productDAO.GetProduct(id);
        }
        public bool UpdateProduct(ProductBDO product, ref string message)
        {
            var productInDB = GetProduct(product.ProductID);
            // invalid product to update
            if (productInDB == null)
            {
                message = "cannot get product for this ID";
                return false;
            }
            // a product can't be discontinued
            // if there are non-fulfilled orders
            else if (product.Discontinued == true&& productInDB.UnitsOnOrder > 0)
            {
                message = "cannot discontinue this product";
                return false;
            }
            // ProductName can't be empty
            else if (string.IsNullOrEmpty(product.ProductName))
            {
                message = "Product name cannot be empty, try again";
                return false;
            }
            else
            {
                // TODO: call data access layer to update product
                return productDAO.UpdateProduct(product, ref message);
            }
        }
    }
}
