﻿/////////////////////////////////////////////////////////////////////////////
// Files:			PlayerController.cs
//
// Author:			Sangbeom Yi
// Description:		Manage BG Scroll
//
// Revision History 10/19/2015 file created
// 					10/20/2015 Add Player Key Input
//					10/20/2015 Change Coin Getting System
//					10/22/2015 Add PlayerState
//					10/22/2015 Add GameManager
//					10/25/2015 Add Dead Flag and Sound
//
//
// Last Modified by	10/26/2015

using UnityEngine;
using System.Collections;

// VELOCITYRANGE UTILITY CLASS +++++++++++++++++++++++++
[System.Serializable]
public class VelocityRange {
	// PUBLIC INSTANCE VARIABLES
	public float vMin, vMax;

	// CONSTRUCTOR ++++++++++++++++++++++++++++++++
	public VelocityRange(float vMin, float vMax) {
		this.vMin = vMin;
		this.vMax = vMax;
	}
}

public enum PlayerState{
	Run,
	Jump,
	D_Jump,
	Death
}

// PLAYERCONTROLLER CLASS +++++++++++++++++++++++++++++++++++++
public class PlayerController : MonoBehaviour {
	//CONST VARIABLES
	public const int PS_Run = 0;
	public const int PS_Jump = 1;
	public const int PS_D_Jump = 2;
	public const int PS_Death = 3;

	//PUBLIC INSTANCE VARIABLES
	public GameManager GM;
	public PlayerState PS;
	public int playerState;

	public float speed = 50f;
	public float jump = 500f;
	public VelocityRange velocityRange = new VelocityRange (300f, 1000f);
	
	//PRIVATE INSTANCE VARIABLES
	private AudioSource[] _audioSources;
	private AudioSource _coinSound;
	private AudioSource _jumpSound;
	private AudioSource _deadSound;
	private AudioSource _PlayBgm;
	private AudioSource _ResultBgm;

	private Rigidbody2D _rigidbody2D;
	private Transform _transform;
	private Animator _animator;

	private float _movingValue = 0;
	private bool _isFacingRight = true;
	private bool _isGrounded = true;

	// Use this for initialization
	void Start () {
		//this.PS = PlayerState.Run;
		this.playerState = PlayerController.PS_Run;

		this._rigidbody2D = gameObject.GetComponent<Rigidbody2D> ();
		this._transform = gameObject.GetComponent<Transform> ();
		this._animator = gameObject.GetComponent<Animator> ();

		this._audioSources = gameObject.GetComponents<AudioSource> ();
		this._coinSound = this._audioSources[0];
		this._jumpSound = this._audioSources [1];
		this._deadSound = this._audioSources [2];
		this._PlayBgm = this._audioSources [3];
		this._ResultBgm = this._audioSources [4];

		if (Application.loadedLevelName == "IntroScene") {
			this.playerState = PlayerController.PS_Death;
			//this.PS = PlayerState.Death;
			this._animator.SetInteger("AnimState", 1);
		}

		this._PlayBgm.Play();
	}

	// Update is called once per frame
	void Update() {
		this.CheckGround();
	}

	void FixedUpdate () {
		float forceX = 0f;
		float forceY = 0f;

		if (_rigidbody2D == null) {
			return;
		}

		float absVelX = Mathf.Abs (this._rigidbody2D.velocity.x);
		float absVelY = Mathf.Abs (this._rigidbody2D.velocity.y);
	
		if (Application.loadedLevelName == "IntroScene") { 
			return;
		}

		this._movingValue = Input.GetAxis ("Horizontal"); // gives moving variable a value of -1 to 1

		//if (this._movingValue != 0 && PS != PlayerState.Death) { // player is moving
		if (this._movingValue != 0 && this.playerState != PlayerController.PS_Death) {
			//check if player is grounded
			//if( PS == PlayerState.Run ) {
			if( this.playerState == PlayerController.PS_Run ) {
				this._animator.SetInteger("AnimState", 1);
				if(this._movingValue > 0) {
					// move right
					if(absVelX < this.velocityRange.vMax) {
						forceX = this.speed;
						this._isFacingRight = true;
						this._flip();
					}
				} else if (this._movingValue < 0) {
					// move left
					if(absVelX < this.velocityRange.vMax) {
						forceX = -this.speed;
						this._isFacingRight = false;
						this._flip ();
					}
				}
			}
		} else {
			// set our idle animation here
			this._animator.SetInteger("AnimState", 0);
		}

		// check if player is jumping
		//if (Input.GetKey ("space") && this.PS != PlayerState.Death) {
		if (Input.GetKey ("space") && this.playerState != PlayerController.PS_Death) {
			// chec if player is grounded
			//if( this.PS == PlayerState.Run && this._isGrounded ==  true ) {
			if( this.playerState == PlayerController.PS_Run && this._isGrounded ==  true ) {
				this._animator.SetInteger("AnimState", 2);
				if(absVelY < this.velocityRange.vMax) {
					forceY = this.jump;
					this._jumpSound.Play();

					//this.PS = PlayerState.Jump;
					this.playerState = PlayerController.PS_Jump;
					//this._isGrounded = false;
				}
			}
		}

		// add force to the player to push him
		this._rigidbody2D.AddForce (new Vector2 (forceX, forceY));
	}

	void OnCollisionEnter2D(Collision2D otherCollider) {
		if (otherCollider.gameObject.CompareTag ("Coin")) {
			this._coinSound.Play();
		}
	}

	// Trigger Event.
	void OnTriggerEnter2D(Collider2D otherCollider) {
		//Debug.Log ("OnTriggerEnterTag:" + otherCollider.tag);
		if (otherCollider.tag == "Coin") {
			//Debug.Log ("OnTriggerEnter IN~!~~~!");
			if( Application.loadedLevelName == "MainPlayScene" ) { 
				this._coinSound.Play();
			}
			this.GetCoin();
		}

		//Debug.Log ("OnTriggerEnterName :" + otherCollider.gameObject.name + ", " + this.PS);
		//if (otherCollider.gameObject.name == "DeathZone" && this.PS != PlayerState.Death) {
		if (otherCollider.gameObject.name == "DeathZone" && this.playerState != PlayerController.PS_Death) {
			this._deadSound.Play();
			this.GameOver();
			this._PlayBgm.Stop();
		}
	}

	void OnCollisionStay2D(Collision2D otherCollider) {
		if (otherCollider.gameObject.CompareTag ("Platform")) {
			// Note : This function moved to CheckGround()
			this._isGrounded =  true;
			//this.PS = PlayerState.Run;
			this.playerState = PlayerController.PS_Run;
		}
	}

	void CheckGround () {
		RaycastHit2D hit = Physics2D.Raycast (transform.position, -Vector2.up, 0.1f);

		Debug.DrawRay(transform.position, -Vector2.up, Color.red);//
			//	Debug.DrawRay(transform.position, Vector3.down * 0.9f, Color.red);//
		
		if(hit.transform.tag == "Platform") {
			this._isGrounded =  true;
			return;
		}
		this._isGrounded =  false;
	}

	void GetCoin() {
		if (GM != null) {
			//Debug.Log ("!!!GetCoin");
			GM.GetCoin ();
		}
	}

	// GameOver Process
	void GameOver() {
		//this.PS = PlayerState.Death;
		this.playerState = PlayerController.PS_Death;
		GM.GameOver ();

		if (_ResultBgm != null) {
			_ResultBgm.Play ();
		}
	}

	// PRIVATE METHODS
	private void _flip() {
		if (this._isFacingRight) {
			this._transform.localScale = new Vector3(1f, 1f, 1f); // reset to normal scale
		} else {
			this._transform.localScale = new Vector3(-1f, 1f, 1f);
		}
	}

}
