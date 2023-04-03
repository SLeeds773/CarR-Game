using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{

    public float speed;
    public Vector3 userDirection = Vector3.one;



    // Start is called before the first frame update
    void Start()
    {
        userDirection.x = 0;
        speed = 1;
    }


    public void SpeedUp(float speedboost)
    {
        speed += speedboost;
    }


    public void SlowDown(float oof)
    {
        speed -= oof;
    }


    // Update is called once per frame
    void Update()
    {
        transform.Translate(userDirection * speed * Time.deltaTime);
    }
}
