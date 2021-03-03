using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerChunkSpawner : MonoBehaviour
{
    private TowerGenerator T;

    void Start()
    {
        T = GameObject.Find("cave").GetComponent<TowerGenerator>();
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))     //put this tag on a mid platform for all the chunks
        {
            //GetComponent<TowerGenerator>().SpawnChunk();
            //T.GetComponent<TowerGenerator>().SpawnChunk();
            T.SpawnChunk();
            gameObject.SetActive(false);
        }
    }
}
