using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Bron_Efekty : MonoBehaviour
{
    [SerializeField] private ParticleSystem efektWystrzalu;
    [SerializeField] private Transform koniecLufy;
    
    private Animator animator;
    private ParticleSystem blysk;

    public void PokazEfektStrzalu()
	{
        blysk.Play();
        animator.SetTrigger("Strzel");
    }

    public void Przeladuj()
	{
        animator.SetTrigger("Przeladuj");
    }

    public void SchowajBron()
	{
        animator.SetTrigger("SchowajBron");
    }

    public void PokazBron()
    {
        animator.SetTrigger("PokazBron");
    }

    // Start is called before the first frame update
    void Start()
    {
        blysk = Instantiate(efektWystrzalu, koniecLufy.position, koniecLufy.rotation, transform);
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        

    }
}
