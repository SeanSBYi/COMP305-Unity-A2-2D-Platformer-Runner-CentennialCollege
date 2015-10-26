///////////////////////////////////////////////////////////////////////////////
// Files:			SendManager.cs
//
// Author:			Sangbeom Yi
// Description:		Manage the Mouse Click
//
// Revision History 10/25/2015 file created
// 					10/25/2015 Create Mouse Event
//					
//
// Last Modified by	10/26/2015

using UnityEngine;
using System.Collections;

public class SendManager : MonoBehaviour {
	//PUBLIC INSTANCE VARIABLES
	public GameObject Target;
	public string MethodName;

	public void OnMouseDown() {
		//Debug.Log ("OnMouseDown~!@#~@#$!");
		this.Target.SendMessage (this.MethodName);
	}
}
