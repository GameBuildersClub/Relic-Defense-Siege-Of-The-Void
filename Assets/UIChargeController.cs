using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIChargeController : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] float chargeCooldown = 5f;
    [SerializeField] float lastConsumptionTime = -5f;
    [SerializeField] bool charged = true;

    private void Awake()
    {
        if (anim == null)
        {
            anim = GetComponent<Animator>();
        }
    }

    private void FixedUpdate()
    {
        if (Time.fixedTime > lastConsumptionTime + chargeCooldown)
        {
            anim.SetBool("Charged", true);
        } else
        {
            anim.SetBool("Charged", false);
        }
    }

    public bool Charged()
    {
        return anim.GetBool("Charged");
    }
}
