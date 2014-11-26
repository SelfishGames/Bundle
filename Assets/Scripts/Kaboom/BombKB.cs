using UnityEngine;
using System.Collections;

public class BombKB : MonoBehaviour
{
    #region Fields
    public GameManagerKB gameManagerKB;

    [HideInInspector]
    public float moveSpeed;
    #endregion

    #region Update
    void Update()
    {
        moveSpeed = gameManagerKB.roundManagerKB.currentBombSpeed;

        //Fall if the game isnt frozen
        if(!gameManagerKB.freezeMovement)
            this.transform.position += Vector3.down * moveSpeed * Time.deltaTime;
    }
    #endregion

    //When the bomb enters a trigger
    #region OnTriggerEnter
    void OnTriggerEnter2D(Collider2D col)
    {
        //If the bomb hits the tires
        if (col.gameObject.name == "Paddle")
        {
            //If the player catches a valid bomb, then add points
            if(this.renderer.material.color == gameManagerKB.roundManagerKB.currentTargetColour)
            {
                gameManagerKB.pointsManagerKB.AddPoints();
                gameManagerKB.roundManagerKB.totalBombsCaught++;
                this.gameObject.SetActive(false);
            }
            //Otherwise, lose a life
            else
            {
                gameManagerKB.roundManagerKB.roundFail = true;
                this.gameObject.SetActive(false);
            }
        }

        if (col.gameObject.name == "Floor")
        {
            //If the player drops a valid bomb, then lose a life
            if(this.renderer.material.color == gameManagerKB.roundManagerKB.currentTargetColour)
            {
                gameManagerKB.roundManagerKB.totalBombsMissed++;
                gameManagerKB.roundManagerKB.roundFail = true;
                this.gameObject.SetActive(false);
            }
            //Otherwise, nothing
            else
            {
                this.gameObject.SetActive(false);
            }
        }
    }
    #endregion
}
