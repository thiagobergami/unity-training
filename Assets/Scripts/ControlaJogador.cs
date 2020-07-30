using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaJogador : MonoBehaviour, IMatavel, ICuravel
{
        
    private Vector3 direcao;
    public LayerMask MascaraChao;
    public GameObject TextoGameover;
    //public bool Vivo = true;
    public ControlaInterface scriptControlaInterface;

    private AnimacaoPersonagem animacaoJogador;
    public Status statusJogador;
    private MovimentoJogador meuMovimentoJogador;
    public AudioClip SomDeDano;

    private void Start()
    {
        int geraTipoJogador = Random.Range(1, 23);
        transform.GetChild(geraTipoJogador).gameObject.SetActive(true);
        animacaoJogador = GetComponent<AnimacaoPersonagem>();
        meuMovimentoJogador = GetComponent<MovimentoJogador>();
        statusJogador = GetComponent<Status>();
        
    }

    // Update is called once per frame
    void Update()
    {

        float eixoX = Input.GetAxis("Horizontal");
        float eixoZ = Input.GetAxis("Vertical");

        direcao = new Vector3(eixoX, 0, eixoZ);

        animacaoJogador.Movimentar(direcao.magnitude);
               
    }

    void FixedUpdate()
    {
        meuMovimentoJogador.Movimentar(direcao, statusJogador.Velocidade);

        meuMovimentoJogador.RotacaoJogador(MascaraChao);
    }

    public void TomarDano(int dano) {

        statusJogador.Vida -= dano;
        scriptControlaInterface.AtualizarSliderVidaJogador();
        ControlaAudio.instancia.PlayOneShot(SomDeDano);

        if (statusJogador.Vida <= 0) {
            Morrer();
            //Vivo = false;
        }
        
    }

    public void Morrer() {

        scriptControlaInterface.GameOver();
    }

    public void CurarVida(int quantidadeDeCura) {
        statusJogador.Vida += quantidadeDeCura;
        if (statusJogador.Vida > statusJogador.VidaInicial) {
            statusJogador.Vida = statusJogador.VidaInicial;
        }
        scriptControlaInterface.AtualizarSliderVidaJogador();
    }
}
