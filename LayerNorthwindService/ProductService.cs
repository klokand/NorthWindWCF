using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using LayerNorthwindBDO;
using LayerNorthwindLogic;

namespace LayerNorthwindService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class ProductService : IProductService
    {
        private void TranslateProductBDOToProductDTO(ProductBDO productBDO, Product product)
        {
            product.ProductID = productBDO.ProductID;
            product.ProductName = productBDO.ProductName;
            product.QuantityPerUnit = productBDO.QuantityPerUnit;
            product.UnitPrice = productBDO.UnitPrice;
            product.Discontinued = productBDO.Discontinued;
        }

        private void TranslateProductDTOToProductBDO(Product product, ProductBDO productBDO){
            productBDO.ProductID = product.ProductID;
            productBDO.ProductName = product.ProductName;
            productBDO.QuantityPerUnit = product.QuantityPerUnit;
            productBDO.UnitPrice = product.UnitPrice;
            productBDO.Discontinued = product.Discontinued;
        }

        public Product GetProduct(int id)
        {
            var productLogic = new ProductLogic();
            var productBDO = productLogic.GetProduct(id);
            var product = new Product();
            if (productBDO != null) {
                TranslateProductBDOToProductDTO(productBDO, product);
            }
            return product;
        }

        public bool UpdateProduct(Product product, ref string message)
        {
            var result = true;
            // first check to see if it is a valid price
            if (product.UnitPrice <= 0)
            {
                message = "Price cannot be <= 0";
                result = false;
            }
            // QuantityPerUnit can't be empty
            else if (string.IsNullOrEmpty(product.QuantityPerUnit))
            {
                message = "Quantity cannot be empty";
                result = false;
            }
            else
            {
                var productLogic = new ProductLogic();
                var productBDO = new ProductBDO();
                TranslateProductDTOToProductBDO(product,productBDO);
                return productLogic.UpdateProduct(productBDO, ref message);
            }
            return result;
        }
    }
}
