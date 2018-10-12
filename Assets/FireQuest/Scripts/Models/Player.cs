using System;
using System.Collections.Generic;

public class Player
{
    #region Properties

    public string email;
    public int score;
    public int level;

    #endregion

    #region Constructors

    public Player(string email, int score, int level)
    {
        this.email = email;
        this.score = score;
        this.level = level;
    }

    public Player(IDictionary<string, object> dict)
    {
        this.email = dict["email"].ToString();
        this.score = Convert.ToInt32(dict["score"]);
        this.level = Convert.ToInt32(dict["level"]);
    }

    #endregion

}