/* 
 * Copyright (C) Luis Galotti Muñoz (Gino) <ginogalotti@gmail.com>
 * Assets are derived from Unity's Survival tutorial project
 * May 2015
 * 
 * Code explanation and Gameplay decisitions covered in EnhancementScriptLoader.cs's header. Please, take a look
 */

using UnityEngine;
using System.Collections;

public class EnemyMutationData {

	//Mutation variables
	public class Mutation
	{
		public float mutationChance;
		public float scoreIncrementFactor;
		
		public Mutation(float mutationChance, float scoreIncrement)
		{
			this.mutationChance = mutationChance;
			scoreIncrementFactor = scoreIncrement;
		}
	}

	public enum ENEMY_MUTATION_CODE {RAGER, INCREASE_HEALTH, POISONER, FREEZER, FAST, NUMBER_OF_MUTATION};
	public static Mutation[] posibleMutations = 
		new Mutation[(int)ENEMY_MUTATION_CODE.NUMBER_OF_MUTATION]{
					//Chance (%), Score increment factor 
			new Mutation (0.05f, 1.5f), 	//RAGER Each hit received, more attack and speed
			new Mutation (0.1f, 1.3f), 		//INCREASE_HEALTH
			new Mutation (0.05f, 1.3f),     //POISONER 	
			new Mutation (0.05f, 1.4f), 	//FREEZER
			new Mutation (0.05f, 1.4f)};	//FAST
	
	public static readonly Color FREEZER_COLOR_MASK = new Color (60f / 255f, 130f / 255f, 255f / 255f);
	public static readonly Color POISONER_COLOR_MASK = new Color (85f / 255f, 255f / 255f, 120f / 255f);
	public static readonly Color RAGER_EYES_COLOR_MASK = Color.red;
	public const float MORE_HEALTH_SIZE_SCALE = 1.8f;

	public const float COMBO_DELTA_SCORE_FACTOR = 15f;
	public const float ENEMY_FAST_FACTOR = 1.6f;
	public const float ENEMY_INCREASE_HEALTH_FACTOR = 1.85f;
	public const float ENEMY_RAGER_INCREASE_ATTACK_PER_HIT = 1.1f;
	public const float ENEMY_RAGER_INCREASE_SPEED_PER_HIT = 1.2f;

	public const float HEALTH_INCREASE_BY_LEVEL = 1.1f;
	public const float DAMAGE_INCREASE_BY_LEVEL = 1.1f;
}
