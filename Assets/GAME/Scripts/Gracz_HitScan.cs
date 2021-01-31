using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Gracz_HitScan : MonoBehaviour
{
	[SerializeField] private float odstepStrzalow;
	[SerializeField] private Transform miejsceStrzalu;
	[SerializeField] private GameObject efektTrafienia;
	[SerializeField] private LayerMask warstwyTrafienia;
	
	[SerializeField] private AudioClip dzwiekStrzalu;
	[SerializeField] private AudioClip dzwiekPrzeladowania;

	[SerializeField] private int maxAmmo;
	[SerializeField] private float dlugoscPrzeladowania;

	private int obecneAmmo;
	private bool czyPrzeladowuje;

	private float nastepnyStrzal = 0;
	private AudioSource odtwarzacz;

	private Bron_Efekty skryptBroni;

	private void Start()
	{
		obecneAmmo = maxAmmo;
		odtwarzacz = GetComponent<AudioSource>();
		UaktualnijBron();
	}

	private void UaktualnijBron()
	{
		skryptBroni = GetComponentInChildren<Bron_Efekty>();
	}

	// Update is called once per frame
	void Update()
    {
		if (CzyMoznaStrzelac())
		{
			if (Input.GetButton("Fire1") && Time.time >= nastepnyStrzal)
			{
				print("Strzelam!");
				StrzalRaycast();
				nastepnyStrzal = Time.time + odstepStrzalow;
				obecneAmmo -= 1;
			}
		}

		if (Input.GetKeyDown(KeyCode.R) && !czyPrzeladowuje)
		{
			StartCoroutine(Przeladuj());
		}
	}

	IEnumerator Przeladuj()
	{
		print("Prze³adowujê...");
		skryptBroni.Przeladuj();

		czyPrzeladowuje = true;
		odtwarzacz.PlayOneShot(dzwiekPrzeladowania);

		yield return new WaitForSeconds(dlugoscPrzeladowania);	
		obecneAmmo = maxAmmo;

		czyPrzeladowuje = false;
		print("Prze³adowanie wykonane!");
	}

	bool CzyMoznaStrzelac()
	{
		if (obecneAmmo <= 0) return false;
		if (czyPrzeladowuje) return false;

		return true;
	}

	void StrzalRaycast()
	{
		RaycastHit cel;

		if (Physics.Raycast(miejsceStrzalu.position, miejsceStrzalu.forward, out cel, warstwyTrafienia))
		{
			if(cel.transform.gameObject.layer != LayerMask.NameToLayer("Gracz"))
			{
				Debug.DrawRay(miejsceStrzalu.position, miejsceStrzalu.forward * 50f, Color.green, 2f);
				var go = Instantiate(efektTrafienia, cel.point, Quaternion.LookRotation(cel.normal));
			}	
		}

		skryptBroni.PokazEfektStrzalu();
		odtwarzacz.PlayOneShot(dzwiekStrzalu);
	}
}
