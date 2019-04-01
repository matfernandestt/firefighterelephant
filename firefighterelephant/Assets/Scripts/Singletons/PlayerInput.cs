using System;
using UnityEngine;

public class PlayerInput : SingletonOfType<PlayerInput>
{
	private bool fireExtinguisherButtonDown;
	private bool switchExtinguisherButtonDown;
	private bool kickDoorButtonDown;
	private bool upStairsButtonDown;
	private bool downStairsButtonDown;

	public KeyCode FireExtinguisherButton;
	public KeyCode SwitchExtinguisherButton;
	public KeyCode KickDoorButton;
	public KeyCode UpStairsButton;
	public KeyCode DownStairsButton;

	public static event Action FireExtinguisherButtonDown;
	public static event Action SwitchExtinguisherButtonDown;
	public static event Action KickDoorButtonDown;
	public static event Action UpStairsButtonDown;
	public static event Action DownStairsButtonDown;

	private void Update()
	{
		fireExtinguisherButtonDown = Input.GetKey(FireExtinguisherButton);
		switchExtinguisherButtonDown = Input.GetKeyDown(SwitchExtinguisherButton);
		kickDoorButtonDown = Input.GetKeyDown(KickDoorButton);
		upStairsButtonDown = Input.GetKeyDown(UpStairsButton);
		downStairsButtonDown = Input.GetKeyDown(DownStairsButton);

		if (fireExtinguisherButtonDown)
		{
			OnFireExtinguisherButtonDown();
		}

		if (switchExtinguisherButtonDown)
		{
			OnSwitchExtinguisherButtonDown();
		}

		if (kickDoorButtonDown)
		{
			OnKickDoorButtonDown();
		}

		if (upStairsButtonDown)
		{
			OnUpStairsButtonDown();
		}

		if (downStairsButtonDown)
		{
			OnDownStairsButtonDown();
		}
	}

	#region Invocators
	private static void OnFireExtinguisherButtonDown()
	{
		FireExtinguisherButtonDown?.Invoke();
	}

	private static void OnSwitchExtinguisherButtonDown()
	{
		SwitchExtinguisherButtonDown?.Invoke();
	}

	private static void OnKickDoorButtonDown()
	{
		KickDoorButtonDown?.Invoke();
	}

	private static void OnUpStairsButtonDown()
	{
		UpStairsButtonDown?.Invoke();
	}

	private static void OnDownStairsButtonDown()
	{
		DownStairsButtonDown?.Invoke();
	}
	#endregion
}
