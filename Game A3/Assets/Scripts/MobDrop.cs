using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MobDrop : MonoBehaviour
{
    public Slider health;
    public GameObject item;
    public GameObject itemParent;
    public GameObject canvas;

    bool alive = true;
    // Update is called once per frame
    void Update()
    {
        if (alive)
        {
            if(health.value == 0)
            {
                alive = false;
                DropItem();
            }
        }
    }
    
    void DropItem()
    {
        
        StartCoroutine(MoveItem());
    }

    IEnumerator MoveItem()
    {
        yield return new WaitForSeconds(0.5f);
        item.GetComponent<Collider>().enabled = true;
        itemParent.transform.SetParent(null);
        item.GetComponent<RotatorZ>().enabled = true;
        item.transform.localEulerAngles = Vector3.zero;
        item.transform.localScale = new Vector3(0.035f, 0.035f, 0.035f);
        canvas.transform.position = item.transform.position + Vector3.up;
        canvas.gameObject.SetActive(true);

        Vector3 finalPos = new Vector3(itemParent.transform.position.x, itemParent.transform.position.y + 0.75f, itemParent.transform.position.z);

        float elapsedTime = 0;
        float waitTime = 2f;
        while (elapsedTime < waitTime)
        {
            itemParent.transform.position = Vector3.Lerp(itemParent.transform.position, finalPos, (elapsedTime / waitTime));
            elapsedTime += Time.deltaTime;
        }
        yield return null;
    }
}
