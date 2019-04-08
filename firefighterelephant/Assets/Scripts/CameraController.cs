using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	public float DampTime = 0.125f;
	public Transform FirstFloor;

	private Vector3 cameraVelocity;
	private Transform actualFocus;

	private void Start()
	{
		cameraVelocity = Vector3.zero;

		ChangeFocus(FirstFloor);
	}

	private void Update()
	{
		Vector3 desiredPosition = new Vector3(actualFocus.position.x, actualFocus.position.y, transform.position.z);
		Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref cameraVelocity, DampTime);
		transform.position = smoothedPosition;
	}

	public void ChangeFocus(Transform newFocus)
	{
		actualFocus = newFocus;
	}
}
