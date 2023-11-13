using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public static event Action OnPlayerDamaged;
    public static event Action OnPlayerDeath;

    public float Health, maxHealth;

    private void Start()
    {
        Health = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        Health -= amount;
        OnPlayerDamaged?.Invoke();
        
        if (Health <= 0 )
        {
            Health = 0;
            Debug.Log("you're dead");
            OnPlayerDeath?.Invoke();
        }
    }
}
