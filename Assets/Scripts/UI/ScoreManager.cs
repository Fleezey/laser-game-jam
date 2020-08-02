using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : Singleton<ScoreManager>
{
	private float m_score;
	// Start is called before the first frame update
	void Start()
	{
		Instance.m_score = 0;
	}

	public void AddScore(float val)
	{
		Instance.m_score += val;
	}
	public float GetScore()
	{
		return Instance.m_score;
	}
}


