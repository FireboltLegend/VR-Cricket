using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wickets : MonoBehaviour
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
        if(other.gameObject == ball)
        {
            Debug.Log("Wicket Down");
            bat.GetComponent<CricketBat>().wicketDown = true;
        }
    }
}
