using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Przeciwnik_Zycie : Zycie
{
	private SkryptSpawnera skryptSpawnera;
	private Animator mojAnimator;
	private bool czyZyje = true;

	private void Start()
	{
		mojAnimator = GetComponent<Animator>();
		skryptSpawnera = FindObjectOfType<SkryptSpawnera>();
	}

	public override void Zgin()
	{
		if (!czyZyje) return;

		skryptSpawnera.DodajZabitego();

		int los = Random.Range(0, 2);

		if (los == 1)
		{
			mojAnimator.SetTrigger("Smierc1");
		}
		else
		{
			mojAnimator.SetTrigger("Smierc2");
		}

		GetComponent<NavMeshAgent>().enabled = false;
		GetComponent<Przeciwnik_Poscig>().enabled = false;
		GetComponent<Collider>().enabled = false;

		czyZyje = false;
	}
}
