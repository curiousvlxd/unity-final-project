using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private UnityEngine.UI.Image countdown;
    [SerializeField] private int countdownRate = 1;
    [SerializeField] float score = 100;
    private bool isActivated = false;
    private float countdownTime = 0;
    [SerializeField] private Gates gates;
    private GameStat gameStat;
    private float timer = 0;
    private bool isChecked = false;
    private bool isFailed = false;
    private MenuCanvas canvas; 
    
    public float Score
    {
        get => score; 
    }
    void Start()
    {
        gameStat = GameObject.FindObjectOfType<GameStat>();
        
    }

    public float Timer
    {
        get => timer; 
    }

    public bool IsChecked
    {
        get => isChecked;
    }

    public bool IsFailed
    {
        get => isFailed;
    }
    // Update is called once per frame
    void Update()
    {
        
        score -= Time.deltaTime*countdownRate;
        if(!isChecked && !isFailed) timer += Time.deltaTime;
        countdown.fillAmount = score/100;
        if (countdown.fillAmount <= 0)
        {
            isFailed = true;

        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        this.gameObject.SetActive(false);
        gates.Lower();
        countdown.fillAmount = 0;
        Debug.Log("collision");
        gameStat.Score += (int)this.Score;
        isChecked = true;
        try
        {
            canvas = GameObject.FindObjectOfType<MenuCanvas>().GetComponent<MenuCanvas>();
            canvas.UpdateCheckpoints();
        }
        catch {}
        

    }
}
