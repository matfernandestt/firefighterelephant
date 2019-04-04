using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
	public static string Velocity = "Velocity";
	public static string Shoot = "Shoot";
	public static string Kick = "Kick";
	public static string Explode = "Explode";
	public static string Switch = "Switch";
	public static string Collect = "Collect";
	public static string Shooting = "Shooting";

	public Animator PlayerAnimator;

	private void Update()
	{
		PlayerAnimator.SetFloat(Velocity, Mathf.Abs(Player.Velocity.x));
	}
}
