using System.Collections;
using UnityEngine;

public class Civilian : MonoBehaviour
{
	private bool canMove;
	private bool safe;
	private Vector3 translation;
	private Player player;

	public float Speed;
	public float RaycastDistance;
	public float SavingDistance;
	public float MinimumPlayerDistance;
	public LayerMask PlayerLayerMask;
	public LayerMask SafeZoneLayerMask;

	private void Start()
	{
		player = FindObjectOfType<Player>();
	}

	private void Update()
	{
		if (safe)
			return;

		if (!canMove)
		{
			if (Physics2D.Raycast(transform.position, Vector2.right, SavingDistance, PlayerLayerMask))
			{
				canMove = true;
				PlayerInput.KickDoorButtonDown += EnterDoor;
				PlayerInput.UpStairsButtonDown += EnterDoor;
				PlayerInput.DownStairsButtonDown += EnterDoor;
			}

			return;
		}

		var hit = Physics2D.Raycast(transform.position, Vector2.down, 2, SafeZoneLayerMask);

		if (hit)
		{
			SafeZone safeZone = hit.transform.GetComponent<SafeZone>();

			if (safeZone != null)
			{
				safe = true;
				PlayerInput.KickDoorButtonDown -= EnterDoor;
				PlayerInput.UpStairsButtonDown -= EnterDoor;
				PlayerInput.DownStairsButtonDown -= EnterDoor;
			}
		}

		if (Vector2.Distance(transform.position, player.transform.position) > MinimumPlayerDistance)
		{
			if (Physics2D.Raycast(transform.position, Vector2.right, RaycastDistance, PlayerLayerMask))
			{
				translation = Vector2.right * Speed * Time.deltaTime;
				transform.Translate(translation);
			}
			else if (Physics2D.Raycast(transform.position, Vector2.left, RaycastDistance, PlayerLayerMask))
			{
				translation = Vector2.left * Speed * Time.deltaTime;
				transform.Translate(translation);
			}
		}

		var position = transform.position;
		Debug.DrawRay(position, Vector2.right * RaycastDistance, Color.red);
		Debug.DrawRay(position, Vector2.left * RaycastDistance, Color.red);
	}

	private void EnterDoor()
	{
		transform.position = player.transform.position;
	}

	private void OnDisable()
	{
		PlayerInput.KickDoorButtonDown -= EnterDoor;
		PlayerInput.UpStairsButtonDown -= EnterDoor;
		PlayerInput.DownStairsButtonDown -= EnterDoor;
	}
}
