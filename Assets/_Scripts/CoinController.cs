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
	
	void OnDestroy() {
		Debug.Log("coin was destroyed");
	}
}
