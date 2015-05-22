/* 
 * Copyright (C) Luis Galotti Muñoz (Gino) <ginogalotti@gmail.com>
 * Assets are derived from Unity's Survival tutorial project
 * May 2015
 * 
 * Code explanation and Gameplay decisitions covered in EnhancementScriptLoader.cs's header. Please, take a look
 */
using UnityEngine;
using UnityEngine.UI;
using System.Text;

using System.Collections;

public class ComboManager : MonoBehaviour {

	//Game objects
	public static ComboManager instance = null;
	public GameObject comboPanel;
	public Text comboChangingText;

	private Animator anim;

	//Game variables
	private float comboPointsMultiplicator = 1f;
	private int currentComboShowing = 0;

	public const int STARTING_COMB0_COUNT = 5;
	private const float COMBO_1_POINT_MULTIPLICATOR = 1.2f;

	private const int STARTING_COMB0_2_COUNT = 10;
	private const float COMBO_2_POINT_MULTIPLICATOR = 1.4f;

	private const int STARTING_COMB0_3_COUNT = 25;
	private const float COMBO_3_POINT_MULTIPLICATOR = 1.6f;

	private const int STARTING_COMB0_4_COUNT = 50;
	private const float COMBO_4_POINT_MULTIPLICATOR = 2f;

	private const string TEXT_TO_SHOW = "{0} hits in a row!\n{1}x points!";


	void Start () {
		doSingletonMagic ();
	}
	
	private void doSingletonMagic()
	{
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);
	}

	public void stopShowing()
	{
		if (currentComboShowing > 0) 
		{
			currentComboShowing = 0;
			comboPointsMultiplicator = 1f;
			comboPanel.SetActive(false);
		}
	}
	
	public void calculateComboRangeAndShowMessage(int consecutiveHits)
	{

		if (consecutiveHits < STARTING_COMB0_2_COUNT) {
			currentComboShowing = 1;
			comboPointsMultiplicator = COMBO_1_POINT_MULTIPLICATOR;
			writeAndShowMessage ();
		} else if (consecutiveHits < STARTING_COMB0_3_COUNT) {
			currentComboShowing = 2;
			comboPointsMultiplicator = COMBO_2_POINT_MULTIPLICATOR;
			writeAndShowMessage ();
		} else if (consecutiveHits < STARTING_COMB0_4_COUNT) {
			currentComboShowing = 3;
			comboPointsMultiplicator = COMBO_3_POINT_MULTIPLICATOR;
			writeAndShowMessage ();
		} else {
			currentComboShowing = 4;
			comboPointsMultiplicator = COMBO_4_POINT_MULTIPLICATOR;
			writeAndShowMessage();
		}
	}

	private void writeAndShowMessage()
	{
		comboChangingText.text = formatStringDependingOnLevel ();
		comboPanel.SetActive(true);
	}

	private string formatStringDependingOnLevel()
	{
		string formattedString = "";
		switch (currentComboShowing) 
		{
		case 1:
			formattedString = string.Format(TEXT_TO_SHOW, STARTING_COMB0_COUNT, COMBO_1_POINT_MULTIPLICATOR);
			break;
		case 2:
			formattedString = string.Format(TEXT_TO_SHOW, STARTING_COMB0_2_COUNT, COMBO_2_POINT_MULTIPLICATOR);
			break;
		case 3:
			formattedString = string.Format(TEXT_TO_SHOW, STARTING_COMB0_3_COUNT, COMBO_3_POINT_MULTIPLICATOR);
			break;
		case 4:
			formattedString = string.Format(TEXT_TO_SHOW, STARTING_COMB0_4_COUNT, COMBO_4_POINT_MULTIPLICATOR);
			break;
		}
		return formattedString;
	}

	public float getCurrentComboPointMultiplicator()
	{
		return comboPointsMultiplicator;
	}

	public bool isShowingCombo()
	{
		return comboPointsMultiplicator != 0;
	}

}
