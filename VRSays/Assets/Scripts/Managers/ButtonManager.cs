using UnityEngine;
using System.Collections;
using DG.Tweening;
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
	private Queue<Button> buttonCheckList;
	private Timer timer;
	static ButtonManager mInstance;
	public bool finishedPickingSequence = true;

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
		generateRound();
	}

	void Update()
	{
		timer.Update (Time.deltaTime);
		finishedPickingSequence = timer.isStopped;
		#if DEBUG
		Debug.Log(timer.isStopped.ToString());
		#endif
	}
	private void pickRandomButton()
	{
		if (buttons.Count > 0) {
			int rand = Random.Range (0, buttons.Count);
			buttons [rand].replaceColor (Color.yellow);
			buttons [rand].gameObject.transform.DOPunchScale (new Vector3 (1f, 1f), 0.3f,2, 1f);
			buttonCheckList.Enqueue (buttons [rand]);
			buttons.RemoveAt (rand);

			// messy I should figure out why reset isn't working
			timer = new Timer (Random.Range (0.5f, 1.5f), pickRandomButton);
		}
	}


	public bool isCorrectButton(Button button)
	{
		if (button == this.buttonCheckList.Dequeue())
			return true;
		else
			return false;
	}

	private void generateRound()
	{
		Sequence spawnSequence = DOTween.Sequence ();
		buttonCheckList = new Queue<Button> ();
		buttons = new List<Button> ();
		Vector3 pos = new Vector3 (0, 0, 0);

		for (int i = 0; i < count; i++) {
			pos.x += size.z + xPadding;
			if (i > 0 && i % 3 == 0) {
				pos.z -= 2;
				pos.x -= (size.z + xPadding) * 3;
			}
			GameObject go = Instantiate(buttonPrefab, pos, Quaternion.identity) as GameObject;
			go.tag = "Button";
			buttonCheckList.Enqueue (go.GetComponent<Button> ());
			spawnSequence.Append(go.transform.DOScale(new Vector3(size.x,size.y,size.z),0.05f).SetEase(Ease.InOutSine));
			spawnSequence.Append(go.transform.DORotate(new Vector3(0f,90f,0f),0.2f).SetEase(Ease.InCirc));
			buttons.Add(go.GetComponent<Button>());

		}	
	}

	private void clearRound()
	{
		GameObject[] gameObjects = GameObject.FindGameObjectsWithTag ("Button");
		foreach (GameObject gameObject in gameObjects) {
			Destroy (gameObject);
		}
		generateRound ();
	}


}
