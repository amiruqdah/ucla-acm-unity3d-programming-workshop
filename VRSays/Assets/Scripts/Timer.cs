using UnityEngine;
using System.Collections;

public class Timer{
	public delegate void TimerCallback();
	protected TimerCallback timerCallbackFunction;
	public float targetTime;
	private bool hasStopped = false;
	private float currentTime;
	private float delay;
	public float CurrentTime { 
		get{ return currentTime; }
	}

	public bool isStopped { 
		get { return hasStopped; } 
	}

	public Timer(float targetTime, TimerCallback timerCallback, float delay=0)
	{
		this.delay = delay;
		currentTime = targetTime;
		this.targetTime = targetTime;
		timerCallbackFunction = timerCallback;
	}
	public void Update(float dt)
	{
		if (hasStopped == false) {
			this.currentTime -= dt;		

			if (this.currentTime <= 0.0f) {
				timerCallbackFunction.Invoke ();
				Stop();
			}
		}
	}
	public void Reset(){
		hasStopped = !hasStopped;
		currentTime = targetTime;
	}
	public void Start(float newTargetTime = 0){
		if (targetTime > 0)
			currentTime = newTargetTime;
		hasStopped = false;
	}
	public void Stop(){
		hasStopped = true;
	}
}
