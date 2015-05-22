/* 
 * Copyright (C) Luis Galotti Muñoz (Gino) <ginogalotti@gmail.com>
 * Assets are derived from Unity's Survival tutorial project
 * May 2015
 * 
 * Code explanation and Gameplay decisitions covered in EnhancementScriptLoader.cs's header. Please, take a look
 */

using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
	//Game objects
	public Transform target;            
	public float smoothing = 5f;        
	
	private Vector3 offset;                     
	
	void Start ()
	{
		offset = transform.position - target.position;
	}
	
	void FixedUpdate ()
	{
		Vector3 targetCamPos = target.position + offset;
		
		transform.position = Vector3.Lerp (transform.position, targetCamPos, smoothing * Time.deltaTime);
	}
}