using System.Collections.Generic;

public class CartController
{
    private Cart _cart;

    public CartController()
    {
        _cart = new Cart(); 
    }

    public void AddProductToCart(Product product, int quantity)
    {
        _cart.AddProduct(product, quantity);
    }

    public void RemoveProductFromCart(Product product)
    {
        _cart.RemoveProduct(product);
    }

    public void UpdateProductQuantity(Product product, int quantity)
    {
        _cart.UpdateQuantity(product, quantity);
    }

    public float GetTotal()
    {
        return _cart.CalculateTotal();
    }

    public void Checkout(User user)
    {
        _cart.Checkout(user);
    }

    public Dictionary<Product, int> GetCartItems()
    {
        return _cart.Items;
    }
}
