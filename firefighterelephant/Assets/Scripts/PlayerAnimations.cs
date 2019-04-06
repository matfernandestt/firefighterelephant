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

	public static Animator PlayerAnimator;
	public PlayerFireExtinguisher FireExtinguisher;

	private Animator animator;

	private void Start()
	{
		animator = GetComponent<Animator>();
		PlayerAnimator = animator;
	}

	private void Update()
	{
		animator.SetFloat(Velocity, Mathf.Abs(Player.Velocity.x));
	}

	public void ReleaseExtinguisherParticle()
	{
		FireExtinguisher.ReleaseParticle();
	}
}
