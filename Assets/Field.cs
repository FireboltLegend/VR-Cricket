using System.Collections;
using System.Collections.Generic;
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
        
    }

    void OnCollisionEnter(Collision other)
    {
        if(bat.GetComponent<CricketBat>().ballHit == true && ball.GetComponent<CricketBall>().gameRunning)
        {
            if(other.gameObject == ball)
            {
                bat.GetComponent<CricketBat>().ballHit = false;
                ball.GetComponent<CricketBall>().field = true;
                Debug.Log("Ball Lands");
                bat.GetComponent<CricketBat>().ballDistance = Vector3.Distance(ball.transform.position, bat.transform.position);
            }
        }
    }
}
