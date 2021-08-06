using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OptionsColor : MonoBehaviour
{
    public TextMeshProUGUI[] settingsOptions;
    
    private static readonly string FullScreenPrefs = "FullScreenPrefs";
    private static readonly string MusicPrefs = "MusicPrefs";
    private static readonly string SoundEffectsPrefs = "SoundEffectsPrefs";

    // Update is called once per frame
    void Update()
    {
        settingsOptions[0].color = PlayerPrefs.GetInt(FullScreenPrefs) == 1 ? Color.red : Color.cyan;
        settingsOptions[1].color = PlayerPrefs.GetInt(MusicPrefs) == 1 ? Color.red : Color.cyan;
        settingsOptions[2].color = PlayerPrefs.GetInt(SoundEffectsPrefs) == 1 ? Color.red : Color.cyan;
    }
}
