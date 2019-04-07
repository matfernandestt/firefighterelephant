using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class Player : MonoBehaviour
{
	public static Vector3 Velocity;
	public static List<Civilian> CiviliansFollowing = new List<Civilian>();

	public float MoveSpeed;

	private float gravity = -20f;
	private PlayerMovement movement;
	private Vector3 input;

	public static bool CanMove;

	private void Start()
	{
		movement = GetComponent<PlayerMovement>();
		CanMove = true;
	}

	private void Update()
	{
		if (!CanMove)
			return;

		input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

		Velocity.x = input.x * MoveSpeed;
		Velocity.y += gravity * Time.deltaTime;
		movement.Move(Velocity * Time.deltaTime);
	}

	public static IEnumerator WaitFor(float seconds)
	{
		CanMove = false;

		yield return new WaitForSeconds(seconds);

		CanMove = true;
	}
}
