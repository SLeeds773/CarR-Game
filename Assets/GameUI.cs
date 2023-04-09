using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public TMP_Text txtscore;
    public double score;
    public Transform player;
    private double penalty;
    public GameObject playerobject;
    public float gasPercent;
    public Image gasMeter;

    public GameObject loseScreen;
    public GameObject winScreen;
    public GameObject loader;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        player.SendMessage("GetPenalty", SendMessageOptions.DontRequireReceiver);
        score = Math.Round(player.transform.position.y) - penalty;
        txtscore.text = "Score: "+ score.ToString();
        
        //print("ui percent = " + gasPercent);

        gasMeter.fillAmount = gasPercent;
    }

    public void SetPenalty(double oopsie)
    {
        penalty = oopsie;
    }

    public void SetGasPercent(float percent)
    {
        gasPercent = percent;
    }

    public void WinText()
    {
        winScreen.SetActive(true);
    }


    public void LoseText()
    {
        loseScreen.SetActive(true);
        print("lose text");
    }
}
