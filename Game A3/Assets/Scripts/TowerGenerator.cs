﻿using System.Collections.Generic;
using UnityEngine;

public class TowerGenerator : MonoBehaviour     //put this script on the player for the tower level
{

    [SerializeField] private int maxChunkCount = 3; //set this to something like 3
    [SerializeField] private List<GameObject> chunks = new List<GameObject>();
    [SerializeField] private int yPosition = 9;
    public GameObject spawner;  //reference this to a platform around the middle, but will I be able to reference something that isnt in the game yet? or will i have to add a invisiable plane to go across the middle of each chunk?
    public GameObject collided;

    //public TowerChunkSpawner T;

    private List<GameObject> currentChunks = new List<GameObject>();
    private Vector3 currentPosition = new Vector3(23, 14, 3);

    void Start()
    {
        for (int i = 0; i < maxChunkCount; i++)
        {
            SpawnChunk();
        }
    }

    /*private void OnTriggerEnter(Collider other)   
    {
        collided = other.gameObject;
        if (collided.CompareTag("spawner"))     //put this tag on a mid platform for all the chunks
        {
            SpawnChunk();
            other.gameObject.SetActive(false);
        }
    }*/

    public void SpawnChunk()
    {
        GameObject terrain = Instantiate(chunks[Random.Range(0, chunks.Count)], currentPosition, Quaternion.identity);
        //GameObject terrain = Instantiate(chunks[Random.Range(0, 0)], currentPosition, Quaternion.identity);

        currentChunks.Add(terrain);
        if (currentChunks.Count > maxChunkCount)
        {
            Destroy(currentChunks[0]);
            currentChunks.RemoveAt(0);
        }
        currentPosition.y += yPosition;   //change this value to something like 50 to see where it will spawn the next chunk
    }
}


