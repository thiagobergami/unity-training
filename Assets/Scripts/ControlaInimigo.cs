﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaInimigo : MonoBehaviour, IMatavel
{

    public GameObject Jogador;
    private Status statusInimigo;
    public float hit = 0.3f;
    private float hitDist;
    public AudioClip AudioMorte;
    private Vector3 posicaoAleatoria;
    private Vector3 direcao;
    private float contadorVagar;
    private float tempoEntrePosicoesAleatorias = 4;
    private float porcentagemGerarKitMedico = 0.1f;
    public GameObject KitMedico;
    private ControlaInterface scriptControlaInterface;
    [HideInInspector]
    public GeradorZumbis meuGerador;
    public bool resolveMinhaVida = false;

    private MovimentoPersonagem movimentaInimigo;
    private AnimacaoPersonagem animacaoInimigo;


    // Use this for initialization
    void Start()
    {
        //Informando que o Tipo de objeto está sendo trocada pela TAG do Personagem
        Jogador = GameObject.FindWithTag("Jogador");

        movimentaInimigo = GetComponent<MovimentoPersonagem>();
        statusInimigo = GetComponent<Status>();

        //Automatizar a distância entre zumbi e personagem
        float zumbi = GetComponent<CapsuleCollider>().radius;
        float player = Jogador.GetComponent<CapsuleCollider>().radius;

        animacaoInimigo = GetComponent<AnimacaoPersonagem>();
        if (resolveMinhaVida == false) {
            animacaoInimigo.AletorizarZumbi();
        }
       

        hitDist = zumbi + player + hit;

        //Preenchendo Objeto com o FindObjectTypew
        scriptControlaInterface = GameObject.FindObjectOfType(typeof(ControlaInterface)) as ControlaInterface;
    }
    
    void FixedUpdate()
    {
        float distancia = Vector3.Distance(transform.position, Jogador.transform.position);
        

        movimentaInimigo.Rotacionar(direcao);
        animacaoInimigo.Movimentar(direcao.magnitude);

        //Script de atacar

        if (distancia > 15) {
            //Criando movimentação Padrão pro zumbi;
            Vagar();

        }else if (distancia > hitDist){
            statusInimigo.Velocidade = 6;
            direcao = Jogador.transform.position - transform.position;
            movimentaInimigo.Movimentar(direcao, statusInimigo.Velocidade);
            animacaoInimigo.Atacar(false);
        }else {
            direcao = Jogador.transform.position - transform.position;
            animacaoInimigo.Atacar(true);
        }
    }

    void AtacaJogador()
    {
        int dano = Random.Range(20, 31);
        Jogador.GetComponent<ControlaJogador>().TomarDano(dano);       
    }

    public void TomarDano(int dano)
    {
        statusInimigo.Vida -= dano;
        if (statusInimigo.Vida <= 0) {
            Morrer();
        }
    }

    public void Morrer()
    {
        ControlaAudio.instancia.PlayOneShot(AudioMorte);
        Destroy(gameObject,2);
        animacaoInimigo.Morrer();
        this.enabled = false;
        movimentaInimigo.Morrer();

        VerificarGeracaoKitMedico(porcentagemGerarKitMedico);
        scriptControlaInterface.AtualizarQuantidadeDeZumbisMortos();
        meuGerador.DiminuirQuantidadeDeZumbisVivos();
    }

    void VerificarGeracaoKitMedico(float porcentagemGeracao) {
        if (Random.value <= porcentagemGeracao) {
            Instantiate(KitMedico, transform.position, Quaternion.identity);
        }
    }

    void Vagar()
    {
        contadorVagar -= Time.deltaTime;

        if (contadorVagar <= 0)
        {
            posicaoAleatoria = AleatorizarPosicao();
            contadorVagar += tempoEntrePosicoesAleatorias + Random.Range(-1f, 1f);
        }
        bool ficouPertoOSuficiente = Vector3.Distance(transform.position, posicaoAleatoria) <= 0.05;

        if (ficouPertoOSuficiente == false)
        {
            direcao = posicaoAleatoria - transform.position;
            movimentaInimigo.Movimentar(direcao, statusInimigo.Velocidade);
        }

    }

    Vector3 AleatorizarPosicao() {
        Vector3 posicao = Random.insideUnitSphere * 28;
        posicao += transform.position;
        posicao.y = transform.position.y;

        return posicao;
    }
}
