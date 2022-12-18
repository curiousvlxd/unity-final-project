using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Events : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject canvas;
    [SerializeField] private Camera switchCamera;

    private GameStat gameStat;
    void Start()
    {
        gameStat = GameObject.FindObjectOfType<GameStat>();
        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            
            canvas.SetActive(!canvas.activeSelf);
            canvas.GetComponent<MenuCanvas>().UpdateCheckpoints();
            canvas.GetComponent<MenuCanvas>().SetStartButtonText("Continue");
            gameStat.IsRunning = !gameStat.IsRunning;
            switchCamera.enabled = !switchCamera.enabled;
            switchCamera.gameObject.SetActive(switchCamera.gameObject.activeSelf);
            
        }
    }
}
