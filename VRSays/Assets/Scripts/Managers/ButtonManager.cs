using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// A singleton that handles the spawning, deletion, and positional arrangement
/// of the buttons themselves.
/// </summary>
public class ButtonManager : MonoBehaviour {

	public GameObject buttonPrefab;
	public Vector3 size = new Vector3 (1f, 1f, 1f);
	public int count;
	[Range(0.0f,5.0f)]
	public float xPadding;
	private List<Button> buttons;
	private Timer timer;

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
	public int Count {
		get;
		set;
	}
	void Start()
	{	
		timer = new Timer(5f,pickRandomButton);
		buttons = new List<Button> ();
		buttonPrefab.GetComponent<Button>().color = Color.white;
		Vector3 pos = new Vector3 (0, 0, 0);
		for (int i = 0; i < count; i++) {
			pos.x += size.z + xPadding;
			if (i > 0 && i % 3 == 0) {
				pos.z -= 2;
			}
			// declare a game object
			GameObject go = Instantiate(buttonPrefab, pos, Quaternion.identity) as GameObject;
			go.transform.localScale = this.size;
			buttons.Add(go.GetComponent<Button>());
			// initalize game object into scene

		}	
	}

	void Update()
	{
		timer.Update (Time.deltaTime);
		Debug.Log (timer.CurrentTime);
	}
	private void pickRandomButton()
	{
		int rand = Random.Range (0, count);
		buttons[rand].replaceColor(Color.yellow);
	}
}
