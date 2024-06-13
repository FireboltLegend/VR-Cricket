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
	[SerializeField] private GameObject bowlerHand;
	Vector3 prevPosition;
    Vector3 curPosition;
	public float ballLaunchSpeed;
	Rigidbody rb;
	public bool gameRunning = false;
	public bool field = false;
	public bool boundary = false;
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
		if(Vector3.Distance(new Vector3(5.41190004f, 1.95720005f, -0.310400009f), bowlerHand.transform.position) < 0.2f)
		{
			result.text = "";
			gameRunning = true;
			boundary = false;
			field = false;
			rb.velocity = Vector3.zero;
			ballLaunchSpeed = Random.Range(lowerBoundBallLaunchSpeed, upperBoundBallLaunchSpeed);
			transform.position = new Vector3(5.41190004f, 1.95720005f, -0.310400009f);
			rb.AddForce((Vector3.forward + new Vector3(-1, 0, -0.975f)) * ballLaunchSpeed, ForceMode.Impulse);
			GetComponent<LineRenderer>().positionCount = 0;
		}
		if(GetComponent<LineRenderer>().positionCount >= 1)
		{
			GetComponent<LineRenderer>().positionCount += 1;
			GetComponent<LineRenderer>().SetPosition(GetComponent<LineRenderer>().positionCount - 1, transform.position);
		}
		if(transform.position.y < -1 && Vector3.Distance(transform.position, outfield.transform.position) < 25f)
		{
			GetComponent<Rigidbody>().velocity = Vector3.zero;
			transform.position = new Vector3(transform.position.x, 0.078f, transform.position.z);
			field = true;
		}
		else if (transform.position.y < -1 && Vector3.Distance(transform.position, outfield.transform.position) >= 25f)
		{
			GetComponent<Rigidbody>().velocity = Vector3.zero;
			transform.position = new Vector3(transform.position.x, 0.078f, transform.position.z);
			boundary = true;
		}
		if(Vector3.Distance(curPosition, prevPosition) < 1f && bat.GetComponent<CricketBat>().ballHit == true && field == true)
		{
			bat.GetComponent<CricketBat>().ballHit = false;
			bat.GetComponent<CricketBat>().ballDistance = Vector3.Distance(transform.position, bat.transform.position);
			bat.GetComponent<CricketBat>().makeDecision = true;
			Debug.Log("Ball Stopped");
		}
	}
}