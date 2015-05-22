/* 
 * Copyright (C) Luis Galotti Muñoz (Gino) <ginogalotti@gmail.com>
 * Assets are derived from Unity's Survival tutorial project
 * May 2015
 * 
 * Code explanation and Gameplay decisitions covered in EnhancementScriptLoader.cs's header. Please, take a look
 */

using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
	//Game objects
    public GameObject enemy;
	public Transform[] spawnPoints;

	//Gameplay variables
    private const float SPAWN_TIME_IN_SEC = 3f;

    void Start ()
    {
        InvokeRepeating ("Spawn", SPAWN_TIME_IN_SEC, SPAWN_TIME_IN_SEC);
    }

    void Spawn ()
    {
		if(GameStatusManager.instance.getStatus() == GameStatusManager.GAME_STATUS.PLAYING)
        {
			int spawnPointIndex = Random.Range (0, spawnPoints.Length);
			
			Instantiate (enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
        }
    }
}
