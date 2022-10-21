using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DebugActions : MonoBehaviour
{
    private void Update()
    {
        if (Keyboard.current.numpad7Key.wasPressedThisFrame)
        {
            foreach (var player in FindObjectsOfType<InputArbiter>(true))
            {
                player.GetComponent<Health>().Revive();
            }
        }

        if (Keyboard.current.numpad8Key.wasPressedThisFrame)
        {
            foreach (var enemy in FindObjectsOfType<EnemyBase>(true))
            {
                enemy.GetComponent<Health>().Revive();
            }
        }

        if (Keyboard.current.numpad9Key.wasPressedThisFrame)
        {
            foreach (var player in FindObjectsOfType<InputArbiter>(true))
            {
                player.transform.position = Vector3.zero;
                player.GetComponent<Rigidbody>().velocity = Vector3.zero;
            }
        }

        if (Keyboard.current.numpad4Key.wasPressedThisFrame)
        {
            foreach (var player in FindObjectsOfType<InputArbiter>(true))
            {
                player.GetComponent<Rage>().ragePercent = 1.0f;
            }
        }
    }

    private void OnGUI()
    {
        GUI.color = Color.red;
        GUI.Label(new Rect(10, 10, 500, 20), "DEBUG ACTIONS ENABLED");
    }
}
