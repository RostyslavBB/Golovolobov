using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CartItemView : MonoBehaviour
{
    [SerializeField] private Image productImage;
    [SerializeField] private TMP_Text productName;
    [SerializeField] private TMP_Text productQuantity;
    [SerializeField] private Button removeButton;
    [SerializeField] private Button addOneButton;
    [SerializeField] private Button removeOneButton;
    [SerializeField] private Button removeAllButton;

    private Product _currentProduct;

    public void Setup(Product product, int quantity, CartView cartView)
    {
        _currentProduct = product;
        productName.text = product.Name;
        productQuantity.text = $"x{quantity}";
        productImage.sprite = product.ProductImage;

        // Setup button listeners
        addOneButton.onClick.RemoveAllListeners();
        addOneButton.onClick.AddListener(() => cartView.UpdateProductQuantity(_currentProduct, quantity + 1));

        removeOneButton.onClick.RemoveAllListeners();
        removeOneButton.onClick.AddListener(() => cartView.UpdateProductQuantity(_currentProduct, Mathf.Max(0, quantity - 1)));

        removeAllButton.onClick.RemoveAllListeners();
        removeAllButton.onClick.AddListener(() => cartView.RemoveProductFromCart(_currentProduct));

        removeButton.onClick.RemoveAllListeners();
        removeButton.onClick.AddListener(() => cartView.RemoveProductFromCart(_currentProduct));
    }
}
