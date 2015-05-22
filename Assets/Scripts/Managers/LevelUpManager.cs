/* 
 * Copyright (C) Luis Galotti Muñoz (Gino) <ginogalotti@gmail.com>
 * Assets are derived from Unity's Survival tutorial project
 * May 2015
 * 
 * Code explanation and Gameplay decisitions covered in EnhancementScriptLoader.cs's header. Please, take a look
 */

using UnityEngine;
using System.Collections;

public class LevelUpManager : MonoBehaviour {

	//Game objects
	public PlayerHealth playerHealth;
	public PlayerShooting playerShooting;
	public PlayerMovement playerMovement;
	public GameObject levelUpPanel;
	public static LevelUpManager instance = null;

	//Gameplay variables
	public int firstLevelDefaultXP = 100;
	public const float XP_NEEDED_INCREMENT = 1.75f;
	public int currentLevel = 1;
	
	private const float UPGRADE_HEALTH_FACTOR = 1.25f;
	private const float UPGRADE_SPEED_FACTOR = 1.10f;
	private const float UPGRADE_DAMAGE_FACTOR = 1.25f;
	private const float UPGRADE_LUCK_FACTOR = 1.20f;
	private const float UPGRADE_STATUS_DURATION_FACTOR = 1.15f;

	private int nextLevel = 100;
	private int pendingLevelUpUpgrades = 0;

	//String constants
	public enum UPGRADE_CHOICES
	{
		SPEED, HEALTH, DMG, LUCK, STATUS, NUMBER_OF_CHOICES
	};
	
	void Awake () {
		doSingletonMagic ();
		nextLevel = firstLevelDefaultXP;
	}

	private void doSingletonMagic()
	{
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);
	}
	
	public void levelUp()
	{
		pendingLevelUpUpgrades++;
		showLevelUp ();
		currentLevel++;
		nextLevel = (int) (nextLevel  * XP_NEEDED_INCREMENT);
	}

	public int getNextXPNeeded()
	{
		return nextLevel;
	}

	public void choseUpgrade(int upgradeIndex)
	{
		Debug.Log ("Im being clicked");
		UPGRADE_CHOICES upgrade = (UPGRADE_CHOICES)upgradeIndex;
		if (isStillPendingLevelUp()) {
			pendingLevelUpUpgrades--;
			hideLevelUpPanelWhenNeeded();
			switch (upgrade)
			{
			case UPGRADE_CHOICES.HEALTH:
				playerHealth.LevelUpHeal(UPGRADE_HEALTH_FACTOR);
				break;
			case UPGRADE_CHOICES.SPEED:
				playerMovement.LevelUpSpeed(UPGRADE_SPEED_FACTOR);
				break;
			case UPGRADE_CHOICES.DMG:
				playerShooting.LevelUpDamage(UPGRADE_DAMAGE_FACTOR);
				break;
			case UPGRADE_CHOICES.LUCK:
				PowerUpManager.instance.LevelUpLuck(UPGRADE_LUCK_FACTOR);
				break;
			case UPGRADE_CHOICES.STATUS:
				PowerUpManager.instance.levelUpEffectDuration(UPGRADE_STATUS_DURATION_FACTOR);
				break;
			}
		}
	}

	public bool isStillPendingLevelUp()
	{
		return pendingLevelUpUpgrades >0;
	}

	public void powerUpTriggerActivated()
	{
		pendingLevelUpUpgrades++;
		showLevelUp ();
		currentLevel++;
	}

	private void showLevelUp()
	{
		levelUpPanel.SetActive(true);
	}

	private void hideLevelUpPanelWhenNeeded()
	{
		if (pendingLevelUpUpgrades == 0)
			levelUpPanel.SetActive (false);
	}

}
