/* 
 * Copyright (C) Luis Galotti Muñoz (Gino) <ginogalotti@gmail.com>
 * Assets are derived from Unity's Survival tutorial project
 * May 2015
 * 
 * Code explanation and Gameplay decisitions covered in EnhancementScriptLoader.cs's header. Please, take a look
 */

using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {
	//Game Objects
	public AudioClip deathClip;
	
	private Animator anim;
	private AudioSource enemyAudio;
	private ParticleSystem hitParticles;
	private CapsuleCollider capsuleCollider;
	private EnemyMutator enemyMutatorScript;
	//private Renderer enemyRenderer;
	
	//Gameplay variables
    public int startingHealth = 100;
    public float sinkSpeed = 2.5f;
    public int scoreValue = 10;

	//Game Variables
	private int currentHealth;
	private bool living = true;
	private bool sinking;
	private bool isRageEnemy;

	//Gameplay varaibles


    void Awake ()
    {
        anim = GetComponent <Animator> ();
        enemyAudio = GetComponent <AudioSource> ();
        hitParticles = GetComponentInChildren <ParticleSystem> ();
        capsuleCollider = GetComponent <CapsuleCollider> ();
		enemyMutatorScript = GetComponent<EnemyMutator> ();
		//enemyRenderer = GetComponent<Renderer> ();
		currentHealth = startingHealth;
    }

    void Update ()
    {
        if(sinking)
        {
            transform.Translate (-Vector3.up * sinkSpeed * Time.deltaTime);
        }
    }

    public void TakeDamage (int amount, Vector3 hitPoint)
    {
		if (living) {

			if (isRageEnemy)
				enemyMutatorScript.ragerBeingHit();
			enemyAudio.Play ();
			
			currentHealth -= amount;
			
			hitParticles.transform.position = hitPoint;
			hitParticles.Play();
			
			if(currentHealth <= 0)
			{
				Death ();
			}
		}		
	}
	
	void Death ()
	{
		living = false;

        capsuleCollider.isTrigger = true;

        anim.SetTrigger ("Dead");

        enemyAudio.clip = deathClip;
        enemyAudio.Play ();
    }
	
    public void StartSinking ()
    {
        GetComponent <NavMeshAgent> ().enabled = false;
        GetComponent <Rigidbody> ().isKinematic = true;
        sinking = true;
		ScoreManager.instance.addScore (scoreValue);
        Destroy (gameObject, 2f);
    }

	public bool isAlive()
	{
		return living;
	}

	public void makeEnemyRager()
	{
		isRageEnemy = true;
	}

	public void increaseEnemyHealth(float delta)
	{
		currentHealth =(int) (startingHealth * delta);
	}

	public void increaseEnemyScore(float delta)
	{
		scoreValue = (int) (scoreValue * delta);
	}

}
