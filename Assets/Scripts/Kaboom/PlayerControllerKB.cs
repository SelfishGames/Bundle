using UnityEngine;
using System.Collections;

public class PlayerControllerKB : MonoBehaviour
{
    #region Fields
    public GameManagerKB gameManagerKB;

    private Transform selected;
    private Vector3 offset;
    private Vector3 screenPoint;

    private float leftWall = -6.5f,
        rightWall = 6.5f;
    #endregion

    #region Update
    void Update()
    {
        //If the game isnt frozen
        if (!gameManagerKB.freezeMovement)
        {
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);

            //float x = Camera.main.ScreenToWorldPoint(curScreenPoint).x + offset.x;
            float x = Camera.main.ScreenToWorldPoint(curScreenPoint).x;

            Vector3 dragPos = transform.position;
            dragPos.x = x;
            dragPos.x = Mathf.Clamp(dragPos.x, leftWall, rightWall);

            transform.position = dragPos;
        }
    }
    #endregion

    //#region OnMouseDown
    //void OnMouseDown()
    //{
    //    screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
 
    //    offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
    //}
    //#endregion

    //#region OnMouseDrag
    //void OnMouseDrag()
    //{
    //    //If the game isnt frozen
    //    if (!gameManagerKB.freezeMovement)
    //    {
    //        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);

    //        //float x = Camera.main.ScreenToWorldPoint(curScreenPoint).x + offset.x;
    //        float x = Camera.main.ScreenToWorldPoint(curScreenPoint).x;

    //        Vector3 dragPos = transform.position;
    //        dragPos.x = x;
    //        dragPos.x = Mathf.Clamp(dragPos.x, leftWall, rightWall);

    //        transform.position = dragPos;
    //    }
    //}
    //#endregion
}
