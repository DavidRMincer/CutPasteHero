using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI_script : MonoBehaviour
{
    public float walkspeed,
        viewDistance,
        attackReach;
    public GameObject target;

    internal Rigidbody rb;
    internal Vector3 moveVec;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        moveVec = Vector3.zero + (Vector3.up * rb.velocity.y);
    }

    private void Update()
    {
        moveVec = Vector3.zero;
        moveVec.y = rb.velocity.y;

        Debug.Log(TargetinRange(viewDistance));
        if (TargetinRange(viewDistance))
        {
            Pursue();
        }
    }

    public virtual void Attack()
    {

    }

    private void Pursue()
    {
        moveVec = (target.transform.position - transform.position).normalized * walkspeed;

        moveVec.y = rb.velocity.y;
        Debug.Log(moveVec);
        rb.velocity = moveVec;
    }

    private bool TargetinRange(float range)
    {
        Vector3 distance = target.transform.position - transform.position;

        return range >= distance.magnitude;
    }
}
