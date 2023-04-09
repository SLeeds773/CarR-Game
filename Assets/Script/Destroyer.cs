using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{

    public float speed;
    public Vector3 userDirection = Vector3.one;

    public Transform playerLocation;
    public Transform destroyerLocation;


    // Start is called before the first frame update
    void Start()
    {
        print(this.tag);
        userDirection.x = 0;
        speed = 10;
    }

    public void SetSpeed(float otherspeed)
    {
        speed = otherspeed;
    }

    public void SpeedUp(float speedboost)
    {
        speed += speedboost;
    }


    public void SlowDown(float oof)
    {
        speed -= oof;
    }

    public void GetTransform()
    {

    }

    public void SetTransform(Transform other)
    {
        destroyerLocation = other;
    }


    // Update is called once per frame
    void Update()
    {

        this.gameObject.transform.position = playerLocation.transform.position - Vector3.one * 50f;

    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        print("trigger");
        print(other.gameObject.tag);
        print(other.gameObject);
    }
    public void OnCollisionEnter2D(Collision2D other)
    {
        print("Collision");
        print(other.gameObject.tag);
        print(other.gameObject);
    }

}
