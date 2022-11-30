using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWithDelay : MonoBehaviour
{
    [SerializeField] float delay;
    [SerializeField] bool disable;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(delay);

        if (disable) gameObject.SetActive(false);
        else Destroy(gameObject, delay);
    }
}
