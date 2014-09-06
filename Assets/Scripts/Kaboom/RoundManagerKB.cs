using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoundManagerKB : MonoBehaviour
{
    public GameManagerKB gameManagerKB;

    private int currentRound;

    private List<int> bombsPerRound = new List<int>();

    // Use this for initialization
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

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetupRound()
    {
        //Sets the number of bombs to be dropped for this round
        gameManagerKB.bombDropperKB.bombsThisRound = bombsPerRound[currentRound];
        
        //The value of each bomb is the same as the round number
        //E.g Round 1 bombs are worth 1 point, Round 2 bombs worth 2 points etc
        gameManagerKB.pointsManagerKB.bombValue = currentRound;
    }
}
