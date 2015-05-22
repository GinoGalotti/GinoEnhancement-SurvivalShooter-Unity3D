/* 
 * Copyright (C) Luis Galotti Muñoz (Gino) <ginogalotti@gmail.com>
 * Assets are derived from Unity's Survival tutorial project
 * May 2015
 * 
 * Code explanation and Gameplay decisitions covered in EnhancementScriptLoader.cs's header. Please, take a look
 */

using UnityEngine;
using System.Collections;
using System;
using System.Text;
using UnityEngine.UI;

public class StatusManager : MonoBehaviour {

	//Game objects
	public Text currentStatusText;
	public static StatusManager instance = null;
	
	private Animator anim;

	//Game variables
	private StringBuilder statusToShow = new StringBuilder("");
	private bool isAnyStatusActive = false;

	void Start () {
		doSingletonMagic ();
		anim = GetComponent<Animator>();
	}

	private void doSingletonMagic()
	{
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);
	}
	
	void Update()
	{
		if (isAnyStatusActive) 
		{
			isAnyStatusActive = false;
			statusToShow.Remove(0,statusToShow.Length);
			addEveryWeaponEffectActiveToText();
			decreaseEveryplayerBeneficialStatusAndCalculateCurrentStatusText(Time.deltaTime);
			decreaseEveryplayerHarmfulStatusAndCalculateCurrentStatusText(Time.deltaTime);
			decreaseEnemyStatus(Time.deltaTime);
			if (isAnyStatusActive) 
			{
				showCurrentStatusMessage();
			} else
			{
				stopShowingCurrentStatusMessage();
			}
		}
	}

	private void addEveryWeaponEffectActiveToText()
	{
		foreach (Status weaponEffect in StatusData.currentWeaponEffects) 
		{
			if (weaponEffect.isActive())
			{
				isAnyStatusActive = true;
				addWeaponEffectToText(weaponEffect.getName(), weaponEffect.getStringIntegerDuration());
			}
		}
	}

	private void addWeaponEffectToText (string statusName, string ammo)
	{
		statusToShow.Append (statusName).Append (" ").Append (ammo).Append (" bullets!\n");
	}
	
	private void decreaseEveryplayerBeneficialStatusAndCalculateCurrentStatusText(float deltaTime)
	{
		foreach (Status playerStatus in StatusData.currentPlayerBeneficialStatus) 
		{
			if (playerStatus.isActive())
			{
				playerStatus.decreaseDuration(deltaTime);
				isAnyStatusActive = true;
				addPlayerStatusToText(playerStatus.getName(), playerStatus.getString2DecimalsDuration());
			}
		}
	}

	private void decreaseEveryplayerHarmfulStatusAndCalculateCurrentStatusText(float deltaTime)
	{
		foreach (Status playerStatus in StatusData.currentPlayerHarmfulStatus) 
		{
			if (playerStatus.isActive())
			{
				playerStatus.decreaseDuration(deltaTime);
				isAnyStatusActive = true;
				addPlayerStatusToText(playerStatus.getName(), playerStatus.getString2DecimalsDuration());
			}
		}
	}
	
	private void decreaseEnemyStatus(float deltaTime)
	{
		foreach (Status enemyStatus in StatusData.currentEnemyStatus) 
		{
			if (enemyStatus.isActive()){
				enemyStatus.decreaseDuration(deltaTime);
				isAnyStatusActive = true;
				addPlayerStatusToText(enemyStatus.getName(), enemyStatus.getString2DecimalsDuration());
			}
		}
	}

	private void addPlayerStatusToText(string statusName, string duration)
	{
		statusToShow.Append (statusName).Append(" ").Append(duration).Append("s!\n");
	}

	public void decreaseWeaponEffect(int decreaseValue)
	{
		foreach (Status weaponEffect in StatusData.currentWeaponEffects) 
		{
			if (weaponEffect.isActive())
			{
				weaponEffect.decreaseDuration((float) decreaseValue);
			}
		}
	}

	private void showCurrentStatusMessage()
	{
		currentStatusText.text = statusToShow.ToString();
		anim.SetBool ("StatusTextActive", true);
	}

	private void stopShowingCurrentStatusMessage()
	{
		anim.SetBool ("StatusTextActive", false);
	}

	public void addDuration(StatusData.GAME_MODIFICATION_TYPE statusType, int statusIndex, float increaseDuration)
	{
		switch (statusType) 
		{
		case StatusData.GAME_MODIFICATION_TYPE.PLAYER_BENEFICIAL_STATUS:
			StatusData.currentPlayerBeneficialStatus[statusIndex].addDuration (increaseDuration);
			break;
		case StatusData.GAME_MODIFICATION_TYPE.PLAYER_HARMFUL_STATUS:
			StatusData.currentPlayerHarmfulStatus[statusIndex].addDuration (increaseDuration);
			break;
		case StatusData.GAME_MODIFICATION_TYPE.WEAPON_STATUS:
			StatusData.currentWeaponEffects [statusIndex].addDuration (increaseDuration);
			break;
		case StatusData.GAME_MODIFICATION_TYPE.TRIGGER:
			if (statusIndex == (int)StatusData.TRIGGER_EFFECT_CODE.FREEZE_ENEMIES)
				StatusData.currentEnemyStatus[(int)StatusData.ENEMIES_STATUS_CODE.FREEZE].addDuration(increaseDuration);
			break;
		}	
		isAnyStatusActive = true;
	}

	public bool isPlayerBeneficialStatusActive (StatusData.PLAYER_BENEFICIAL_STATUS_CODE statusCode)
	{
		return StatusData.currentPlayerBeneficialStatus [(int)statusCode].isActive ();
	}

	public bool isPlayerHarmfulStatusActive (StatusData.PLAYER_HARMFUL_STATUS_CODE statusCode)
	{
		return StatusData.currentPlayerHarmfulStatus [(int)statusCode].isActive ();
	}

	public bool isEnemyStatusActive (StatusData.ENEMIES_STATUS_CODE statusCode)
	{
		return StatusData.currentEnemyStatus [(int)statusCode].isActive ();
	}

	public bool isWeaponEffectActive (StatusData.WEAPON_EFFECT_CODE effectCode)
	{
		return StatusData.currentWeaponEffects [(int)effectCode].isActive ();
	}

}
