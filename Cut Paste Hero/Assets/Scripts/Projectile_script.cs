using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_script : MonoBehaviour
{
    public enum OwnerEnum
    {
        PLAYER, ENEMY
    }

    public float speed,
                    life;
    public int damage;
    public Transform directionPointer;
    
    private float _remainingLife;
    private Rigidbody _rb;
    private OwnerEnum _owner;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();

        _remainingLife = life;
        _rb.velocity = directionPointer.forward * speed;
    }

    private void Update()
    {
        if (gameObject.activeInHierarchy)
        {
            if (_remainingLife > 0f)
            {
                _remainingLife -= Time.deltaTime;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch (_owner)
        {
            case OwnerEnum.PLAYER:
                if (collision.collider.CompareTag("Enemy"))
                {
                    collision.collider.gameObject.GetComponent<HealthSystem_script>().AddHealth(-damage);
                    Destroy(gameObject);
                }
                break;
            case OwnerEnum.ENEMY:
                if (collision.collider.CompareTag("Player"))
                {
                    collision.collider.gameObject.GetComponent<HealthSystem_script>().AddHealth(-damage);
                    Destroy(gameObject);
                }
                break;
            default:
                break;
        }
    }

    public void SetOwner(OwnerEnum owner)
    {
        _owner = owner;
    }
}
