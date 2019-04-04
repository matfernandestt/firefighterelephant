using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
	private Player player;

	private void Start()
	{
		player = GetComponentInParent<Player>();
	}

	private void Update()
	{
		if (Player.Velocity.x > 0)
			transform.eulerAngles = new Vector3(transform.rotation.x, 0f, transform.rotation.z);
		if (Player.Velocity.x < 0)
			transform.eulerAngles = new Vector3(transform.rotation.x, 180f, transform.rotation.z);
	}
}
