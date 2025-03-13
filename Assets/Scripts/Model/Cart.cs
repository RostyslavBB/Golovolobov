using System;
using System.Collections.Generic;

public class Cart
{
    public Dictionary<Product, int> Items { get; private set; }

    public Cart()
    {
        Items = new Dictionary<Product, int>();
    }

    public void AddProduct(Product product, int quantity)
    {
        if (Items.ContainsKey(product))
        {
            Items[product] += quantity;
        }
        else
        {
            Items.Add(product, quantity); 
        }
    }

    public void RemoveProduct(Product product)
    {
        if (Items.ContainsKey(product))
        {
            Items.Remove(product); 
        }
    }

    public void UpdateQuantity(Product product, int quantity)
    {
        if (Items.ContainsKey(product))
        {
            if (quantity <= 0)
            {
                RemoveProduct(product); 
            }
            else
            {
                Items[product] = quantity;
            }
        }
    }

    public float CalculateTotal()
    {
        float total = 0;
        foreach (var item in Items)
        {
            total += item.Key.Price * item.Value; 
        }
        return total;
    }

    public void Checkout(User user)
    {
        Console.WriteLine($"Order placed by {user.Name}.\nTotal: ${CalculateTotal()}");

        Items.Clear();
    }
}
