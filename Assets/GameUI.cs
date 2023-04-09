using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public int health;
    public GameObject bolt1;
    public GameObject bolt2;
    public GameObject bolt3;
    public GameObject bolt4;
    public GameObject bolt5;
    public GameObject bolt6;


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
<<<<<<< Updated upstream
=======
<<<<<<< HEAD
    public void SetHealth()
    {
        health-=1;

        if(health == 6)
        {
           Destroy(bolt6.gameObject);
        }
        if(health == 5)
        {
           Destroy(bolt5.gameObject);
        }
        if(health == 4)
        {
           Destroy(bolt4.gameObject);
        }
        if(health == 3)
        {
           Destroy(bolt3.gameObject);
        }
        if(health == 2)
        {
           Destroy(bolt2.gameObject);
        }
        if(health == 1)
        {
           Destroy(bolt1.gameObject);
        }
=======
>>>>>>> Stashed changes

    public void WinText()
    {
        winScreen.SetActive(true);
    }


    public void LoseText()
    {
        loseScreen.SetActive(true);
        print("lose text");
<<<<<<< Updated upstream
=======
>>>>>>> df590a9adf5fbcb03c81cffb9ac808aacf8d49e7
>>>>>>> Stashed changes
    }
}
