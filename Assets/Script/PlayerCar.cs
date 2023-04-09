using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCar : MonoBehaviour
{
    public bool alive;
    public GameObject destroyer;
    
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
    //public float penalty;
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
        //animator.SetFloat("Health", Health);

        //gas percent used for meter, getting hit lowers gas (adds to time)
        //change all time to gas

        gasPercent = gas / time;
        print(gasPercent);

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
                if(Health == 5)
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

        //print(InControl);


        if (time >= 20f && gear == 0)
        {
            speedUp();
        }
        if(time >= 40f && gear == 1){
            speedUp();
        }
        if(time >= 60f && gear == 2)
        {
            speedUp();
        }if(time >= 80f && gear == 3)
        {
            speedUp();
        }if(time >= 100f && gear == 4)
        {
            speedUp();
        }if(time >= 120f && gear == 5)
        {
            speedUp();
        }

        
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
          
      


        if (isInvincible)
        {

        }


        if(time >= 160f && movespeed >= 60)
        {
            movespeed -= 10;
        }
        if(time >= 180f && movespeed >= 50)
        {
            movespeed -= 10;
        }
        if(time >= 180f && movespeed >= 40)
        {
            movespeed -= 10;
        }
        if(time >= 180f && movespeed >= 30)
        {
            movespeed -= 10;
        }
        if(time >= 180f && movespeed >= 20)
        {
            movespeed -= 10;
        }
        if(time >= 180f && movespeed >= 10)
        {
            movespeed -= 10;
            if (alive)
            {
                //win
            }
        }

            
    }
       
    public void FixedUpdate()
    {
       

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
        if (other.gameObject.CompareTag("Car"))
        {

            Health -= 1;
            print("ow");
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
                movespeed-=1;
                //destroyer.SendMessage("SlowDown", 1);
            }
            if (other.gameObject.CompareTag("Oil Spill"))
            { 
                
                    print("hit");
                    vroom.Stop();
                    InControl=false;
                    spinOutTime = time;
                    spinOutTimeMarker = spinOutTime+1f;
                    animator.Play("Spinout");
            }

            
              
    }   
        
    


   
    


}

