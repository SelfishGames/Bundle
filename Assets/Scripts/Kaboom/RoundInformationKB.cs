using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoundInformationKB : MonoBehaviour 
{
	public GameManagerKB gameManagerKB;

	public float dropperMoveSpeed,
		bombDropDelay,
		bombMoveSpeed;

	public int bombCount,
		falseBombCount,
		bombValue; 

	public Color bombColour;

	private List<Color> randomColours = new List<Color>();

	// Use this for initialization
	void Start () 
	{
		randomColours.Add(Color.red);
		randomColours.Add(Color.blue);
		randomColours.Add(Color.green);
		randomColours.Add(Color.yellow);

		bombColour = randomColours[Random.Range(0, randomColours.Count)];
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
