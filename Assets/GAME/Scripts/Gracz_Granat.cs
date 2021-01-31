using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Gracz_Granat : MonoBehaviour
{
    [SerializeField] private Transform miejsceGranatu;
    [SerializeField] private GameObject granatPref;
    [SerializeField] private float silaRzutu;
    [SerializeField] private float silaRotacji;
    [SerializeField] private float odstepGranatow;

    [SerializeField] private AudioClip dzwiekRzutu;
    [SerializeField] private float opoznienieDzwieku;

    private float nastepnyGranat = 0;
    private AudioSource odtwarzacz;

    // Start is called before the first frame update
    void Start()
    {
        odtwarzacz = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && Time.time >= nastepnyGranat)
        {
            print("Rzucam!");
            StartCoroutine(RzutGranatem());
            nastepnyGranat = Time.time + odstepGranatow;
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
    }
}
