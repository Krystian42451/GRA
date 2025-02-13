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
        Debug.Log($"Przedmiot {itemName} zosta� pomy�lnie dodany do ekwipunku.");
    }

    private GameObject FindNextEmptySlot()
    {
        foreach (GameObject slot in slotList)
        {
            if (slot.transform.childCount == 0) // Je�li slot jest pusty
            {
                return slot; // Zwr�� pusty slot
            }
        }
        return null; // Je�li wszystkie sloty s� zaj�te, zwr�� null
    }

    public bool CheckifFull()
    {
        foreach (GameObject slot in slotList)
        {
            if (slot.transform.childCount == 0) // Je�li znajdzie pusty slot, ekwipunek nie jest pe�ny
            {
                return false;
            }
        }
        return true; // Je�li �aden pusty slot nie zosta� znaleziony, ekwipunek jest pe�ny
    }

}