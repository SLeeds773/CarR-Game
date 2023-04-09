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
    public float movespeed;
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
        
        //Gear 1 speed cap: 15
        if (gasPercent > .05 && gear == 0)
        {
            speedUp();
        }
        if(movespeed <= 15f && gear == 1)
        {
            movespeed += .01f;
        }


        //Gear 2 speed cap: 20
        if(gasPercent > .1 && gear == 1){
            speedUp();
        }
        if(movespeed < 20f && gear == 2)
        {
            movespeed += .01f;
        }

        //Gear 3 speed cap: 25
        if(gasPercent > .2 && gear == 2)
        {
            speedUp();
        }
        if (movespeed < 25f && gear == 3)
        {
            movespeed += .01f;
        }

        //Gear 4 speed cap: 30
        if (gasPercent > .45 && gear == 3)
        {
            speedUp();
        }
        if (movespeed < 30f && gear == 4)
        {
            movespeed += .02f;
        }

        //Gear 5 speed cap: 40
        if (gasPercent > .7 && gear == 4)
        {
            speedUp();
        }
        if (movespeed < 40f && gear == 5)
        {
            movespeed += .03f;
        }
        
        //Gear 6 speed cap: 50
        if (gasPercent > .85 && gear == 5)
        {
            speedUp();
            //movespeed += 5;
        }
        if (movespeed < 50f && gear == 6)
        {
            movespeed += .04f;
        }


        //////////////////////////////
        ///Movement enabler
        //////////////////////////////

        if (alive == true)
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
        ///
        /*
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
        */
        if(gasPercent > .99)
        {
            if(movespeed > 0)
            {
                movespeed -= .1f;
            } else if(movespeed < 0)
            {
                movespeed = 0f;
            }
            if (alive && movespeed == 0)
            {
                vroom.Stop();
                print("win");
                playerUI.SendMessage("WinText", SendMessageOptions.DontRequireReceiver);
                //win
            }
        }

        if (alive)
        {
            playerUI.SendMessage("SetGasPercent", gasPercent);

        }


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
         movespeed += 1f;
         //destroyer.SendMessage("SpeedUp", 5);
         gear++;

        
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Car") && isInvincible == false && alive)
        {
            penalty += 25;
            gasTime += 10f;
            Health -= 1;
            //print("ow");
            isInvincible = true;
            iFrameMarker = time + 1.5f;
            iFrames = time;
            if(movespeed > 15f)
            {
                movespeed -= 5f;
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
                playerUI.SendMessage("LoseText", SendMessageOptions.DontRequireReceiver);


            }
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (alive)
        {
            if (other.gameObject.CompareTag("PotHole"))
            {
                penalty += 5;
                gasTime += 5f;
                movespeed -= 3f;
                //destroyer.SendMessage("SlowDown", 1);
            }
            if (other.gameObject.CompareTag("Oil Spill"))
            {
                penalty += 5;
                gasTime += 5f;
                movespeed -= 2f;
                //print("hit");
                vroom.Stop();
                InControl = false;
                spinOutTime = time;
                spinOutTimeMarker = spinOutTime + 1f;
                animator.Play("Spinout");
            }
        }
           

            
              
    }   
        
    


   
    


}

