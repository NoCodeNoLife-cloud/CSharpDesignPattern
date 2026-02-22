using FactoryMethod.Products;

namespace FactoryMethod.Factories
{
    /// <summary>
    /// Concrete creator for clothing products
    /// Implements factory method for clothing items
    /// </summary>
    public class ClothingFactory : ProductFactory
    {
        public override string FactoryName => "Clothing Factory";

        public override IProduct CreateProduct()
        {
            return new ClothingProduct(
                name: "Premium Cotton T-Shirt",
                price: 29.99m,
                size: "L",
                color: "Navy Blue",
                material: "100% Organic Cotton"
            );
        }
    }
}