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

    public int health;

    public GameObject screw1;
    public GameObject screw2;
    public GameObject screw3;
    public GameObject screw4;
    public GameObject screw5;
    public GameObject screw6;


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
    public void SetHealth()
    {
        health-=1;

        if(health == 5)
        {
            Destroy(screw6.gameObject);
        }
        if(health == 4)
        {
            Destroy(screw5.gameObject);
        }
        if(health == 3)
        {
            Destroy(screw4.gameObject);
        }
        if(health == 2)
        {
            Destroy(screw3.gameObject);
        }
        if(health == 1)
        {
            Destroy(screw2.gameObject);
        }
        if(health == 0)
        {
            Destroy(screw1.gameObject);
        }

    }
}
