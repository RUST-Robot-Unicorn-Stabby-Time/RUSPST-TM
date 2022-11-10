using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActions : MonoBehaviour
{
    [SerializeField] Transform facingContainer;
    [SerializeField] float facingSmoothTime;
    [SerializeField] PlayerWeapon[] attacks;

    CharacterMovement movement;

    float angle;
    float faceVelocity;
    bool attacking;

    public static event System.Action<EnemyActions> EnemyDiedEvent;
    public static event System.Action AllEnemiesDeadEvent;
    public static HashSet<EnemyActions> Enemies = new HashSet<EnemyActions>();

    public Vector3 FaceDirection { get; set; }
    public static int EnemiesAttacking { get; set; }

    public bool Jump { get; set; } 
    public bool Shoot { get; set; }
    public bool Aim { get; set; } 
    public bool Reload { get; set; } 
    public bool Ability { get; set; }

    public Vector2 MoveDirection { get; set; }

    private void Awake()
    {
        movement = GetComponent<CharacterMovement>();
    }

    private void OnEnable()
    {
        Enemies.Add(this);

        foreach (var attack in attacks)
        {
            attack.FinishAttackEvent += OnFinishAttack;
        }
    }

    private void OnFinishAttack()
    {
        EnemiesAttacking--;
        attacking = false;
    }

    public void Attack (int index)
    {
        if (index < 0 || index >= attacks.Length)
        {
            return;
        }

        attacks[index].Attack();
        EnemiesAttacking++;
        attacking = true;
    }

    private void OnDisable()
    {
        EnemyDiedEvent?.Invoke(this);
        Enemies.Remove(this);
        if (Enemies.Count == 0) AllEnemiesDeadEvent?.Invoke();

        foreach (var attack in attacks)
        {
            attack.FinishAttackEvent -= OnFinishAttack;
        }

        if (attacking)
        {
            EnemiesAttacking--;
        }
    }

    private void Update()
    {
        MoveDirection = Vector3.zero;

        float targetAngle = Mathf.Atan2(FaceDirection.x, FaceDirection.z) * Mathf.Rad2Deg;
        angle = Mathf.SmoothDampAngle(angle, targetAngle, ref faceVelocity, facingSmoothTime);

        facingContainer.rotation = Quaternion.Euler(Vector3.up * angle);

        movement.MoveDirection = MoveDirection;
    }
}
