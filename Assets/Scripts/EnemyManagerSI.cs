using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyManagerSI : MonoBehaviour
{
    public List<GameObject> enemyCache = new List<GameObject>();
    public List<GameObject> enemies = new List<GameObject>();
   

    // Use this for initialization
    void Start()
    {
        ArrangeEnemies();      

    }

    // Update is called once per frame
    void Update()
    {

    }

    void ArrangeEnemies()
    {
        int whichEnemy = 0;
        for (int row = 0; row < 5; row++)
        {
            for (int column = 0; column < 10; column++)
            {

                enemies[whichEnemy].transform.position = new Vector3(column, row, 0);
                whichEnemy++;
            }
        }
    }
}
