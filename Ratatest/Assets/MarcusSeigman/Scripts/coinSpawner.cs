﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinSpawner : MonoBehaviour {

    public int maxCoin = 5;
    public float chanceToSpawn = .5f;
    public bool forceSpawnAll = true;


    private GameObject[] coins;

    private void Awake()
    {
        coins = new GameObject[transform.childCount];
            for (int i = 0; i < transform.childCount; i++)
        {
            coins[i] = transform.GetChild(i).gameObject;
        }
        OnDisable();
    }

    private void OnEnable()
    {
        if (Random.Range(0, 1) > chanceToSpawn)
            return;
        if (forceSpawnAll)
            for (int i = 0; i < maxCoin; i++)
            {
                coins[i].SetActive(true);
                coins[i].GetComponent<MeshRenderer>().enabled = true;
            }
        else
        {
            int r = Random.Range(0, maxCoin);
            for (int i = 0; i < r; i++)
            {
                int k = Random.Range(0, maxCoin);
                    coins[k].SetActive(true);
                coins[k].GetComponentInChildren<Transform>().gameObject.SetActive(true);
            }
        }
    }

    private void OnDisable()
    {
        foreach(GameObject go in coins)
        {
            go.SetActive(false);
        }
    }
}
