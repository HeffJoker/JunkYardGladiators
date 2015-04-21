using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreManager : MonoBehaviour {

	public Text ScoreText;

	// Update is called once per frame
	void Update () 
    {
        if (ScoreText)
            ScoreText.text = Score.Instance.Value.ToString();
	}

    void Start()
    {
        if(!ScoreText)
            Debug.LogError("ScoreText not set in '" + gameObject.name + "'");
    }
}


public class Score
{
	private static Score instance = null;
	private int scoreVal = 0;

	public static Score Instance
	{
		get 
		{
			if(instance == null)
				instance = new Score();

			return instance;
		}
	}

	public int Value
	{
		get { return scoreVal; }
	}

	public void Increment(int value)
	{
		scoreVal += value;
	}

	public void Reset()
	{
		scoreVal = 0;
	}

	private Score()
	{}
}
