using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomDoor : MonoBehaviour
{
	public Transform Room;
	public RoomDoor OtherDoor;
	public bool EntranceDoor;

	[HideInInspector] public bool IsOpen;
	[HideInInspector] public Animator Animator;

	private CameraController cameraController;

	private void Start()
	{
		cameraController = FindObjectOfType<CameraController>();
		Animator = GetComponent<Animator>();
	}

	public void EnterDoor()
	{
		cameraController.ChangeFocus(Room);
	}
}
