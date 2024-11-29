using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    
    
    public string ItemName;
    public bool PlayerInRange;


    public string GetItemName()
    {
        return ItemName;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && PlayerInRange && SelectionManager.instance.onTarget)
        {
            Debug.Log("podniesiono obiekt");
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") )
        {
            PlayerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") )
        {
            PlayerInRange = false;
        }
    }
}