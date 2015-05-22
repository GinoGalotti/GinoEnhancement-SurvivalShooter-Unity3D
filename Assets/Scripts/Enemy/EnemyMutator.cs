/* 
 * Copyright (C) Luis Galotti Muñoz (Gino) <ginogalotti@gmail.com>
 * Assets are derived from Unity's Survival tutorial project
 * May 2015
 * 
 * Code explanation and Gameplay decisitions covered in EnhancementScriptLoader.cs's header. Please, take a look
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyMutator : MonoBehaviour {

	//Game Objects
	private EnemyAttack enemyAttackScript;
	private EnemyMovement enemyMovementScript;
	private EnemyHealth enemyHealthScript;

	private bool[] activeMutations = new bool[(int)EnemyMutationData.ENEMY_MUTATION_CODE.NUMBER_OF_MUTATION];
	private float scoreMultiplicatorFactor = 1f;

	void Awake ()
	{
		enemyAttackScript = GetComponent<EnemyAttack> ();
		enemyMovementScript = GetComponent<EnemyMovement> ();
		enemyHealthScript = GetComponent<EnemyHealth> ();
		mutateEnemy ();
		applyColorMask ();
	}

	private void mutateEnemy()
	{
		generateMovementMutation ();
		enemyLevelUp ();
		enemyHealthScript.increaseEnemyScore (scoreMultiplicatorFactor);
	}

	private void generateMovementMutation ()
	{
		for (int index = 0; index <activeMutations.Length; index++) 
		{
			float randomIndex = Random.value;
			if (randomIndex < EnemyMutationData.posibleMutations[index].mutationChance)
			{
				activeMutations[index] = true;
				scoreMultiplicatorFactor *= EnemyMutationData.posibleMutations[index].scoreIncrementFactor;
				applyMutation((EnemyMutationData.ENEMY_MUTATION_CODE) index);
			}
		}

	}

	private void enemyLevelUp()
	{
		int enemyLevel = EnemyLevelManager.instance.getEnemyLevel ();
		enemyHealthScript.increaseEnemyHealth (Mathf.Pow (EnemyLevelManager.ENEMY_HEALTH_INCREASE_PER_LEVEL, enemyLevel));
		enemyAttackScript.increaseDmg (Mathf.Pow(EnemyLevelManager.ENEMY_DAMAGE_INCREASE_PER_LEVEL, enemyLevel));
		scoreMultiplicatorFactor *= Mathf.Pow (EnemyLevelManager.ENEMY_SCORE_INCREASE_PER_LEVEL, enemyLevel);
	}

	private void applyMutation(EnemyMutationData.ENEMY_MUTATION_CODE mutationCode)
	{
		switch (mutationCode) 
		{
		case EnemyMutationData.ENEMY_MUTATION_CODE.FAST:
			enemyMovementScript.increaseSpeed(EnemyMutationData.ENEMY_FAST_FACTOR);
			break;
		case EnemyMutationData.ENEMY_MUTATION_CODE.INCREASE_HEALTH:
			enemyHealthScript.increaseEnemyHealth(EnemyMutationData.ENEMY_INCREASE_HEALTH_FACTOR);
			break;
		case EnemyMutationData.ENEMY_MUTATION_CODE.FREEZER:
			enemyAttackScript.makeEnemyFreezer();
			break;
		case EnemyMutationData.ENEMY_MUTATION_CODE.POISONER:
			enemyAttackScript.makeEnemyPoisoner();
			break;
		case EnemyMutationData.ENEMY_MUTATION_CODE.RAGER:
			enemyHealthScript.makeEnemyRager();
			break;
		}
	}

	public void ragerBeingHit()
	{
		enemyAttackScript.increaseDmg (EnemyMutationData.ENEMY_RAGER_INCREASE_ATTACK_PER_HIT);
		enemyMovementScript.increaseSpeed (EnemyMutationData.ENEMY_RAGER_INCREASE_SPEED_PER_HIT);
	}

	private void  applyColorMask()
	{
		SkinnedMeshRenderer renderer = GetComponentInChildren<SkinnedMeshRenderer>();
		Material enemyMaterial = renderer.materials[0];
		Material eyeMaterial = renderer.materials[1];


		if (isBiggerEnemy()) {
			Transform enemytransform = GetComponent<Transform> ();
			Vector3 startScale = enemytransform.localScale;
			startScale.x = startScale.x * EnemyMutationData.MORE_HEALTH_SIZE_SCALE;
			startScale.y = startScale.y * EnemyMutationData.MORE_HEALTH_SIZE_SCALE;
			startScale.z = startScale.z * EnemyMutationData.MORE_HEALTH_SIZE_SCALE;
			enemytransform.localScale = startScale;
		}


		if (isPoisoner ()) {
			enemyMaterial.color *= EnemyMutationData.POISONER_COLOR_MASK;
		} 
		if (isFreezer ()) {
			enemyMaterial.color *= EnemyMutationData.FREEZER_COLOR_MASK;
		}

		if (isRager ()) {
			eyeMaterial.color = EnemyMutationData.RAGER_EYES_COLOR_MASK;
		}

	}

	private bool isBiggerEnemy()
	{
		return activeMutations [(int)EnemyMutationData.ENEMY_MUTATION_CODE.INCREASE_HEALTH];
	}

	private bool isPoisoner()
	{
		return activeMutations [(int)EnemyMutationData.ENEMY_MUTATION_CODE.POISONER];
	}

	private bool isFreezer()
	{
		return activeMutations [(int)EnemyMutationData.ENEMY_MUTATION_CODE.FREEZER];
	}

	private bool isRager()
	{
		return activeMutations [(int)EnemyMutationData.ENEMY_MUTATION_CODE.RAGER];
	}
}
