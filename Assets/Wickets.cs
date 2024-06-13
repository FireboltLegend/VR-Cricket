using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Wickets : MonoBehaviour
{
    [SerializeField] private GameObject ball;
    [SerializeField] private GameObject bat;
    // Start is called before the first frame update
    void Start()
    {
        GetComponentInParent<Outline>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            GetComponentInParent<Outline>().enabled = false;
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject == ball && bat.GetComponent<CricketBat>().wicketDown == false && ball.GetComponent<CricketBall>().gameRunning == true)
        {
            ball.GetComponent<CricketBall>().gameRunning = false;
            Debug.Log("Wicket Down");
            GetComponentInParent<Outline>().enabled = true;
            bat.GetComponent<CricketBat>().wicketDown = true;
            bat.GetComponent<CricketBat>().makeDecision = true;
        }
    }
}
