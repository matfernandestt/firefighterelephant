using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	private Vector3 offset;

	private void Start()
	{
		var focus = FindObjectOfType<FirstFloor>().transform;
		offset = transform.position - focus.position;

		ChangeFocus(focus);
	}

	public void ChangeFocus(Transform newFocus)
	{
		var position = newFocus.position;
		transform.position = new Vector3(position.x, position.y, transform.position.z);
	}
}
