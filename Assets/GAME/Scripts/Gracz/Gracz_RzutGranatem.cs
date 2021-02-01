using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Gracz_RzutGranatem : MonoBehaviour
{
    [SerializeField] private Transform miejsceGranatu;
    [SerializeField] private GameObject granatPref;
    [SerializeField] private float silaRzutu;
    [SerializeField] private float silaRotacji;
    [SerializeField] private float odstepGranatow;

    [SerializeField] private AudioClip dzwiekRzutu;
    [SerializeField] private float opoznienieDzwieku;

    private bool czyGranatWybrany = false;
    private bool czyRzuca = false;
    private float nastepnyGranat = 0;
    private AudioSource odtwarzacz;
    private Bron_Efekty skryptBroni;

    // Start is called before the first frame update
    void Start()
    {
        odtwarzacz = GetComponent<AudioSource>();
        skryptBroni = GetComponentInChildren<Bron_Efekty>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
			if (Time.time >= nastepnyGranat && !czyGranatWybrany)
			{
                czyGranatWybrany = true;
                skryptBroni.SchowajBron();
            }
            else if(czyGranatWybrany) 
			{
                czyGranatWybrany = false;
                skryptBroni.PokazBron();
            }
        }

		if (czyGranatWybrany && Input.GetMouseButtonDown(1))
		{
            czyRzuca = true;
		}

		if (czyRzuca)
		{
			if (Input.GetMouseButtonUp(1))
			{
                print("Rzucam!");
                StartCoroutine(RzutGranatem());
                nastepnyGranat = Time.time + odstepGranatow;

                czyRzuca = czyGranatWybrany = false;
            }
		}
    }


	private IEnumerator RzutGranatem()
	{
        odtwarzacz.PlayOneShot(dzwiekRzutu);

        yield return new WaitForSeconds(opoznienieDzwieku);

        var go = Instantiate(granatPref, miejsceGranatu.position, Quaternion.identity);
        var rb = go.GetComponent<Rigidbody>();
        rb.AddForce(miejsceGranatu.forward * silaRzutu, ForceMode.VelocityChange);
        rb.AddTorque(Vector3.right * silaRotacji, ForceMode.Impulse);

        yield return new WaitForSeconds(0.5f);

        skryptBroni.PokazBron();
    }
}
