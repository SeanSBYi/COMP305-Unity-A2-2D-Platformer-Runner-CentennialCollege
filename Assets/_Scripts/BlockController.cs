///////////////////////////////////////////////////////////////////////////////
// Files:			BlockContorller.cs
//
// Author:			Sangbeom Yi
// Description:		Manage the Mouse Click
//
// Revision History 10/21/2015 file created
// 					10/22/2015 Add Block Pattern
// 					10/23/2015 Random Block
// 					10/25/2015 Controll Block Speed
//
// Last Modified by	10/25/2015

using UnityEngine;
using System.Collections;

public class BlockController : MonoBehaviour {
	// Public Instance Value.
	public float BlockSpeedNow = 220f;
	public float BlockLevelUpTime = 4f;

	// For scolling Ground Block
	public GameObject[]	Block;
	public GameObject	BlockA;
	public GameObject	BlockB;

	// Private Instance Value.
	private float ModBlockSpeed = 0f;

	// Update is called once per frame
	void Update () {
		this.MoveBlock ();

		this.ModBlockSpeed += Time.deltaTime;
		if (this.ModBlockSpeed > this.BlockLevelUpTime) {
			this.ModBlockSpeed = 0f;
			this.BlockSpeedNow += 40f;
		}
	}

	// Move the Ground Block
	void MoveBlock() {
		this.BlockA.transform.Translate (Vector3.left * this.BlockSpeedNow * Time.deltaTime);
		this.BlockB.transform.Translate (Vector3.left * this.BlockSpeedNow * Time.deltaTime);

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
