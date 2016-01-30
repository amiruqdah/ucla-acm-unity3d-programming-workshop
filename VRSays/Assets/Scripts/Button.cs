using UnityEngine;
using DG.Tweening;
using System.Collections;

[RequireComponent(typeof(Renderer))]
public class Button : MonoBehaviour {

	private delegate void ColorChange(Color color);
	private ColorChange colorChange;
	private Color mColor;
	private Renderer mRenderer;

	public Color color {
		get;
		set;
	}

	// Use this for initialization
	void Start () {
		this.mRenderer = this.GetComponent<Renderer>();
		this.mRenderer.material.SetColor ("_Color", new Color(Random.value, Random.value, Random.value));
		this.mColor = mRenderer.material.GetColor ("_Color");

		colorChange = new ColorChange (this.replaceColor);
		colorChange += BackgroundManager.changeColor;
	}

	// Update is called once per frame
	void Update () {

	}
	void OnMouseDown()
	{
		if (ButtonManager.Instance.finishedPickingSequence) {
			if (Input.GetMouseButtonDown (0)) {
				if (ButtonManager.Instance.isCorrectButton (this)) {
					this.GetComponent<Renderer> ().material.DOColor (color, "_Color", 0.5f);
					Debug.Log ("correct");
				} else {
					//this.GetComponent<Renderer> ().material.DOColor (Color.red, "_Color", 0.5f);
					//ButtonManager.Instance.clearRound ();
				}
			}
		}

	}

	public void replaceColor(Color color)
	{
		Sequence colorPunch = DOTween.Sequence();
		//TODO: Need to tween this
		colorPunch.Append(this.GetComponent<Renderer>().material.DOColor(color,"_Color", 0.5f));
		colorPunch.Append (this.GetComponent<Renderer> ().material.DOColor (mColor, "_Color", 0.6f));

		#if DEBUG
		Debug.Log("Color has been changed!");
		#endif
	}


}
