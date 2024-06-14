using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fielders : MonoBehaviour
{
    [SerializeField] private GameObject[] fielders;
    [SerializeField] private GameObject bat;
    [SerializeField] private GameObject ball;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(bat.GetComponent<CricketBat>().ballHit == true)
        {
            foreach(GameObject fielder in fielders)
            {
                if(Vector3.Distance(fielder.transform.position, ball.transform.position) < 0.5f && !ball.GetComponent<CricketBall>().field)
                {
                    Debug.Log("Fielder Catches the Ball");
                    bat.GetComponent<CricketBat>().ballHit = false;
                    ball.GetComponent<Rigidbody>().useGravity = false;
                    bat.GetComponent<CricketBat>().wicketDown = true;
                    bat.GetComponent<CricketBat>().makeDecision = true;
                }
                else if(!ball.GetComponent<CricketBall>().field)
                {
                    Debug.Log("Fielder Running to Catch the Ball");
                    transform.position = Vector3.MoveTowards(transform.position, ball.transform.position, 0.1f);
                }
                else
                {
                    Debug.Log("Fielder Running to Stop the Ball");
                    transform.position = Vector3.MoveTowards(transform.position, ball.transform.position, 0.1f);
                }
            }
        }
    }
}
