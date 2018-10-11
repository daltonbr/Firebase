using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using System.Threading.Tasks;

public class AuthManager : MonoBehaviour
{

    // Firebase API variables
    Firebase.Auth.FirebaseAuth auth;

    // Delegates
    public delegate IEnumerator AuthCallback(Task<Firebase.Auth.FirebaseUser> task, string operation);
    public event AuthCallback authCallback;

	void Awake ()
    {
        auth = FirebaseAuth.DefaultInstance;
	}
	
    public void SignUpNewUser(string email, string password)
    {
        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
        {
            if (task.IsFaulted || task.IsCanceled)
            {
                Debug.LogError("Sorry, there was an error creating your new account. ERROR: " + task.Exception);
            }
            else if (task.IsCompleted)
            {
                Firebase.Auth.FirebaseUser newPlayer = task.Result;
                Debug.Log("Welcome to FireQuest " + newPlayer.Email);
                //auth.CurrentUser // is another possible way to access our logged user
            }
        });
    }

}
