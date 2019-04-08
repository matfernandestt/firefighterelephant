using System.Collections;
using UnityEngine;

public class Civilian : MonoBehaviour
{
	private bool canMove;
	private bool safe;
	private Vector3 translation;
	private Player player;
	private CivilianAnimations civilianAnimations;

	public float Speed;
	public float RaycastDistance;
	public float SavingDistance;
	public float MinimumPlayerDistance;
	public LayerMask PlayerLayerMask;
	public LayerMask SafeZoneLayerMask;

	[HideInInspector] public float Velocity;

	private void Start()
	{
		player = FindObjectOfType<Player>();
		civilianAnimations = GetComponentInChildren<CivilianAnimations>();
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

				civilianAnimations.CivilianAnimator.SetTrigger("Celebrate");

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

				civilianAnimations.Parent.transform.eulerAngles = new Vector3(transform.rotation.x, 0f, transform.rotation.z);

				Velocity = 1;

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

				civilianAnimations.Parent.transform.eulerAngles = new Vector3(transform.rotation.x, 180f, transform.rotation.z);

				Velocity = 1;

				var playerHit = hitLeft.transform.GetComponent<Player>();

				if (playerHit != null)
				{
					transform.position = new Vector3(transform.position.x, player.transform.position.y);
				}
			}
			else
			{
				Velocity = 0;
			}

		}
		else
		{
			Velocity = 0;
		}

		var position = transform.position;
		Debug.DrawRay(position, Vector2.right * RaycastDistance, Color.red);
		Debug.DrawRay(position, Vector2.left * RaycastDistance, Color.red);
	}

	private void EnterDoor()
	{
		transform.position = player.transform.position;
	}
}
