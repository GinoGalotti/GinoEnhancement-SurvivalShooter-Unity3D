/* 
 * Copyright (C) Luis Galotti Muñoz (Gino) <ginogalotti@gmail.com>
 * Assets are derived from Unity's Survival tutorial project
 * May 2015
 * 
 * Code explanation and Gameplay decisitions covered in EnhancementScriptLoader.cs's header. Please, take a look
 */

using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
	//Game Objects
    private Transform player;
	private EnemyHealth enemyHealth;
	private NavMeshAgent nav;
	private Animator anim;
	private Transform randomPointForInvisible;

    void Awake ()
    {
        player = GameObject.FindGameObjectWithTag ("Player").transform;
        enemyHealth = GetComponent <EnemyHealth> ();
        nav = GetComponent <NavMeshAgent> ();
		anim = GetComponent <Animator> ();
    }

    void Update ()
    {
		if(enemyHealth.isAlive() && GameStatusManager.isPlayingStatus() && !isEnemyFrozen())
        {
			if (playerVisible()) 
			{
				nav.enabled = true;
 				nav.SetDestination (player.position);
			} 
        }
        else
        {
			anim.SetBool("GoIdle", true);
            nav.enabled = false;
        }
    }

	private bool playerVisible()
	{
		return !StatusManager.instance.isPlayerBeneficialStatusActive (StatusData.PLAYER_BENEFICIAL_STATUS_CODE.INVISIBLE);
	}

	private bool isEnemyFrozen()
	{
		return StatusManager.instance.isEnemyStatusActive (StatusData.ENEMIES_STATUS_CODE.FREEZE);
	}

	public void increaseSpeed(float delta)
	{
		nav.speed = nav.speed * delta;
	}
}
