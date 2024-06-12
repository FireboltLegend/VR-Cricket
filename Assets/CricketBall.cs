using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CricketBall : MonoBehaviour
{
	[SerializeField] float lowerBoundBallLaunchSpeed;
	[SerializeField] float upperBoundBallLaunchSpeed;
	public float ballLaunchSpeed;
	Rigidbody rb;
	public bool gameRunning = false;
	public bool field = false;
	public bool boundary = false;
	// Start is called before the first frame update
	void Start()
	{
		rb = GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	void Update()
	{
		if(Input.GetKeyDown(KeyCode.Space))
		{
			gameRunning = true;
			rb.velocity = Vector3.zero;
			ballLaunchSpeed = Random.Range(lowerBoundBallLaunchSpeed, upperBoundBallLaunchSpeed);
			transform.position = new Vector3(6f, 1.1f, 0);
			rb.AddForce((Vector3.forward + new Vector3(-1, 0, -1)) * ballLaunchSpeed, ForceMode.Impulse);
			GetComponent<LineRenderer>().positionCount = 0;
		}
		if(GetComponent<LineRenderer>().positionCount >= 1)
		{
			GetComponent<LineRenderer>().positionCount += 1;
			GetComponent<LineRenderer>().SetPosition(GetComponent<LineRenderer>().positionCount - 1, transform.position);
		}
		if(transform.position.y < -1)
		{
			transform.position = new Vector3(transform.position.x, 0.0775f, transform.position.z);
		}
	}
}