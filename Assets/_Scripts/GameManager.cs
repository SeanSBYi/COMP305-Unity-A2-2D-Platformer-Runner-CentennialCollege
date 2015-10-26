///////////////////////////////////////////////////////////////////////////////
// Files:			GameManager.cs
//
// Author:			Sangbeom Yi
// Description:		Manage the Game System.
//
// Revision History 10/22/2015 file created
// 					10/22/2015 Add GameState
// 					10/24/2015 Add Menu Object
// 					10/25/2015 Add Scene Function
//					
//
// Last Modified by	10/26/2015


using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum GameState{
	Play,
	Pause,
	End
}

public class GameManager : MonoBehaviour {
	//PUBLIC INSTANCE VARIABLES
	public GameState GS;

	public Text mLabel;
	public Text scoreLabel;

	// About Result Text.
	public Text ResultTitle;
	public Text ResultDistance;
	public Text ResultDistanceNum;
	public Text ResultCoin;
	public Text ResultCoinNum;

	public float Speed;
	public float Meter;
	public int Coin;

	public GameObject PauseBtn;
	public GameObject PauseUI;
	public GameObject ResultUI;

	// PRIVATE INSTANCE VARIABLES
	private float TempTime;

	// Use this for initialization
	void Start () {
		this.GS = GameState.Play;

		if (Application.loadedLevelName == "IntroScene") { 
			this.GS = GameState.End;
		}

		if (this.mLabel != null) {
			this.mLabel.text = "0";	
		}

		// UI Init Setting.
//		this.PauseBtn.SetActive (true);
		this.PauseUI.SetActive (false);
		this.ResultUI.SetActive (false);

		this.ResultTitle.enabled = false;

		this.ResultDistance.enabled = false;
		this.ResultDistanceNum.enabled = false;
		
		this.ResultCoin.enabled = false;
		this.ResultCoinNum.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (this.GS == GameState.Play ) {
			this.TempTime += Time.deltaTime * Speed;
			this.mLabel.text = "" + (int)this.TempTime;
		}
	}

	// When player get the coin.
	public void GetCoin() {
		//Debug.Log ("GetCoin");
		this.Coin += 1;
		this.scoreLabel.text = "" + this.Coin;
	}

	// When player is gameOver.
	public void GameOver() {
		this.GS = GameState.End;

		this.PauseBtn.SetActive (false);

		this.ResultUI.SetActive (true);
		this.ResultTitle.enabled = true;

		this.ResultDistance.enabled = true;
		this.ResultDistanceNum.enabled = true;
		this.ResultDistanceNum.text = "" + (int)this.TempTime + " m";
		
		this.ResultCoin.enabled = true;
		this.ResultCoinNum.enabled = true;
		this.ResultCoinNum.text = "" + this.Coin;
	}

	// Game Play Again.
	public void Replay() {
		Time.timeScale = 1f;	// Return the Game Time.
		Application.LoadLevel ("MainPlayScene");
	}

	// Go To Main Menu.
	public void GoMain() {
		Time.timeScale = 1f;	// Return the Game Time.
		Application.LoadLevel ("IntroScene");
	}

	// About Pause Menu.
	public void Pause() {
		this.GS = GameState.Pause;
		Time.timeScale = 0f;	// Stop the Game.
		this.PauseUI.SetActive (true);
		this.PauseBtn.SetActive (false);
	}

	public void UnPause() {
		this.GS = GameState.Play;
		Time.timeScale = 1f;
		this.PauseUI.SetActive (false);
		this.PauseBtn.SetActive (true);
	}
}
