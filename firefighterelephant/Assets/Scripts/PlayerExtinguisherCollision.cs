using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExtinguisherCollision : MonoBehaviour
{
	private Character character;
	private Extinguisher actualExtinguisher;

	public LayerMask ExtinguisherLayerMask;

	private void Start()
	{
		character = GetComponentInChildren<Character>();
	}

	private void OnEnable()
	{
		PlayerInput.KickDoorButtonDown += CollectExtinguisher;
	}

	private void OnDisable()
	{
		PlayerInput.KickDoorButtonDown -= CollectExtinguisher;
	}

	private void CollectExtinguisher()
	{
		if (!CheckForExtinguisher())
		{
			return;
		}

		PlayerAnimations.PlayerAnimator.SetTrigger(PlayerAnimations.Collect);

		StartCoroutine(SwallowExtinguisher());
	}

	private bool CheckForExtinguisher()
	{
		var position = transform.position;
		var hit = Physics2D.Raycast(position, character.transform.right, 1f, ExtinguisherLayerMask);

		Debug.DrawRay(position, character.transform.right, Color.red);

		if (hit)
		{
			var extinguisher = hit.transform.GetComponent<Extinguisher>();

			if (extinguisher != null)
			{
				actualExtinguisher = extinguisher;
				PlayerAnimations.NewExtinguisher = extinguisher.GetComponent<SpriteRenderer>().sprite;
				Destroy(extinguisher.gameObject, 0.66f);

				switch (extinguisher.ExtinguisherType)
				{
					case FireType.A:
						PlayerFireExtinguisher.UnlockedExtinguishers = 1;
						break;
					case FireType.B:
						PlayerFireExtinguisher.UnlockedExtinguishers = 2;
						break;
					case FireType.C:
						PlayerFireExtinguisher.UnlockedExtinguishers = 3;
						break;
					default:
						throw new ArgumentOutOfRangeException();
				}

				return true;
			}
		}

		return false;
	}

	private IEnumerator SwallowExtinguisher()
	{
		Player.CanMove = false;

		yield return new WaitForSeconds(2f);

		Player.CanMove = true;

		PlayerFireExtinguisher.OnUnlockExtinguisher();
	}
}
