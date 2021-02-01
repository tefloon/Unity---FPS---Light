using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zycie : MonoBehaviour
{
    public int ObenceHP;

    public void ZadajObrazenia(int obrazenia)
	{
        ObenceHP -= obrazenia;

		if (ObenceHP <= 0)
		{
            Zgin();
		}

    }

	public virtual void Zgin()
	{
        print("Zgin¹³em - " + transform.name);
        Destroy(gameObject);
	}

}
