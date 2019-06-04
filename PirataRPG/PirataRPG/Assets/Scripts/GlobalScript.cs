using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalScript : MonoBehaviour
{
    public int LeftScore;
    public int RightScore;
    public TextMesh LeftScoreText;
    public TextMesh RightScoreText;
    // Start is called before the first frame update
    void Start()
    {
        LeftScore = 0;
        RightScore = 0;


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IncrementScore(bool IsLeftPlayer)
    {
        if (IsLeftPlayer)
        {
            LeftScore++;
            LeftScoreText.text = LeftScore.ToString();
        }

        else
        {
            RightScore++;
            RightScoreText.text = RightScore.ToString();
        }

    }
}
