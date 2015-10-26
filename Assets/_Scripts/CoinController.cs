// Files:			CoinController.cs
//
// Author:			Sangbeom Yi
// Description:		Coin Contoller
//
// Revision History 10/23/2015 file created
//
//
// Last Modified by	10/23/2015


using UnityEngine;
using System.Collections;

public class CoinController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D otherCollider) {
		if (otherCollider.gameObject.CompareTag ("Player")) {
			Destroy (gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D otherCollider) {
		if (otherCollider.gameObject.CompareTag ("Player")) {
			Destroy (gameObject);
		}
	}
	
	void OnDestroy() {
		//Debug.Log("coin was destroyed");
	}
}
