using UnityEngine;

[SelectionBase]
[DisallowMultipleComponent]
[RequireComponent(typeof(Rigidbody))]
public sealed class CharacterMovement : MonoBehaviour
{
    [SerializeField] Stat moveSpeed = new Stat("Speed", 10.0f);
    [SerializeField] Stat groundAcceleration = new Stat("Acceleration", 80.0f);

    [Space]
    [SerializeField] float airMoveForce = 8.0f;

    [Space]
    [SerializeField] float jumpHeight = 3.5f;
    [SerializeField] float upGravity = 2.0f;
    [SerializeField] float downGravity = 3.0f;
    [SerializeField] float jumpSpringPauseTime = 0.1f;

    [Space]
    [SerializeField] float springDistance = 1.2f;
    [SerializeField] float springForce = 250.0f;
    [SerializeField] float springDamper = 15.0f;
    [SerializeField] float groundCheckRadius = 0.2f;
    [SerializeField] LayerMask groundCheckMask = 0b1;

    bool previousJumpState;
    float lastJumpTime;

    public event System.Action JumpEvent;

    public Stat MoveSpeed => moveSpeed;
    public Rigidbody DrivingRigidbody { get; private set; }

    public Vector3 MoveDirection { get; set; }
    public bool JumpState { get; set; }
    public float DistanceToGround { get; private set; }
    public bool IsGrounded => DistanceToGround < springDistance;

    private void Awake()
    {
        DrivingRigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        DistanceToGround = GetDistanceToGround();

        MoveCharacter();

        if (JumpState && !previousJumpState)
        {
            Jump();
        }
        previousJumpState = JumpState;

        ApplySpring();
        ApplyGravity();
    }

    private void ApplySpring()
    {
        if (IsGrounded && Time.time > lastJumpTime + jumpSpringPauseTime)
        {
            float contraction = 1.0f - (DistanceToGround / springDistance);
            DrivingRigidbody.velocity += Vector3.up * contraction * springForce * Time.deltaTime;
            DrivingRigidbody.velocity -= Vector3.up * DrivingRigidbody.velocity.y * springDamper * Time.deltaTime;
        }
    }

    private void ApplyGravity()
    {
        DrivingRigidbody.useGravity = false;
        DrivingRigidbody.velocity += GetGravity() * Time.deltaTime;
    }

    private void MoveCharacter()
    {
        if (IsGrounded)
        {
            float moveSpeed = this.moveSpeed.GetFor(this);
            Vector3 target = MoveDirection * moveSpeed;
            Vector3 current = DrivingRigidbody.velocity;

            Vector3 delta = Vector3.ClampMagnitude(target - current, moveSpeed);
            delta.y = 0.0f;

            Vector3 force = delta / moveSpeed * groundAcceleration.GetFor(this);

            DrivingRigidbody.velocity += force * Time.deltaTime;
        }
        else
        {
            DrivingRigidbody.velocity += MoveDirection * airMoveForce * Time.deltaTime;
        }
    }

    private void Jump()
    {
        if (IsGrounded)
        {
            float gravity = Vector3.Dot(Vector3.down, GetGravity());
            float jumpForce = Mathf.Sqrt(2.0f * gravity * jumpHeight);
            DrivingRigidbody.velocity = new Vector3(DrivingRigidbody.velocity.x, jumpForce, DrivingRigidbody.velocity.z);

            lastJumpTime = Time.time;

            JumpEvent?.Invoke();
        }
    }

    private Vector3 GetGravity()
    {
        float scale = upGravity;
        if (!JumpState)
        {
            scale = downGravity;
        }
        else if (DrivingRigidbody.velocity.y < 0.0f)
        {
            scale = downGravity;
        }

        return Physics.gravity * scale;
    }

    public float GetDistanceToGround()
    {
        if (Physics.SphereCast(DrivingRigidbody.position + Vector3.up * groundCheckRadius, groundCheckRadius, Vector3.down, out var hit, 1000.0f, groundCheckMask))
        {
            return hit.distance;
        }
        else return float.PositiveInfinity;
    }

    private void OnDrawGizmosSelected()
    {
        if (!DrivingRigidbody) DrivingRigidbody = GetComponent<Rigidbody>();
        float dist = GetDistanceToGround();
        Gizmos.color = dist < springDistance ? Color.green : Color.red;

        Gizmos.DrawRay(transform.position, Vector3.down * dist);

        Gizmos.color = Color.white;
    }
}
