using UnityEngine;
using System.Collections;

public class BombKB : MonoBehaviour
{
    public GameManagerKB gameManagerKB;

    [HideInInspector]
    public float totalDropSpeed;

    private float baseDropSpeed = 3;

    // Use this for initialization
    void Start()
    {

    }

    #region Update
    void Update()
    {
        //Sets speed to be base value + the bonus that changes depending on the round
        //Clamps it between the base value and 8
        totalDropSpeed = Mathf.Clamp(baseDropSpeed + gameManagerKB.roundManagerKB.bonusDropSpeed, 3, 8);

        //Fall if the game isnt frozen
        if(!gameManagerKB.freezeMovement)
            this.transform.position += Vector3.down * totalDropSpeed * Time.deltaTime;
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
