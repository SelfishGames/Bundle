using UnityEngine;
using System.Collections;

public class RoundInformationKB : MonoBehaviour 
{
	public GameManagerKB gameManagerKB;

	public float dropperMoveSpeed,
		bombDropDelay,
		bombMoveSpeed;

	public int bombCount,
		falseBombCount,
		bombValue; 

	public Color targetColour;

	// Use this for initialization
	void Start () 
	{
		//Selects a random colour from the list
		targetColour = gameManagerKB.randomColours[Random.Range(0, 3)];
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
