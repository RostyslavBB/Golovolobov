using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProductView : MonoBehaviour
{
    public Image productImage;
    public TMP_Text productName;
    public TMP_Text productPrice;
    public Button productButton;

    private Product currentProduct;
    private ShopManager shopManager;

    public void Setup(Product product, ShopManager manager)
    {
        currentProduct = product;
        shopManager = manager;

        productName.text = product.Name;
        productPrice.text = $"${product.Price}";
        productImage.sprite = product.ProductImage;

        productButton.onClick.AddListener(() => shopManager.ShowProductInfo(currentProduct));
    }
}
