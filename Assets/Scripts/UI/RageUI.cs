using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RageUI : MonoBehaviour
{
    public Image rageBar;

    private Rage rage;

    void Start()
    {
        rage = GetComponent<Rage>();
    }
    void Update()
    {
        rageBar.fillAmount = rage.ragePercent;
    }
}
