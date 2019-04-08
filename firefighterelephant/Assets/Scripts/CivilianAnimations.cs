using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CivilianAnimations : MonoBehaviour
{
	[HideInInspector] public Animator CivilianAnimator;
	public Civilian Civilian;
	public Transform Parent;

	private void Start()
	{
		CivilianAnimator = GetComponent<Animator>();
	}

	private void Update()
	{
		CivilianAnimator.SetFloat("Speed", Mathf.Abs(Civilian.Velocity));
	}
}
