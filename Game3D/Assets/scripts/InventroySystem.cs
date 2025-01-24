using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{

    public static InventorySystem Instance { get; set; }

    public GameObject inventoryScreenUI;
    public bool isOpen;

    public List<GameObject> slotList = new List<GameObject>();
    public List<string> itemList = new List<string>();

    private GameObject itemToAdd;
    private GameObject whatSlotToEquip;



    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }


    void Start()
    {
        isOpen = false;

        PopulateSlotList();

    }

    private void PopulateSlotList()
    {
        foreach (Transform child in inventoryScreenUI.transform)
        {
            if (child.CompareTag("Slot"))
            {
                slotList.Add(child.gameObject);
            }
        }
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.I) && !isOpen)
        {

            Debug.Log("i is pressed");
            inventoryScreenUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            isOpen = true;

        }
        else if (Input.GetKeyDown(KeyCode.I) && isOpen)
        {
            inventoryScreenUI.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            isOpen = false;
        }
    }

    public void AddToInventory(string itemName)
    {
        whatSlotToEquip = FindNextEmptySlot();

        if (whatSlotToEquip == null)
        {
            Debug.Log("Brak miejsca w ekwipunku!");
            return;
        }

        Debug.Log($"Dodawanie przedmiotu {itemName} do slotu {whatSlotToEquip.name}");
        itemToAdd = Instantiate(Resources.Load<GameObject>(itemName), whatSlotToEquip.transform.position, whatSlotToEquip.transform.rotation);
        itemToAdd.transform.SetParent(whatSlotToEquip.transform);

        itemList.Add(itemName);
        Debug.Log($"Przedmiot {itemName} zosta³ pomyœlnie dodany do ekwipunku.");
    }

    private GameObject FindNextEmptySlot()
    {
        foreach (GameObject slot in slotList)
        {
            if (slot.transform.childCount == 0) // Jeœli slot jest pusty
            {
                return slot; // Zwróæ pusty slot
            }
        }
        return null; // Jeœli wszystkie sloty s¹ zajête, zwróæ null
    }

    public bool CheckifFull()
    {
        foreach (GameObject slot in slotList)
        {
            if (slot.transform.childCount == 0) // Jeœli znajdzie pusty slot, ekwipunek nie jest pe³ny
            {
                return false;
            }
        }
        return true; // Jeœli ¿aden pusty slot nie zosta³ znaleziony, ekwipunek jest pe³ny
    }

}