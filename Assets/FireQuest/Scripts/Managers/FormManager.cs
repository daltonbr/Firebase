using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Firebase;
using Firebase.Auth;
using UnityEngine.SceneManagement;

public class FormManager : MonoBehaviour {

	// UI objects linked from the inspector
	public InputField emailInput;
	public InputField passwordInput;

	public Button signUpButton;
	public Button loginButton;

	public Text statusText;

	public AuthManager authManager;

	void Awake()
    {
		ToggleButtonStates (false);

        // Auth delegate subscriptions
        authManager.authCallback += HandleAuthCallback;
	}

    private void OnDestroy()
    {
        authManager.authCallback -= HandleAuthCallback;
    }

    /// <summary>
    /// Validates the email input
    /// </summary>
    public void ValidateEmail() {
		string email = emailInput.text;
		var regexPattern = @"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
                + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
                + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
                + @"([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})$";

		if (email != "" && Regex.IsMatch(email, regexPattern)) {
			ToggleButtonStates (true);
		} else {
			ToggleButtonStates (false);
		}
	}

	// Firebase methods
	public void OnSignUp()
    {
        Debug.Log ("Sign Up");
        authManager.SignUpNewUser(emailInput.text, passwordInput.text);
	}

	public void OnLogin() {
		Debug.Log ("Login");
        authManager.LoginExistingUser(emailInput.text, passwordInput.text);
	}

    IEnumerator HandleAuthCallback(Task<Firebase.Auth.FirebaseUser> task, string operation)
    {
        if (task.IsFaulted || task.IsCanceled)
        {
            UpdateStatus("Sorry, there was an error creating your new account.");
            Debug.LogWarning("Sorry, there was an error creating your new account. ERROR: " + task.Exception);
        }
        else if (task.IsCompleted)
        {
            if (operation == "sign_up")
            {
                Firebase.Auth.FirebaseUser newPlayer = task.Result;
                Debug.LogFormat("Welcome to FireQuest {0}", newPlayer.Email);

                Player player = new Player(newPlayer.Email, 0, 1);
                DatabaseManager.sharedInstance.CreateNewPlayer(player, newPlayer.UserId);
            }
            UpdateStatus("Loading the game scene");

            yield return new WaitForSeconds(1.5f);
            SceneManager.LoadScene("Player List");
        }
        yield return null;
    }

    // Utilities
    private void ToggleButtonStates(bool toState) {
		signUpButton.interactable = toState;
		loginButton.interactable = toState;
	}

	private void UpdateStatus(string message) {
		statusText.text = message;
	}
}
