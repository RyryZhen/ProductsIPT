using System.Xml.Linq;
using AssignmentFinals.Models;

namespace AssignmentFinals.Services
{
    public class ProductXmlService
    {
        private readonly IWebHostEnvironment _environment;

        public ProductXmlService(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        private string GetFilePath()
        {
            return Path.Combine(_environment.ContentRootPath, "Data", "products.xml");
        }

        // READ ALL
        public List<Product> GetProducts()
        {
            XDocument doc = XDocument.Load(GetFilePath());

            return doc.Descendants("Product")
                .Select(x => new Product
                {
                    ProductID = x.Element("ProductID")?.Value,
                    ProductName = x.Element("ProductName")?.Value,
                    Category = x.Element("Category")?.Value,
                    Price = decimal.Parse(x.Element("Price")?.Value ?? "0"),
                    Quantity = int.Parse(x.Element("Quantity")?.Value ?? "0")
                })
                .ToList();
        }

        // READ ONE
        public Product GetProductById(string id)
        {
            return GetProducts()
                .FirstOrDefault(p => p.ProductID == id);
        }

        // CREATE
        public void AddProduct(Product product)
        {
            XDocument doc = XDocument.Load(GetFilePath());

            doc.Root.Add(
                new XElement("Product",
                    new XElement("ProductID", product.ProductID),
                    new XElement("ProductName", product.ProductName),
                    new XElement("Category", product.Category),
                    new XElement("Price", product.Price),
                    new XElement("Quantity", product.Quantity)
                )
            );

            doc.Save(GetFilePath());
        }

        // UPDATE
        public void UpdateProduct(Product product)
        {
            XDocument doc = XDocument.Load(GetFilePath());

            var existing = doc.Descendants("Product")
                .FirstOrDefault(x => x.Element("ProductID")?.Value == product.ProductID);

            if (existing != null)
            {
                existing.Element("ProductName")!.Value = product.ProductName;
                existing.Element("Category")!.Value = product.Category;
                existing.Element("Price")!.Value = product.Price.ToString();
                existing.Element("Quantity")!.Value = product.Quantity.ToString();

                doc.Save(GetFilePath());
            }
        }

        //     // DELETE
        //     public void DeleteProduct(string id)
        //     {
        //         XDocument doc = XDocument.Load(GetFilePath());

        //         var product = doc.Descendants("Product")
        //             .FirstOrDefault(x => x.Element("ProductID")?.Value == id);

        //         if (product != null)
        //         {
        //             product.Remove();
        //             doc.Save(GetFilePath());
        //         }
        //     }
        // }
        public void DeleteProduct(string id)
        {
            XDocument doc = XDocument.Load(GetFilePath());

            var product = doc.Descendants("Product")
                .FirstOrDefault(x =>
                    x.Element("ProductID")?.Value == id ||
                    string.IsNullOrWhiteSpace(x.Element("ProductID")?.Value)
                );

            if (product != null)
            {
                product.Remove();
                doc.Save(GetFilePath());
            }
        }
    }
}