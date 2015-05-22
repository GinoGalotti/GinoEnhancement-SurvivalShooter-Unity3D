/* 
 * Copyright (C) Luis Galotti Muñoz (Gino) <ginogalotti@gmail.com>
 * Assets are derived from Unity's Survival tutorial project
 * May 2015
 * 
 * Code explanation and Gameplay decisitions covered in EnhancementScriptLoader.cs's header. Please, take a look
 */

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
	//Game Objects
	public Slider healthSlider;
	public Text healthText;
	public Image damageImage;
	public AudioClip deathClip;

	private Animator anim;
	private AudioSource playerAudio;
	private PlayerMovement playerMovement;
	private PlayerShooting playerShooting;

	//Gameplay constants
	private const int STARTING_HEALTH = 100;
	private const float FLASH_SPEED = 5f;
	private Color FLASH_COLOR = new Color(1f, 0f, 0f, 0.1f);
	
	//Gameplay variables
    private int currentHealth;
	private int maximumHealth;
	private bool playerDead;
	private bool damaged;

	private float timeBetweenPoisonDamage; 

	//Cleaness constants
	private const bool HAS_BEEN_UPGRADED = true;
	private const bool NOT_AN_UPGRADE = false;

    void Awake ()
    {
        anim = GetComponent <Animator> ();
        playerAudio = GetComponent <AudioSource> ();
        playerMovement = GetComponent <PlayerMovement> ();
        playerShooting = GetComponentInChildren <PlayerShooting> ();
		currentHealth = STARTING_HEALTH;
		maximumHealth = STARTING_HEALTH;
		timeBetweenPoisonDamage = StatusData.PLAYER_POISON_TIME_TO_TICK_IN_SECS;
		playerDead = false;
    }


    void Update ()
    {
		if (needToUpdateDamageImage())
		{
			beingHitImage();
		}
		if (isPlayerPoisoned ()) 
		{

			takePoisonDamage();
		}
	}

	private bool needToUpdateDamageImage()
	{
		return (damageImage.color != Color.clear) || damaged;
	}

	private void beingHitImage()
	{
		if(damaged)
		{
			damageImage.color = FLASH_COLOR;
		}
		else
		{
			damageImage.color = Color.Lerp (damageImage.color, Color.clear, FLASH_SPEED * Time.deltaTime);
		}
		damaged = false;
	}
	
	private bool isPlayerPoisoned()
	{
		return StatusManager.instance.isPlayerHarmfulStatusActive (StatusData.PLAYER_HARMFUL_STATUS_CODE.POISON);
	}

	private void takePoisonDamage()
	{
		timeBetweenPoisonDamage -= Time.deltaTime;
		if (timeBetweenPoisonDamage <= 0)
		{
			timeBetweenPoisonDamage = StatusData.PLAYER_POISON_TIME_TO_TICK_IN_SECS;
			TakeDamage(StatusData.PLAYER_POISON_HIT_DMG);
		}

	}

    public void TakeDamage (int amount)
    {
		if (notInmortal()) 
		{
			damaged = true;
			
			currentHealth -= amount;
			
			if (currentHealth < 0)
				currentHealth = 0;
			
			updateHealthHUD (NOT_AN_UPGRADE);
			
			playerAudio.Play ();
			
			if(!playerDead && currentHealth <= 0)
			{
				Death ();
			}
		}
	}

	public void healPlayer (int amount)
	{
		currentHealth += amount;
		if (currentHealth > maximumHealth)
			currentHealth = maximumHealth;
		updateHealthHUD (false);
	}

	private bool notInmortal()
	{ 
		return !StatusManager.instance.isPlayerBeneficialStatusActive (StatusData.PLAYER_BENEFICIAL_STATUS_CODE.INMMORTAL);
	}

    void Death ()
    {
		playerDead = true;

        playerShooting.DisableEffects ();

        anim.SetTrigger ("Die");

        playerAudio.clip = deathClip;
        playerAudio.Play ();

        playerMovement.enabled = false;
        playerShooting.enabled = false;

		GameStatusManager.instance.playerDied ();
    }

	public void LevelUpHeal(float delta)
	{
		maximumHealth = (int)(maximumHealth * delta);
		currentHealth = maximumHealth;
	
		updateHealthHUD (HAS_BEEN_UPGRADED);
	}

	private void updateHealthHUD(bool isAnUpgrade)
	{
		if (isAnUpgrade) 
		{
			healthSlider.maxValue = maximumHealth;
		}
		healthSlider.value = currentHealth;

		healthText.text = currentHealth + "/" + maximumHealth;
	}

}
