/* 
 * Copyright (C) Luis Galotti Muñoz (Gino) <ginogalotti@gmail.com>
 * Assets are derived from Unity's Survival tutorial project
 * May 2015
 * 
 * Code explanation and Gameplay decisitions covered in EnhancementScriptLoader.cs's header. Please, take a look
 */

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MessagesManager : MonoBehaviour {

	//Game objects
	public Text messagesText;
	public static MessagesManager instance = null;

	//Gameplay variables
	public const string DEFAULT_ENEMY_LEVEL_UP_MESSAGE = "Your nightmares are getting stronger...";
	public const string DEFAULT_POWER_UP_MESSAGE = "Look for radiation powers!";

	//Game variables
	private string messageToShow;
	private float remainingDuration;
	private const float TIME_OF_MESSAGE_IN_SEC = 2f;

	void Start () {
		doSingletonMagic ();
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
		if (GameStatusManager.instance.isPlayingStatus ()) 
		{
			if (remainingDuration > 0) {
				remainingDuration -= Time.deltaTime;
				messagesText.text = messageToShow;
				messagesText.enabled = true;
			} else {
				if (messagesText.enabled)
					messagesText.enabled = false;
			}
		}
	}

	public void showMessage(string message)
	{
		messageToShow = message;
		remainingDuration = TIME_OF_MESSAGE_IN_SEC;
	}
}
