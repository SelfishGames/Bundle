﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BombDropperKB : MonoBehaviour
{
    public List<GameObject> bombList;
    public float speed;
    public float bombDropDelay;

    [HideInInspector]
    public int bombsThisRound,
        totalBombsDropped,
        totalBombsCaught;

    private float timer = 0;

    private Vector3 targetPos;
    private float leftWall = -4.5f,
        rightWall = 4.5f;

    private bool isMoving = false;

    void Start()
    {
        targetPos = this.transform.position;

        totalBombsDropped = 0;
    }

    #region Update
    void Update()
    {

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

        if (totalBombsDropped < bombsThisRound)
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
                    transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * speed);
                else
                    isMoving = false;
            }

            //Every time the timer passes the drop delay
            if (timer > bombDropDelay)
            {
                Transform nextBomb = GetBomb();

                //Drop the bomb from the bombDroppers position
                Vector3 dropPos = this.transform.position;
                nextBomb.position = dropPos;

                //Activates the dropped bomb
                nextBomb.gameObject.SetActive(true);

                totalBombsDropped++;

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
