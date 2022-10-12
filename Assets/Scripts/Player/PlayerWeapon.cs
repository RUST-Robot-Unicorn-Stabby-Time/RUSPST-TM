using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{

    private Animation animation;
    public float cooldown;

    void Start()
    {
        animation = GetComponent<Animator>();
    }
    void Update()
    {
        
    }

    void OnClick()
    {

    }
}
