/* 
 * Copyright (C) Luis Galotti Muñoz (Gino) <ginogalotti@gmail.com>
 * Assets are derived from Unity's Survival tutorial project
 * May 2015
 * 
 * Code explanation and Gameplay decisitions covered in EnhancementScriptLoader.cs's header. Please, take a look
 */
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
	//Game objects
	private Ray shootRay;
	private RaycastHit shootHit;
	private ParticleSystem gunParticles;
	private LineRenderer gunLine;
	private AudioSource gunAudio;
	private int shootableMask;
	private Light gunLight;
	private PlayerHealth playerHealthScript;
	
	//Gameplay variables
	public float startingShootingCadency = 0.15f;
	public int startingDamagePerShot = 20;
	
    public float range = 100f;
	private const float EFFECTS_DISPLAY_TIME = 0.2f;

	//Game variables
    private float timer;
	private float shootingCadency;
	private int damagePerShot;
	private int hitsInARow = 0;

    void Awake ()
    {
        shootableMask = LayerMask.GetMask ("Shootable");
        gunParticles = GetComponent<ParticleSystem> ();
        gunLine = GetComponent <LineRenderer> ();
        gunAudio = GetComponent<AudioSource> ();
        gunLight = GetComponent<Light> ();
		playerHealthScript = FindObjectOfType<PlayerHealth> ();
		shootingCadency = startingShootingCadency;
		damagePerShot = startingDamagePerShot;
    }

	void Update ()
    {
        timer += Time.deltaTime;

		if(playerPressShot() && playerCanShot())
        {
            Shoot ();
        }

        if(timer >= shootingCadency * EFFECTS_DISPLAY_TIME)
        {
            DisableEffects ();
        }
    }

	private bool playerPressShot()
	{
		return Input.GetButton ("Fire1");
	}

	private bool playerCanShot()
	{
		float currentShotingCadency = isIncreasedCadencyActive () ? shootingCadency / StatusData.REDUCE_CADENCY_FACTOR : shootingCadency;
		return timer >= currentShotingCadency && Time.timeScale != 0;
	}

	private bool isIncreasedCadencyActive()
	{
		return StatusManager.instance.isWeaponEffectActive(StatusData.WEAPON_EFFECT_CODE.INCREASED_CADENCY);
	}

    public void DisableEffects ()
    {
        gunLine.enabled = false;
        gunLight.enabled = false;
    }
	
    void Shoot ()
    {
        timer = 0f;

		activateShootingEffects ();

		StatusManager.instance.decreaseWeaponEffect (1);

        if(Physics.Raycast (shootRay, out shootHit, range, shootableMask))
        {
            EnemyHealth enemyHealth = shootHit.collider.GetComponent <EnemyHealth> ();
            if(enemyHealth != null)
            {
				int currentDamagePerShot = isWeaponMoreDMGStatus() ?(int) (damagePerShot * StatusData.WEAPON_MORE_DMG_FACTOR) : damagePerShot;
				enemyHealth.TakeDamage (currentDamagePerShot, shootHit.point);
				hitsInARow++;
				if (isLifeStealAmmo())
					healPlayer();
				if (hitsInARow >= ComboManager.STARTING_COMB0_COUNT)
					ComboManager.instance.calculateComboRangeAndShowMessage(hitsInARow);
            } else {
				hitsInARow = 0;
				if (ComboManager.instance.isShowingCombo())
					ComboManager.instance.stopShowing();
			}
            gunLine.SetPosition (1, shootHit.point);
        }
        else
        {
            gunLine.SetPosition (1, shootRay.origin + shootRay.direction * range);
        }
    }

	private void activateShootingEffects()
	{
		gunAudio.Play ();
		
		gunLight.enabled = true;
		
		gunParticles.Stop ();
		gunParticles.Play ();
		
		gunLine.enabled = true;
		gunLine.SetPosition (0, transform.position);
		
		shootRay.origin = transform.position;
		shootRay.direction = transform.forward;
	}

	private bool isWeaponMoreDMGStatus()
	{
		return StatusManager.instance.isWeaponEffectActive(StatusData.WEAPON_EFFECT_CODE.EXTRA_DAMAGE);
	}

	private bool isLifeStealAmmo()
	{
		return StatusManager.instance.isWeaponEffectActive(StatusData.WEAPON_EFFECT_CODE.LIFE_STEAL_SHOTS);
	}

	private void healPlayer()
	{
		playerHealthScript.healPlayer (StatusData.LIFE_STEAL_PER_SHOT);
	}

	public void LevelUpDamage(float delta)
	{
		damagePerShot = (int)(damagePerShot * delta);
	}

	public void LevelUpCadency(float delta)
	{
		shootingCadency = shootingCadency * delta;
	}

}
