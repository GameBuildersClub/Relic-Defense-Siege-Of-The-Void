using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float speed = 5;
    [SerializeField] Vector3 targetVelocity;
    [SerializeField] float health = 100;
    [SerializeField] UIHealthController healthController;
    [SerializeField] UIChargeController chargeController;

    private void Awake()
    {
        healthController.UpdateHealth(health);
        Debug.Log("Player health is " + health);
    }

    public void Move(InputAction.CallbackContext ctxt)
    {
        Vector2 direction = ctxt.ReadValue<Vector2>();
        bool isMoving = direction.magnitude > 0;
        anim.SetBool("Running", isMoving);
        targetVelocity = direction * speed;

        if (direction.x < 0)
        {
            Vector3 scale = transform.localScale;
            scale.x = -1;
            transform.localScale = scale;
        } else if (direction.x > 0)
        {
            Vector3 scale = transform.localScale;
            scale.x = 1;
            transform.localScale = scale;
        }
    }

    public void Punch(InputAction.CallbackContext ctxt)
    {
        if (ctxt.started)
        {
            anim.SetTrigger("Punch");
        }
    }

    private void FixedUpdate()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("WiseOnePunch"))
        {
            rb.velocity = Vector3.zero;
        } else
        {
            rb.velocity = targetVelocity;
        }
    }

    public void Damage(float damage)
    {
        health -= damage;
        healthController.UpdateHealth(health);
        Debug.Log("Player health is " + health);
    }

    public bool PunchCharged()
    {
        return chargeController.Charged();
    }
}
