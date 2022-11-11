using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockControls : MonoBehaviour
{
    private void OnEnable()
    {
        PlayerController.UnlockControls(true);
    }

    private void OnDisable()
    {
        PlayerController.UnlockControls(false);
    }
}
