using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Field : MonoBehaviour
{
    [SerializeField] private GameObject ball;
    [SerializeField] private GameObject bat;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(ball.transform.position.y <= 0.085f && bat.GetComponent<CricketBat>().ballHit == true && (MathF.Pow(ball.transform.position.x, 2)/MathF.Pow(25, 2) + MathF.Pow(ball.transform.position.z, 2)/MathF.Pow(22.5f, 2)) <= 1 && ball.GetComponent<CricketBall>().field == false)
        {
            ball.GetComponent<CricketBall>().field = true;
            bat.GetComponent<CricketBat>().catchBall = false;
            Debug.Log("Ball is on the ground");
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject == ball && bat.GetComponent<CricketBat>().ballHit == true && ball.GetComponent<CricketBall>().field == false)
        {
            ball.GetComponent<CricketBall>().field = true;
            bat.GetComponent<CricketBat>().catchBall = false;
            Debug.Log("Ball Lands");
        }
    }
}
