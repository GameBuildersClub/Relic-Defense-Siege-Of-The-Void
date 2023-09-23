using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator), typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] protected Animator anim;
    [SerializeField] protected Rigidbody2D rb;

    [SerializeField] protected float speed = 10;

    private void Awake()
    {
        if (!anim)
            anim = GetComponent<Animator>();
        if (!rb)
            rb = GetComponent<Rigidbody2D>();
    }

    public void Move(InputAction.CallbackContext input)
    {
        Vector2 direction = input.ReadValue<Vector2>();
        rb.velocity = direction * speed;
    }

    private void FixedUpdate()
    {

    }
}
