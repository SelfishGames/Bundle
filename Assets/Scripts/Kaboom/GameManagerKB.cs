using UnityEngine;
using System.Collections;

public class GameManagerKB : MonoBehaviour
{
    public PointsManagerKB pointsManagerKB;
    public RoundManagerKB roundManagerKB;
    public BombDropperKB bombDropperKB;
    public Transform tires;

    [HideInInspector]
    public bool freezeMovement;

    private int playerLives;

    private bool gameOver;

    private int currentState;
    private const int MENU_STATE = 0,
        ROUND_START_STATE = 1,
        PLAYING_STATE = 2,
        ROUND_END_STATE = 3,
        GAMEOVER_STATE = 4;

    #region Start
    void Start()
    {
        playerLives = 3;
        gameOver = false;
        currentState = MENU_STATE;
    }
    #endregion

    #region Update
    void Update()
    {
        Debug.Log("currentState " + currentState);

        switch (currentState)
        {
            case MENU_STATE:
                {
                    if (Input.GetKey(KeyCode.Space))
                        currentState = ROUND_START_STATE;
                    break;
                }
            case ROUND_START_STATE:
                {
                    roundManagerKB.SetupRound();
                    Debug.Log("Round number: " + roundManagerKB.currentRound);

                    currentState = PLAYING_STATE;

                    break;
                }
            case PLAYING_STATE:
                {
                    //If a bomb has not been missed
                    if (!roundManagerKB.roundFail)
                    {
                        //Continue to drop bombs and check for success
                        bombDropperKB.DropBombs();

                        if (roundManagerKB.roundSuccess)
                        {
                            currentState = ROUND_END_STATE;
                        }
                    }
                    else
                    {
                        //Freeze the game and destroy all other active bombs
                        freezeMovement = true;
                        roundManagerKB.DestroyActiveBombs();

                        //Reset the number of bombsDropped so that the remaining number
                        //of bombs for this round can be dropped
                        bombDropperKB.totalBombsDropped = roundManagerKB.totalBombsCaught;

                        if (Input.GetKey(KeyCode.Space))
                        {
                            roundManagerKB.roundFail = false;
                            freezeMovement = false;
                        }
                    }

                    break;
                }
            case ROUND_END_STATE:
                {
                    if (Input.GetKey(KeyCode.Space))
                    {
                        //Increase round number
                        roundManagerKB.currentRound++;

                        //Reset comparison variables
                        bombDropperKB.totalBombsDropped = 0;
                        roundManagerKB.roundSuccess = false;

                        //Move to the next round
                        currentState = ROUND_START_STATE;
                    }

                    break;
                }
            case GAMEOVER_STATE:
                {
                    break;
                }
        }
    }
    #endregion

    //Minus a life from the player
    #region LoseALife
    public void LoseALife()
    {
        tires.GetChild(playerLives - 1).gameObject.SetActive(false);

        playerLives--;
        Debug.Log("Lives left: " + playerLives);

        if (playerLives == 0)
            gameOver = true;
    }
    #endregion

    #region GainALife
    public void GainALife()
    {
        //If the player doesnt have maximum lives already
        if (playerLives < 3)
        {
            tires.GetChild(playerLives).gameObject.SetActive(true);
            playerLives++;
        }
    }
    #endregion
}
