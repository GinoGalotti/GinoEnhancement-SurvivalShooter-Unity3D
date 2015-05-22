
/* 
 * Copyright (C) Luis Galotti Muñoz (Gino) <ginogalotti@gmail.com>
 * May 2015
 */

/*
 * CODE EXPLANATION:
 * I'm doing this in a way to learn basics of Game design, improve my C# expertise and my general coding skills.
 * My mantra is simple: make the project something to be proud of showing, both as a game and as code itself.
 * Obviously, I have still a lot to learn as a programmer, and I've just started as a designer; so probably the things that you
 * are going to see are probably not best industry practices.
 * 
 * If you have something to add, would change anything or just want to give some feedback; PLEASE, send me an email (ginogalotti@gmail.com). I would thank some advice!
 * 
 * In this project, I tried to manage the system without a GameManager object, forcing entities to update themself. Because of that, I'm always trying to minimize what
 * each entity does during "Update", reducing if possible that call. We can split the logic of the application in the following code:
 * 
 * Clases that call Update (called Managers)
 * * CameraFollow: Directional camera that follow the player. Update needed.
 * * Player:
 * * * PlayerHealth: Handle the health of the player and define when the player is dead.
 * * * PlayerShooting: Handle each time the player aim to shot.
 * * * PlayerMovement: It controls the interaction with the player (movement, chosing upgrades), and regular updates to de player. I've had problems to rename the script.
 * * Enemy:
 * * * EnemyManager: Spawn enemies on the given points (one per enemy type).
 * * * EnemyHealth:
 * * * EnemyMovement:
 * * * EnemyAttack:
 * * Score:
 * * * ScoreManager: Handle the scoring text on demand, and inform levelUpManager when the player has leveled up.
 * * LevelUp:
 * * * LevelUpManager: Will use ScoreManager as a way to know when the player levelUp..
 * GameOverManager:
 * 
 * Gameplay design:
 * After enhancing the project with a levelUp system, my main concern was make rewarding moving around the level. As monster's are always spawning in the same points,
 * player can stay always as far as possible and should be easy to survive. That's how I came up with spawning fun to play powerups, clustering some spawning points near 
 * monsters' ones.
 * With upgrade and powerups, killing this monsters has become quite simple, that's why my next focus was enhancing monster with an affix-like scheme (allowing me to change
 * the skin of the model, as I'm not an artist). This is also going to increase overall score, making more important the level upgrades.
 * I, as a player, was always shooting; that's why I thought that a hitting combo feature would reward the player that don't waste ammo. As I don't want to limit ammo, this
 * approach worked as intended. 
 * 
 * 
 * Explanation last update: 01/05/15
*/ 

/*
 * 
 * WIP:
 * 
 * 
 * TO IMPROVE: 
 * GAMEPLAY:
 * .Add brief history: Player is obssesed with their toys, having nightmares.
 * .Add mobile support.
 * .Add possibility to chose upgrades with clicks.
 * .Add Level Up posibilities: weapon Cadency, Lifesteal
 * .Add random picking upgrades 
 * .Add more powerups:
 * * .Player: Tiny (move faster, more difficult to hit), Change appeareance, Vampire mode (insane melee dmg, heal)
 * * .Weapon: RA-TA-TA (cadency increaser), Ricochet, Explosion, Charming shot, Slower shot, Burning ammo, Piercing ammo, Life steal, Knock back, Disorient
 * * .Trigger: Enemies fear, SuperNova (clear monsters ??), Spawn companion??
 * * .New types of weapon: Katana, Flame, Contiousray
 * .Add Color mask to the weapon.
 * .Add more Enemies mutation:
 * * .Vampire (hit self heal), Tiny enemy, Enemy blinker, Burning trail, Life link
 * .Add Effects to Level Up
 * .Add share points via Twitter
 * .Add custom chance per power up.
 * 
 * TECHNICAL:
 * .Add Unit tests
 * .Add mobile support
 * .Add appeareance invisibility
 * .Refactor
 * .Destructible enviroment that recalculate path.
 * .Link unity with GIT.
 */

using UnityEngine;
using System.Collections;

public class EnhancementScreenLoader : MonoBehaviour {

	private const float WAIT_TIME_IN_SECOND = 3f;
	
	void Update () 
	{
		if (playerSkipEnhancement ()) 
		{
			Invoke ("loadMainLevel", WAIT_TIME_IN_SECOND);
		}
	}

	private bool playerSkipEnhancement()
	{
		return Input.anyKey;
	}
	
	private void loadMainLevel()
	{
		Application.LoadLevel("Main_Level");
	}
}
