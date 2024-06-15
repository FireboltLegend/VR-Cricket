using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fielders : MonoBehaviour
{
    [SerializeField] private GameObject bat;
    [SerializeField] private GameObject ball;
    private Vector3 fielderPosition;
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
            bat.GetComponent<CricketBat>().catchBall = false;
            bat.GetComponent<CricketBat>().ballCaught = false;
        }
        if(bat.GetComponent<CricketBat>().catchBall == true && bat.GetComponent<CricketBat>().ballCaught == false)
        {
            if(Vector3.Distance(transform.position, ball.transform.position) < 0.5f && !ball.GetComponent<CricketBall>().field)
            {
                Debug.Log("Fielder Catches the Ball");
                bat.GetComponent<CricketBat>().ballHit = false;
                bat.GetComponent<CricketBat>().ballCaught = true;
                ball.GetComponent<Rigidbody>().useGravity = false;
                bat.GetComponent<CricketBat>().wicketDown = true;
                bat.GetComponent<CricketBat>().makeDecision = true;
            }
            else if(!ball.GetComponent<CricketBall>().field)
            {
                Debug.Log("Fielder Running to Catch the Ball");
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(ball.transform.position.x, transform.position.y, ball.transform.position.z), 0.05f);
            }
            else
            {
                Debug.Log("Fielder Running to Stop the Ball");
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(ball.transform.position.x, transform.position.y, ball.transform.position.z), 0.05f);
            }
        }
    }
}
