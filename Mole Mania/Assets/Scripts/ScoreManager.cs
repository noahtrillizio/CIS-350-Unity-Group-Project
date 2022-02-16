/*
 * Jacob Zydorowicz
 * Project 2 Mole Mania
 * Controls display of score
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
 
    public TextMesh scoreText;
    public int score = 0;
  
    void Update()
    {
        //scoreText.text = "Score: " +score;
    }
}
