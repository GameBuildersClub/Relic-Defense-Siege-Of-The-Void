using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator), typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] protected Animator anim;
    [SerializeField] protected Rigidbody2D rb;

    [SerializeField] protected float speed = 10;
    [SerializeField] protected bool flipped = false;

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
            rb.velocity = Vector3.zero;
        else
        {
            Vector2 direction = input.ReadValue<Vector2>();
            rb.velocity = direction * speed;
        }
        anim.SetFloat("Speed", rb.velocity.magnitude);
        CheckFlip();
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

    private void FixedUpdate()
    {

    }
}
