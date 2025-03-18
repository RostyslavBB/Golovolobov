using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopPanelView : MonoBehaviour
{
    [SerializeField] private GameObject shopPanel;
    [SerializeField] private Image productImage;
    [SerializeField] private TMP_Text productName;
    [SerializeField] private TMP_Text productCategory;
    [SerializeField] private TMP_Text productBrand;
    [SerializeField] private TMP_Text productPrice;
    [SerializeField] private Button addToCartButton;
    [SerializeField] private Button backButton;

    private Product _currentProduct;
    private CartController _cartController;

    private void Start()
    {
        _cartController = new CartController();
        shopPanel.SetActive(false);

        addToCartButton.onClick.AddListener(AddToCart);
        backButton.onClick.AddListener(ClosePanel);
    }

    public void OpenPanel(Product product)
    {
        _currentProduct = product;
        productName.text = product.Name;
        productCategory.text = "Category: " + product.Category;
        productBrand.text = "Brand: " + product.Brand;
        productPrice.text = "Price: $" + product.Price;
        productImage.sprite = product.ProductImage;

        shopPanel.SetActive(true);
    }

    private void AddToCart()
    {
        _cartController.AddProductToCart(_currentProduct, 1);
        Debug.Log(_currentProduct.Name + " added to cart!");
    }

    private void ClosePanel()
    {
        shopPanel.SetActive(false);
    }
}
