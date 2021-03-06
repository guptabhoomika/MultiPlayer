﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameManager : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject canvas;
    public GameObject sceneCam;
    private void Awake()
    {
        canvas.SetActive(true); 
    }
    public void  SpawnPlayer()
    {
        float randVal = Random.Range(-5, 5);
        PhotonNetwork.Instantiate(playerPrefab.name, new Vector2(playerPrefab.transform.position.x * randVal, playerPrefab.transform.position.y), Quaternion.identity,0);
        canvas.SetActive(false);
        sceneCam.SetActive(false);
    
    }

}
