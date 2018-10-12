using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using System.Collections;
using System.Collections.Generic;
using System;

public class DatabaseManager : MonoBehaviour
{

	public static DatabaseManager sharedInstance = null;

	/// <summary>
	/// Awake this instance and initialize sharedInstance for Singleton pattern
	/// </summary>
	void Awake()
    {
		if (sharedInstance == null)
        {
			sharedInstance = this;
		} else if (sharedInstance != this)
        {
			Destroy (gameObject);  
		}

		DontDestroyOnLoad(gameObject);

        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://firequest-e340d.firebaseio.com/");

        Debug.Log(Router.Players());
        //Router.Players().SetValueAsync("testing 1, 2");
	}

    public void CreateNewPlayer(Player player, string uid)
    {
        string playerJSON = JsonUtility.ToJson(player);
        Router.PlayerWIthUID(uid).SetRawJsonValueAsync(playerJSON);
    }

    public void GetPlayers(Action<List<Player>> completionBlock)
    {
        List<Player> tempList = new List<Player>();
        Router.Players().GetValueAsync().ContinueWith(task =>
        {
            DataSnapshot players = task.Result;

            foreach (DataSnapshot playerNode in players.Children)
            {
                var playerDict = (IDictionary<string, object>) playerNode.Value;
                Player newPlayer = new Player(playerDict);
                tempList.Add(newPlayer);
            }

            completionBlock(tempList);
        });
    }
}
