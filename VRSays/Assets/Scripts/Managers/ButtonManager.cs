using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// A singleton that handles the spawning, deletion, and positional arrangement
/// of the buttons themselves.
/// </summary>
public class ButtonManager : MonoBehaviour {

	static ButtonManager mInstance;
	public static ButtonManager Instance {
		get {
			if (mInstance == null) {
				GameObject go = new GameObject ();
				mInstance = go.AddComponent<ButtonManager> ();
			}
			return mInstance;
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
