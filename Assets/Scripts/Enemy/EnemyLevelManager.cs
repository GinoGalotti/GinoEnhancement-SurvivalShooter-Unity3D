/* 
 * Copyright (C) Luis Galotti Muñoz (Gino) <ginogalotti@gmail.com>
 * Assets are derived from Unity's Survival tutorial project
 * May 2015
 * 
 * Code explanation and Gameplay decisitions covered in EnhancementScriptLoader.cs's header. Please, take a look
 */

using UnityEngine;
using System.Collections;

public class EnemyLevelManager : MonoBehaviour {
	
	//Game Objects
	public static EnemyLevelManager instance = null;

	//Gameplay Variables
	public const float ENEMY_HEALTH_INCREASE_PER_LEVEL = 1.2f;
	public const float ENEMY_DAMAGE_INCREASE_PER_LEVEL = 1.15f;
	public const float ENEMY_SCORE_INCREASE_PER_LEVEL = 1.1f;
	public const float ENEMY_MUIATION_CHANCE_INCREASE_PER_LEVEL = 1.05f;

	private const float TIME_BETWEEN_ENEMIES_LEVELING_UP_IN_SECS = 20f;
	private float remainingTime = TIME_BETWEEN_ENEMIES_LEVELING_UP_IN_SECS;
	private int enemyLevel = 0;
	
	void Awake () {
		doSingletonMagic ();
	}
	
	private void doSingletonMagic()
	{
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);
	}

	public int getEnemyLevel()
	{
		return enemyLevel;
	}

	
	void Update () {
		remainingTime -= Time.deltaTime;
		if (remainingTime <= 0) 
		{
			remainingTime = TIME_BETWEEN_ENEMIES_LEVELING_UP_IN_SECS;
			enemyLevel++;
			MessagesManager.instance.showMessage(MessagesManager.DEFAULT_ENEMY_LEVEL_UP_MESSAGE);
		}
	}
}
