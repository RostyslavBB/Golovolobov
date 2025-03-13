using System;
using UnityEngine;

[Serializable]
public class Product
{
    public int Id;
    public string Name;
    public string Category;
    public string Brand;
    public float Price;
    public Sprite ProductImage;

    public string GetInfo()
    {
        return $"ID: {Id}\nName: {Name}\nCategory: {Category}\nBrand: {Brand}\nPrice: ${Price}";
    }
}
