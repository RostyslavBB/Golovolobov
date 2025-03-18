using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class UserView : MonoBehaviour
{
    [SerializeField] private TMP_InputField nameInput;
    [SerializeField] private TMP_InputField emailInput;
    [SerializeField] private TMP_InputField passwordInput;
    [SerializeField] private Button registerButton;
    [SerializeField] private Button loginButton;
    [SerializeField] private TextMeshProUGUI feedbackText;
    [SerializeField] private TextMeshProUGUI feedbackText2;
    [SerializeField] private TMP_InputField editNameInput;
    [SerializeField] private TMP_InputField editEmailInput;
    [SerializeField] private TMP_InputField editPasswordInput;
    [SerializeField] private Button editProfileButton;

    [SerializeField] private TextMeshProUGUI currentNameText;
    [SerializeField] private TextMeshProUGUI currentEmailText;

    [SerializeField] private GameObject signPanel;
    [SerializeField] private GameObject shopPanel;

    private UserController _userController = new UserController();
    private User _currentUser;

    private void Start()
    {
        registerButton.onClick.AddListener(OnRegisterClicked);
        loginButton.onClick.AddListener(OnLoginClicked);
        editProfileButton.onClick.AddListener(OnEditProfileClicked);
    }

    private void OnRegisterClicked()
    {
        try
        {
            User newUser = _userController.RegisterUser(nameInput.text, emailInput.text, passwordInput.text);
            if (newUser != null)
            {
                feedbackText.text = $"User {newUser.Name} registered!";
                _currentUser = newUser;
                UpdateUserInfo(_currentUser);
            }
            else
            {
                feedbackText.text = "Registration failed.";
            }
        }
        catch (Exception ex)
        {
            feedbackText.text = ex.Message;
        }
    }

    private void OnLoginClicked()
    {
        if (_userController.Login(emailInput.text, passwordInput.text, out User loggedInUser))
        {
            _currentUser = loggedInUser;
            feedbackText.text = $"Welcome, {_currentUser.Name}!";

            signPanel.SetActive(false);
            shopPanel.SetActive(true);

            UpdateUserInfo(_currentUser);
        }
        else
        {
            feedbackText.text = "Invalid email or password.";
        }
    }

    private void OnEditProfileClicked()
    {
        if (_currentUser == null)
        {
            feedbackText2.text = "You need to be logged in to edit your profile.";
            return;
        }

        try
        {
            bool success = _userController.EditProfile(
                _currentUser.Id,
                editNameInput.text,
                editEmailInput.text,
                editPasswordInput.text
            );

            if (success)
            {
                feedbackText2.text = "Profile updated successfully!";

                _currentUser.Name = string.IsNullOrEmpty(editNameInput.text) ? _currentUser.Name : editNameInput.text;
                _currentUser.Email = string.IsNullOrEmpty(editEmailInput.text) ? _currentUser.Email : editEmailInput.text;

                UpdateUserInfo(_currentUser);
            }
            else
            {
                feedbackText2.text = "Profile update failed.";
            }
        }
        catch (Exception ex)
        {
            feedbackText2.text = ex.Message;
        }
    }

    private void UpdateUserInfo(User user)
    {
        currentNameText.text = $"Current Name: {user.Name}";
        currentEmailText.text = $"Current Email: {user.Email}";
    }
}
