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
        totalBombsMissed,
        currentRound;

    [HideInInspector]
    public float bonusDropSpeed;

    private List<int> bombsPerRound = new List<int>();
    #endregion

    #region Start
    void Start()
    {
        bombsPerRound.Add(0); //This element wont be used, I just want the first set of bombs to be in element 1
        bombsPerRound.Add(10);
        bombsPerRound.Add(20);
        bombsPerRound.Add(30);
        bombsPerRound.Add(40);
        bombsPerRound.Add(50);
        bombsPerRound.Add(75);
        bombsPerRound.Add(100);
        bombsPerRound.Add(150);

        currentRound = 1;
    }
    #endregion

    #region Update
    void Update()
    {
        //If all the bombs in this round are accounted for
        if (totalBombsCaught + totalBombsMissed == bombsPerRound[currentRound])
        {
            roundSuccess = true;
        }
    }
    #endregion

    //Sets the number of bombs to be dropped and the point value of each bomb
    #region SetupRound
    public void SetupRound()
    {
        //Sets the number of bombs to be dropped for this round
        gameManagerKB.bombDropperKB.bombsThisRound = bombsPerRound[currentRound];
        
        //The value of each bomb is the same as the round number
        //E.g Round 1 bombs are worth 1 point, Round 2 bombs worth 2 points etc
        gameManagerKB.pointsManagerKB.bombValue = currentRound;

        totalBombsCaught = 0;
        totalBombsMissed = 0;
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
