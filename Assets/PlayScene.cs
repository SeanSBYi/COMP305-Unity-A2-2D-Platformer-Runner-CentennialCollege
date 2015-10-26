///////////////////////////////////////////////////////////////////////////////
// Files:			PlayScene.cs
//
// Author:			Sangbeom Yi
// Description:		Move and Play New Scene
//
// Revision History 10/26/2015 file created
//					
//
// Last Modified by	10/26/2015

using UnityEngine;
using System.Collections;

public class PlayScene : MonoBehaviour {
	// Public Instance Value.
	public string SceneName;
	
	public void OnMouseDown(){		
		Application.LoadLevel( SceneName );		
		
	}
}