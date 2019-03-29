using System;
using UnityEngine;

public class PlayerInput : SingletonOfType<PlayerInput>
{
	private bool fireExtinguisherButtonDown;
	private bool switchExtinguisherButtonDown;
	private bool kickDoorButtonDown;

	public KeyCode FireExtinguisherButton;
	public KeyCode SwitchExtinguisherButton;
	public KeyCode KickDoorButton;

	public static event Action FireExtinguisherButtonDown;
	public static event Action SwitchExtinguisherButtonDown;
	public static event Action KickDoorButtonDown;

	private void Update()
	{
		fireExtinguisherButtonDown = Input.GetKey(FireExtinguisherButton);
		switchExtinguisherButtonDown = Input.GetKey(SwitchExtinguisherButton);
		kickDoorButtonDown = Input.GetKey(KickDoorButton);

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
	#endregion
}
