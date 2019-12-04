using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // this is how we get and access the UI elements such as text and whatnot 

public class ScoreBoard : MonoBehaviour
{
    [SerializeField] int scoreIncrease = 12;

    int score = 0;
    Text scoreText;

    // Start is called before the first frame update
    void Start()
    {
        scoreText = GetComponent<Text>();
        scoreText.text = score.ToString();
    }

    public void ScoreHit(int scoreIncrease) 
    {
        score = score + scoreIncrease;
        scoreText.text = score.ToString();
    }
}
