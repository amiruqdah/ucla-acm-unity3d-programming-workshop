using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Renderer))]
public class Button : MonoBehaviour {
	private delegate void ColorChange(Color color);
	private ColorChange colorChange;
	private Color mColor;
	private Renderer mRenderer;
	public Color color {
		get{ return mColor;}
		set{ mColor = color;}
	}
	// Use this for initialization
	void Start () {
		this.mRenderer = this.GetComponent<Renderer>();
		this.mColor = mRenderer.material.GetColor ("_Color");

		colorChange = new ColorChange (this.replaceColor);
		colorChange += BackgroundManager.changeColor;
	}
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Space)) {
			colorChange (Color.red);
		}
	}
	private void replaceColor(Color color)
	{
		this.mRenderer.material.SetColor ("_Color", color);
		#if DEBUG
		Debug.Log("Color has been changed!");
		#endif
	}
}
