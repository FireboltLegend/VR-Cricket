using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CricketBall : MonoBehaviour
{
	[SerializeField] private GameObject outfield;
	[SerializeField] private GameObject bat;
	[SerializeField] private TextMeshProUGUI result;
	[SerializeField] float lowerBoundBallLaunchSpeed;
	[SerializeField] float upperBoundBallLaunchSpeed;
	[SerializeField] private Vector3 ballLaunchDirection;
	[SerializeField] private GameObject bowlerHand;
	public bool launch = false;
	Vector3 prevPosition;
    Vector3 curPosition;
	public float ballLaunchSpeed;
	Rigidbody rb;
	public bool gameRunning = false;
	public bool field = false;
	public bool boundary = false;
	public bool ballThrown = false;
	// Start is called before the first frame update
	void Start()
	{
		rb = GetComponent<Rigidbody>();
		prevPosition = transform.position;
        curPosition = transform.position;
	}

	// Update is called once per frame
	void Update()
	{
		prevPosition = curPosition;
        curPosition = transform.position;
		if(Input.GetKeyDown(KeyCode.Space) || launch == true)
		{
			launch = false;
			result.text = "";
			gameRunning = true;
			boundary = false;
			field = false;
			ballThrown = false;
			GetComponent<Rigidbody>().useGravity = true;
			GetComponent<LineRenderer>().positionCount = 0;
		}
		if(Vector3.Distance(new Vector3(5.41190004f, 1.95720005f, -0.310400009f), bowlerHand.transform.position) < 0.2f)
		{
			rb.velocity = Vector3.zero;
			ballLaunchSpeed = UnityEngine.Random.Range(lowerBoundBallLaunchSpeed, upperBoundBallLaunchSpeed);
			transform.position = new Vector3(5.41190004f, 1.95720005f, -0.310400009f);
			rb.AddForce((Vector3.forward + ballLaunchDirection) * ballLaunchSpeed, ForceMode.Impulse);
			ballThrown = true;
		}
		if(GetComponent<LineRenderer>().positionCount >= 1)
		{
			GetComponent<LineRenderer>().positionCount += 1;
			GetComponent<LineRenderer>().SetPosition(GetComponent<LineRenderer>().positionCount - 1, transform.position);
		}
		if(transform.position.y < -1 && (MathF.Pow(transform.position.x, 2)/MathF.Pow(25, 2) + MathF.Pow(transform.position.z, 2)/MathF.Pow(22.5f, 2)) <= 1)
		{
			GetComponent<Rigidbody>().velocity = Vector3.zero;
			transform.position = new Vector3(transform.position.x, 0.078f, transform.position.z);
			field = true;
		}
		else if (transform.position.y < -1 && (MathF.Pow(transform.position.x, 2)/MathF.Pow(25, 2) + MathF.Pow(transform.position.z, 2)/MathF.Pow(22.5f, 2)) > 1)
		{
			GetComponent<Rigidbody>().velocity = Vector3.zero;
			transform.position = new Vector3(transform.position.x, 0.078f, transform.position.z);
			boundary = true;
			bat.GetComponent<CricketBat>().makeDecision = true;
		}
		if(ballThrown && transform.position.x < -6 && bat.GetComponent<CricketBat>().wicketDown == false)
		{
			bat.GetComponent<CricketBat>().makeDecision = true;
		}
	}
}