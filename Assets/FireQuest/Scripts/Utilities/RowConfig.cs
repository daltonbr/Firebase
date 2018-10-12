using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RowConfig : MonoBehaviour
{
    public Text score;
    public Text email;
    public Text level;
    public Image profilePic;
    //random select profile pics, before polling images from outside is out of the scope of this project
    public List<Sprite> imagesList;

    public void Initialise(Player player)
    {
        this.score.text = player.score.ToString();
        this.email.text = player.email;
        this.level.text = player.level.ToString();

        this.profilePic.sprite = imagesList[Random.Range(0, 2)];
    }

}
