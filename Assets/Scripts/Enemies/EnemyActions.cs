using System.Collections.Generic;
using UnityEngine;

public class EnemyActions : MonoBehaviour
{
    [SerializeField] Transform facingContainer;
    [SerializeField] float facingSmoothTime;
    [SerializeField] PlayerWeapon[] attacks;
    [SerializeField] float attackRange;

    [Space]
    [SerializeField] Vector3 rootRotationOffset;

    CharacterMovement movement;

    float angle;
    float faceVelocity;
    bool attacking;

    public static event System.Action<EnemyActions> EnemySpawnedEvent;
    public static event System.Action<EnemyActions> EnemyDiedEvent;
    public static event System.Action AllEnemiesDeadEvent;
    public static HashSet<EnemyActions> Enemies = new HashSet<EnemyActions>();

    HashSet<PlayerWeapon> weapons;

    public Vector3 FaceDirection { get; set; }
    public static int EnemiesAttacking { get; set; }

    public bool Jump { get; set; } 
    public bool Shoot { get; set; }
    public bool Aim { get; set; } 
    public bool Reload { get; set; } 
    public bool Ability { get; set; }

    public Vector3 MoveDirection { get; set; }

    private void Awake()
    {
        movement = GetComponent<CharacterMovement>();
        weapons = new HashSet<PlayerWeapon>(GetComponentsInChildren<PlayerWeapon>());
    }

    private void OnEnable()
    {
        Enemies.Add(this);
        EnemySpawnedEvent?.Invoke(this);

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

    public void Attack (Vector3 targetPoint, int index)
    {
        if (index < 0 || index >= attacks.Length)
        {
            return;
        }

        Vector3 direction = (targetPoint - transform.position).normalized;
        transform.rotation = Quaternion.LookRotation(direction);

        if ((targetPoint - transform.position).sqrMagnitude > attackRange * attackRange)
        {
            FaceDirection = direction;
            MoveDirection = direction;
        }
        else
        {
            attacks[index].Attack();
            EnemiesAttacking++;
            attacking = true;
        }
    }

    private void OnDisable()
    {
        Enemies.Remove(this);
        EnemyDiedEvent?.Invoke(this);
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
        movement.MoveDirection = MoveDirection;
        MoveDirection = Vector3.zero;
    }

    private void LateUpdate()
    {
        foreach (var weapon in weapons)
        {
            if (weapon.Attacking && weapon.FreezeMovement) return;
        }

        float targetAngle = Mathf.Atan2(FaceDirection.x, FaceDirection.z) * Mathf.Rad2Deg;
        angle = Mathf.SmoothDampAngle(angle, targetAngle, ref faceVelocity, facingSmoothTime);

        transform.rotation = Quaternion.Euler(Vector3.up * angle);
        facingContainer.rotation = Quaternion.Euler(Vector3.up * angle) * Quaternion.Euler(rootRotationOffset);
    }
}
