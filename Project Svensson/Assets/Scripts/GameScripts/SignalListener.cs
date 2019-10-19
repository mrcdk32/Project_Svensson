using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SignalListener : MonoBehaviour
{
    public Signal_Event signal;
    public UnityEvent signal_Event;
    public void OnSignalRaised()
    {
        signal_Event.Invoke();
    }
    private void OnEnable()
    {
        signal.RegisterLitsener(this);

    }
    private void OnDisable()
    {
        signal.DeRegisterListener(this);
    }
}
