using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCar : MonoBehaviour
{
    public bool alive;
    public GameObject destroyer;
    public GameObject playerUI;
    
    public new Camera camera;
    public Animator animator;
    public Rigidbody2D rb;
    public Rigidbody2D Car;
    public int movespeed;
    Vector2 mousePos;

    public int gear = 0;

    Vector3 movement;

    public float time;
    public float gas;
    public float gasTime;
    public double penalty;
    public float gasPercent;
    public bool InControl;
    public float spinOutTime;
    public float spinOutTimeMarker;

    public Vector3 userDirection = Vector3.one;



    public int maxHealth = 5;
    public int Health;
    public bool isInvincible;
    public float iFrames;
    public float iFrameMarker;
    public bool isAirborn;


    public AudioSource musicIntro;
    public AudioSource musicLoop;
    public AudioSource vroom;

    public float songtimer;
    private float songCount = 7.35f;
    private int song1Change = 0;


    public GameObject smoke1;
    public GameObject smoke2;
    public GameObject fire;    

    void Start()
    {
        alive=true;
        musicIntro.Play();
        Health = maxHealth;
        animator.SetFloat("Health", maxHealth);
    }


    public void Update()
    {
        time +=Time.deltaTime;
        gasTime += Time.deltaTime;
        //animator.SetFloat("Health", Health);

        //gas percent used for meter, getting hit lowers gas (adds to time)
        //change all time to gas

        gasPercent = gasTime / gas;



        ////////////////////////////
        ///Spin Out
        ////////////////////////////

        if(InControl==false)
        {
            spinOutTime += Time.deltaTime;
            //print("No Control");
            if(spinOutTime >= spinOutTimeMarker)
            {
                vroom.Play();
                InControl=true;
                spinOutTime=0;
                spinOutTimeMarker=0;
                if(Health >= 5)
                {
                    animator.Play("Driving");
                }
                if (Health <= 4)
                {
                    animator.Play("Driving hurt 1");

                }
                if (Health <= 2)
                {
                    animator.Play("Driving hurt 2");
                }
            }
        }

        
        /////////////////////////////
        ///Song Changer
        /////////////////////////////

        if (songtimer >= songCount && song1Change == 0)
        {

            song1Change = 1;
        }
        else if (song1Change == 0 && songtimer < songCount)
        {
            songtimer += Time.deltaTime;
        }

        if (song1Change == 1)
        {

            musicIntro.Stop();
            musicLoop.Play();
            song1Change = 2;
        }

        

        /////////////////////////////
        ///Speed Up
        /////////////////////////////
        if (gasPercent > .05 && gear == 0)
        {
            speedUp();
        }
        if(gasPercent > .1 && gear == 1){
            speedUp();
        }
        if(gasPercent > .15 && gear == 2)
        {
            speedUp();
        }if(gasPercent > .25 && gear == 3)
        {
            speedUp();
        }if(gasPercent > .4 && gear == 4)
        {
            speedUp();
        }if(gasPercent > .5 && gear == 5)
        {
            speedUp();
        }

        
        //////////////////////////////
        ///Movement enabler
        //////////////////////////////

        if(alive == true)
        {
        mousePos = camera.ScreenToWorldPoint(Input.mousePosition);

        movement.x = Input.GetAxisRaw("Horizontal");
        transform.Translate(userDirection * movespeed * Time.deltaTime);
        }

        if(InControl == true)
        { 
            userDirection.x = movement.x;
        }
          
      

        ////////////////////////////
        ///Invincibility
        ////////////////////////////
        if (isInvincible)
        {
            iFrames += Time.deltaTime;
            if(iFrames >= iFrameMarker)
            {
                isInvincible = false;
            }
        }

        /////////////////////////////
        ///Slow Down and Win
        /////////////////////////////
        if(gasPercent > .6 && movespeed >= 60)
        {
            movespeed -= 10;
        }
        if(gasPercent > .7 && movespeed >= 50)
        {
            movespeed -= 10;
        }
        if(gasPercent > .75 && movespeed >= 40)
        {
            movespeed -= 10;
        }
        if(gasPercent > .8 && movespeed >= 30)
        {
            movespeed -= 10;
        }
        if(gasPercent > .9 && movespeed >= 20)
        {
            movespeed -= 10;
        }
        if(gasPercent > .99 && movespeed >= 10)
        {
            if(movespeed > 0)
            {
                movespeed = 0;
            }
            if (alive)
            {
                //win
            }
        }

        playerUI.SendMessage("SetGasPercent", gasPercent);


        //////////////////////////////////////
        ///End of Update()
        //////////////////////////////////////
    }
       
    public void FixedUpdate()
    {
       

    }

    public void GetGasPercent()
    {
        playerUI.SendMessage("SetGasPercent", gasPercent);
        print("player gaspercent " + gasPercent);
    }

    public void GetPenalty()
    {
        playerUI.SendMessage("SetPenalty", penalty);
        //print("player penalty = " + penalty);
    }

    public void speedUp()
    {

         //movespeed *= 2;
         movespeed += 5;
         //destroyer.SendMessage("SpeedUp", 5);
         gear++;

        
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Car") && isInvincible == false)
        {
            penalty += 25;
            gasTime += 10f;
            Health -= 1;
            //print("ow");
            isInvincible = true;
            iFrameMarker = time + 1.5f;
            iFrames = time;
            if(movespeed > 15)
            {
                movespeed -= 5;
            }
            if (Health <= 4)
            {
                animator.Play("Driving hurt 1");
                smoke1.SetActive(true);
            }
            if (Health <= 2)
            {
                animator.Play("Driving hurt 2");
                //Instantiate(smoke2, this.transform);
                smoke2.SetActive(true);
            }
            if (Health <= 0)
            {
                alive = false;
                //Instantiate(fire, this.transform);
                fire.SetActive(true);
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
            if (other.gameObject.CompareTag("PotHole"))
            {
                penalty += 5;
                gasTime += 5f;
                movespeed-=3;
                //destroyer.SendMessage("SlowDown", 1);
            }
            if (other.gameObject.CompareTag("Oil Spill"))
            {
                    penalty += 5;
                    gasTime += 5f;
                    movespeed -= 2;
                    //print("hit");
                    vroom.Stop();
                    InControl=false;
                    spinOutTime = time;
                    spinOutTimeMarker = spinOutTime+1f;
                    animator.Play("Spinout");
            }

            
              
    }   
        
    


   
    


}

