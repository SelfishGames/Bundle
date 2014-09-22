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
    public Color currentTargetColour;
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

    //Called at the start of each new round
    #region SetupRound
    public void SetupRound()
    {
        //The number of bombs to be dropped, the delay between dropping bombs and the 
        //speed that the dropper moves at
        gameManagerKB.bombDropperKB.bombsThisRound = rounds[currentRound].bombCount;
        gameManagerKB.bombDropperKB.bombDropDelay = rounds[currentRound].bombDropDelay;
        gameManagerKB.bombDropperKB.moveSpeed = rounds[currentRound].dropperMoveSpeed;

        //The value of catching a bomb, the speed that the bombs drop at and the target colour for this round
        gameManagerKB.pointsManagerKB.bombValue = rounds[currentRound].bombValue;
        currentBombSpeed = rounds[currentRound].bombMoveSpeed;
        currentTargetColour = rounds[currentRound].targetColour;

        //Resets the variables
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
