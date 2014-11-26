using UnityEngine;
using System.Collections;

public class PointsManagerKB : MonoBehaviour
{
    #region Fields
    public GameManagerKB gameManagerKB;

    public int bombValue;
    [HideInInspector]
    public int currentScore;
    [HideInInspector]
    public int highscore;

    private int bonusLifeMark,
        nextBonusLife;
    #endregion

    void Start()
    {
        bonusLifeMark = 1000;
        nextBonusLife = 1;

        highscore = PlayerPrefs.GetInt("HighScore");
    }

    void Update()
    {

    }

    #region AddPoints
    public void AddPoints()
    {
        currentScore += bombValue;

        if (currentScore > highscore)
            PlayerPrefs.SetInt("HighScore", currentScore);

        //Checks if the score has crossed the next multiple of bonusLifeMark
        if(currentScore >= nextBonusLife * bonusLifeMark)
        {
            //If so, give a life and increment the threshold
            gameManagerKB.GainALife();
            nextBonusLife++;
        }
    }
    #endregion
}
