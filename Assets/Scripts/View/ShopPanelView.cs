using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopPanelView : MonoBehaviour
{
    public GameObject shopPanel;
    public Image productImage;
    public TMP_Text productName;
    public TMP_Text productCategory;
    public TMP_Text productBrand;
    public TMP_Text productPrice;
    public Button addToCartButton;
    public Button backButton;

    private Product currentProduct;
    private CartController cartController;

    private void Start()
    {
        cartController = new CartController();
        shopPanel.SetActive(false);

        addToCartButton.onClick.AddListener(AddToCart);
        backButton.onClick.AddListener(ClosePanel);
    }

    public void OpenPanel(Product product)
    {
        currentProduct = product;
        productName.text = product.Name;
        productCategory.text = "Category: " + product.Category;
        productBrand.text = "Brand: " + product.Brand;
        productPrice.text = "Price: $" + product.Price;
        productImage.sprite = product.ProductImage;

        shopPanel.SetActive(true);
    }

    private void AddToCart()
    {
        cartController.AddProductToCart(currentProduct, 1);
        Debug.Log(currentProduct.Name + " added to cart!");
    }

    private void ClosePanel()
    {
        shopPanel.SetActive(false);
    }
}
