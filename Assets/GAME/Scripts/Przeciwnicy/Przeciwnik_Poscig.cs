using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using System;

[RequireComponent(typeof(NavMeshAgent))]
public class Przeciwnik_Poscig : MonoBehaviour
{

	[SerializeField] private Transform pozycjaGracza;
	[SerializeField] private float promienWyszukiwania;

	private NavMeshAgent mojNavMeshAgent;
	private Animator mojAnimator;
	private Collider[] znalezioneObiekty;
	public bool CzyNavMeshAktywny = true;

	private bool czyAtakuje;
	private bool czyMamCel;

	void Start()
	{
		mojNavMeshAgent = GetComponent<NavMeshAgent>();
		mojAnimator = GetComponent<Animator>();
		czyMamCel = false;
	}

	void Update()
	{
		WyszukajCel();

		if (CzyDoszedlem()) ProbaAtaku();
		else
		{
			mojAnimator.SetBool("AtakBool", false);
			czyAtakuje = false;
		}
	} 

	private bool CzyDoszedlem()
	{
		if (mojNavMeshAgent.remainingDistance <= mojNavMeshAgent.stoppingDistance)
		{
			ZatrzymajSie();
			return true;
		}

		return false;
	}

	private void ZacznijIsc(Vector3 cel)
	{
		if (!CzyNavMeshAktywny) return;
		mojNavMeshAgent.SetDestination(cel);

		if (!mojAnimator.GetBool("CzyIdzie"))
		{
			print("Ustawiam ide na true");
			mojAnimator.SetBool("CzyIdzie", true);
		}
		
	} 

	private void ZatrzymajSie()
	{
		if (mojAnimator.GetBool("CzyIdzie"))
		{
			mojAnimator.SetBool("CzyIdzie", false);
		}
	}

	private void WyszukajCel()
	{
		czyMamCel = false;
		znalezioneObiekty = Physics.OverlapSphere(transform.position, promienWyszukiwania);

		foreach (Collider obiekt in znalezioneObiekty)
		{
			if (obiekt.CompareTag("Gracz"))
			{
				ZacznijIsc(obiekt.transform.position);
				czyMamCel = true;
				print("Znalaz³em Cel");
			}
		}

		if (!czyMamCel)
		{
			mojNavMeshAgent.isStopped = true;
			mojNavMeshAgent.ResetPath();
			ZatrzymajSie();
		}
	}

	private void ProbaAtaku()
	{
		if (!czyMamCel)	return;
		if (czyAtakuje) return;

		czyAtakuje = true;
		PrzeprowadzAtak();
	}

	private void PrzeprowadzAtak()
	{
		mojAnimator.SetBool("AtakBool", true);
	}
}
