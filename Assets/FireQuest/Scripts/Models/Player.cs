public class Player
{
    #region Properties

    public string email;
    public int score;
    public int level;

    #endregion

    public Player(string email, int score, int level)
    {
        this.email = email;
        this.score = score;
        this.level = level;
    }

}