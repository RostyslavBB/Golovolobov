using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CartItemView : MonoBehaviour
{
    public Image productImage;
    public TMP_Text productName;
    public TMP_Text productQuantity;
    public Button removeButton;
    public Button addOneButton; 
    public Button removeOneButton; 
    public Button removeAllButton; 

    private Product currentProduct;
    private CartView cartView;

    public void Setup(Product product, int quantity, CartView cartView)
    {
        currentProduct = product;
        this.cartView = cartView;

        productName.text = product.Name;
        productQuantity.text = $"x{quantity}";
        productImage.sprite = product.ProductImage;

        // Setup button listeners
        addOneButton.onClick.RemoveAllListeners();
        addOneButton.onClick.AddListener(() => cartView.UpdateProductQuantity(currentProduct, quantity + 1));

        removeOneButton.onClick.RemoveAllListeners();
        removeOneButton.onClick.AddListener(() => cartView.UpdateProductQuantity(currentProduct, Mathf.Max(0, quantity - 1)));

        removeAllButton.onClick.RemoveAllListeners();
        removeAllButton.onClick.AddListener(() => cartView.RemoveProductFromCart(currentProduct));

        removeButton.onClick.RemoveAllListeners();
        removeButton.onClick.AddListener(() => cartView.RemoveProductFromCart(currentProduct));
    }
}
