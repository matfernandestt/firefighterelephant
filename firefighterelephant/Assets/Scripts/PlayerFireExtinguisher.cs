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

    private int _dir = 1;

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
		var origin = SpawnPoint.localPosition;

        if (Player.Velocity.x > 0)
            _dir = 1;
        if(Player.Velocity.x < 0)
            _dir = -1;

        origin = new Vector3(transform.position.x + origin.x * _dir, SpawnPoint.position.y, SpawnPoint.position.z);

        var hit = Physics2D.Raycast(origin, (transform.right * _dir) * ExtinguisherRange, FireLayerMask);
		Debug.DrawRay(origin, (transform.right * _dir) * ExtinguisherRange, Color.red);

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
