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
    public int health;

    public GameObject loseScreen;
    public GameObject winScreen;
    public GameObject loader;

    public GameObject health1;
    public GameObject health2;
    public GameObject health3;
    public GameObject health4;
    public GameObject health5;
    public GameObject health6;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        player.SendMessage("GetPenalty", SendMessageOptions.DontRequireReceiver);
        score = (Math.Round(player.transform.position.y) *10)- penalty;
        txtscore.text = "Score: "+ score.ToString();
        //SetHealth();
        HealthUI();

        //print("ui percent = " + gasPercent);

        gasMeter.fillAmount = gasPercent;
    }

    public void SetPenalty(double oopsie)
    {
        penalty = oopsie;
    }

    public void SetGasPercent(float percent)
    {
        gasPercent = 1-percent;
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

    public void SetHealth(int hp)
    {
        health = hp;
    }

    public void HealthUI()
    {
        if(health == 6)
        {

        }
        else if(health == 5)
        {
            health6.SetActive(false);
        }
        else if(health == 4)
        {
            health5.SetActive(false);
        }
        else if(health == 3)
        {
            health4.SetActive(false);
        }
        else if(health == 2)
        {
            health3.SetActive(false);
        }
        else if(health == 1)
        {
            health2.SetActive(false);
        }
        else if(health == 0)
        {
            health1.SetActive(false);
        }
    }
}
