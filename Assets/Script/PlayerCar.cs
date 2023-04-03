using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCar : MonoBehaviour
{
    public int health=3;
    
    public Camera camera;
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

    public void Update()
    {
        time +=Time.deltaTime;

        if(InControl==false)
        {
            spinOutTime += Time.deltaTime;
            print("No Control");
            if(spinOutTime >= spinOutTimeMarker)
            {
                InControl=true;
                spinOutTime=0;
                spinOutTimeMarker=0;
            }
        }

        //print(InControl);


        if(time >= 5f && gear == 0)
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
       
            
    }
       
    public void FixedUpdate()
    {
       

    }

    public void speedUp()
    {

         //movespeed *= 2;
         movespeed += 5;
         gear++;
        
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PotHole"))
        {
            movespeed-=1;
        }
        if (other.gameObject.CompareTag("Oil Spill"))
        {
            InControl=false;
            spinOutTime = time;
            spinOutTimeMarker = spinOutTime+2f;
            animator.Play("Spinout");
        }
    }

    /*public void SpinOut()
    {
        InControl=false;
        spinOutTime = time;
        spinOutTimeMarker = spinOutTime+2f;
        animator.Play("Spinout");
    }*/


}
