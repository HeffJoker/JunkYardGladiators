using UnityEngine;
using System.Collections;

public class ScoreOnDeath : MonoBehaviour {

	public int ScoreMag = 10;

	void OnDestroy()
	{
		Score.Instance.Increment(ScoreMag);
	}
}
