using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public static PlayerState Instance { get; set; }

    // -------- Player Health -------- //
    public float currentHealth;
    public float maxHealth;

    float distanceTravelled = 0;
    Vector3 lastPosition;
    public GameObject playerBody;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        Debug.Log($"Current Health: {currentHealth}/{maxHealth}");

        // Obs³uga wciœniêcia klawisza N
        if (Input.GetKeyDown(KeyCode.N))
        {
            currentHealth -= 10;
            if (currentHealth < 0)
            {
                currentHealth = 0; // Zapobieganie ujemnym wartoœciom
                
            }
        }

        if (IsDead())
        {
            Debug.Log("Player is dead!");
            // Tutaj mo¿esz dodaæ np. restart gry
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }
    }

    public void Heal(float amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    public bool IsDead()
    {
        return currentHealth <= 0;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Debug.Log("Player is dead");
        }
        else
        {
            Debug.Log("Player is hurt");
        }
    }

}
