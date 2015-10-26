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
		this.mLabel.text = "0";	

		// UI Init Setting.
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
		if (this.GS == GameState.Play) {
			this.TempTime += Time.deltaTime * Speed;
			this.mLabel.text = "" + (int)this.TempTime;
		}
	}

	// When player get the coin.
	public void GetCoin() {
		Debug.Log ("GetCoin");
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
		this.ResultDistanceNum.text = "" + (int)this.TempTime;
		
		this.ResultCoin.enabled = true;
		this.ResultCoinNum.enabled = true;
		this.ResultCoinNum.text = this.Coin + " m";
	}
}
