using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class GUIManagerKB : MonoBehaviour
{
    [ExecuteInEditMode]

    #region Fields
    public GameManagerKB gameManagerKB;

    public List<Texture2D> buttonTextureList;
    public List<Text> textList;

    public int currentState;
    private const int MAIN_MENU = 0,
        OPTIONS_MENU = 1,
        STATISTICS_MENU = 2,
        PRE_PLAYING = 3,
        PLAYING = 4,
        POST_PLAYING = 5;

    private string roundText,
        scoreText,
        finalScoreText,
        highScoreText;

    public Font myFont;
    public Color myColor;
    public GUIStyle myGUIStyle;
    public GUIStyle sliderStyle;
    public GUISkin myGUISkin;

    private float buttonWidth,
        buttonHeight;

    public AudioSource soundFXObject;

    private float originalWidth = 522f,
        originalHeight = 326f;
    private Vector3 scale;

    private AudioSource[] soundFXList;
    private float tempMusicVolume = 0f,
        tempSFXVolume = 0f;
    #endregion

    #region Start
    void Start()
    {
        soundFXList = soundFXObject.GetComponents<AudioSource>();

        tempSFXVolume = PlayerPrefs.GetFloat("prefSFXVol");
        tempMusicVolume = PlayerPrefs.GetFloat("prefMusicVol");

        buttonWidth = myGUISkin.button.normal.background.width;
        buttonHeight = myGUISkin.button.normal.background.height;
    }
    #endregion

    #region Update
    void Update()
    {
        //Current round text
        roundText = "Round : " + gameManagerKB.roundManagerKB.currentRound;
        //Currect score text
        scoreText = "Score : " + gameManagerKB.pointsManagerKB.currentScore;
        //Final score text
        finalScoreText = "Final score : " + gameManagerKB.pointsManagerKB.currentScore;
        //Highscore text
        int tempHighscore = PlayerPrefs.GetInt("HighScore");
        highScoreText = "Your highscore : " + tempHighscore;
    }
    #endregion

    private void OnGUI()
    {
         scale.x = Screen.width/originalWidth; // calculate hor scale
         scale.y = Screen.height/originalHeight; // calculate vert scale
         scale.z = 1;
         var svMat = GUI.matrix; // save current matrix
         // substitute matrix - only scale is altered from standard
         GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, scale);

         GUI.skin = myGUISkin; 

        switch(currentState)
        {
            case MAIN_MENU:
                {
                    myGUISkin.label.fontSize = 70;
                    //Game title
                    GUI.Label(new Rect(70, 90, 400, 120), "K a b l a m");

                    myGUISkin.button.fontSize = 15;
                    //Play button
                    if (GUI.Button(new Rect(50, 190, buttonWidth / 2, buttonHeight / 2), "Play"))
                    {
                        soundFXList[1].Play();
                        gameManagerKB.currentState = 1;
                        currentState = PRE_PLAYING;
                    }

                    //Options button
                    if (GUI.Button(new Rect(205, 190, buttonWidth / 2, buttonHeight / 2), "Options"))
                    {
                        soundFXList[1].Play();
                        currentState = OPTIONS_MENU;
                    }

                    myGUISkin.button.fontSize = 13;
                    //Statistics button
                    if (GUI.Button(new Rect(350, 190, buttonWidth / 2, buttonHeight / 2), "Statistics"))
                    {
                        soundFXList[1].Play();
                        currentState = STATISTICS_MENU;
                    }

                    break;
                }
            case OPTIONS_MENU:
                {
                    myGUISkin.button.fontSize = 15;
                    myGUISkin.label.fontSize = 15;

                    //Music volume
                    GUI.Label(new Rect(50, 120, 200, 25), "Music Volume:");
                    tempMusicVolume = GUI.HorizontalSlider(new Rect(225, 120, 256, 28), tempMusicVolume, 0, 100);
                    PlayerPrefs.SetFloat("prefMusicVol", tempMusicVolume);

                    //SFX volume
                    GUI.Label(new Rect(50, 150, 200, 25), "SFX Volume:");
                    tempSFXVolume = GUI.HorizontalSlider(new Rect(225, 150, 256, 28), tempSFXVolume, 0, 100);
                    PlayerPrefs.SetFloat("prefSFXVol", tempSFXVolume);

                    //Back button
                    if (GUI.Button(new Rect(370, 250, buttonWidth / 2, buttonHeight / 2), "Back"))
                    {
                        soundFXList[1].Play();
                        currentState = MAIN_MENU;
                    }

                    break;
                }
            case STATISTICS_MENU:
                {
                    myGUISkin.button.fontSize = 15;
                    myGUISkin.label.fontSize = 15;

                    //Games Played
                    int tempGamesPlayed = PlayerPrefs.GetInt("TotalGamesPlayed");
                    GUI.Label(new Rect(30, 150, 200, 25), "# of Games Played : " + tempGamesPlayed);

                    //Rounds beaten
                    int tempRoundsBeaten = PlayerPrefs.GetInt("TotalRoundsBeaten");
                    GUI.Label(new Rect(30, 200, 200, 25), "# of Rounds Beaten : " + tempRoundsBeaten);

                    //HighScore
                    GUI.Label(new Rect(30, 250, 200, 25), highScoreText);

                    //Back button
                    if (GUI.Button(new Rect(370, 250, buttonWidth / 2, buttonHeight / 2), "Back"))
                    {
                        soundFXList[1].Play();
                        currentState = MAIN_MENU;
                    }

                    break;
                }
            case PRE_PLAYING:
                {
                    myGUISkin.label.fontSize = 15;

                    GUI.Label(new Rect(70, 90, 400, 120), "Click to begin");

                    if (Input.GetMouseButtonDown(0))
                    {
                        gameManagerKB.currentState = 2;
                        currentState = PLAYING;
                    }

                    break;
                }
            case PLAYING:
                {
                    //If the game is not at the menu state or gameover state 
                    if (gameManagerKB.currentState != 5 && gameManagerKB.currentState != 0)
                    {
                        myGUISkin.label.fontSize = 20;
                        //Current round
                        GUI.Label(new Rect(50, 10, 150, 30), roundText);
                        //Current score
                        GUI.Label(new Rect(220, 10, 150, 30), scoreText);
                    }

                    //If the game is in the gameover state
                    if (gameManagerKB.currentState == 5)
                    {
                        myGUISkin.label.fontSize = 20;
                        //Restart text
                        GUI.Label(new Rect(100, 120, 350, 30), textList[4].text);
                        //Players score for that game
                        GUI.Label(new Rect(100, 150, 350, 30), finalScoreText);
                        //Players highscore
                        GUI.Label(new Rect(100, 180, 350, 30), highScoreText);
                    }

                    break;
                }
            case POST_PLAYING:
                {
                    GUI.Label(new Rect(70, 90, 400, 120), "Round complete\nWell done!");

                    break;
                }
        }

        // restore matrix before returning
        GUI.matrix = svMat;
    }
}
