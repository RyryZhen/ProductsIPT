namespace AssignmentFinals.Models
{
    public class Product
    {
        public string ProductID { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;

        public decimal Price { get; set; }
        public int Quantity { get; set; }

        // Automatically computed status (DO NOT store in XML or DB)
        public string Status
        {
            get
            {
                if (Quantity <= 0)
                    return "Out of Stock";

                if (Quantity >= 1 && Quantity <= 9)
                    return "Low Stock";

                return "Available";
            }
        }
    }
}