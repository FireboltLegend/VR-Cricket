using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fielders : MonoBehaviour
{
    [SerializeField] private GameObject bat;
    [SerializeField] private GameObject ball;
    [SerializeField] private GameObject wicket;
    [SerializeField] private GameObject otherWicket;
    private Vector3 fielderPosition;
    private bool hasBall = false;
    // Start is called before the first frame update
    void Start()
    {
        fielderPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            transform.position = fielderPosition;
            hasBall = false;
            bat.GetComponent<CricketBat>().catchBall = false;
            bat.GetComponent<CricketBat>().ballCaught = false;
            bat.GetComponent<CricketBat>().ballStopped = false;
        }
        if(bat.GetComponent<CricketBat>().catchBall == true && bat.GetComponent<CricketBat>().ballCaught == false)
        {
            Debug.Log("Fielder Running to Catch the Ball");
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(ball.transform.position.x, transform.position.y, ball.transform.position.z), 0.05f);
        }
        else if (ball.GetComponent<CricketBall>().field && bat.GetComponent<CricketBat>().ballStopped == false)
        {
            Debug.Log("Fielder Running to Stop the Ball");
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(ball.transform.position.x, transform.position.y, ball.transform.position.z), 0.05f);
        }
        else if(transform.position != fielderPosition && !hasBall)
        {
            Debug.Log("Fielder Returning to Start Position");
            transform.position = Vector3.MoveTowards(transform.position, fielderPosition, 0.05f);
        }
        if(hasBall)
        {
            if(Vector3.Distance(transform.position, wicket.transform.position) < Vector3.Distance(transform.position, otherWicket.transform.position))
            {
                Debug.Log("Fielder Running to Wicket");
                transform.position = Vector3.MoveTowards(transform.position, wicket.transform.position, 0.05f);
            }
            else
            {
                Debug.Log("Fielder Running to Other Wicket");
                transform.position = Vector3.MoveTowards(transform.position, otherWicket.transform.position, 0.05f);
            }
            ball.transform.position = transform.position;
            if(Vector3.Distance(transform.position, wicket.transform.position) < 0.2f || Vector3.Distance(transform.position, otherWicket.transform.position) < 0.2f)
            {
                Debug.Log("Fielder Reaches Wicket");
                hasBall = false;
                bat.GetComponent<CricketBat>().ballHit = false;
                bat.GetComponent<CricketBat>().ballAtWicket = true;
                ball.GetComponent<Rigidbody>().useGravity = true;
                ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
                ball.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == ball)
        {
            Debug.Log("Fielder Collides with Ball");
            if(bat.GetComponent<CricketBat>().catchBall == true && bat.GetComponent<CricketBat>().ballCaught == false && !ball.GetComponent<CricketBall>().field)
            {
                Debug.Log("Fielder Catches the Ball");
                bat.GetComponent<CricketBat>().ballHit = false;
                bat.GetComponent<CricketBat>().ballCaught = true;
                ball.GetComponent<Rigidbody>().useGravity = false;
                bat.GetComponent<CricketBat>().wicketDown = true;
                bat.GetComponent<CricketBat>().makeDecision = true;
            }
            else
            {
                Debug.Log("Fielder Stops the Ball");
                bat.GetComponent<CricketBat>().ballStopped = true;
                hasBall = true;
                bat.GetComponent<CricketBat>().ballHit = false;
                ball.GetComponent<CricketBall>().field = true;
                ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
                ball.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                ball.GetComponent<Rigidbody>().useGravity = false;
            }
        }
    }
}
