using System.Collections;
using UnityEngine;

public class ItemPickup : Interactable {

	public Item item;	// Item to put in the inventory on pickup
    public Money player;
	Inventory inv;

	public Item potion1;
	public Item potion2;
	public Item dagger;

	public GameObject text;

	public bool reappear = true;

    public void Start()
    {
        inv = Inventory.instance;
    }
    // When the player interacts with the item
    public override void Interact()
	{
		base.Interact();

		
        if (item.cost <= player.count)
        {
			if (!inv.items.Contains(item)) { 
				PickUp();
				player.count = player.count - item.cost;
				player.countText.text = "Gold: " + player.count.ToString();
            }
            else
            {
				if(item.name.Equals(potion1.name) || item.name.Equals(potion2.name))
                {
					PickUp();
					player.count = player.count - item.cost;
					player.countText.text = "Gold: " + player.count.ToString();
				}
            }


		}// Pick it up!

        else
        {
        }
	}

	// Pick up the item
	void PickUp ()
	{
		Debug.Log("Picking up " + item.name);
		bool wasPickedUp = Inventory.instance.Add(item);    // Add to inventory

		// If successfully picked up
		if (wasPickedUp)
			StartCoroutine(Picked());
			//Destroy(gameObject);    // Destroy item from scene
			//Destroy(text);
	}

	IEnumerator Picked()
    {
		gameObject.GetComponent<Collider>().enabled = false;
		gameObject.GetComponent<MeshRenderer>().enabled = false;

        if (!reappear)
        {
			text.SetActive(false);
		}

		yield return new WaitForSeconds(1f);

        if (reappear) { 
			gameObject.GetComponent<Collider>().enabled = true;
			gameObject.GetComponent<MeshRenderer>().enabled = true;
			gameObject.SetActive(true);
        }
	}

}
