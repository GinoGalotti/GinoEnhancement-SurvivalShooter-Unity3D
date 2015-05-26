/* 
 * Copyright (C) Luis Galotti Muñoz (Gino) <ginogalotti@gmail.com>
 * Assets are derived from Unity's Survival tutorial project
 * May 2015
 * 
 * Code explanation and Gameplay decisitions covered in EnhancementScriptLoader.cs's header. Please, take a look
 */

using UnityEngine;
using System.Collections;

public class PlayerManager : Singleton<PlayerManager> {

	//Game objects
	private static PlayerHealth playerHealth;
	private static PlayerShooting playerShooting;
	private static PlayerAppeareanceModificator playerAppeareance;
	private static PlayerMovement playerMovement;
	private static PlayerInteraction playerInteraction;

	void Start () {
		playerHealth = GetComponent <PlayerHealth> ();
		playerAppeareance = GetComponent <PlayerAppeareanceModificator> ();
		playerMovement = GetComponent <PlayerMovement> ();
		playerInteraction = GetComponent <PlayerInteraction> ();
		playerShooting.GetComponentInChildren<PlayerShooting> ();
	}

	public static void playerWantToShoot(float deltaTime)
	{

	}



}
