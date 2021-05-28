using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem_script : MonoBehaviour
{
    public int maxHealth;
    public UIManager_script ui;
    public float damageDelay;
    public Death_script death;

    private int _health;
    private float _damageCounter;

    private void Start()
    {
        _health = maxHealth;
        _damageCounter = 0f;

        if (ui)
            ui.UpdateHealth(_health);
    }

    private void Update()
    {
        if (_damageCounter > 0f)
            _damageCounter -= Time.deltaTime;
    }

    public int GetHealth()
    {
        return _health;
    }

    public void AddHealth(int additionalHealth)
    {
        Debug.Log("Deal Damage");
        if (additionalHealth > 0 || _damageCounter <= 0f)
        {
            _health += additionalHealth;

            if (_health > maxHealth)
                _health = maxHealth;
            else if (_health < 0)
                _health = 0;

            if (ui)
                ui.UpdateHealth(_health);

            if (additionalHealth < 0)
                _damageCounter = damageDelay;
        }

        if (_health == 0)
        {
            death.Die();
        }
    }
}
