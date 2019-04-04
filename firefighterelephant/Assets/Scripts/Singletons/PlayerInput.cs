using System;
using UnityEngine;

public class PlayerInput : SingletonOfType<PlayerInput>
{
	private bool fireExtinguisherButtonUp;
	private bool fireExtinguisherButtonHold;
	private bool fireExtinguisherButtonDown;
	private bool switchExtinguisherButtonDown;
	private bool kickDoorButtonDown;
	private bool upStairsButtonDown;
	private bool downStairsButtonDown;
    private bool pauseButtonDown;

    public KeyCode FireExtinguisherButton;
	public KeyCode SwitchExtinguisherButton;
	public KeyCode KickDoorButton;
	public KeyCode UpStairsButton;
	public KeyCode DownStairsButton;
    public KeyCode PauseButton;

    public static event Action FireExtinguisherButtonUp;
    public static event Action FireExtinguisherButtonHold;
	public static event Action FireExtinguisherButtonDown;
	public static event Action SwitchExtinguisherButtonDown;
	public static event Action KickDoorButtonDown;
	public static event Action UpStairsButtonDown;
	public static event Action DownStairsButtonDown;
    public static event Action PauseButtonDown;

    private void Update()
	{
		fireExtinguisherButtonUp = Input.GetKeyUp(FireExtinguisherButton);
		fireExtinguisherButtonHold = Input.GetKey(FireExtinguisherButton);
		fireExtinguisherButtonDown = Input.GetKeyDown(FireExtinguisherButton);
		switchExtinguisherButtonDown = Input.GetKeyDown(SwitchExtinguisherButton);
		kickDoorButtonDown = Input.GetKeyDown(KickDoorButton);
		upStairsButtonDown = Input.GetKeyDown(UpStairsButton);
		downStairsButtonDown = Input.GetKeyDown(DownStairsButton);
        pauseButtonDown = Input.GetKeyDown(PauseButton);

        if (fireExtinguisherButtonUp)
        {
	        OnFireExtinguisherButtonUp();
        }

        if (fireExtinguisherButtonHold)
        {
	        OnFireExtinguisherButtonHold();
        }

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

        if (pauseButtonDown)
        {
            OnPauseButtonDown();
        }
	}

	#region Invocators
	private static void OnFireExtinguisherButtonUp()
	{
		FireExtinguisherButtonUp?.Invoke();
	}

	private static void OnFireExtinguisherButtonDown()
	{
		FireExtinguisherButtonDown?.Invoke();
	}

	private static void OnFireExtinguisherButtonHold()
	{
		FireExtinguisherButtonHold?.Invoke();
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

    private static void OnPauseButtonDown()
    {
        PauseButtonDown?.Invoke();
    }
	#endregion

}
