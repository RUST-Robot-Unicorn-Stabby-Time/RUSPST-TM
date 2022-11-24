using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flash : MonoBehaviour
{
    [SerializeField] float duration;

    private void OnEnable()
    {
        StartCoroutine(FlashRoutine());
    }

    private IEnumerator FlashRoutine()
    {
        yield return new WaitForSeconds(duration);
        gameObject.SetActive(false);
    }
}
