using System.Collections;
using System.Collections.Generic;
using Game.Audio;
using UnityEngine;

public class OptionsMenu : MonoBehaviour
{
	// Start is called before the first frame update
	public void ChangeMasterVolume(float percent)
	{
		AudioManager.Instance.SetVolume(percent, AudioManager.AudioChannel.Master);
	}
	
	public void ChangeSFXVolume(float percent)
	{
		AudioManager.Instance.SetVolume(percent, AudioManager.AudioChannel.Sfx);
	}
	
	public void ChangeMusicVolume(float percent)
	{
		AudioManager.Instance.SetVolume(percent, AudioManager.AudioChannel.Music);
	}
}
