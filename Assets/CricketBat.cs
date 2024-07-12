using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CricketBat : MonoBehaviour
{
	[SerializeField] private GameObject ball;
	[SerializeField] private TextMeshProUGUI score;
	[SerializeField] private TextMeshProUGUI result;
	[SerializeField] private GameObject player;
	[SerializeField] private GameObject playerCamera;
	[SerializeField] private GameObject wicket;
	[SerializeField] private GameObject otherWicket;
	[SerializeField] private GameObject outfield;
	[SerializeField] private GameObject outSound;
	public bool launch = false;
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
	public bool makeDecision = false;
	public bool catchBall = false;
	public bool ballCaught = false;
	public bool ballStopped = false;
	private bool canRun = false;
	private Vector3 playerStartPosition;
	private Vector3 batStartPosition;
	private Vector3 batStartRotation;
	public bool ballAtWicket = false;
	private int potentialRuns = 0;
	private int runStartLocation = 0;
	private bool runStart = false;
	private bool runEnd = false;
	private bool restartGame = false;
	private bool halfCentury = false;
	private bool century = false;
	private bool gameOver = false;
	// Start is called before the first frame update
	void Start()
	{
		prevPosition = transform.position;
		curPosition = transform.position;
		rb = GetComponent<Rigidbody>();
		playerStartPosition = player.transform.position;
		batStartPosition = transform.position;
		batStartRotation = transform.rotation.eulerAngles;
	}

	// Update is called once per frame
	void Update()
	{
		if(Input.GetKeyDown(KeyCode.Space) || launch == true)
		{
			launch = false;
			potentialRuns = 0;
			canRun = false;
			ballAtWicket = false;
			runStartLocation = 0;
			runStart = false;
			runEnd = false;
			wicketDown = false;
			catchBall = false;
			makeDecision = false;
			player.transform.position = playerStartPosition;
			transform.position = batStartPosition;
			transform.rotation = Quaternion.Euler(batStartRotation);
			if(restartGame)
			{
				restartGame = false;
				runs = 0;
				wickets = 0;
			}
			gameOver = false;
			outfield.GetComponent<AudioSource>().Stop();
			wicket.GetComponent<AudioSource>().Stop();
		}
		rb.velocity = Vector3.zero;
		rb.angularVelocity = Vector3.zero;

		prevPosition = curPosition;
		curPosition = transform.position;
		swingSpeed = Vector3.Distance(curPosition, prevPosition) / Time.deltaTime;
		swingDirection = curPosition - prevPosition;
		if(canRun)
		{
			if(swingSpeed > 0 && Math.Abs(player.transform.position.x) <= 6)
			{
				if(playerCamera.transform.rotation.eulerAngles.y > 0 && playerCamera.transform.rotation.eulerAngles.y < 180)
				{
					if(!runStart && runStartLocation == 0)
					{
						runEnd = false;
						runStart = true;
					}
					player.transform.position = Vector3.MoveTowards(player.transform.position, new Vector3(6, player.transform.position.y, player.transform.position.z), 0.0075f * swingSpeed);
					if(player.transform.position.x >= 4 && !runEnd && runStartLocation == 0)
					{
						runEnd = true;
						runStartLocation = 1;
						potentialRuns++;
						runStart = false;
					}
					else if(player.transform.position.x >= 4 && runStart && runStartLocation == 1)
					{
						runEnd = true;
						runStart = false;
						ballAtWicket = true;
					}
				}
				else
				{
					if(!runStart && runStartLocation == 1)
					{
						runEnd = false;
						runStart = true;
					}
					player.transform.position = Vector3.MoveTowards(player.transform.position, new Vector3(-6, player.transform.position.y, player.transform.position.z), 0.0075f * swingSpeed);
					if(player.transform.position.x <= -4 && !runEnd && runStartLocation == 1)
					{
						runEnd = true;
						runStartLocation = 0;
						potentialRuns++;
						runStart = false;
					}
					else if(player.transform.position.x <= -4 && runStart && runStartLocation == 0)
					{
						runEnd = true;
						runStart = false;
						ballAtWicket = true;
					}
				}
				if(ballAtWicket)
				{
					if(Math.Abs(player.transform.position.x) < 4)
					{
						wicketDown = true;
						ballAtWicket = false;
						canRun = false;
						makeDecision = true;
						wicket.GetComponent<Outline>().enabled = true;
						otherWicket.GetComponent<Outline>().enabled = true;
					}
					else
					{
						canRun = false;
						runs += potentialRuns;
						result.text = potentialRuns + " Runs";
						player.transform.position = playerStartPosition;
						transform.position = batStartPosition;
						makeDecision = true;
					}
				}
			}
		}
		if(makeDecision && ball.GetComponent<CricketBall>().gameRunning)
		{
			if(wicketDown)
			{
				Debug.Log("Wicket Down");
				wickets++;
				wicketDown = false;
				result.text = "Wicket Down";
				canRun = false;
				player.transform.position = playerStartPosition;
				transform.position = batStartPosition;
				outSound.GetComponent<AudioSource>().Play();
			}
			else if(ball.GetComponent<CricketBall>().boundary)
			{
				if(!ball.GetComponent<CricketBall>().field)
				{
					Debug.Log("Ball Hit for 6 runs");
					runs += 6;
					ballDistance = 0;
					ball.GetComponent<CricketBall>().boundary = false;
					ball.GetComponent<CricketBall>().field = false;
					ballHit = false;
					result.text = "6 Runs";
					player.transform.position = playerStartPosition;
					transform.position = batStartPosition;
					outfield.GetComponent<AudioSource>().Play();
				}
				else if(ball.GetComponent<CricketBall>().field)
				{
					Debug.Log("Ball Hit for 4 runs");
					runs += 4;
					ballDistance = 0;
					ball.GetComponent<CricketBall>().field = false;
					ball.GetComponent<CricketBall>().boundary = false;
					result.text = "4 Runs";
					player.transform.position = playerStartPosition;
					transform.position = batStartPosition;
					outfield.GetComponent<AudioSource>().Play();
				}
			}
			makeDecision = false;
			ball.GetComponent<CricketBall>().gameRunning = false;
			player.transform.position = playerStartPosition;
			transform.position = batStartPosition;
			canRun = false;
		}
		score.text = "Runs: " + runs + "\nWickets: " + wickets;
		if(wickets == 10 && gameOver == false)
		{
			result.text = "Game Over!";
			restartGame = true;
			gameOver = true;
			wicket.GetComponent<AudioSource>().Play();
		}
		else if(runs >= 50 && halfCentury == false)
		{
			halfCentury = true;
			result.text = "Half Century!";
			outfield.GetComponent<AudioSource>().Play();
		}
		else if(runs >= 100 && century == false)
		{
			century = true;
			result.text = "Century!";
			outfield.GetComponent<AudioSource>().Play();
		}
	}

	void OnCollisionEnter(Collision other)
	{
		if(other.gameObject == ball && ball.GetComponent<CricketBall>().gameRunning && ball.GetComponent<CricketBall>().ballThrown)
		{
			ball.GetComponent<CricketBall>().ballThrown = false;
			ballHit = true;
			canRun = true;
			catchBall = true;
			GetComponent<AudioSource>().Play();
			Debug.Log("Ball Hit\nSwing Speed: " + swingSpeed + "\nSwing Direction: " + swingDirection.normalized);
			ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
			ball.GetComponent<Rigidbody>().AddForce(swingDirection.normalized * swingSpeed, ForceMode.Impulse);
			ball.GetComponent<LineRenderer>().positionCount = 1;
			ball.GetComponent<LineRenderer>().SetPosition(0, ball.transform.position);
		}
	}
}
