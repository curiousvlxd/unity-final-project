using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Camera switchCamera;
    [SerializeField] private Canvas canvas;
    private GameStat _gameStat;
    void Start()
    {
        _gameStat = GameObject.FindObjectOfType<GameStat>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("finish");

        canvas.gameObject.SetActive(true);
        canvas.GetComponent<MenuCanvas>().UpdateCheckpoints();
        canvas.GetComponent<MenuCanvas>().SetStartButtonText("Restart");
        _gameStat.IsRunning = false;
        _gameStat.SaveData();
    }
}
