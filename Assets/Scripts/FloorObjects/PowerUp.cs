
/* 
 * Copyright (C) Luis Galotti Muñoz (Gino) <ginogalotti@gmail.com>
 * Assets are derived from Unity's Survival tutorial project
 * May 2015
 * 
 * Code explanation and Gameplay decisitions covered in EnhancementScriptLoader.cs's header. Please, take a look
 */

using UnityEngine;
using System.Collections;

public class PowerUp : MonoBehaviour {

	//Game objects
	public Animator anim;

	private GameObject player;

	//Game variables
	private float remainingTime;
	private StatusData.GAME_MODIFICATION_TYPE powerUpType;
	private StatusData.PLAYER_BENEFICIAL_STATUS_CODE playerStatusCode;
	private StatusData.WEAPON_EFFECT_CODE weaponEffectCode;
	private StatusData.TRIGGER_EFFECT_CODE triggeredEffectCode;
	private const float START_BLINKING_TIME = 5f;
	private Color haloColor;
	private bool isTriggered = false;
	
	public void Awake () {
		Light halo = GetComponent<Light>();
		player = GameObject.FindGameObjectWithTag ("Player");
		randomizePowerUp ();
		halo.color = haloColor;
		remainingTime = PowerUpManager.instance.getPowerUpSpawnDuration ();
	}

	private void randomizePowerUp()
	{

		powerUpType = (StatusData.GAME_MODIFICATION_TYPE) pickRandomPowerUpTypeIndex ();
		int randomIndex;
		switch (powerUpType) 
		{
		case StatusData.GAME_MODIFICATION_TYPE.PLAYER_BENEFICIAL_STATUS:
			randomIndex = generateRandomPlayerStatusIndex();
			playerStatusCode = (StatusData.PLAYER_BENEFICIAL_STATUS_CODE) randomIndex;
			haloColor = PowerUpManager.playerTypePowerUpColors[randomIndex];
			break;
		case StatusData.GAME_MODIFICATION_TYPE.WEAPON_STATUS:
			randomIndex = generateRandomWeaponEffectIndex();
			weaponEffectCode = (StatusData.WEAPON_EFFECT_CODE) randomIndex;
			haloColor = PowerUpManager.weaponTypePowerUpColors[randomIndex];
			break;
		case StatusData.GAME_MODIFICATION_TYPE.TRIGGER:
			randomIndex = generateRandomStatusDataIndex();
			triggeredEffectCode = (StatusData.TRIGGER_EFFECT_CODE) randomIndex;
			haloColor = PowerUpManager.triggerTypePowerUpColors[randomIndex];
			break;
		}
	}

	public int pickRandomPowerUpTypeIndex()
	{
		int chosenPowerUpIndex = (int) StatusData.GAME_MODIFICATION_TYPE.TRIGGER;
		int totalOfPowerUps = (int)StatusData.PLAYER_BENEFICIAL_STATUS_CODE.NUMBER_OF_STATUS + (int)StatusData.WEAPON_EFFECT_CODE.NUMBER_OF_EFFECTS + (int)StatusData.TRIGGER_EFFECT_CODE.NUMBER_OF_EFFECTS;
		//int randomIndex = Random.Range ((int)StatusData.PLAYER_BENEFICIAL_STATUS_CODE.NUMBER_OF_STATUS, (int)StatusData.PLAYER_BENEFICIAL_STATUS_CODE.NUMBER_OF_STATUS + (int)StatusData.WEAPON_EFFECT_CODE.NUMBER_OF_EFFECTS +1);
		int randomIndex = Random.Range (0, totalOfPowerUps);
		if (randomIndex <= (int)StatusData.PLAYER_BENEFICIAL_STATUS_CODE.NUMBER_OF_STATUS) {
			chosenPowerUpIndex = (int)StatusData.GAME_MODIFICATION_TYPE.PLAYER_BENEFICIAL_STATUS;
		} else if (randomIndex <= (int)StatusData.PLAYER_BENEFICIAL_STATUS_CODE.NUMBER_OF_STATUS + (int)StatusData.WEAPON_EFFECT_CODE.NUMBER_OF_EFFECTS) {
			chosenPowerUpIndex = (int)StatusData.GAME_MODIFICATION_TYPE.WEAPON_STATUS;
		}

		return chosenPowerUpIndex;
	}
	
	public int generateRandomPlayerStatusIndex()
	{
		//TODO ZOMBIE_MODE and CHANGE_APPAREANCE not yet done. This will require more work.
		return Random.Range(0, (int)(StatusData.PLAYER_BENEFICIAL_STATUS_CODE.NUMBER_OF_STATUS  - 2));
	}
	
	public int generateRandomWeaponEffectIndex()
	{
		//TODO Ricochet, explosive and charming has not been implemented yet.
		return Random.Range (0, (int)StatusData.WEAPON_EFFECT_CODE.NUMBER_OF_EFFECTS - 3);
	}
	
	public int generateRandomStatusDataIndex()
	{
		//TODO SUPER_NOVA not yet implemented
		return Random.Range(0, (int)StatusData.TRIGGER_EFFECT_CODE.NUMBER_OF_EFFECTS - 1);
	}
	
	void Update () {
		remainingTime -= Time.deltaTime;
		if (remainingTime <= START_BLINKING_TIME) 
		{
			if (!anim.GetBool("PowerUpBlinking"))
				anim.SetBool("PowerUpBlinking", true);

			if (remainingTime <= 0) 
			{
				Die ();
			}
		}
	}

	void OnTriggerEnter (Collider other)
	{
		if(other.gameObject == player && !isTriggered)
		{
			isTriggered = true;
			switch (powerUpType)
			{
			case StatusData.GAME_MODIFICATION_TYPE.TRIGGER:
				PowerUpManager.instance.activateEffect(powerUpType, (int) triggeredEffectCode);
				Debug.Log ("Trigered effect "+ triggeredEffectCode.ToString());
				break;
			case StatusData.GAME_MODIFICATION_TYPE.PLAYER_BENEFICIAL_STATUS:
				PowerUpManager.instance.activateEffect(powerUpType, (int) playerStatusCode);
				Debug.Log ("Player effect "+ playerStatusCode.ToString());
				break;
			case StatusData.GAME_MODIFICATION_TYPE.WEAPON_STATUS:
				PowerUpManager.instance.activateEffect(powerUpType, (int) weaponEffectCode);
				Debug.Log ("Weapon player effect "+ weaponEffectCode.ToString());
				break;
			}
			Die ();
		}
	}

	private void Die()
	{
		Destroy (gameObject);
	}
	
}
 