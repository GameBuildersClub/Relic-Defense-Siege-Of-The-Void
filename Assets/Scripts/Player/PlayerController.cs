using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator), typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] protected Animator anim;
    [SerializeField] protected Rigidbody2D rb;

    [SerializeField] protected float speed = 10;
    [SerializeField] protected float rollSpeed = 15;
    [SerializeField] protected Vector2 inputVelocity = Vector2.zero;
    [SerializeField] protected Vector2 cachedDirection = Vector2.zero;
    [SerializeField] protected bool flipped = false;

    [SerializeField] protected bool shouldPunch = false;
    [SerializeField] protected bool shouldBlock = false;
    [SerializeField] protected bool shouldRoll = false;
    [SerializeField] protected float blockDuration = 0.5f;
    [SerializeField] protected float blockFinishTime = 0f;

    public bool Punching { get { return anim.GetCurrentAnimatorStateInfo(0).IsName("Punch") || anim.GetCurrentAnimatorStateInfo(0).IsName("PunchPowered"); } }
    public bool BlockReady { get { return anim.GetCurrentAnimatorStateInfo(0).IsName("Block"); } }
    public bool Blocking { get { return anim.GetCurrentAnimatorStateInfo(0).IsName("Block") || FinishingBlock; } }
    public bool FinishingBlock { get { return anim.GetCurrentAnimatorStateInfo(0).IsName("BlockFinish"); } }
    public bool Rolling { get { return anim.GetCurrentAnimatorStateInfo(0).IsName("Rolling"); } }
    public bool CanAct { get { return !Punching && !Blocking && !Rolling; } }

    private void Awake()
    {
        if (!anim)
            anim = GetComponent<Animator>();
        if (!rb)
            rb = GetComponent<Rigidbody2D>();
    }

    public void Move(InputAction.CallbackContext input)
    {
        if (input.canceled)
            inputVelocity = Vector3.zero;
        else
        {
            Vector2 direction = input.ReadValue<Vector2>();
            inputVelocity = direction * speed;
        }
    }

    public void TrySetMove()
    {
        if (CanAct)
        {
            rb.velocity = inputVelocity;
            anim.SetFloat("Speed", rb.velocity.magnitude);
            CheckFlip();
        }
        else if (Rolling)
        {
            rb.velocity = cachedDirection * rollSpeed;
        }
        else
        {
            rb.velocity = Vector2.zero;
            anim.SetFloat("Speed", 0);
        }
    }

    public void CheckFlip()
    {
        bool shouldFlip = false;
        if (!flipped && rb.velocity.x < 0)
        {
            shouldFlip = true;
        }
        else if (flipped && rb.velocity.x > 0)
        {
            shouldFlip = true;
        }

        if (shouldFlip)
            Flip();
    }

    protected void Flip()
    {
        flipped = !flipped;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    public void Punch(InputAction.CallbackContext input)
    {
        if (input.started)
        {
            shouldPunch = true;
        }
    }

    public void Block(InputAction.CallbackContext input)
    {
        if (input.started)
        {
            shouldBlock = true;
        }
    }

    public void Roll(InputAction.CallbackContext input)
    {
        if (input.started)
        {
            shouldRoll = true;
        }
    }

    private void FixedUpdate()
    {
        TrySetMove();

        bool triggeredAction = false;

        if (shouldPunch && CanAct)
        {
            anim.SetTrigger("Punch");
            anim.SetBool("Powered", true);
            shouldPunch = false;
            triggeredAction = true;
        }
        else if (shouldBlock && CanAct)
        {
            anim.SetTrigger("Block");
            blockFinishTime = Time.fixedTime + blockDuration;
            shouldBlock = false;
            triggeredAction = true;
        } else if (shouldRoll && CanAct)
        {
            anim.SetTrigger("Roll");
            cachedDirection = inputVelocity.magnitude != 0 ? inputVelocity.normalized : flipped ? new Vector2(-1, 0) : new Vector2(1, 0);
            shouldRoll = false;
            triggeredAction = true;
        }

        if (BlockReady && Time.fixedTime > blockFinishTime)
        {
            anim.SetTrigger("BlockFinished");
        }

        if (triggeredAction)
        {
            shouldPunch = false;
            shouldBlock = false;
            shouldRoll = false;
        }

    }
}
