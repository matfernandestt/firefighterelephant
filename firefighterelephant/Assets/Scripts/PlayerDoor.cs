using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDoor : MonoBehaviour
{
	public LayerMask DoorLayerMask;

	private RoomDoor actualDoor;

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
		PlayerAnimations.PlayerAnimator.SetTrigger(PlayerAnimations.Kick);

		if (!CheckForDoor())
			return;

		StartCoroutine(KickDoor());
	}

	private bool CheckForDoor()
	{
		var position = transform.position;
		var hit = Physics2D.Raycast(position, Vector2.down, 2f, DoorLayerMask);

		Debug.DrawRay(position, Vector2.down * 2f, Color.red);

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
		yield return new WaitForSeconds(1f);

		actualDoor.EnterDoor();
		var doorTransform = actualDoor.OtherDoor.transform;

		Player.Velocity = Vector3.zero;

		transform.position = doorTransform.position;
	}
}
