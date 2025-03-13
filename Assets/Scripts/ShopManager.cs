using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ShopManager : MonoBehaviour
{
    public GameObject productPrefab;
    public Transform contentParent;
    public List<Product> products;

    [Header("Panels")]
    public GameObject shopPanel;
    public GameObject editProfilePanel;

    [Header("Info Panel")]
    public GameObject infoPanel;
    public Image infoImage;
    public TMP_Text infoName;
    public TMP_Text infoCategory;
    public TMP_Text infoBrand;
    public TMP_Text infoPrice;
    public Button closeButton;
    public Button addToCartButton;

    [Header("Cart System")]
    public CartView cartView;
    public GameObject cartPanel;
    public Button openCartButton;
    public Button closeCartButton;

    [Header("Search & Sorting")]
    public TMP_InputField searchByNameInputField;
    public TMP_InputField searchByCategoryInputField;
    public Button searchButton;
    public TMP_Dropdown sortDropdown;

    [Header("Profile Management")]
    public Button editProfileButton;
    public Button closeEditProfileButton;

    private void Start()
    {
        GenerateProductUI();

        closeButton.onClick.AddListener(() => infoPanel.SetActive(false));
        addToCartButton.onClick.AddListener(AddCurrentProductToCart);
        searchButton.onClick.AddListener(OnSearchClicked);

        if (sortDropdown != null)
        {
            sortDropdown.onValueChanged.AddListener(SortProducts);
        }

        if (openCartButton != null && closeCartButton != null)
        {
            openCartButton.onClick.AddListener(() => cartPanel.SetActive(true));
            closeCartButton.onClick.AddListener(() => cartPanel.SetActive(false));
        }

        if (editProfileButton != null && closeEditProfileButton != null)
        {
            editProfileButton.onClick.AddListener(() => TogglePanels(true));
            closeEditProfileButton.onClick.AddListener(() => TogglePanels(false));
        }
    }

    private void TogglePanels(bool editMode)
    {
        shopPanel.SetActive(!editMode);
        editProfilePanel.SetActive(editMode);
    }

    private void GenerateProductUI(List<Product> filteredProducts = null)
    {
        foreach (Transform child in contentParent)
        {
            Destroy(child.gameObject);
        }

        var productsToShow = filteredProducts ?? products;

        foreach (var product in productsToShow)
        {
            GameObject productUI = Instantiate(productPrefab, contentParent);
            productUI.GetComponent<ProductView>()?.Setup(product, this);
        }
    }

    private Product currentProduct;

    public void ShowProductInfo(Product product)
    {
        currentProduct = product;
        infoPanel.SetActive(true);
        infoImage.sprite = product.ProductImage;
        infoName.text = product.Name;
        infoCategory.text = product.Category;
        infoBrand.text = product.Brand;
        infoPrice.text = $"${product.Price}";
    }

    private void AddCurrentProductToCart()
    {
        if (currentProduct != null)
        {
            cartView.AddProductToCart(currentProduct, 1);
        }
    }

    private void OnSearchClicked()
    {
        string searchByName = searchByNameInputField.text.ToLower();
        string searchByCategory = searchByCategoryInputField.text.ToLower();

        var filteredProducts = products
            .Where(product => product.Name.ToLower().Contains(searchByName) &&
                              product.Category.ToLower().Contains(searchByCategory))
            .ToList();

        GenerateProductUI(filteredProducts);
    }

    private void SortProducts(int option)
    {
        if (option == 0)
        {
            products = products.OrderBy(p => p.Price).ToList();
        }
        else if (option == 1)
        {
            products = products.OrderByDescending(p => p.Price).ToList();
        }

        GenerateProductUI();
    }
}
