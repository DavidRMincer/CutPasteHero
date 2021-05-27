using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI_script : MonoBehaviour
{
    public float walkspeed,
                    viewDistance,
                    attackReach,
                    stoppingDistance,
                    firingDelay;
    public GameObject target,
                        projectilePrefab;
    public Transform firingPoint;
    public bool canFly;

    internal Rigidbody rb;
    internal Vector3 moveVec;

    private float _firingCounter;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        moveVec = Vector3.zero + (Vector3.up * rb.velocity.y);
        _firingCounter = firingDelay;
    }

    private void Update()
    {
        transform.rotation = Quaternion.LookRotation((target.transform.position - firingPoint.transform.position).normalized, Vector3.up);

        moveVec = Vector3.zero;
        if (!canFly)
            moveVec.y = rb.velocity.y;

        if (_firingCounter > 0f)
        {
            _firingCounter -= Time.deltaTime;
        }
        
        if (TargetinRange(viewDistance) && !TargetinRange(stoppingDistance))
        {
            Pursue();
        }
        if (TargetinRange(attackReach))
        {
            Attack();
        }

        rb.velocity = moveVec;
    }

    public virtual void Attack()
    {
        if (_firingCounter <= 0f)
        {
            GameObject newProjectile = Instantiate(projectilePrefab, firingPoint.position, firingPoint.transform.rotation);
            newProjectile.GetComponent<Projectile_script>().SetOwner(Projectile_script.OwnerEnum.ENEMY);
            _firingCounter = firingDelay;
        }
    }

    private void Pursue()
    {
        moveVec = (target.transform.position - transform.position).normalized * walkspeed;

        if (!canFly)
            moveVec.y = rb.velocity.y;
    }

    private bool TargetinRange(float range)
    {
        Vector3 distance = target.transform.position - transform.position;

        return range >= distance.magnitude;
    }
}
