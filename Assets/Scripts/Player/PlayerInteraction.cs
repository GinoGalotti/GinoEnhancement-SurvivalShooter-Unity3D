/* 
 * Copyright (C) Luis Galotti Muñoz (Gino) <ginogalotti@gmail.com>
 * Assets are derived from Unity's Survival tutorial project
 * May 2015
 * 
 * Code explanation and Gameplay decisitions covered in EnhancementScriptLoader.cs's header. Please, take a look
 */

using UnityEngine;
using System.Collections;

public class PlayerInteraction : MonoBehaviour {
	
	void FixedUpdate(){
		if (GameStatusManager.isPlayingStatus())
	    {
			if (LevelUpManager.instance.isStillPendingLevelUp()) {
				checkIfPlayerPickUpgrade ();
			}



		}
	}

	private void checkIfPlayerPickUpgrade()
	{
		//TODO Add touch support
		if (Input.GetKeyDown(KeyCode.Alpha1)){
			LevelUpManager.instance.choseUpgrade((int)LevelUpManager.UPGRADE_CHOICES.SPEED);
		} else if (Input.GetKeyDown(KeyCode.Alpha2)) {
			LevelUpManager.instance.choseUpgrade((int)LevelUpManager.UPGRADE_CHOICES.HEALTH);
		} else if (Input.GetKeyDown(KeyCode.Alpha3)){
			LevelUpManager.instance.choseUpgrade((int)LevelUpManager.UPGRADE_CHOICES.DMG);
		}else if (Input.GetKeyDown(KeyCode.Alpha4)){
			LevelUpManager.instance.choseUpgrade((int)LevelUpManager.UPGRADE_CHOICES.LUCK);
		}else if (Input.GetKeyDown(KeyCode.Alpha5)){
			LevelUpManager.instance.choseUpgrade((int)LevelUpManager.UPGRADE_CHOICES.STATUS);
		}
	}


}
