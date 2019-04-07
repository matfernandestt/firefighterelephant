using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFireExtinguisher : MonoBehaviour
{
	[Header("Properties")] public LayerMask FireLayerMask;
	public Transform SpawnPoint;
	public float ExtinguisherRange;
	public ExtinguisherIcon ExtinguisherIcon;

	[Header("Particles")] public GameObject ExtinguisherParticleA;
	public GameObject ExtinguisherParticleB;
	public GameObject ExtinguisherParticleC;
	public Transform ParticleParent;

	public static int UnlockedExtinguishers;
	public static event Action UnlockExtinguisher;

	private FireType actualExtinguisherType;
	private GameObject actualExtinguisherParticle;
	private Character character;

	#region Delegates

	private void OnEnable()
	{
		PlayerInput.FireExtinguisherButtonUp += EndParticle;
		PlayerInput.FireExtinguisherButtonDown += StartShooting;
		PlayerInput.FireExtinguisherButtonHold += Shoot;
		PlayerInput.SwitchExtinguisherButtonDown += SwitchExtinguisher;
		UnlockExtinguisher += SwitchExtinguisher;
	}

	private void OnDisable()
	{
		PlayerInput.FireExtinguisherButtonUp -= EndParticle;
		PlayerInput.FireExtinguisherButtonDown -= StartShooting;
		PlayerInput.FireExtinguisherButtonHold -= Shoot;
		PlayerInput.SwitchExtinguisherButtonDown -= SwitchExtinguisher;
		UnlockExtinguisher -= SwitchExtinguisher;
	}

	#endregion

	private void Start()
	{
		actualExtinguisherType = FireType.A;
		ParticleParent = FindObjectOfType<DynamicObjects>().transform;
		character = GetComponentInChildren<Character>();

		if (UnlockedExtinguishers == 0)
		{
			ExtinguisherIcon.GetComponent<SpriteRenderer>().enabled = false;
		}
	}

	public void Shoot()
	{
		if (!PlayerAnimations.IsShooting)
		{
			return;
		}

		ReleaseParticle();

		var hit = Physics2D.Raycast(SpawnPoint.position, character.transform.right, ExtinguisherRange, FireLayerMask);
		Debug.DrawRay(SpawnPoint.position, character.transform.right * ExtinguisherRange, Color.red);

		if (hit)
		{
			var fire = hit.collider.GetComponent<Fire>();

			if (fire != null)
			{
				if (fire.Type == actualExtinguisherType)
				{
					fire.Extinguishing = true;
				}
				else
				{
					fire.Explode();
					Debug.Log("Explode");
				}
			}
		}
	}

	private void StartShooting()
	{
		if (UnlockedExtinguishers == 0)
			return;

		PlayerAnimations.PlayerAnimator.SetTrigger(PlayerAnimations.Shoot);
		PlayerAnimations.PlayerAnimator.SetBool(PlayerAnimations.Shooting, true);
		Player.CanMove = false;
	}

	public void ReleaseParticle()
	{
		switch (actualExtinguisherType)
		{
			case FireType.A:
				InstantiateParticle(ExtinguisherParticleA);
				break;
			case FireType.B:
				InstantiateParticle(ExtinguisherParticleB);
				break;
			case FireType.C:
				InstantiateParticle(ExtinguisherParticleC);
				break;
			default:
				Debug.LogError("Extinguisher not found!");
				break;
		}
	}

	private void InstantiateParticle(GameObject particle)
	{
		actualExtinguisherParticle =
			Instantiate(particle, SpawnPoint.position, character.transform.rotation, ParticleParent);
	}

	private void EndParticle()
	{
		PlayerAnimations.IsShooting = false;
		PlayerAnimations.PlayerAnimator.SetBool(PlayerAnimations.Shooting, false);
		Player.CanMove = true;
	}

	private void ExtinguishFire()
	{
		Debug.Log("Extinguish");
	}

	private void SwitchExtinguisher()
	{
		if (UnlockedExtinguishers <= 1)
		{
			ExtinguisherIcon.GetComponent<SpriteRenderer>().enabled = true;

			ExtinguisherIcon.Animator.SetTrigger("First");
			return;
		}

		PlayerAnimations.PlayerAnimator.SetTrigger(PlayerAnimations.Switch);

		if (UnlockedExtinguishers < 3)
		{
			actualExtinguisherType = actualExtinguisherType == FireType.A ? FireType.B : FireType.A;
		}
		else
		{
			actualExtinguisherType = actualExtinguisherType != FireType.C ? actualExtinguisherType + 1 : FireType.A;
		}

		switch (actualExtinguisherType)
		{
			case FireType.A:
				ExtinguisherIcon.Animator.SetTrigger(UnlockedExtinguishers < 3 ? "First" : "SwitchToA");
				break;
			case FireType.B:
				ExtinguisherIcon.Animator.SetTrigger("SwitchToB");
				break;
			case FireType.C:
				ExtinguisherIcon.Animator.SetTrigger("SwitchToC");
				break;
		}

		StartCoroutine(Player.WaitFor(0.8f));
	}

	public static void OnUnlockExtinguisher()
	{
		UnlockExtinguisher?.Invoke();
	}
}

