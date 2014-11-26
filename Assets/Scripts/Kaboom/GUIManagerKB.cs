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

    private bool mainMenu = true,
        optionsMenu = false,
        challengeMenu = false,
        playing = false;

    public Font myFont;
    public Color myColor;
    public GUIStyle myGUIStyle;
    public GUIStyle sliderStyle;
    public GUISkin myGUISkin;

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
    }
    #endregion

    #region Update
    void Update()
    {
        //Current round text
        textList[2].text = "Round : " + gameManagerKB.roundManagerKB.currentRound;
        //Currect score text
        textList[3].text = "Score : " + gameManagerKB.pointsManagerKB.currentScore;
        //Final score text
        textList[5].text = "Final score : " + gameManagerKB.pointsManagerKB.currentScore;
        //Highscore text
        int tempHighscore = PlayerPrefs.GetInt("HighScore");
        textList[6].text = "Your highscore : " + tempHighscore;
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

        if(mainMenu)
        {
            myGUISkin.label.fontSize = 70;
            //Game title
            GUI.Label(new Rect(70, 100, 400, 120), "K a b l a m");

            //Play button
            if (GUI.Button(new Rect(10, 200, 
                buttonTextureList[0].width / 1.5f, buttonTextureList[0].height / 1.5f), 
                buttonTextureList[0]))
            {
                soundFXList[1].Play();
                gameManagerKB.currentState = 1;
                mainMenu = false;
                playing = true;
            }

            //Options button
            if (GUI.Button(new Rect(175, 200,
                buttonTextureList[1].width / 1.5f, buttonTextureList[1].height / 1.5f), 
                buttonTextureList[1]))
            {
                soundFXList[1].Play();
                mainMenu = false;
                optionsMenu = true;
            }

            //Challenges button
            if (GUI.Button(new Rect(340, 200,
                buttonTextureList[2].width / 1.5f, buttonTextureList[2].height / 1.5f), 
                buttonTextureList[2]))
            {
                soundFXList[1].Play();
                mainMenu = false;
                challengeMenu = true;
            }

            myGUISkin.label.fontSize = 15;
            //Credits 
            GUI.Label(new Rect(50, 10, 460, 20), "Created by Andrew Ashton, looked at by Rob Brooks");
        }

        if(optionsMenu)
        {
            //Music volume
            GUI.Label(new Rect(50, 120, 200, 25), "Music Volume");
            tempMusicVolume = GUI.HorizontalSlider(new Rect(225, 120, 256, 28), tempMusicVolume, 0, 100);
            PlayerPrefs.SetFloat("prefMusicVol", tempMusicVolume);

            //SFX volume
            GUI.Label(new Rect(50, 150, 200, 25), "SFX Volume");
            tempSFXVolume = GUI.HorizontalSlider(new Rect(225, 150, 256, 28), tempSFXVolume, 0, 100);
            PlayerPrefs.SetFloat("prefSFXVol", tempSFXVolume);

            //Back button
            if (GUI.Button(new Rect(340, 200,
                buttonTextureList[2].width / 1.5f, buttonTextureList[2].height / 1.5f), 
                buttonTextureList[3]))
            {
                soundFXList[1].Play();
                optionsMenu = false;
                mainMenu = true;
            }
        }

        if(challengeMenu)
        {
            //Back button
            if (GUI.Button(new Rect(340, 200,
                buttonTextureList[2].width / 1.5f, buttonTextureList[2].height / 1.5f), 
                buttonTextureList[3]))
            {
                soundFXList[1].Play();
                challengeMenu = false;
                mainMenu = true;
            }
        }

        if (playing)
        {
            //If the game is not at the menu state or gameover state 
            if (gameManagerKB.currentState != 5 && gameManagerKB.currentState != 0)
            {
                myGUISkin.label.fontSize = 20;
                //Current round
                GUI.Label(new Rect(50, 10, 150, 30), textList[2].text);
                //Current score
                GUI.Label(new Rect(220, 10, 150, 30), textList[3].text);
            }

            //If the game is in the gameover state
            if(gameManagerKB.currentState == 5)
            {
                myGUISkin.label.fontSize = 20;
                //Restart text
                GUI.Label(new Rect(100, 120, 350, 30), textList[4].text);
                //Players score for that game
                GUI.Label(new Rect(100, 150, 350, 30), textList[5].text);
                //Players highscore
                GUI.Label(new Rect(100, 180, 350, 30), textList[6].text);
            }
        }

        // restore matrix before returning
        GUI.matrix = svMat;
    }

    //Activates the GUI elements for the title screen
    #region TitleScreen
    public void DisplayTitleScreen()
    {
        //textList[0].gameObject.SetActive(true);
        //textList[1].gameObject.SetActive(true);
    }
    #endregion

    //Activates the GUI elements for the in-game screen
    #region InGameScreen
    public void DisplayInGameScreen()
    {
        //textList[2].gameObject.SetActive(true);
        //textList[3].gameObject.SetActive(true);
    }
    #endregion

    //Activates the GUI elements for the gameover screen
    #region GameOverScreen
    public void DisplayGameOverScreen()
    {
        //textList[4].gameObject.SetActive(true);
        //textList[5].gameObject.SetActive(true);
        //if(PlayerPrefs.HasKey("HighScore"))
        //    textList[6].gameObject.SetActive(true);
    }
    #endregion

    //Deactivates all currently active GUI elements
    #region ResetGUI
    public void ResetGUI()
    {
        foreach(Text text in textList)
        {
            if (text.gameObject.activeSelf)
                text.gameObject.SetActive(false);
        }
    }
    #endregion
}
