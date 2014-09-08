using UnityEngine;
using System.Collections;

public class BombKB : MonoBehaviour
{
    public GameManagerKB gameManagerKB;

    public float speed;

    // Use this for initialization
    void Start()
    {

    }

    #region Update
    void Update()
    {
        //Fall if the game isnt frozen
        if(!gameManagerKB.freezeMovement)
            this.transform.position += Vector3.down * speed * Time.deltaTime;
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
            Debug.Log("Bombs caught: " + gameManagerKB.roundManagerKB.totalBombsCaught);
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
