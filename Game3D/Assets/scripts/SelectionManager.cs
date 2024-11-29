using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionManager : MonoBehaviour
{
    public static SelectionManager instance { get; set; }

    public bool onTarget;

    public GameObject interaction_Info_UI;
    Text interaction_text;

    private void Start()
    {
        interaction_text = interaction_Info_UI.GetComponent<Text>();
        onTarget = false;
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            var selectionTransform = hit.transform;

            InteractableObject Interactable = selectionTransform.GetComponent<InteractableObject>();

            if (Interactable && Interactable.PlayerInRange)
            {
                onTarget = true;

                interaction_text.text = Interactable.GetItemName();
                interaction_Info_UI.SetActive(true);

            }
            else
            {
                onTarget= false; 
                interaction_Info_UI.SetActive(false);
            }

        }
        else
        {
            onTarget = false;
            interaction_Info_UI.SetActive(false);
        }
    }
}