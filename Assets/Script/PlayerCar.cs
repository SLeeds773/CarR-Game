using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCar : MonoBehaviour
{

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
    public bool InControl;
    public float spinOutTime;
    public float spinOutTimeMarker;

    public Vector3 userDirection = Vector3.one;



    public int maxHealth = 3;
    public int Health;
    public bool isInvincible;
    public float iFrames;
    public bool isAirborn;

    public AudioSource musicIntro;
    public AudioSource musicLoop;
    public AudioSource vroom;

    public float songtimer;
    private float songCount = 7.35f;
    private int song1Change = 0;

    void Start()
    {
        musicIntro.Play();
        Health = maxHealth;
    }


    public void Update()
    {
        time +=Time.deltaTime;

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


        if (time >= 5f && gear == 0)
        {
            speedUp();
        }
        if(time >= 10f && gear == 1){
            speedUp();
        }

        

        mousePos = camera.ScreenToWorldPoint(Input.mousePosition);

        movement.x = Input.GetAxisRaw("Horizontal");
        transform.Translate(userDirection * movespeed * Time.deltaTime);

        if(InControl == true)
        { 
            userDirection.x = movement.x;
        }

        if (isInvincible)
        {
            if(time >= iFrames)
            {
                isInvincible = false;
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
         destroyer.SendMessage("SpeedUp", 5);
         gear++;

        
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PotHole"))
        {
            movespeed-=1;
            destroyer.SendMessage("SlowDown", 1);
        }
        if (other.gameObject.CompareTag("Oil Spill"))
        {
            vroom.Stop();
            InControl=false;
            spinOutTime = time;
            spinOutTimeMarker = spinOutTime+2f;
            animator.Play("Spinout");
        }
    }


    public void OnCollisionEnter2D(Collision2D other)
    {
        /*
        if(other.GameObject.CompareTag("NPC") || other.GameObject.CompareTag("Wall") || other.GameObject.CompareTag("Obstacle"))
        {
            Health -= 1;
            isInvincible = true;
            iFrames += time;
        }*/
    }
    


}
