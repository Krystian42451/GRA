using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animal : MonoBehaviour
{
    public string animalName;
    public bool playerInRange;

    [SerializeField] int currentHealthl;
    [SerializeField] int maxHealth;

    private Animator animator;
    public bool isDead;

    enum AnimalType
    {   
        Bear    
    }

    [SerializeField] AnimalType thisAnimalType;
    
    // Start is called before the first frame update
    void Start()
    {
        currentHealthl = maxHealth;
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        if (isDead == false)
        {
            currentHealthl -= damage;

            animator.SetTrigger("DIE");
            isDead = true;

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
    
}
