using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GUIManagerKB : MonoBehaviour
{
    #region Fields
    public GameManagerKB gameManagerKB;

    public Text roundText,
        scoreText,
        gameOverText;
    #endregion

    #region Start
    void Start()
    {
        gameOverText.gameObject.SetActive(false);
    }
    #endregion

    #region Update
    void Update()
    {
        roundText.text = "Current round: " + gameManagerKB.roundManagerKB.currentRound;

        scoreText.text = "Score: " + gameManagerKB.pointsManagerKB.currentScore;
    }
    #endregion
}
