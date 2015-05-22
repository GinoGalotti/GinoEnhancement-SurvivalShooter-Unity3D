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

public class ScoreManager : MonoBehaviour
{
	//Game Objects
	public Text lastScoreText;
	public static ScoreManager instance = null;
	
    private Text scoreText;

	//Game variables
	private static int score;
	private int nextLevelXP = 100;
	private const string SCORE_TEXT = "Score: ";

    void Awake ()
    {
		doSingletonMagic ();
		scoreText = GetComponent <Text> ();
        score = 0;
    }

	private void doSingletonMagic()
	{
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);
	}
	
	public void addScore(int pointsToAdd)
	{
		if (GameStatusManager.instance.getStatus() == GameStatusManager.GAME_STATUS.PLAYING) 
		{
			nextLevelXP = LevelUpManager.instance.getNextXPNeeded ();

			pointsToAdd = (int) (pointsToAdd * getPointsModificators());

			score += pointsToAdd;

			scoreText.text = SCORE_TEXT + score;
			lastScoreText.text = getLastScoreString(pointsToAdd);

			if (nextLevelXP <= score)
			{
				LevelUpManager.instance.levelUp();
			}
		}
	}

	private float getPointsModificators()
	{
		float pointModificator = 1f;

		if (isPlayerMorePointMode ()) {
			pointModificator = StatusData.PLAYER_MORE_POINTS_MODE_FACTOR;
		}

		pointModificator *= ComboManager.instance.getCurrentComboPointMultiplicator ();

		return pointModificator;
	}

	private string getLastScoreString(int lastScore)
	{
		return "+ " + lastScore + "!";
	}

	private bool isPlayerMorePointMode()
	{
		return StatusManager.instance.isPlayerBeneficialStatusActive (StatusData.PLAYER_BENEFICIAL_STATUS_CODE.MORE_POINTS_MODE);
	}

}
