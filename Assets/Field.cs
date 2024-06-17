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
        if(bat.GetComponent<CricketBat>().ballHit == true)
        {
            if(other.gameObject == ball)
            {
                ball.GetComponent<CricketBall>().field = true;
                bat.GetComponent<CricketBat>().catchBall = false;
                Debug.Log("Ball Lands");
            }
        }
    }
}
