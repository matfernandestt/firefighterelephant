using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypesOfStairs
{
	Up,
	Down
}

public class PlayerStairsCollision : MonoBehaviour
{
	public LayerMask StairLayerMask;

	private CameraController cameraController;
	private Stair actualStair;
	private bool inFrontOfAStair;

	private void Start()
	{
		cameraController = FindObjectOfType<CameraController>();
	}

	private void OnEnable()
	{
		PlayerInput.UpStairsButtonDown += GoUpStairs;
		PlayerInput.DownStairsButtonDown += GoDownStairs;
	}

	private void OnDisable()
	{
		PlayerInput.UpStairsButtonDown -= GoUpStairs;
		PlayerInput.DownStairsButtonDown -= GoDownStairs;
	}

	private void GoUpStairs()
	{
		if (!CheckForStair())
			return;

		Go(TypesOfStairs.Up);
	}

	private void GoDownStairs()
	{
		if (!CheckForStair())
			return;

		Go(TypesOfStairs.Down);
	}

	private bool CheckForStair()
	{
		var hit = Physics2D.Raycast(transform.position, Vector2.down, 2f, StairLayerMask);

		Debug.DrawRay(transform.position, Vector2.down, Color.red);

		if (hit)
		{
			var stair = hit.transform.GetComponent<Stair>();

			if (stair != null)
			{
				actualStair = stair;
				return true;
			}
		}

		return false;
	}

	private void Go(TypesOfStairs typeOfStair)
	{
		var upStair = actualStair.UpStair;
		var downStair = actualStair.DownStair;

		switch (typeOfStair)
		{
			case TypesOfStairs.Up:

				if (upStair != null)
				{
					transform.position = upStair.transform.position;
					cameraController.ChangeFocus(upStair.Room);
				}

				break;

			case TypesOfStairs.Down:

				if (downStair != null)
				{
					transform.position = downStair.transform.position;
					cameraController.ChangeFocus(downStair.Room);
				}

				break;

			default:
				throw new ArgumentOutOfRangeException(nameof(typeOfStair), typeOfStair, null);
		}
	}
}
