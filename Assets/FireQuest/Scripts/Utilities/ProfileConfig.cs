using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Auth;

public class ProfileConfig : MonoBehaviour
{
    public Text emailString;
    public Image profilePic;

    public void Config(Firebase.Auth.FirebaseUser user)
    {
        this.emailString.text = string.Format("You are signed in as {0}", user.Email);
    }

}
