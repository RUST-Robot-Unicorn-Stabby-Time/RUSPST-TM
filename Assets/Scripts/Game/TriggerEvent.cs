using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerEvent : MonoBehaviour
{
    public UnityEvent triggerEnterEvent;
    public UnityEvent triggerStay;
    public UnityEvent triggerExitEvent;
    private void OnTriggerEnter(Collider collider)
    {
        triggerEnterEvent.Invoke();
    }
    private void OnTriggerStay(Collider other)
    {
        triggerStay.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        triggerExitEvent.Invoke();  
    }
}
