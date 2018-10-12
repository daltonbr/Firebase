using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using System.Threading.Tasks;

public enum Operation
{
    SignUp,
    Login
}

public class AuthManager : MonoBehaviour
{

    // Firebase API variables
    Firebase.Auth.FirebaseAuth auth;

    // Delegates
    public delegate IEnumerator AuthCallback(Task<Firebase.Auth.FirebaseUser> task, Operation operation);
    public event AuthCallback authCallback;

	void Awake ()
    {
        auth = FirebaseAuth.DefaultInstance;
	}
	
    public void SignUpNewUser(string email, string password)
    {
        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
        {
            StartCoroutine(authCallback(task, Operation.SignUp));
        });
    }

    public void LoginExistingUser(string email, string password)
    {
        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
        {
            StartCoroutine(authCallback(task, Operation.Login));
        });
    }

}
