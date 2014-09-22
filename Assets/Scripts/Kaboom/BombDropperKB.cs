using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BombDropperKB : MonoBehaviour
{
    #region Fields
    public GameManagerKB gameManagerKB;

    public List<GameObject> bombList;
    public float moveSpeed;
    public float bombDropDelay;

    [HideInInspector]
    public int bombsThisRound;

    private float timer = 0;

    private Vector3 targetPos;
    private float leftWall = -6.5f,
        rightWall = 6.5f;

    private bool isMoving = false;
    #endregion

    #region Start
    void Start()
    {
        targetPos = this.transform.position;
    }
    #endregion

    //Gets an inactive bomb to drop
    #region GetBomb
    Transform GetBomb()
    {
        //Find and return an inactive bomb
        for (int i = 0; i < bombList.Count; i++)
        {
            if (!bombList[i].activeSelf)
            {
                return bombList[i].transform;
            }
        }
        return null;
    }
    #endregion

    //Moves the dropper side to side and drops the bombs
    #region DropBombs
    public void DropBombs()
    {
        timer += Time.deltaTime;

        //If there are still more bombs to be dropped
        if (gameManagerKB.roundManagerKB.totalBombsDropped < bombsThisRound)
        {
            //If the dropper is not moving, select a targetPosition and begin moving it
            if (!isMoving)
            {
                SetTargetPosition();
                isMoving = true;
            }
            else
            {
                //If the dropper is not close to its targetPosition, then keep moving it closer
                if (Vector3.Distance(transform.position, targetPos) >= 0.5f)
                    transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * moveSpeed);
                else
                    isMoving = false;
            }

            //Every time the timer passes the drop delay
            if (timer > bombDropDelay)
            {
                //Gets an inactive bomb
                Transform nextBomb = GetBomb();

                //Drop the bomb from the bombDroppers position
                Vector3 dropPos = this.transform.position;
                nextBomb.position = dropPos;

                //A random chance to drop a wrong coloured bomb 30% of the time
                if(Random.Range(0, 10) < 3)
                {
                    while(nextBomb.renderer.material.color == gameManagerKB.roundManagerKB.currentTargetColour)
                        nextBomb.renderer.material.color = gameManagerKB.randomColours[Random.Range(0, 3)];
                }
                else
                {
                    nextBomb.renderer.material.color = gameManagerKB.roundManagerKB.currentTargetColour;

                    //Only increment when a correct bomb is dropped, to ensure the correct
                    //number of valid bombs is always dropped
                    gameManagerKB.roundManagerKB.totalBombsDropped++;
                }

                //Activates the dropped bomb
                nextBomb.gameObject.SetActive(true);

                //Resets the timer
                timer = 0;
            }
        }
    }
    #endregion

    //Moves the dropper side to side
    #region SetTargetPosition
    void SetTargetPosition()
    {
        //Picks a random position for the dropper to move to
        targetPos.x = Random.Range(leftWall, rightWall);
    }
    #endregion
}
