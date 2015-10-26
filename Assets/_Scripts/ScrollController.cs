using UnityEngine;
using System.Collections;

public class ScrollController : MonoBehaviour {
	// Public Instance Value.
	public float ScrollSpeed = 0.5f;

	// Private Instance Value.
	private float TargetOffset;

	// Update is called once per frame
	void Update () {
		// Moving BG Scroll
		this.TargetOffset += Time.deltaTime * this.ScrollSpeed;
		GetComponent<Renderer>().material.mainTextureOffset = new Vector2 (this.TargetOffset, 0);
	
	}
}
