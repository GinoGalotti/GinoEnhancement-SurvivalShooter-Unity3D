/* 
 * Copyright (C) Luis Galotti Muñoz (Gino) <ginogalotti@gmail.com>
 * Assets are derived from Unity's Survival tutorial project
 * May 2015
 * 
 * Code explanation and Gameplay decisitions covered in EnhancementScriptLoader.cs's header. Please, take a look
 */

using UnityEngine;
using System.Collections;

public class GameStatusManager : MonoBehaviour {

	//Game Objects
	public static GameStatusManager instance = null;

	private Animator anim;
	
	//Game variables
	public enum GAME_STATUS {PLAYING, PLAYER_DEAD}
	public const float RESTART_DELAY = 6f;

	private GAME_STATUS currentStatus;
	private float restartTimer;
	
	void Start () {
		doSingletonMagic ();
		anim = GetComponent<Animator> ();
		currentStatus = GAME_STATUS.PLAYING;
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
		if (currentStatus == GAME_STATUS.PLAYER_DEAD) 
		{
			restartTimer += Time.deltaTime;
			
			if (restartTimer >= RESTART_DELAY)
			{
				restartLevel();
			}
		}
	}

	public GAME_STATUS getStatus()
	{
		return currentStatus;
	}

	public void playerDied()
	{
		currentStatus = GAME_STATUS.PLAYER_DEAD;
		anim.SetTrigger ("GameOver");
	}

	public void restartLevel()
	{
		Application.LoadLevel (Application.loadedLevel);
	}

	public bool isPlayingStatus()
	{
		return currentStatus == GAME_STATUS.PLAYING;
	}

}
