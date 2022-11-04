using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerEvent : MonoBehaviour
{
    public UnityEvent triggerEnterEvent;
    public UnityEvent triggerStay;
    public UnityEvent triggerExitEvent;
    public LayerMask mask;

    private void OnTriggerEnter(Collider other)
    {
        if (mask != (mask | (1 << other.transform.root.gameObject.layer))) return;

        print("hehe");

        triggerEnterEvent.Invoke();
    }
    private void OnTriggerStay(Collider other)
    {
        if (mask != (mask | (1 << other.transform.root.gameObject.layer))) return;

        triggerStay.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        if (mask != (mask | (1 << other.transform.root.gameObject.layer))) return;

        triggerExitEvent.Invoke();  
    }
}
