using UnityEngine;
using System.Collections;

public class GameManagerKB : MonoBehaviour
{
    public PointsManagerKB pointsManagerKB;
    public RoundManagerKB roundManagerKB;
    public BombDropperKB bombDropperKB;

    private int playerLives;

    private bool gameOver;

    private int currentState;
    private const int MENU_STATE = 0,
        ROUND_START_STATE = 1,
        PLAYING_STATE = 2,
        ROUND_END_STATE = 3;

    // Use this for initialization
    void Start()
    {
        playerLives = 3;
        gameOver = false;
        currentState = MENU_STATE;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("currentState " + currentState);

        switch(currentState)
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

                    currentState = PLAYING_STATE;

                    break;
                }
            case PLAYING_STATE:
                {
                    bombDropperKB.DropBombs();



                    break;
                }
            case ROUND_END_STATE:
                {
                    bombDropperKB.totalBombsDropped = 0;

                    break;
                }
        }
    }

    //Minus a life from the player
    #region LoseALife
    public void LoseALife()
    {
        playerLives--;

        if (playerLives == 0)
            gameOver = true;
    }
    #endregion
}
