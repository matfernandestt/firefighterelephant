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
				Player.CiviliansFollowing.Add(this);
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

				GameController.CiviliansSaved++;
				GameController.OnSaveCivilian();

				Player.CiviliansFollowing.Remove(this);
			}
		}

		if (Vector2.Distance(transform.position, player.transform.position) > MinimumPlayerDistance)
		{
			RaycastHit2D hitRight = Physics2D.Raycast(transform.position, Vector2.right, RaycastDistance, PlayerLayerMask);
			RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, Vector2.left, RaycastDistance, PlayerLayerMask);

			if (hitRight)
			{
				translation = Vector2.right * Speed * Time.deltaTime;
				transform.Translate(translation);

				var playerHit = hitRight.transform.GetComponent<Player>();

				if (playerHit != null)
				{
					transform.position = new Vector3(transform.position.x, player.transform.position.y);
				}
			}
			else if (hitLeft)
			{
				translation = Vector2.left * Speed * Time.deltaTime;
				transform.Translate(translation);

				var playerHit = hitLeft.transform.GetComponent<Player>();

				if (playerHit != null)
				{
					transform.position = new Vector3(transform.position.x, player.transform.position.y);
				}
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
