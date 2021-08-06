﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsController : MonoBehaviour
{
   private static readonly string FullScreenPrefs = "FullScreenPrefs";
   private static readonly string MusicPrefs = "MusicPrefs";
   private static readonly string SoundEffectsPrefs = "SoundEffectsPrefs";

   private void Awake()
   {
      SetSettings();
   }

   public void ToggleFullScreen()
   {
      Screen.fullScreen = !Screen.fullScreen;

      PlayerPrefs.SetInt(FullScreenPrefs, Screen.fullScreen ? 1 : 0);   //Saves FullScreen On Playerprefs
   }

   public void ToggleMusic()
   {
      PlayerPrefs.SetInt(MusicPrefs, PlayerPrefs.GetInt(MusicPrefs) == 0 ? 1 : 0);   //Saves Music On/Off on PlayerPrefs
   }

   public void ToggleSFX()
   {
      PlayerPrefs.SetInt(SoundEffectsPrefs, PlayerPrefs.GetInt(SoundEffectsPrefs) == 0 ? 1 : 0);    //Saves SFX On/Off on PlayerPrefs
   }

   private void  SetSettings()
   {
      Screen.fullScreen = PlayerPrefs.GetInt(FullScreenPrefs) == 1;
   }
}
