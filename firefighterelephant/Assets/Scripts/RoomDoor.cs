using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomDoor : MonoBehaviour
{
	public Transform Room;
	public RoomDoor OtherDoor;
	public bool EntranceDoor;

	private CameraController cameraController;

	private void Start()
	{
		cameraController = FindObjectOfType<CameraController>();
	}

	public void EnterDoor()
	{
		cameraController.ChangeFocus(Room);
	}
}
