using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class UserView : MonoBehaviour
{
    public TMP_InputField nameInput;
    public TMP_InputField emailInput;
    public TMP_InputField passwordInput;
    public Button registerButton;
    public Button loginButton;
    public TextMeshProUGUI feedbackText;
    public TextMeshProUGUI feedbackText2;
    public TMP_InputField editNameInput;
    public TMP_InputField editEmailInput;
    public TMP_InputField editPasswordInput;
    public Button editProfileButton;

    public TextMeshProUGUI currentNameText;
    public TextMeshProUGUI currentEmailText;

    public GameObject signPanel;
    public GameObject shopPanel;

    private UserController userController = new UserController();
    private User currentUser = null; 

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
            User newUser = userController.RegisterUser(nameInput.text, emailInput.text, passwordInput.text);
            if (newUser != null)
            {
                feedbackText.text = $"User {newUser.Name} registered!";
                currentUser = newUser;  
                UpdateUserInfo(currentUser);
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
        if (userController.Login(emailInput.text, passwordInput.text, out User loggedInUser))
        {
            currentUser = loggedInUser;
            feedbackText.text = $"Welcome, {currentUser.Name}!";

            signPanel.SetActive(false);
            shopPanel.SetActive(true);

            UpdateUserInfo(currentUser);
        }
        else
        {
            feedbackText.text = "Invalid email or password.";
        }
    }

    private void OnEditProfileClicked()
    {
        if (currentUser == null)
        {
            feedbackText2.text = "You need to be logged in to edit your profile.";
            return;
        }

        try
        {
            bool success = userController.EditProfile(
                currentUser.Id,
                editNameInput.text,
                editEmailInput.text,
                editPasswordInput.text
            );

            if (success)
            {
                feedbackText2.text = "Profile updated successfully!";

                currentUser.Name = string.IsNullOrEmpty(editNameInput.text) ? currentUser.Name : editNameInput.text;
                currentUser.Email = string.IsNullOrEmpty(editEmailInput.text) ? currentUser.Email : editEmailInput.text;

                UpdateUserInfo(currentUser);
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
