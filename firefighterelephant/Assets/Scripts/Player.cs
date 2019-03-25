using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class Player : MonoBehaviour
{
	public float MoveSpeed;

	private Vector3 velocity;
	private float gravity = -20f;
	private PlayerMovement movement;
	private Vector3 input;

	private void Start()
	{
		movement = GetComponent<PlayerMovement>();
	}

	private void Update()
	{
		input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

		velocity.x = input.x * MoveSpeed;
		velocity.y += gravity * Time.deltaTime;
		movement.Move(velocity * Time.deltaTime);
	}
}
