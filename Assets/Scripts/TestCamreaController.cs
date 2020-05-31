using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCamreaController : MonoBehaviour {

	public float speed = 10f;

	void Update () {
		float h = Input.GetAxisRaw("Horizontal");
		float v = Input.GetAxisRaw("Vertical");

		Vector3 direction = new Vector3(h, v).normalized;

		transform.Translate(direction * speed * Time.deltaTime);

		if (Input.GetKey(KeyCode.Space))
		{
			speed = 20f;
		}
		else
		{
			speed = 10f;
		}
	}
}
