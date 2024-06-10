using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CricketBat : MonoBehaviour
{
    [SerializeField] private GameObject ball;
    float swingSpeed = 0f;
    float swingAngle = 0f;
    Vector3 prevPosition;
    Vector3 curPosition;
    // Start is called before the first frame update
    void Start()
    {
        prevPosition = transform.position;
        curPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        prevPosition = curPosition;
        curPosition = transform.position;
        swingSpeed = (curPosition - prevPosition).magnitude / Time.deltaTime;
        swingAngle = Vector3.Angle(curPosition - prevPosition, Vector3.forward);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == ball)
        {
            Debug.Log("Ball Hit");
            ball.transform.Translate(Vector3.forward * swingSpeed * Time.deltaTime);
        }
    }
}
