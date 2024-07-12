using System.Collections;
using System.Collections.Generic;
using CricketBowlingAnimations;
using UnityEngine;

public class Launch : MonoBehaviour
{
	[SerializeField] private GameObject leftHand;
	[SerializeField] private GameObject rightHand;
	private GameObject ball;
	private GameObject bat;
	private GameObject bowler;
	private Fielders[] fielder;
	private Wickets[] wicket;
	private bool control = false;
	// Start is called before the first frame update
	void Start()
	{
		bat = FindObjectsOfType<CricketBat>()[0].gameObject;
		ball = FindObjectsOfType<CricketBall>()[0].gameObject;
		bowler = FindObjectsOfType<AnimationTester>()[0].gameObject;
		fielder = FindObjectsOfType<Fielders>();
		wicket = FindObjectsOfType<Wickets>();
	}

	// Update is called once per frame
	void Update()
	{
		if(control == false && (Vector3.Distance(transform.position, rightHand.transform.position) < 0.25f || Vector3.Distance(transform.position, leftHand.transform.position) < 0.25f) && ball.GetComponent<CricketBall>().gameRunning == false)
		{
			control = true;
			bat.GetComponent<CricketBat>().launch = true;
			ball.GetComponent<CricketBall>().launch = true;
			bowler.GetComponent<AnimationTester>().launch = true;
			foreach(Fielders f in fielder)
			{
				f.launch = true;
			}
			foreach(Wickets w in wicket)
			{
				w.launch = true;
			}
		}
		else if(Vector3.Distance(transform.position, rightHand.transform.position) >= 0.25f || Vector3.Distance(transform.position, leftHand.transform.position) >= 0.25f)
		{
			control = false;
		}
	}
}
