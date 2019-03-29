using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFireExtinguisher : MonoBehaviour
{
	[Header("Properties")]
	public LayerMask FireLayerMask;
	public Transform SpawnPoint;
	public float ExtinguisherRange;

	private FireType actualExtinguisherType;

	#region Delegates
	private void OnEnable()
	{
		PlayerInput.FireExtinguisherButtonDown += Shoot;
		PlayerInput.SwitchExtinguisherButtonDown += SwitchExtinguisher;
	}

	private void OnDisable()
	{
		PlayerInput.FireExtinguisherButtonDown -= Shoot;
		PlayerInput.SwitchExtinguisherButtonDown -= SwitchExtinguisher;
	}
	#endregion

	private void Start()
	{
		actualExtinguisherType = FireType.A;
	}

	public void Shoot()
	{
		var origin = SpawnPoint.position;

		var hit = Physics2D.Raycast(origin, Vector2.right * ExtinguisherRange, FireLayerMask);
		Debug.DrawRay(origin, Vector2.right * ExtinguisherRange, Color.red);

		if (hit)
		{
			var fire = hit.transform.GetComponent<Fire>();

			if (fire != null)
			{
				if (fire.Type == actualExtinguisherType)
				{
					ExtinguishFire();
				}
				else
				{
					Explode();
				}
			}
		}
	}

	private void ExtinguishFire()
	{
		Debug.Log("Extinguish");
	}

	private void Explode()
	{
		Debug.Log("Explode");
	}

	private void SwitchExtinguisher()
	{
		actualExtinguisherType = actualExtinguisherType != FireType.C ? actualExtinguisherType++ : FireType.A;
		Debug.Log("Changed to " + actualExtinguisherType);
	}
}
