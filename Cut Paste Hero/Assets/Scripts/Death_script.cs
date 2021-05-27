using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class Death_script : MonoBehaviour
{
    public float destroyDelay;
    public bool player;
    public Fungus.Flowchart flowchart;
    public GameObject[] deactivates,
        activates,
        instantiates;
    public ParticleSystem particles;

    public void Die()
    {
        if (particles)
        {
            ParticleSystem newParticles = Instantiate(particles, transform.position, transform.rotation);
            newParticles.Play();
            Destroy(newParticles, destroyDelay);
        }

        foreach (var item in instantiates)
        {
            GameObject newItem = Instantiate(item, transform.position, transform.rotation);
            //transform.parent = newItem.transform;
            Debug.Log(newItem);
        }
        foreach (var item in activates)
        {
            item.SetActive(true);
        }
        foreach (var item in deactivates)
        {
            item.SetActive(false);
        }
        
        if (player)
        {
            flowchart.ExecuteBlock("Player Death");
            gameObject.SetActive(false);
        }
        else
            Destroy(gameObject, destroyDelay);
    }
}
