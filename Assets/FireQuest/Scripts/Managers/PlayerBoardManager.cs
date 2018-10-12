using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;

public class PlayerBoardManager : MonoBehaviour
{
    public List<Player> playerList = new List<Player>();

    public GameObject rowPrefab;
    public GameObject scrollContainer;

    private void Awake()
    {
        playerList.Clear();

        DatabaseManager.sharedInstance.GetPlayers(result =>
        {
            playerList = result;
            Debug.Log(playerList[0].email);

            InitialiseUI();
        });

        //Router.Players().ChildAdded += NewPlayerAdded;

        //Router.Players().OrderByChild("level");
        //Router.Players().LimitToFirst(10);
        //// just one filter per query
        //Router.Players().OrderByChild("level").LimitToLast(5);
    }

    void InitialiseUI()
    {
        foreach (var player in playerList)
        {
            CreateRow(player);
        }
    }

    void CreateRow(Player player)
    {
        GameObject newRow = Instantiate(rowPrefab as GameObject);
        newRow.GetComponent<RowConfig>().Initialise(player);
        newRow.transform.SetParent(scrollContainer.transform, false);
    }

    void NewPlayerAdded(object sender, ChildChangedEventArgs args)
    {
        if (args.Snapshot.Value == null)
        {
            Debug.Log("Sorry, there was no data at that node.");
        }
        else
        {
            Debug.Log("New player has joined the game!");
        }
    }
}
