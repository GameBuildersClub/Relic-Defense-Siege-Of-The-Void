using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController
{
    protected float time;
    public float Time { get { return time; } }

    protected float timeScale;
    public float TimeScale { get { return timeScale; } set { Mathf.Clamp(value, 0, Mathf.Infinity); timeScale = value; } }
    public event System.Action<float> onTimeScaleUpdate;

    public float deltaTime { get { return UnityEngine.Time.fixedDeltaTime * TimeScale; } }

    public TimeController()
    {
        time = 0;
        timeScale = 1;
    }

    public void Tick()
    {
        time += deltaTime;
    }
}
