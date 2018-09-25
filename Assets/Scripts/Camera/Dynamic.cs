using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dynamic : MonoBehaviour
{
	public GameObject player1;
	public GameObject player2;

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		Vector3 position = (player1.transform.position + player2.transform.position) / 2;
		transform.SetPositionAndRotation(position, transform.rotation);
		transform.Translate(-Vector3.forward * (Mathf.Abs((player1.transform.position - player2.transform.position).magnitude) + 5));
	}
}