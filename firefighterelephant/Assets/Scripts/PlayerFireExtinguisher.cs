using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFireExtinguisher : MonoBehaviour
{
	[Header("Properties")] public LayerMask FireLayerMask;
	public Transform SpawnPoint;
	public float ExtinguisherRange;

	[Header("Particles")] public GameObject ExtinguisherParticleA;
	public GameObject ExtinguisherParticleB;
	public GameObject ExtinguisherParticleC;
	public Transform ParticleParent;

	private FireType actualExtinguisherType;
	private GameObject actualExtinguisherParticle;

	private int direction = 1;

	#region Delegates

	private void OnEnable()
	{
		PlayerInput.FireExtinguisherButtonUp += EndParticle;
		PlayerInput.FireExtinguisherButtonDown += StartShooting;
		PlayerInput.FireExtinguisherButtonHold += Shoot;
		PlayerInput.SwitchExtinguisherButtonDown += SwitchExtinguisher;
	}

	private void OnDisable()
	{
		PlayerInput.FireExtinguisherButtonUp -= EndParticle;
		PlayerInput.FireExtinguisherButtonDown -= StartShooting;
		PlayerInput.FireExtinguisherButtonHold -= Shoot;
		PlayerInput.SwitchExtinguisherButtonDown -= SwitchExtinguisher;
	}

	#endregion

	private void Start()
	{
		actualExtinguisherType = FireType.A;
	}

	public void Shoot()
	{
		var origin = SpawnPoint.localPosition;

		if (Player.Velocity.x > 0)
			direction = 1;
		if (Player.Velocity.x < 0)
			direction = -1;

		origin = new Vector3(transform.position.x + origin.x * direction, SpawnPoint.position.y, SpawnPoint.position.z);

		var hit = Physics2D.Raycast(origin, (transform.right * direction) * ExtinguisherRange, FireLayerMask);
		Debug.DrawRay(origin, (transform.right * direction) * ExtinguisherRange, Color.red);

		if (hit)
		{
			var fire = hit.collider.GetComponent<Fire>();

			if (fire != null)
			{
				if (fire.Type == actualExtinguisherType)
				{
					ExtinguishFire();
				}
				else
				{
					fire.Explode();
				}
			}
		}
	}

	private void StartShooting()
	{
		PlayerAnimations.PlayerAnimator.SetTrigger(PlayerAnimations.Shoot);
	}

	public void ReleaseParticle()
	{
		PlayerAnimations.PlayerAnimator.SetBool(PlayerAnimations.Shooting, true);

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
				throw new ArgumentOutOfRangeException();
		}
	}

	private void InstantiateParticle(GameObject particle)
	{
		actualExtinguisherParticle =
			Instantiate(particle, SpawnPoint.position, particle.transform.rotation, ParticleParent);
	}

	private void EndParticle()
	{
		PlayerAnimations.PlayerAnimator.SetBool(PlayerAnimations.Shooting, false);
		Destroy(actualExtinguisherParticle);
	}

	private void ExtinguishFire()
	{
		Debug.Log("Extinguish");
	}

	private void SwitchExtinguisher()
	{
		PlayerAnimations.PlayerAnimator.SetTrigger(PlayerAnimations.Switch);

		actualExtinguisherType = actualExtinguisherType != FireType.C ? actualExtinguisherType + 1 : FireType.A;
		Debug.Log("Changed to " + actualExtinguisherType);

		switch (actualExtinguisherType)
		{
			case FireType.A:
				ExtinguisherIcon.Animator.SetTrigger("SwitchToA");
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
}
	
