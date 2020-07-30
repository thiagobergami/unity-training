using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ControlaArma : MonoBehaviour{

    //preparandoParaOJson

    public GameObject Bala;
    public GameObject CanoDaArma;
    //public GameObject CanoLanterna;
    public AudioClip SomDoTiro;
    //public float proximoTiro;
    //public float tempoResfriamento = 20;
    public int municao;
    private int geraArma;
    public EscolheArma arma;
    public GameObject[] teste;
    private float nextShoot = 0;


    // Use this for initialization
    void Start () {
        //Escolher uma Arma
        arma = PegaArma();
        teste = GameObject.FindGameObjectsWithTag("Hand");
        //teste = transform.Find("Hand_Right_int").gameObject;
        teste[0].transform.GetChild(arma.PosicaoArma).gameObject.SetActive(true);
    }
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Fire1"))
        {            
            if (nextShoot <= 0) {
                ControlaAudio.instancia.PlayOneShot(SomDoTiro);
                Instantiate(Bala, CanoDaArma.transform.position, CanoDaArma.transform.rotation);
                nextShoot = arma.ProximoTiro;
            }           
        }
        nextShoot -= Time.deltaTime * arma.TempoResfriamento;
	}

    EscolheArma PegaArma() {
        geraArma = Random.Range(0, 8);

        switch (geraArma)
        {
            case (int) EscolheArma.Armas.Pistola:
                arma = new EscolheArma(geraArma, 20, 50);
                break;
            case (int) EscolheArma.Armas.Prego:
                arma = new EscolheArma(geraArma, 5, 50);
                break;
            case (int)EscolheArma.Armas.Desert:
                arma = new EscolheArma(geraArma, 5, 50);
                break;
            case (int)EscolheArma.Armas.MP:
                arma = new EscolheArma(geraArma, 5, 50);
                break;
            case (int)EscolheArma.Armas.Pistola_USP:
                arma = new EscolheArma(geraArma, 5, 50);
                break;
            case (int)EscolheArma.Armas.PistoAgua:
                arma = new EscolheArma(geraArma, 5, 50);
                break;
            case (int)EscolheArma.Armas.Revolver01:
                arma = new EscolheArma(geraArma, 5, 50);
                break;
            case (int)EscolheArma.Armas.Revolver02:
                arma = new EscolheArma(geraArma, 5, 50);
                break;
            default:
                break;
        }
        //string Arma = EscolheArma.
        
        return arma;
    }
}
