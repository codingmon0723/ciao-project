using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(TrackObjectController))]
public class SawTrap : MonoBehaviour {

	public Transform saw;
	public float rotateSpeed;

	TrackObjectController trackController;
	
	void Awake()
	{
		trackController = GetComponent<TrackObjectController>();
	}

	void Update () {
		Vector3 velocity = trackController.CalculatemMovement();

		saw.transform.rotation = Quaternion.AngleAxis(rotateSpeed, Vector3.forward) * saw.transform.rotation;

		transform.Translate(velocity);
	}
}
