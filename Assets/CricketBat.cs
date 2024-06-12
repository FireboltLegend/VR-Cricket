using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CricketBat : MonoBehaviour
{
    [SerializeField] private GameObject ball;
    [SerializeField] private TextMeshProUGUI Text;
    int runs = 0;
    int wickets = 0;
    public float swingSpeed = 0f;
    public Vector3 swingDirection;
    Vector3 prevPosition;
    Vector3 curPosition;
    Rigidbody rb;
    public bool ballHit = false;
    public float ballDistance = 0f;
    public bool wicketDown = false;
    // Start is called before the first frame update
    void Start()
    {
        prevPosition = transform.position;
        curPosition = transform.position;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        prevPosition = curPosition;
        curPosition = transform.position;
        swingSpeed = Vector3.Distance(curPosition, prevPosition) / Time.deltaTime;
        swingDirection = curPosition - prevPosition;
        if(wicketDown)
        {
            Debug.Log("Wicket Down");
            wickets++;
            wicketDown = false;
            ball.GetComponent<CricketBall>().gameRunning = false;
        }
        else
        {
            if(ball.GetComponent<CricketBall>().boundary && !ball.GetComponent<CricketBall>().field)
            {
                Debug.Log("Ball Hit for 6 runs");
                runs += 6;
                ballDistance = 0;
                ball.GetComponent<CricketBall>().gameRunning = false;
                ball.GetComponent<CricketBall>().boundary = false;
                ball.GetComponent<CricketBall>().field = false;
                ballHit = false;
            }
            else if(ball.GetComponent<CricketBall>().boundary && ball.GetComponent<CricketBall>().field)
            {
                Debug.Log("Ball Hit for 4 runs");
                runs += 4;
                ballDistance = 0;
                ball.GetComponent<CricketBall>().gameRunning = false;
                ball.GetComponent<CricketBall>().field = false;
            }
            else if(ballDistance > 20 && ball.GetComponent<Rigidbody>().velocity.magnitude <= 0.5f)
            {
                Debug.Log("Ball Hit for 3 runs");
                runs += 3;
                ballDistance = 0;
                ball.GetComponent<CricketBall>().gameRunning = false;
                ball.GetComponent<CricketBall>().field = false;
            }
            else if(ballDistance > 15 && ball.GetComponent<Rigidbody>().velocity.magnitude <= 0.5f)
            {
                Debug.Log("Ball Hit for 2 runs");
                runs += 2;
                ballDistance = 0;
                ball.GetComponent<CricketBall>().gameRunning = false;
                ball.GetComponent<CricketBall>().field = false;
            }
            else if(ballDistance > 10 && ball.GetComponent<Rigidbody>().velocity.magnitude <= 0.5f)
            {
                Debug.Log("Ball Hit for 1 run");
                runs += 1;
                ballDistance = 0;
                ball.GetComponent<CricketBall>().gameRunning = false;
                ball.GetComponent<CricketBall>().field = false;
            }
            else if(ballDistance > 0 && ball.GetComponent<Rigidbody>().velocity.magnitude <= 0.5f)
            {
                Debug.Log("Ball Hit for 0 runs");
                runs += 0;
                ballDistance = 0;
                ball.GetComponent<CricketBall>().gameRunning = false;
                ball.GetComponent<CricketBall>().field = false;
            }
        }
        Text.text = "Runs: " + runs + "\nWickets: " + wickets;
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject == ball)
        {
            ballHit = true;
            Debug.Log("Ball Hit\nSwing Speed: " + swingSpeed + "\nSwing Direction: " + swingDirection.normalized);
            ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
            ball.GetComponent<Rigidbody>().AddForce(swingDirection.normalized * swingSpeed, ForceMode.Impulse);
            ball.GetComponent<LineRenderer>().positionCount = 1;
            ball.GetComponent<LineRenderer>().SetPosition(0, ball.transform.position);
        }
    }
}
