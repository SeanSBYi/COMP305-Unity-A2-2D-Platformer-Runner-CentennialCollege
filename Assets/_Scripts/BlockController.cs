using UnityEngine;
using System.Collections;

public class BlockController : MonoBehaviour {
	// Public Instance Value.
	public float BlockSpeed = 2;

	// For scolling Ground Block
	public GameObject[]	Block;
	public GameObject	BlockA;
	public GameObject	BlockB;

	// Update is called once per frame
	void Update () {
		this.MoveBlock ();
	}

	// Move the Ground Block
	void MoveBlock() {
		this.BlockA.transform.Translate (Vector3.left * this.BlockSpeed * Time.deltaTime);
		this.BlockB.transform.Translate (Vector3.left * this.BlockSpeed * Time.deltaTime);

		// Check the block, if it is outside of screen.
		if (this.BlockA.transform.position.x <= -330) {		
			Destroy(this.BlockA);

			this.BlockA = this.BlockB;
			this.MakeBlock();
		}
	}


	// Make the Ground Block
	void MakeBlock() {
		int LocalRandom = Random.Range (0, Block.Length);

		// Random Making.
		this.BlockB = Instantiate (this.Block[LocalRandom], new Vector3 (this.BlockA.transform.position.x + 900, -112, 0), GetComponent<Transform> ().rotation) as GameObject;
	}

}
