using UnityEngine;
using System.Collections;

public class PointsManagerKB : MonoBehaviour
{
    public int bombValue;

    private int currentScore;

    void Start()
    {

    }

    void Update()
    {

    }

    public void AddPoints()
    {
        currentScore += bombValue;
    }
}
