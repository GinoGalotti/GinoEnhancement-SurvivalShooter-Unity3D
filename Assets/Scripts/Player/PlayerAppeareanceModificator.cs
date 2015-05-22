/* 
 * Copyright (C) Luis Galotti Muñoz (Gino) <ginogalotti@gmail.com>
 * Assets are derived from Unity's Survival tutorial project
 * May 2015
 * 
 * Code explanation and Gameplay decisitions covered in EnhancementScriptLoader.cs's header. Please, take a look
 */

using UnityEngine;
using System.Collections;

public class PlayerAppeareanceModificator : MonoBehaviour {

	//Game Objects
	private Material playerRendererMaterial;

	//Color masks
	public static readonly Color FROZEN_COLOR_MASK = new Color (35f / 255f, 175f / 255f, 255f / 255f);
	public static readonly Color POISONED_COLOR_MASK = new Color (100f / 255f, 255f / 255f, 100f / 255f);

	void Start () {
		GameObject playerBody = GameObject.Find ("PlayerBody");
		playerRendererMaterial = playerBody.GetComponent<SkinnedMeshRenderer> ().material;;
	}
	
	void Update () {
		applyColorMask ();
	}

	private void applyColorMask()
	{
		
		if (!isPlayerFrozen () && !isPlayerPoisoned ()) {
			playerRendererMaterial.color = Color.white;
		} else {
			if (isPlayerFrozen ()) {
				playerRendererMaterial.color *= FROZEN_COLOR_MASK;
			} 
			if (isPlayerPoisoned ()) {
				playerRendererMaterial.color *= POISONED_COLOR_MASK;
			} 
		}
		
		//Seems imposible to set the material transparent right now in Unity
		//		if (isPlayerInvisible ()) {
		//			playerRendererMaterial.SetFloat("_Mode", 3.0f);
		//		} else {
		//			playerRendererMaterial.SetFloat("_Mode", 0.0f);
		//		}
	}

	private bool isPlayerPoisoned()
	{
		return StatusManager.instance.isPlayerHarmfulStatusActive (StatusData.PLAYER_HARMFUL_STATUS_CODE.POISON);
	}
	
	private bool isPlayerFrozen()
	{
		return StatusManager.instance.isPlayerHarmfulStatusActive (StatusData.PLAYER_HARMFUL_STATUS_CODE.SLOWED);
	}
	
	private bool isPlayerInvisible()
	{
		return StatusManager.instance.isPlayerBeneficialStatusActive (StatusData.PLAYER_BENEFICIAL_STATUS_CODE.INVISIBLE);
	}
}
