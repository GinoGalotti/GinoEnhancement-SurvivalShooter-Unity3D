/* 
 * Copyright (C) Luis Galotti Muñoz (Gino) <ginogalotti@gmail.com>
 * Assets are derived from Unity's Survival tutorial project
 * May 2015
 * 
 * Code explanation and Gameplay decisitions covered in EnhancementScriptLoader.cs's header. Please, take a look
 */

using UnityEngine;
using System.Collections;

public class PowerUpManager : MonoBehaviour {

	//Game objects
	public static PowerUpManager instance = null;
	
	//Power up handling variables
	public static Color[] triggerTypePowerUpColors = new Color[(int)StatusData.TRIGGER_EFFECT_CODE.NUMBER_OF_EFFECTS]{new Color(0.65f, 0.8f, 0f),Color.cyan, Color.blue};  
	public static Color[] playerTypePowerUpColors = new Color[(int)StatusData.PLAYER_BENEFICIAL_STATUS_CODE.NUMBER_OF_STATUS]{Color.yellow, Color.grey, Color.black, Color.green, new Color(0.95f, 0.6f, 0f)};  
	public static Color[] weaponTypePowerUpColors = new Color[(int)StatusData.WEAPON_EFFECT_CODE.NUMBER_OF_EFFECTS]{Color.red, new Color(0.3f, 0.2f, 0.9f), new Color(0.8f, 0.1f, 0.6f), Color.black, new Color(0.9f, 0.4f, 0.1f), Color.magenta}; 

	//Gameplay variables
	private float spawnDurationOfPowerUpsInSecs = 20f;
	private float chanceOfPowerUp = 0.3f;
	private float durationOfPowerUpModificator = 1f;

	void Start ()
	{
		doSingletonMagic ();
	}

	private void doSingletonMagic()
	{
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);
	}

	public void activateEffect (StatusData.GAME_MODIFICATION_TYPE type, int statusCodeIndex) 
	{
		switch (type) 
		{
		case StatusData.GAME_MODIFICATION_TYPE.PLAYER_BENEFICIAL_STATUS:
			StatusManager.instance.addDuration(type, statusCodeIndex, StatusData.STARTING_PLAYER_BENEFICIAL_STATUS_DURATION_IN_SEC[statusCodeIndex] * durationOfPowerUpModificator);
			break;
		case StatusData.GAME_MODIFICATION_TYPE.WEAPON_STATUS:
			StatusManager.instance.addDuration(type, statusCodeIndex, StatusData.STARTING_WEAPON_EFFECT_DURATION_IN_AMMO[statusCodeIndex] * durationOfPowerUpModificator);
			break;
		case StatusData.GAME_MODIFICATION_TYPE.TRIGGER:
			StatusData.TRIGGER_EFFECT_CODE triggerCode = (StatusData.TRIGGER_EFFECT_CODE) statusCodeIndex;
			if (triggerCode == StatusData.TRIGGER_EFFECT_CODE.FREEZE_ENEMIES)
				StatusManager.instance.addDuration(type, statusCodeIndex, StatusData.STARTING_ENEMIES_STATUS_DURATION_IN_SEC[(int)StatusData.ENEMIES_STATUS_CODE.FREEZE] * durationOfPowerUpModificator);
			else if (triggerCode == StatusData.TRIGGER_EFFECT_CODE.ADD_LEVEL)
				LevelUpManager.instance.powerUpTriggerActivated();
			break;
		}
	}

	public void LevelUpLuck(float delta)
	{
		chanceOfPowerUp *= delta;
	}

	public float getChanceOfPowerUp()
	{
		return chanceOfPowerUp;
	}

	public void levelUpPowerUpAliveDuration(float deltaInSecs)
	{
		spawnDurationOfPowerUpsInSecs *= deltaInSecs;
	}

	public void levelUpEffectDuration(float delta)
	{
		durationOfPowerUpModificator *= delta;
	}

	public float getPowerUpSpawnDuration()
	{
		return spawnDurationOfPowerUpsInSecs;
	}

}
