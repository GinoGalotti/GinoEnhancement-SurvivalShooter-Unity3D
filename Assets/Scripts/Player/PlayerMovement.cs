/* 
 * Copyright (C) Luis Galotti Muñoz (Gino) <ginogalotti@gmail.com>
 * Assets are derived from Unity's Survival tutorial project
 * May 2015
 * 
 * Code explanation and Gameplay decisitions covered in EnhancementScriptLoader.cs's header. Please, take a look
 */

using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	//Game objects
	private Vector3 movement;
	private Animator animator;
	private Rigidbody playerRigidBody;
	private int floorMask;

	//Gameplay variables
	private const float PLAYER_INITIAL_SPEED = 6f;
	private float playerSpeed;
	private float camRayLenght = 100f;


	void Awake(){
		floorMask = LayerMask.GetMask ("Floor");
		animator = GetComponent<Animator> ();
		playerRigidBody = GetComponent<Rigidbody> ();
		playerSpeed = PLAYER_INITIAL_SPEED;
	}

	void FixedUpdate(){
		float h = Input.GetAxisRaw ("Horizontal");
		float v = Input.GetAxisRaw ("Vertical");

		Move (h, v);
		Turning ();
		Animating (h, v);

	}

	void Move (float h, float v) {
		movement.Set (h, 0f, v);
		float currentSpeed = isPlayerSlowed () ? playerSpeed * StatusData.PLAYER_SLOWED_FACTOR : playerSpeed;

		movement = movement.normalized * currentSpeed * Time.deltaTime;

		playerRigidBody.MovePosition (transform.position + movement);
	}

	private bool isPlayerSlowed()
	{
		return StatusManager.instance.isPlayerHarmfulStatusActive (StatusData.PLAYER_HARMFUL_STATUS_CODE.SLOWED);
	}
	
	void Turning() {
		Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);

		RaycastHit floorHit;

		if (Physics.Raycast (camRay, out floorHit, camRayLenght, floorMask)){		    

			Vector3 playerToMouse = floorHit.point - transform.position;
			playerToMouse.y = 0;

			Quaternion newRotation = Quaternion.LookRotation (playerToMouse);
			playerRigidBody.MoveRotation(newRotation);
		}
	}

	void Animating (float h, float v){
		bool walking = h != 0f || v != 0f;
		animator.SetBool ("IsWalking", walking);
	}

	public void LevelUpSpeed (float delta)
	{
		playerSpeed *= delta;
	}

}
