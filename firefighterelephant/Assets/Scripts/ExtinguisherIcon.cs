using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtinguisherIcon : MonoBehaviour
{
	public static Animator Animator;

	private void Start()
	{
		Animator = GetComponent<Animator>();
	}
}
