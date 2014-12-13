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
    public AudioManagerKB audioManagerKB;
    public Transform paddles;

    [HideInInspector]
    public bool freezeMovement;

    //For the bomb and player things colours
    public List<Color> randomColours = new List<Color>(4);

    private int playerLives;

    private bool gameOver = false;

    [HideInInspector]
    public int currentState;
    private int roundIndex;
    private const int MENU_STATE = 0,
        ROUND_START_STATE = 1,
        PLAYING_STATE = 2,
        BOMB_MISSED_STATE = 3,
        ROUND_END_STATE = 4,
        GAMEOVER_STATE = 5;

    private int totalGamesPlayed,
        totalRoundsBeaten;
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

        totalGamesPlayed = PlayerPrefs.GetInt("TotalGamesPlayed");
        totalRoundsBeaten = PlayerPrefs.GetInt("TotalRoundsBeaten");
    }
    #endregion

    #region Update
    void Update()
    {
        switch (currentState)
        {
            case MENU_STATE:
                {
                    freezeMovement = true;

                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        currentState = ROUND_START_STATE;
                    }
                    break;
                }
            case ROUND_START_STATE:
                {
                    //Setup the number of bombs to be dropped and the point value of each bomb
                    roundManagerKB.SetupRound();

                    guiManagerKB.currentState = 3;

                    roundIndex = roundManagerKB.currentRound;

                    if(!audioManagerKB.musicList[0].isPlaying)
                        audioManagerKB.musicList[0].Play();

                    break;
                }
            case PLAYING_STATE:
                {
                    //Unfreeze the game
                    freezeMovement = false;

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
                        LoseALife();

                        //If all lives are lost, then end the game
                        if (playerLives == 0)
                        {
                            currentState = GAMEOVER_STATE;
                        }
                        //Otherwise continue the game
                        else
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

                    if (Input.GetMouseButton(0))
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
                    guiManagerKB.currentState = 5;

                    if (Input.GetMouseButtonDown(0))
                    {
                        //Increase round number
                        roundManagerKB.currentRound++;

                        roundManagerKB.roundSuccess = false;

                        //Increment the total rounds beaten for statistics
                        totalRoundsBeaten++;

                        //Move to the next round
                        currentState = ROUND_START_STATE;
                    }

                    break;
                }
            case GAMEOVER_STATE:
                {
                    roundManagerKB.DestroyActiveBombs();

                    if (Input.GetMouseButton(0))
                    {
                        //Increase and assign the # of games played
                        totalGamesPlayed++;
                        PlayerPrefs.SetInt("TotalGamesPlayed", totalGamesPlayed);

                        //Assigns the # of rounds beaten 
                        PlayerPrefs.SetInt("TotalRoundsBeaten", totalRoundsBeaten);

                        Application.LoadLevel("Kaboom");
                    }
                    break;
                }
        }
    }
    #endregion

    //Minus a life from the player
    #region LoseALife
    public void LoseALife()
    {
        paddles.GetChild(playerLives - 1).gameObject.SetActive(false);

        playerLives--;
    }
    #endregion

    //Plus a life to the player
    #region GainALife
    public void GainALife()
    {
        //If the player doesnt have maximum lives already
        if (playerLives < 3)
        {
            paddles.GetChild(playerLives).gameObject.SetActive(true);
            playerLives++;
        }
    }
    #endregion
}
