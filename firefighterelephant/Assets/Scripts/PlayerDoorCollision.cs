using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDoorCollision : MonoBehaviour
{
	public LayerMask DoorLayerMask;

	private RoomDoor actualDoor;
	private Character character;

	private void Start()
	{
		character = GetComponentInChildren<Character>();
	}

	private void OnEnable()
	{
		PlayerInput.KickDoorButtonDown += OpenDoor;
	}

	private void OnDisable()
	{
		PlayerInput.KickDoorButtonDown -= OpenDoor;
	}

	private void OpenDoor()
	{
		if (!CheckForDoor())
		{
			return;
		}

		if (!actualDoor.IsOpen)
			PlayerAnimations.PlayerAnimator.SetTrigger(PlayerAnimations.Kick);

		StartCoroutine(KickDoor());
	}

	private bool CheckForDoor()
	{
		var position = transform.position;
		var hit = Physics2D.Raycast(position, character.transform.right, 1f, DoorLayerMask);

		Debug.DrawRay(position, character.transform.right, Color.red);

		if (hit)
		{
			var door = hit.transform.GetComponent<RoomDoor>();

			if (door != null)
			{
				actualDoor = door;
				return true;
			}
		}

		return false;
	}

	private IEnumerator KickDoor()
	{
		ScreenFade.Fade();
		actualDoor.Animator.SetTrigger("Open");

		Player.CanMove = false;

		yield return new WaitForSeconds(1f);

		actualDoor.EnterDoor();
		actualDoor.IsOpen = true;

		var doorTransform = actualDoor.OtherDoor.transform;

		Player.Velocity = Vector3.zero;

		transform.position = doorTransform.position;

		Player.CanMove = true;
	}
}
