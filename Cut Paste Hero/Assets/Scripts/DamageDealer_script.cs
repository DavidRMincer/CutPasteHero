using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer_script : MonoBehaviour
{
    public int damage;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Enemy"))
        {
            other.GetComponent<HealthSystem_script>().AddHealth(-damage);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.collider.tag);
        if (collision.collider.CompareTag("Player") || collision.collider.CompareTag("Enemy"))
        {
            Debug.Log("Collide");
            collision.collider.GetComponent<HealthSystem_script>().AddHealth(-damage);
        }
    }
}
