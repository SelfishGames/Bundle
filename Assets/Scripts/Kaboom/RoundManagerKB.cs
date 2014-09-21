using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class RoundManagerKB : MonoBehaviour
{
    #region Fields
    public GameManagerKB gameManagerKB;

    [HideInInspector]
    public bool roundSuccess,
        roundFail;

    [HideInInspector]
    public int totalBombsCaught,
        totalBombsDropped,
        totalBombsMissed,
        currentRound;

    public List<RoundInformationKB> rounds;

    [HideInInspector]
    public float currentBombSpeed;

    [HideInInspector]
    public Color targetBombColour;
    #endregion

    #region Start
    void Start()
    {
        currentRound = 1;

        currentBombSpeed = rounds[currentRound].bombMoveSpeed;
    }
    #endregion

    #region Update
    void Update()
    {
        //If all the bombs in this round are accounted for
        if (totalBombsCaught + totalBombsMissed == rounds[currentRound].bombCount)
        {
            roundSuccess = true;
        }
    }
    #endregion

    //Sets the number of bombs to be dropped and the point value of each bomb
    #region SetupRound
    public void SetupRound()
    {
        //Sets the number of bombs to be dropped and the rate they get dropped at
        gameManagerKB.bombDropperKB.bombsThisRound = rounds[currentRound].bombCount;
        gameManagerKB.bombDropperKB.bombDropDelay = rounds[currentRound].bombDropDelay;
        gameManagerKB.bombDropperKB.moveSpeed = rounds[currentRound].dropperMoveSpeed;

        //The value of each bomb is the same as the round number
        //E.g Round 1 bombs are worth 1 point, Round 2 bombs worth 2 points etc
        gameManagerKB.pointsManagerKB.bombValue = rounds[currentRound].bombValue;
        currentBombSpeed = rounds[currentRound].bombMoveSpeed;
        targetBombColour = rounds[currentRound].bombColour;

        totalBombsCaught = 0;
        totalBombsMissed = 0;
        totalBombsDropped = 0;
    }
    #endregion

    //Destroys all currently active bombs
    #region DestroyActiveBombs
    public void DestroyActiveBombs()
    {
        //Gets all the currently active bombs 
        var activeBombs = from bomb
                          in gameManagerKB.bombDropperKB.bombList
                          where bomb.activeSelf
                          select bomb;

        //And destroys them
        foreach (GameObject bomb in activeBombs)
        {
            bomb.SetActive(false);
        }
    }
    #endregion
}
