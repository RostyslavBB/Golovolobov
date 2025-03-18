using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProductView : MonoBehaviour
{
    [SerializeField] private Image productImage;
    [SerializeField] private TMP_Text productName;
    [SerializeField] private TMP_Text productPrice;
    [SerializeField] private Button productButton;

    private Product _currentProduct;
    private ShopManager _shopManager;

    public void Setup(Product product, ShopManager manager)
    {
        _currentProduct = product;
        _shopManager = manager;

        productName.text = product.Name;
        productPrice.text = $"${product.Price}";
        productImage.sprite = product.ProductImage;

        productButton.onClick.AddListener(() => _shopManager.ShowProductInfo(_currentProduct));
    }
}
