using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameUI : MonoBehaviour
{
    public TMP_Text txtscore;
    public double score;
    public Transform player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        score = Math.Round(player.transform.position.y);
        txtscore.text = "Score: "+ score.ToString();
    }
}
