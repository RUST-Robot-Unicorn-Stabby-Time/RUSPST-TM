using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{

    private Animator animation;
    public float cooldown = 2f;
    float lastClickedTime = 0;
    int numberOfClicks = 0;

    void Start()
    {
        animation = GetComponent<Animator>();
    }
    void Update()
    {
        if (animation.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && animation.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            animation.SetBool("Attack", false);
            numberOfClicks = 0;
        }

        if (Time.time - lastClickedTime > cooldown)
        {

        }
    }

    void OnClick()
    {
        lastClickedTime = Time.time;
        numberOfClicks++;
        if (numberOfClicks == 1)
        {
            animation.SetBool("Attack", true);
        }
        numberOfClicks = Mathf.Clamp(numberOfClicks, 0, 3);

        //if (numberOfClicks >= 2 && animation.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && animation.GetCurrentAnimatorStateInfo(0).IsName("Attack")) {animation.SetBool("attack", false);}
    }
}
