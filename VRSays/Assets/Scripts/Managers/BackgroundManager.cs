using UnityEngine;
using System.Collections;

public class BackgroundManager : MonoBehaviour {
	static BackgroundManager mInstance;
	public BackgroundManager Instance {
		get {
			if (mInstance == null) {
				GameObject go = new GameObject ();
				mInstance = go.AddComponent<BackgroundManager> ();
			}
			return mInstance;
		}
	}
	public static void changeColor(Color color)
	{
		// TODO: Add tweening here
		color = new Color (color.r * 1 / 4, color.g * 1 / 4, color.b * 1 / 4); // tint
		Camera.main.backgroundColor = color;
	}
}
