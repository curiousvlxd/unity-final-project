using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using TMPro;
using UnityEngine;

public class MenuCanvas : MonoBehaviour
{
    // Start is called before the first frame update
    private GameStat gameStat;
    private TextMeshProUGUI totalScoreText; 
    private TextMeshProUGUI timeInGameText; 
    private TextMeshProUGUI checkpointsText;
    private TextMeshProUGUI startButtonText;
    
    public void SetTitleText(string text)
    {
        
    }

    public void SetStartButtonText(string text)
    {
        startButtonText.text = text;
    }
    void Start()
    {
        totalScoreText = gameObject.transform.Find("TotalScore").gameObject.GetComponent<TextMeshProUGUI>();
        timeInGameText = gameObject.transform.Find("Time").gameObject.GetComponent<TextMeshProUGUI>();
        checkpointsText = gameObject.transform.Find("Checkpoints").gameObject.GetComponent<TextMeshProUGUI>();
        startButtonText = gameObject.transform.Find("StartButton").GetChild(0).GetComponent<TextMeshProUGUI>();
        gameStat = GameObject.FindObjectOfType<GameStat>();
        UpdateCheckpoints();
        SetStartButtonText("Start");
    }

    // Update is called once per frame
    void Update()
    {
        totalScoreText.text = $"Total score: {gameStat.Score.ToString()}, n. {gameStat.GetScorePosition()} in top";
        timeInGameText.text = $"Time in game: {gameStat.TimeString}";
        

    }
    public string MakeCheckpointsString(List<GameObject> checkpoints)
    {
        string str = "";
        int i = 1;
        
        foreach (var obj in checkpoints)
        {
            str += $"Checkpoint {i.ToString()}: ";
            var cp = obj.transform.GetChild(0).gameObject.GetComponent<Checkpoint>();

            if (!cp.IsChecked && !cp.IsFailed) str += " in progress";
            else if (cp.IsFailed) str += " failed";
            else if (cp.IsChecked && !cp.IsFailed)
            {
                str += $"passed @ {cp.Timer % 60} s.";
            }

            str += '\n';
            i++;


        }
        return str;
        
    }
    public void UpdateCheckpoints()
    {
        
        
        checkpointsText.text = MakeCheckpointsString(gameStat.Checkpoints);
    }
 
    void Hide()
    {
        gameObject.SetActive(false);
        gameStat.IsRunning = true;
    }

    void Toggle()
    {
        gameObject.SetActive(gameObject.activeSelf);
    }
    
}
