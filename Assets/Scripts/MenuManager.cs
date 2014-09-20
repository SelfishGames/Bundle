using UnityEngine;
using System.Collections.Generic;

public class MenuManager : MonoBehaviour
{

    public List<GameObject> menuObjects = new List<GameObject>();

    public void ChangeScene(int scene)
    {
        Application.LoadLevel(scene);
    }

    public void Settings()
    {
        foreach(GameObject mo in menuObjects)
        {
            if(mo)
            {
                mo.SetActive(!mo.activeSelf);
            }
        }
    }

    public void Quit()
    {
        Application.Quit();
    }

}

