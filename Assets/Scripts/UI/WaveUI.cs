using System.Collections;
using System.Collections.Generic;
using Game.Wave;
using UnityEngine;
using UnityEngine.UI;

public class WaveUI : MonoBehaviour{
    public Text waveText;

    private void Update()
    {
        waveText.text = "Completed Waves: " + WaveManager.Instance.NumWavesCompleted +"\nEnemies remaining: " + WaveManager.Instance.GetNumRemainingEnemies();
    }
}
