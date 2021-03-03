using UnityEngine;
using UnityEngine.UI;

/* The base item class. All items should derive from this. */

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject {

    
	new public string name = "New Item";	// Name of the item
	public Sprite icon = null;				// Item icon
	public bool isDefaultItem = false;      // Is the item default wear?
    public int cost;

	// Called when the item is pressed in the inventory
	public virtual void Use ()
	{
        // Use the item
        // Something might happen
        if (name.Equals("HealthPotion"))
        {
			GameObject.Find("PlayerStatsCanvas").transform.GetChild(0).GetComponent<Slider>().value += 25;
			RemoveFromInventory();
		}
		if(name.Equals("Armour potion"))
        {
			GameObject.Find("PlayerStatsCanvas").transform.GetChild(1).GetComponent<Slider>().value += 25;
			RemoveFromInventory();
		}

		//Debug.Log("Using " + name);
	}

	public void RemoveFromInventory ()
	{
		Inventory.instance.Remove(this);
	}
	
}
