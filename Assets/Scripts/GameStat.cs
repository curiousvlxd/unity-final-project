using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class GameStat : MonoBehaviour
{
    // Start is called before the first frame update
    public int Score { get; set; }
    private float  gameTime;
    private List<GameObject> checkpoints = new List<GameObject>();
    private bool _isRunning = false;
    private List<int> _scores = new List<int>();
    private const string MaxScoreFilename = "maxdata.sav";
    public bool IsRunning
    {
        get
        {
            return _isRunning;
        }
        set
        {
            _isRunning = value;
            if (!value)
            {
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
            }
        }
    }

    private void SortScores(List<int> scores)
    {
        scores.Sort(new Comparison<int>((i, i1) =>
        {
            if (i < i1) return 1;
            else if (i > i1) return -1;
            return 0;
        }));
    }
    public int GetScorePosition()
    {
        int[] testArray =new int[_scores.Count];
        _scores.CopyTo(testArray);
        List<int> testScores = new List<int>(testArray);
        
        testScores.Add(Score);
        SortScores(testScores);
        
        int pos = testScores.FindIndex((i => i==Score))+1;
        return pos;
    }
    void Start()
    {
        checkpoints.Add(GameObject.Find("Checkpoint 0"));
        checkpoints.Add(GameObject.Find("Checkpoint 1"));
        checkpoints.Add(GameObject.Find("Checkpoint 2"));
        checkpoints.Add(GameObject.Find("Checkpoint 3"));
        
        if (System.IO.File.Exists(MaxScoreFilename))
        {
            string[] lines = 
                System.IO.File.ReadAllLines(MaxScoreFilename, System.Text.Encoding.UTF8);

            foreach(string line in lines)
            {
                try
                {
                    _scores.Add(Int32.Parse(line));
                }
                catch
                {
                    _scores.Add(0);
                }
            }
        }
        else
        {
            System.IO.File.WriteAllText(MaxScoreFilename, "0\n0");
            
        }
        
    }

    public List<GameObject> Checkpoints
    {
        get { return checkpoints; }
    }
    public float GameTime { 
        get => gameTime;
        set
        {
            gameTime = value;
            
        }
    }
    
    
    public string TimeString
    {
        get
        {
            return Utils.MakeTimeString(gameTime);
        }
    }

    // Update is called once per frame
    void Update()
    {
        GameTime += Time.deltaTime;
    }

    public void SaveData()
    {
        SortScores(_scores);
        var sw = System.IO.File.AppendText(MaxScoreFilename);
        foreach (var score in _scores)
        {
            sw.WriteLine(score);
        }
        sw.Close();
    }
}
