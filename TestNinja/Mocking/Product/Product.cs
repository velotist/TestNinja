namespace TestNinja.Mocking.Product
{
    public class Product
    {
        public float ListPrice { get; set; }

        public float GetPrice(Customer customer)
        {
            if (customer.IsGold)
                return ListPrice * 0.7f;

            return ListPrice;
        }
    }
}