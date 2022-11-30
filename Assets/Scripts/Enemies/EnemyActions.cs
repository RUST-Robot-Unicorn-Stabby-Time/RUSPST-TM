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
    float targetAngle;
    public bool IsAttacking { get; set; }

    public static event System.Action<EnemyActions> EnemySpawnedEvent;
    public static event System.Action<EnemyActions> EnemyDiedEvent;
    public static event System.Action AllEnemiesDeadEvent;
    public static HashSet<EnemyActions> Enemies = new HashSet<EnemyActions>();

    Blackboard blackboard;
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
        blackboard = GetComponent<BehaviourTree>().blackboard;
    }

    private void OnEnable()
    {
        Enemies.Add(this);
        EnemySpawnedEvent?.Invoke(this);

        EQS.Blockers.Add(this, () => transform.position);
    }

    private void Start()
    {
        ExitDoor door = FindObjectOfType<ExitDoor>();
        if (door) door.WinConditions.Add(() => this ? !gameObject.activeSelf : true);
    }

    public void Attack(int index)
    {
        if (index < 0 || index >= attacks.Length)
        {
            return;
        }

        IsAttacking = true;
        EnemiesAttacking++;

        attacks[index].Attack();
        IsAttacking = true;
    }

    private void OnDisable()
    {
        Enemies.Remove(this);
        EnemyDiedEvent?.Invoke(this);
        if (Enemies.Count == 0) AllEnemiesDeadEvent?.Invoke();

        EQS.Blockers.Remove(this);
    }

    private void Update()
    {
        movement.MoveDirection = MoveDirection;
        MoveDirection = Vector3.zero;

        blackboard.SetValue<Vector3>("position", transform.position);
    }

    private void LateUpdate()
    {
        targetAngle = Mathf.Atan2(FaceDirection.x, FaceDirection.z) * Mathf.Rad2Deg;
        angle = Mathf.SmoothDampAngle(angle, targetAngle, ref faceVelocity, facingSmoothTime);

        transform.rotation = Quaternion.Euler(Vector3.up * angle);
        facingContainer.rotation = Quaternion.Euler(Vector3.up * angle) * Quaternion.Euler(rootRotationOffset);

        EnemiesAttacking = 0;
        IsAttacking = false;
    }
}
