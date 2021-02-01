using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(AudioSource))]
public class Granat_Wybuch : MonoBehaviour
{
    [SerializeField] private GameObject prefabrykatEksplozji;
    [SerializeField] private float promienEksplozji;
    [SerializeField] private float silaEksplozji;
    [SerializeField] private LayerMask warstwyEksplozji;
    [SerializeField] private int maxObrazenia;

    [SerializeField] private bool czyOpoznicWybuch;
    [SerializeField] private float opoznienie;
    [SerializeField] private AudioClip[] dzwiekiOdbicia;


    private Collider[] znalezioneObiekty;
    private bool czyOdpalony = false;
    private AudioSource odtwarzacz;

	private void Start()
	{
        odtwarzacz = GetComponent<AudioSource>();
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (!czyOdpalony)
		{
            if (czyOpoznicWybuch)
            {
                StartCoroutine(OpoznienieWybuchu());
            }
            else
            {
                WykonajWybuch();
            }

            czyOdpalony = true;
        }

        OdtworzDzwiekOdbicia();
    }

    private void ZadajObrazenia()
	{

	}

    private void OdtworzDzwiekOdbicia()
	{
        int indeksDzwieku = Random.Range(1, dzwiekiOdbicia.Length - 1);
        AudioClip wybrany = dzwiekiOdbicia[indeksDzwieku];

        dzwiekiOdbicia[indeksDzwieku] = dzwiekiOdbicia[0];
        dzwiekiOdbicia[0] = wybrany;

        odtwarzacz.PlayOneShot(wybrany);
    }

    private IEnumerator OpoznienieWybuchu() 
    {
        yield return new WaitForSeconds(opoznienie);
        WykonajWybuch();
    }

    private void WykonajWybuch()
	{

        NadajOdrzut();
        StworzEfekt();
        Destroy(gameObject);
    }

    private void StworzEfekt()
    {
        Instantiate(prefabrykatEksplozji, transform.position, Quaternion.identity);
    }

    private void NadajOdrzut()
    {
        znalezioneObiekty = Physics.OverlapSphere(transform.position, promienEksplozji, warstwyEksplozji);

        foreach (var rzecz in znalezioneObiekty)
        {
            Rigidbody rb = rzecz.GetComponent<Rigidbody>();

            if (rb)
            {
                rb.AddExplosionForce(silaEksplozji, transform.position, promienEksplozji);
            }

            Zycie zycieSkrypt = rzecz.GetComponent<Zycie>();

            if (zycieSkrypt)
            {
                float odleglosc = Vector3.Distance(transform.position, rzecz.transform.position);
                int obrazeniaDoZadania = (int)(maxObrazenia * (odleglosc / promienEksplozji));

                zycieSkrypt.ZadajObrazenia(obrazeniaDoZadania);
            }
        }
    }
}