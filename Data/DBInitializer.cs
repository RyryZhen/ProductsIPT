using System.Xml.Linq;
using AssignmentFinals.Models;

namespace AssignmentFinals.Data
{
    public static class DbInitializer
    {
        public static void Seed(AppDbContext context, IWebHostEnvironment env)
        {
            // Ensure the database is created
            context.Database.EnsureCreated();

            // Check if the Products table already has data. If it does, don't seed again.
            if (context.Products.Any())
            {
                return; 
            }

            // Find the products.xml path
            string filePath = Path.Combine(env.ContentRootPath, "Data", "products.xml");

            if (File.Exists(filePath))
            {
                XDocument doc = XDocument.Load(filePath);

                var productsFromXml = doc.Descendants("Product").Select(p => new Product
                {
                    // If your MySQL table is set to auto-increment, you can omit mapping ProductID,
                    // but since the XML provides it, we turn off auto-increment or explicitly map it:
                    ProductID = int.Parse(p.Element("ProductID")?.Value ?? "0"),
                    ProductName = p.Element("ProductName")?.Value ?? string.Empty,
                    Category = p.Element("Category")?.Value ?? string.Empty,
                    Price = decimal.Parse(p.Element("Price")?.Value ?? "0.00"),
                    Quantity = int.Parse(p.Element("Quantity")?.Value ?? "0")
                }).ToList();

                // Add the parsed list to our MySQL context and save changes
                context.Products.AddRange(productsFromXml);
                context.SaveChanges();
            }
        }
    }
}