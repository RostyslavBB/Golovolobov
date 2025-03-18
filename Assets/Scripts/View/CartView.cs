using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CartView : MonoBehaviour
{
    [SerializeField] private Transform cartContentParent;
    [SerializeField] private GameObject cartItemPrefab;
    [SerializeField] private TMP_Text totalPriceText;
    [SerializeField] private Button checkoutButton;

    [SerializeField] private GameObject cartPanel;

    private CartController _cartController;
    private List<GameObject> _spawnedCartItems = new List<GameObject>();

    void Start()
    {
        _cartController = new CartController();
        checkoutButton.onClick.AddListener(OnCheckoutClicked);
    }

    public void AddProductToCart(Product product, int quantity)
    {
        _cartController.AddProductToCart(product, quantity);
        UpdateCartUI();
    }

    public void RemoveProductFromCart(Product product)
    {
        _cartController.RemoveProductFromCart(product);
        UpdateCartUI();
    }

    public void UpdateProductQuantity(Product product, int quantity)
    {
        _cartController.UpdateProductQuantity(product, quantity);
        UpdateCartUI();
    }

    private void UpdateCartUI()
    {
        foreach (var item in _spawnedCartItems)
        {
            Destroy(item);
        }
        _spawnedCartItems.Clear();

        foreach (var item in _cartController.GetCartItems())
        {
            SpawnCartItem(item.Key, item.Value);
        }

        totalPriceText.text = "Total: $" + _cartController.GetTotal();
    }

    private void SpawnCartItem(Product product, int quantity)
    {
        GameObject cartItem = Instantiate(cartItemPrefab, cartContentParent);
        cartItem.GetComponent<CartItemView>().Setup(product, quantity, this);
        _spawnedCartItems.Add(cartItem);
    }

    private void OnCheckoutClicked()
    {
        User currentUser = new User("John Doe", "john.doe@example.com");
        _cartController.Checkout(currentUser);
        UpdateCartUI();
    }

    public void ToggleCartPanel()
    {
        if (cartPanel != null)
        {
            cartPanel.SetActive(!cartPanel.activeSelf);
        }
    }
}
