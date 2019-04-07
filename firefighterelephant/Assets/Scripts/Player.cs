using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class Player : MonoBehaviour
{
	public static Vector3 Velocity;

	public float MoveSpeed;

	private float gravity = -20f;
	private PlayerMovement movement;
	private Vector3 input;

	private static bool canMove;

	private void Start()
	{
		movement = GetComponent<PlayerMovement>();
		canMove = true;
	}

	private void Update()
	{
		if (!canMove)
			return;

		input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

		Velocity.x = input.x * MoveSpeed;
		Velocity.y += gravity * Time.deltaTime;
		movement.Move(Velocity * Time.deltaTime);
	}

	public static IEnumerator WaitFor(float seconds)
	{
		canMove = false;

		yield return new WaitForSeconds(seconds);

		canMove = true;
	}
}
