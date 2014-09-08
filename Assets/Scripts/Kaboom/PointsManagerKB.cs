using UnityEngine;
using System.Collections;

public class PointsManagerKB : MonoBehaviour
{
    #region Fields
    public GameManagerKB gameManagerKB;

    public int bombValue;

    private int currentScore;
    private int bonusLifeMark,
        nextBonusLife;
    #endregion

    void Start()
    {
        bonusLifeMark = 1000;
        nextBonusLife = 1;
    }

    void Update()
    {

    }

    #region AddPoints
    public void AddPoints()
    {
        currentScore += bombValue;

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
