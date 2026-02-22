using FactoryMethod.Products;

namespace FactoryMethod.Factories
{
    /// <summary>
    /// Concrete creator for food products
    /// Implements factory method for food items
    /// </summary>
    public class FoodFactory : ProductFactory
    {
        public override string FactoryName => "Food Factory";

        public override IProduct CreateProduct()
        {
            return new FoodProduct(
                name: "Organic Apple Juice",
                price: 3.99m,
                expiryDate: DateTime.Now.AddDays(30),
                weight: 1000,
                isOrganic: true
            );
        }
    }
}