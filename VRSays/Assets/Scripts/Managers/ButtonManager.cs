using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// A singleton that handles the spawning, deletion, and positional arrangement
/// of the buttons themselves.
/// </summary>
public class ButtonManager : MonoBehaviour {

	public GameObject buttonPrefab;
	public int count;
	[Range(0.0f,10.0f)]
	public int radius;

	private List<Button> buttons;
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
		buttons = new List<Button> ();
		buttonPrefab.GetComponent<Button>().color = Color.white;
		for (int i = 0; i < count; i++) {
			// arrange objects in a circle
			float angle = i * Mathf.PI * 2 /count;
			Vector3 position = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * radius;
			// declare a game object
			GameObject go = buttonPrefab;
			Debug.Log (go.GetComponent<Button> ().GetType ());
			buttons.Add(go.GetComponent<Button>());
			// initalize game object into scene
			Instantiate(go, position, Quaternion.identity);
		}	
	}

	void Update()
	{
		
	}

	private void removeButton(Button button){
		buttons.Remove(button);
	}
	private void addButton(Button button){
		buttons.Add(button);
	}
	private Button pickButton(Button button)
	{
		return buttons.Find (x => x.GetInstanceID () == button.GetInstanceID ());
	}
}
