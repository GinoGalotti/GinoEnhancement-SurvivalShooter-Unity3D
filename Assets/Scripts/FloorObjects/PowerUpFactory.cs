/* 
 * Copyright (C) Luis Galotti Muñoz (Gino) <ginogalotti@gmail.com>
 * Assets are derived from Unity's Survival tutorial project
 * May 2015
 * 
 * Code explanation and Gameplay decisitions covered in EnhancementScriptLoader.cs's header. Please, take a look
 */

using UnityEngine;
using System.Collections;

public class PowerUpFactory : MonoBehaviour {

	//Game objects
	public GameObject powerUp;
	public Transform[] availablePowerUpPositions;

	//Gameplay variables
	private const float SPAWN_TIME_IN_SEC = 5f;

	//Game variables
	private int lastPowerUpPosition;
	private const int MAXIMUM_INCREASE_OF_POWER_UP_POSITION = 3;
	private bool needShowPowerUpMessage = true;
	
	void Start ()
	{
		InvokeRepeating ("Spawn", SPAWN_TIME_IN_SEC, SPAWN_TIME_IN_SEC);
		lastPowerUpPosition = Random.Range(0, availablePowerUpPositions.Length);
	}
	
	private void Spawn ()
	{
		if(GameStatusManager.isPlayingStatus())
		{
			if (powerUpSpawns()){
				if (needShowPowerUpMessage) 
				{
					needShowPowerUpMessage = false;
					MessagesManager.instance.showMessage(MessagesManager.DEFAULT_POWER_UP_MESSAGE);
				}
				int spawnPointIndex = chooseNextPosition();
				Instantiate (powerUp, availablePowerUpPositions[spawnPointIndex].position, availablePowerUpPositions[spawnPointIndex].rotation);
			}
		}
	}

	private bool powerUpSpawns()
	{
		return PowerUpManager.instance.getChanceOfPowerUp() >= Random.Range (0, 1);
	}
	
	private int chooseNextPosition()
	{	
		int newSpwnPosition = (lastPowerUpPosition + Random.Range (1, MAXIMUM_INCREASE_OF_POWER_UP_POSITION)) % availablePowerUpPositions.Length;
		lastPowerUpPosition = newSpwnPosition;
		return newSpwnPosition;
	}
}
