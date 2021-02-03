using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gracz_Rakieta_Explode : MonoBehaviour
{
    public GameObject eksplozja;

	private void OnTriggerEnter(Collider other)
	{
		if (!other.CompareTag("Player"))
		{
			Instantiate(eksplozja, transform.position, Quaternion.identity);
			Destroy(gameObject);
		}
	}
}
