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

    // Update is called once per frame
    void Update()
    {
        this.transform.position += Vector3.down * speed * Time.deltaTime;
    }

    #region OnTriggerEnter
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == "Tires")
        {
            gameManagerKB.pointsManagerKB.AddPoints();
            gameManagerKB.bombDropperKB.totalBombsCaught++;
            this.gameObject.SetActive(false);
        }

        if (col.gameObject.name == "Floor")
        {
            this.gameObject.SetActive(false);
        }
    }
    #endregion
}
