using UnityEngine;

/* This object updates the inventory UI. */

public class InventoryUI : MonoBehaviour {

	public Transform itemsParent;	// The parent object of all the items
	public GameObject inventoryUI;  // The entire UI

	public Item IceSword;
	public Item Dagger;
	public Item Shield;
	public Item HealthPotion;
	public Item ArmourPotion;

	Inventory inventory;	// Our current inventory

	InventorySlot[] slots;	// List of all the slots

	void Start () {
		inventory = Inventory.instance;
		inventory.items.Clear();

		string itemListStr = PlayerPrefs.GetString("Inventory");
		foreach(string s in itemListStr.Split('|'))
        {
            if (!s.Equals("")) { 
                switch (s)
                {
					case "HolySword":
						inventory.items.Add(IceSword);
						break;
					case "Dagger":
                        if (!inventory.items.Contains(Dagger)) { 
							inventory.items.Add(Dagger);
						}
						break;
					case "Shield":
						inventory.items.Add(Shield);
						break;
					case "HealthPotion":
						inventory.items.Add(HealthPotion);
						break;
					case "Armour potion":
						inventory.items.Add(ArmourPotion);
						break;
				}
					
			}
		}
		inventory.onItemChangedCallback += UpdateUI;	// Subscribe to the onItemChanged callback

		// Populate our slots array
		slots = itemsParent.GetComponentsInChildren<InventorySlot>();
		UpdateUI();
	}
	
	void Update () {
		// Check to see if we should open/close the inventory
		if (Input.GetButtonDown("Inventory"))
		{
			inventoryUI.SetActive(!inventoryUI.activeSelf);

			Cursor.visible = !Cursor.visible;
			if(Cursor.lockState == CursorLockMode.None)
            {
				Cursor.lockState = CursorLockMode.Locked;
            }
            else { 
				Cursor.lockState = CursorLockMode.None;
			}
		}

	}

	// Update the inventory UI by:
	//		- Adding items
	//		- Clearing empty slots
	// This is called using a delegate on the Inventory.
	void UpdateUI ()
	{
		// Loop through all the slots
		for (int i = 0; i < slots.Length; i++)
		{
			if (i < inventory.items.Count)	// If there is an item to add
			{
				string itemsListStr = "";
				foreach(Item x in inventory.items)
                {
					itemsListStr += x.name + "|";
                }
				PlayerPrefs.SetString("Inventory", itemsListStr);

				slots[i].AddItem(inventory.items[i]);	// Add it
			} else
			{
				// Otherwise clear the slot
				slots[i].ClearSlot();
			}
		}
	}
}
