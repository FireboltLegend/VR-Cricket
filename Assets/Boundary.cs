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
        
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject == ball && ball.GetComponent<CricketBall>().gameRunning)
        {
            Debug.Log("Boundary");
            ball.GetComponent<CricketBall>().boundary = true;
            other.gameObject.GetComponent<CricketBall>().gameRunning = false;
        }
    }
}
