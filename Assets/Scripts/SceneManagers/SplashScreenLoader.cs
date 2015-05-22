/* 
 * Copyright (C) Luis Galotti Muñoz (Gino) <ginogalotti@gmail.com>
 * Assets are derived from Unity's Survival tutorial project
 * May 2015
 * 
 * Code explanation and Gameplay decisitions covered in EnhancementScriptLoader.cs's header. Please, take a look
 */

using UnityEngine;
using System.Collections;

public class SplashScreenLoader : MonoBehaviour {
	
	private const float WAIT_ONE_SECOND = 1f;
	
	void Awake () 
	{
		Invoke ("loadEnhancementLevel", WAIT_ONE_SECOND);
	}
	
	private void loadEnhancementLevel()
	{
		Application.LoadLevel("Enhancement_Level");
	}
	
}