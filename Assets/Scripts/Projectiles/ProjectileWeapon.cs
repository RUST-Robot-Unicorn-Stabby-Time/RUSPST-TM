using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeapon : MonoBehaviour
{
    public Projectile projectilePrefab;
    public Transform muzzle;
    public float firerate;

    float lastFireTime;

    public bool FireState { get; set; }

    private void Update()
    {
        if (FireState)
        {
            if (Time.time > lastFireTime + 60.0f / firerate)
            {
                SpawnProjectiles();
                lastFireTime = Time.time;
            }
        }
    }

    private void SpawnProjectiles()
    {
        Instantiate(projectilePrefab, muzzle.position, muzzle.rotation);
    }
}
