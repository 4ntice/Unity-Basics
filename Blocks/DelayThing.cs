
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DelayThing : MonoBehaviour
{
    //public float delayTime = 0;
    //float timer = 0;

    public UnityEvent e;

    void Update()
    {

    }

    public void SetTimerGo(float seconds)
    {
        StartCoroutine(TimerGo(seconds));
    }

    public IEnumerator TimerGo(float seconds)
    {
        //Debug.Log(seconds);
        yield return new WaitForSeconds(seconds);

        e.Invoke();
    }
}
