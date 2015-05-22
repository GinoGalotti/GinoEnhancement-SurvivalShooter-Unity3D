/* 
 * Copyright (C) Luis Galotti Muñoz (Gino) <ginogalotti@gmail.com>
 * Assets are derived from Unity's Survival tutorial project
 * May 2015
 * 
 * Code explanation and Gameplay decisitions covered in EnhancementScriptLoader.cs's header. Please, take a look
 */

using UnityEngine;
using System.Collections;

public class StatusData {

	//Type of status modifications
	public enum GAME_MODIFICATION_TYPE {TRIGGER, PLAYER_BENEFICIAL_STATUS, PLAYER_HARMFUL_STATUS, WEAPON_STATUS, NUMBER_OF_TYPES};

	//Playey Beneficial variables
	public enum PLAYER_BENEFICIAL_STATUS_CODE {INMMORTAL, INVISIBLE, MORE_POINTS_MODE, CHANGE_APPAREANCE, ZOMBIE_MODE, NUMBER_OF_STATUS};
	public static Status[] currentPlayerBeneficialStatus = 	
	new Status[(int)PLAYER_BENEFICIAL_STATUS_CODE.NUMBER_OF_STATUS] {new Status("Inmortal"), new Status("Invisible"),new Status("Extra points"), new Status("Transformed"), 
		new Status("Hungry")};
	public readonly static float[] STARTING_PLAYER_BENEFICIAL_STATUS_DURATION_IN_SEC = new float[(int)PLAYER_BENEFICIAL_STATUS_CODE.NUMBER_OF_STATUS] { 5f, 5f, 10f, 5f, 10f};

	public const float PLAYER_MORE_POINTS_MODE_FACTOR = 1.5f;

	//Playey Harmful variables
	public enum PLAYER_HARMFUL_STATUS_CODE { POISON, SLOWED, NUMBER_OF_STATUS};
	public static Status[] currentPlayerHarmfulStatus = 	new Status[(int)PLAYER_HARMFUL_STATUS_CODE.NUMBER_OF_STATUS] {new Status("Intoxicated"), new Status("Slowed")};
	public readonly static float[] STARTING_PLAYER_HARMFUL_STATUS_DURATION_IN_SEC = new float[(int)PLAYER_HARMFUL_STATUS_CODE.NUMBER_OF_STATUS] { 5f, 10f};

	public const float PLAYER_DEFAULT_SLOWED_DURATION_IN_SECS = 4f;
	public const float PLAYER_SLOWED_FACTOR = 0.7f;
	public const float PLAYER_DEFAULT_POISON_DURATION_IN_SECS = 5f;
	public const int PLAYER_POISON_HIT_DMG = 1;
	public const float PLAYER_POISON_TIME_TO_TICK_IN_SECS = 0.2f;

	//Weapon effect variables
	public enum WEAPON_EFFECT_CODE {EXTRA_DAMAGE, INCREASED_CADENCY, LIFE_STEAL_SHOTS, RICOCHET, EXPLOSIVE_AMMO, CHARMING_AMMO, NUMBER_OF_EFFECTS};
	public static Status[] currentWeaponEffects = 
			new Status[(int)WEAPON_EFFECT_CODE.NUMBER_OF_EFFECTS] {new Status("Killer"), new Status("RA-TA-TA"), new Status("Life stealer shots"), new Status("Ricochet shot"), new Status("Explosive ammo"), new Status("Charming ammo")};
	public readonly static float[] STARTING_WEAPON_EFFECT_DURATION_IN_AMMO = new float[(int)WEAPON_EFFECT_CODE.NUMBER_OF_EFFECTS] { 25f, 30f, 10f, 10f, 10f, 1f};

	public const float WEAPON_MORE_DMG_FACTOR = 1.6f;
	public const float REDUCE_CADENCY_FACTOR = 2f;
	public const int LIFE_STEAL_PER_SHOT = 4;

	//Enemies effect variables
	public enum ENEMIES_STATUS_CODE{ FREEZE, NUMBER_OF_STATUS};
	public static Status[] currentEnemyStatus = new Status[(int)ENEMIES_STATUS_CODE.NUMBER_OF_STATUS] {new Status("Enemies frozen")};
	public readonly static float[] STARTING_ENEMIES_STATUS_DURATION_IN_SEC = new float[(int)ENEMIES_STATUS_CODE.NUMBER_OF_STATUS] { 3f};

	//Trigger effect variables
	public enum TRIGGER_EFFECT_CODE {ADD_LEVEL, FREEZE_ENEMIES, SUPER_NOVA, NUMBER_OF_EFFECTS};

}
