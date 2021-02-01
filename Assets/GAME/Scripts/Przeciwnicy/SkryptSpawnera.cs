using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkryptSpawnera : MonoBehaviour
{
    [SerializeField] private GameObject przeciwnikPrefab;
    [Space()]
    [Space()]
    [SerializeField] private int liczbaPrzeciwnikow;
    [SerializeField] private int coIleZwiekszamyLiczbe;
    [Space()]
    [Space()]
    [SerializeField] private int coIleZwiekszamyStatsy;
    [SerializeField] private int wartoscZycia;
    [SerializeField] private float mnoznikZycia;
    [Space()]
    [SerializeField] private float wartoscredkosci;
    [SerializeField] private float mnoznikPredkosci;
    [Space()]
    [Space()]
    [SerializeField] private Transform[] platformySpawnow;

    private int liczbaWtejRundzie = 0;
    private int liczbaZabitych = 0;
    private int liczbaZabitychRunda = 0;

	private void Start()
	{
        NowaFala();
	}

	private void Update()
	{
       SprawdzCzyZabici();
	}

	private void SprawdzCzyZabici()
	{
		if (liczbaZabitychRunda >= liczbaWtejRundzie)
		{
            print("Koniec rundy!");
            NowaFala();
		}
	}

	public void DodajZabitego()
	{
        liczbaZabitych += 1;
        liczbaZabitychRunda += 1;
        print("Liczba zabitych ogó³em: " + liczbaZabitych);
        print("Liczba w rundzie: " + liczbaZabitychRunda + "/" + liczbaPrzeciwnikow);

		if (liczbaZabitych % coIleZwiekszamyLiczbe == 0)
		{
            liczbaPrzeciwnikow += 1;
		}
        if (liczbaZabitych % coIleZwiekszamyStatsy == 0)
		{
            wartoscZycia *= Mathf.FloorToInt(mnoznikZycia);
            wartoscredkosci *= mnoznikPredkosci;
		}
	}

	private void NowaFala()
	{
        int indeksPlatformy = 0;
        liczbaZabitychRunda = 0;
        liczbaWtejRundzie = liczbaPrzeciwnikow;

        for (int i = 0; i < liczbaPrzeciwnikow; i++)
		{
            var zombie = Instantiate(przeciwnikPrefab, platformySpawnow[indeksPlatformy].position, Quaternion.identity);
            var skryptZombie = zombie.GetComponent<Przeciwnik_Poscig>();
            skryptZombie.Setup(wartoscZycia, wartoscredkosci);

            indeksPlatformy += 1;

            if (indeksPlatformy == platformySpawnow.Length) indeksPlatformy = 0;
		}
	}
}
