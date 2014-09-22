using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManagerKB : MonoBehaviour
{
    #region Fields
    public PointsManagerKB pointsManagerKB;
    public RoundManagerKB roundManagerKB;
    public BombDropperKB bombDropperKB;
    public GUIManagerKB guiManagerKB;
    public Transform tires;

    [HideInInspector]
    public bool freezeMovement;

    [HideInInspector]
    //For the bomb and player things colours
    public List<Color> randomColours = new List<Color>();

    private int playerLives;

    private bool gameOver;

    private int currentState,
        roundIndex;
    private const int MENU_STATE = 0,
        ROUND_START_STATE = 1,
        PLAYING_STATE = 2,
        BOMB_MISSED_STATE = 3,
        ROUND_END_STATE = 4,
        GAMEOVER_STATE = 5;
    #endregion

    #region Start
    void Start()
    {
        randomColours.Add(Color.red);
        randomColours.Add(Color.blue);
        randomColours.Add(Color.green);
        randomColours.Add(Color.yellow);

        playerLives = 3;
        gameOver = false;
        currentState = MENU_STATE;
    }
    #endregion

    #region Update
    void Update()
    {
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
                    //Setup the number of bombs to be dropped and the point value of each bomb
                    roundManagerKB.SetupRound();

                    roundIndex = roundManagerKB.currentRound;

                    currentState = PLAYING_STATE;

                    break;
                }
            case PLAYING_STATE:
                {
                    //If the player loses all 3 lives during play
                    if (gameOver)
                        currentState = GAMEOVER_STATE;

                    //Continue to drop bombs
                    bombDropperKB.DropBombs();

                    //Check if the round has succeeded by catching all the bombs
                    //or failed by missing a bomb
                    if (roundManagerKB.roundSuccess)
                    {
                        currentState = ROUND_END_STATE;
                    }
                    else if(roundManagerKB.roundFail)
                    {
                        currentState = BOMB_MISSED_STATE;
                    }
                    
                    break;
                }
            case BOMB_MISSED_STATE:
                {
                    //Freeze the game and destroy all other active bombs
                    freezeMovement = true;
                    roundManagerKB.DestroyActiveBombs();
                    
                    //Reset the number of bombsDropped so that the remaining number
                    //of bombs for this round can be dropped
                    roundManagerKB.totalBombsDropped = roundManagerKB.totalBombsCaught;

                    if (Input.GetKey(KeyCode.Space))
                    {
                        //Reduces the speed difficulty to match that of the previous round,
                        //within the range of the specified rounds
                        roundIndex--;
                        roundIndex = Mathf.Clamp(roundIndex, 1, 8);

                        roundManagerKB.currentBombSpeed = roundManagerKB.rounds[roundIndex].bombMoveSpeed;
                        bombDropperKB.moveSpeed = roundManagerKB.rounds[roundIndex].dropperMoveSpeed;
                        bombDropperKB.bombDropDelay = roundManagerKB.rounds[roundIndex].bombDropDelay;

                        roundManagerKB.roundFail = false;
                        freezeMovement = false;
                        currentState = PLAYING_STATE;
                    }

                    break;
                }
            case ROUND_END_STATE:
                {
                    if (Input.GetKey(KeyCode.Space))
                    {
                        //Increase round number
                        roundManagerKB.currentRound++;

                        roundManagerKB.roundSuccess = false;

                        //Move to the next round
                        currentState = ROUND_START_STATE;
                    }

                    break;
                }
            case GAMEOVER_STATE:
                {
                    guiManagerKB.gameOverText.gameObject.SetActive(true);

                    if (Input.GetKey(KeyCode.Space))
                        Application.LoadLevel("Kaboom");

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

    //Plus a life to the player
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
