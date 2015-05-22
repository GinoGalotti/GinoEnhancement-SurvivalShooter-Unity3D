/* 
 * Copyright (C) Luis Galotti Muñoz (Gino) <ginogalotti@gmail.com>
 * Assets are derived from Unity's Survival tutorial project
 * May 2015
 * 
 * Code explanation and Gameplay decisitions covered in EnhancementScriptLoader.cs's header. Please, take a look
 */

using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour
{
	//Gameplay constants
    private const float TIME_BETWEEN_ATTACKS_IN_SECS = 0.5f;
    
	//Game Objects
	private GameObject player;
	private PlayerHealth playerHealth;
	private EnemyHealth enemyHealth;

	//Gameplay variables
	public int attackDamage = 10;

	private bool playerInRange;
	private float timer;
	private bool isPoisoner;
	private bool isFreezer;

    void Awake ()
    {
        player = GameObject.FindGameObjectWithTag ("Player");
        playerHealth = player.GetComponent <PlayerHealth> ();
        enemyHealth = GetComponent<EnemyHealth>();
    }
	
    void OnTriggerEnter (Collider other)
    {
        if(other.gameObject == player)
        {
            playerInRange = true;
        }
    }


    void OnTriggerExit (Collider other)
    {
        if(other.gameObject == player)
        {
            playerInRange = false;
        }
    }

    void Update ()
    {
		if (GameStatusManager.instance.getStatus() == GameStatusManager.GAME_STATUS.PLAYING) {
			timer += Time.deltaTime;
		
			if (playerInRange && enemyHealth.isAlive() && timer >= TIME_BETWEEN_ATTACKS_IN_SECS ) {
				Attack ();
			}
		}
	}
	
    void Attack ()
    {
        timer = 0f;

		triggerMutationEffects ();

		playerHealth.TakeDamage (attackDamage);

    }

	private void triggerMutationEffects()
	{
		if (isPoisoner) 
		{
			StatusManager.instance.addDuration(StatusData.GAME_MODIFICATION_TYPE.PLAYER_HARMFUL_STATUS, (int) StatusData.PLAYER_HARMFUL_STATUS_CODE.POISON, StatusData.PLAYER_DEFAULT_POISON_DURATION_IN_SECS);
		}
		
		if (isFreezer) 
		{
			StatusManager.instance.addDuration(StatusData.GAME_MODIFICATION_TYPE.PLAYER_HARMFUL_STATUS, (int) StatusData.PLAYER_HARMFUL_STATUS_CODE.SLOWED, StatusData.PLAYER_DEFAULT_SLOWED_DURATION_IN_SECS);
		}
	}

	public void increaseDmg (float delta)
	{
		attackDamage = (int) (attackDamage * delta);
	}

	public void makeEnemyPoisoner()
	{
		isPoisoner = true;
	}

	public void makeEnemyFreezer()
	{
		isFreezer = true;
	}

}
