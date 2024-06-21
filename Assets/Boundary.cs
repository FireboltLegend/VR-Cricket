using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundary : MonoBehaviour
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
        // if(ball.transform.position.y <= 0.085f && bat.GetComponent<CricketBat>().ballHit == true && ball.GetComponent<CricketBall>().boundary == false)
        // {
        //     bat.GetComponent<CricketBat>().ballHit = false;
        //     Debug.Log("Boundary");
        //     ball.GetComponent<CricketBall>().boundary = true;
        //     bat.GetComponent<CricketBat>().makeDecision = true;
        // }
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject == ball && bat.GetComponent<CricketBat>().ballHit == true)
        {
            bat.GetComponent<CricketBat>().ballHit = false;
            Debug.Log("Boundary");
            ball.GetComponent<CricketBall>().boundary = true;
            bat.GetComponent<CricketBat>().makeDecision = true;
        }
    }
}
