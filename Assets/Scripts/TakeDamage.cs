using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TakeDamage : MonoBehaviour
{
    HealthBarUI healthBarUI = null;

    public UnityEvent onDied;

    CircleCollider2D circleCollider = null;

    void Start()
    {
        healthBarUI = GetComponentInChildren<HealthBarUI>();
        if(GetComponent<CircleCollider2D>() != null)
        {
            circleCollider = GetComponent<CircleCollider2D>();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        float damageReceived = other.gameObject.GetComponent<DealDamage>().DamageDealt;
        CharacterStat getStatComponent = gameObject.GetComponent<CharacterStat>();
        getStatComponent.Health -= damageReceived - (damageReceived * (getStatComponent.Shield / 100));
        if(getStatComponent.Health <= 0)
        {
            circleCollider.enabled = false;
        }

        if(healthBarUI != null)
        {
            healthBarUI.UpdateHealthBar(getStatComponent.Health);
            if(gameObject.name == "Player" &&getStatComponent.Health == 0)
            {
                onDied.Invoke();
            }
        }
    }



}
