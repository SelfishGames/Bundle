using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GUIManagerKB : MonoBehaviour
{
    public GameManagerKB gameManagerKB;

    public Text roundText,
        scoreText,
        gameOverText;
    
    void Start()
    {
        gameOverText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        roundText.text = "Current round: " + gameManagerKB.roundManagerKB.currentRound;

        scoreText.text = "Score: " + gameManagerKB.pointsManagerKB.currentScore;
    }
}
