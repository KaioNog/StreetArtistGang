using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class playerData : MonoBehaviour
{
    public static playerData playerDataInstance;
    public TextMeshProUGUI score;
    private int numberScore = 0;

    private void Awake()
    {
        if(playerDataInstance == null)
        {
            playerDataInstance = this;
        }
    }

    public void AddScore(int m)
    {
        numberScore += m;
        score.text = "Pontos:  " + numberScore;
    }
}
