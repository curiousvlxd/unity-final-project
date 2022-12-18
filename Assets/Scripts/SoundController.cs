using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using Slider = UnityEngine.UI.Slider;

public class SoundController : MonoBehaviour
{
    // Start is called before the first frame update
    private bool _soundOn = true;
    private Button toggle;
    private GameObject imageOn;
    private GameObject imageOff;
    private Slider slider;
    [SerializeField] private AudioSource musicPlayer;
    [SerializeField] private AudioSource effectsPlayer;

    private const string settingsFile = "settings.json"; 
    
    public bool SoundOn
    {
        get
        {
            return _soundOn;
        }
        set
        {
            _soundOn = value;
            if (value)
            {
                imageOff.SetActive(false);
                imageOn.SetActive(true);
                if (Volume == 0)
                {
                    Volume = 0.5f;
                    slider.value = 0.5f;
                }
            }
            else
            {
                imageOff.SetActive(true);
                imageOn.SetActive(false);
                Volume = 0;
                slider.value = 0;
            }
        }
    }

    private float _volume;

    
    public float Volume
    {
        get
        {
            return _volume;
        }
        set
        {
            _volume = value;
            slider.value = _volume;
            musicPlayer.volume = _volume;
            effectsPlayer.volume = _volume;

        }
    }

    void Start()
    {
        toggle = this.transform.GetChild(0).gameObject.GetComponent<Button>();
        slider = this.transform.GetChild(1).gameObject.GetComponent<Slider>();
        imageOn = toggle.transform.GetChild(0).gameObject;
        imageOff = toggle.transform.GetChild(1).gameObject;
        try
        {
            StreamReader sr = new StreamReader(settingsFile);
            string json = sr.ReadToEnd();
            SoundSettings settings = JsonUtility.FromJson<SoundSettings>(json);
            this.Volume = settings.volume;
            sr.Close();
        }
        catch
        {
            Debug.Log("No saved settings found");
            _volume = 0.5f;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ToggleSound()
    {
        SoundOn = !SoundOn;
    }

    public void OnSliderChange()
    {
        Volume = slider.value;
        if (Volume == 0)
        {
            SoundOn = false;
        }
        else
        {
            SoundOn = true;
        }
    }

    private void OnDestroy()
    {
        SoundSettings settings = new SoundSettings();
        settings.volume = this.Volume;
        StreamWriter sw = new StreamWriter(settingsFile);
        sw.Write(JsonUtility.ToJson(settings));
        sw.Close();
    }
}

[System.Serializable]
class SoundSettings
{
    [Serialize] public float volume;
}
