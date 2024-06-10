using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CricketBall : MonoBehaviour
{
	float ballLaunchSpeed;
	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		if(Input.GetKeyDown(KeyCode.Space))
		{
			ballLaunchSpeed = Random.Range(10f, 20f);
			transform.position = new Vector3(6f, 1.1f, 0);
			transform.Translate(Vector3.forward * ballLaunchSpeed * Time.deltaTime);
		}
	}
}