using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private GameObject productPrefab;
    [SerializeField] private Transform contentParent;
    [SerializeField] private List<Product> products;

    [Header("Panels")]
    [SerializeField] private GameObject shopPanel;
    [SerializeField] private GameObject editProfilePanel;

    [Header("Info Panel")]
    [SerializeField] private GameObject infoPanel;
    [SerializeField] private Image infoImage;
    [SerializeField] private TMP_Text infoName;
    [SerializeField] private TMP_Text infoCategory;
    [SerializeField] private TMP_Text infoBrand;
    [SerializeField] private TMP_Text infoPrice;
    [SerializeField] private Button closeButton;
    [SerializeField] private Button addToCartButton;

    [Header("Cart System")]
    [SerializeField] private CartView cartView;
    [SerializeField] private GameObject cartPanel;
    [SerializeField] private Button openCartButton;
    [SerializeField] private Button closeCartButton;

    [Header("Search & Sorting")]
    [SerializeField] private TMP_InputField searchByNameInputField;
    [SerializeField] private TMP_InputField searchByCategoryInputField;
    [SerializeField] private Button searchButton;
    [SerializeField] private TMP_Dropdown sortDropdown;

    [Header("Profile Management")]
    [SerializeField] private Button editProfileButton;
    [SerializeField] private Button closeEditProfileButton;

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

    private Product _currentProduct;

    public void ShowProductInfo(Product product)
    {
        _currentProduct = product;
        infoPanel.SetActive(true);
        infoImage.sprite = product.ProductImage;
        infoName.text = product.Name;
        infoCategory.text = product.Category;
        infoBrand.text = product.Brand;
        infoPrice.text = $"${product.Price}";
    }

    private void AddCurrentProductToCart()
    {
        if (_currentProduct != null)
        {
            cartView.AddProductToCart(_currentProduct, 1);
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
