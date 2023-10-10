using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float speed = 5;

    public void Move(InputAction.CallbackContext ctxt)
    {
        Vector2 direction = ctxt.ReadValue<Vector2>();
        bool isMoving = direction.magnitude > 0;
        anim.SetBool("Running", isMoving);
        rb.velocity = direction * speed;
    }
}
