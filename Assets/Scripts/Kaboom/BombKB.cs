using UnityEngine;
using System.Collections;

public class BombKB : MonoBehaviour
{
    #region Fields
    public GameManagerKB gameManagerKB;

    [HideInInspector]
    public float totalDropSpeed;
    public float moveSpeed;

    private float baseDropSpeed = 3;
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
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == "Tires")
        {
            gameManagerKB.pointsManagerKB.AddPoints();
            gameManagerKB.roundManagerKB.totalBombsCaught++;
            this.gameObject.SetActive(false);
        }

        if (col.gameObject.name == "Floor")
        {
            gameManagerKB.LoseALife();
            gameManagerKB.roundManagerKB.totalBombsMissed++;
            gameManagerKB.roundManagerKB.roundFail = true;
            this.gameObject.SetActive(false);
        }
    }
    #endregion
}
