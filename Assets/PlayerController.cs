using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    private Transform selected;
    private Vector3 offset;
    private Vector3 screenPoint;

    #region Update
    void Update()
    {

    }
    #endregion

    void OnMouseDown()
    {
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
 
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
    }

    void OnMouseDrag()
    {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);

        float x = Camera.main.ScreenToWorldPoint(curScreenPoint).x + offset.x;

        Vector3 dragPos = transform.position;
        dragPos.x = x;
        dragPos.x = Mathf.Clamp(dragPos.x, -5f, 5f);

        transform.position = dragPos;
    }
}
