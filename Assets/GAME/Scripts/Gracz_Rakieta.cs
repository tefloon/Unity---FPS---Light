using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gracz_Rakieta : MonoBehaviour
{
    public GameObject rakieta;
    public Transform miejsce;
    public float predkosc;
    public float rotacja;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		if (Input.GetKeyDown(KeyCode.L))
		{
            
            var go = Instantiate(rakieta, miejsce.position, Quaternion.identity);
            var rb = go.GetComponent<Rigidbody>();
            rb.AddForce(miejsce.forward * predkosc, ForceMode.VelocityChange);
            //rb.AddTorque(miejsce.forward * rotacja, ForceMode.VelocityChange);
		}
    }
}
