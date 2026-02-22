using FactoryMethod.Products;

namespace FactoryMethod.Factories
{
    /// <summary>
    /// Concrete creator for electronic products
    /// Implements factory method for electronic items
    /// </summary>
    public class ElectronicFactory : ProductFactory
    {
        public override string FactoryName => "Electronic Factory";

        public override IProduct CreateProduct()
        {
            return new ElectronicProduct(
                name: "Wireless Headphones",
                price: 129.99m,
                brand: "TechBrand",
                model: "TH-2024",
                isWireless: true
            );
        }
    }
}