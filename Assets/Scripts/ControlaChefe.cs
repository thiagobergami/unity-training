﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class ControlaChefe : MonoBehaviour, IMatavel
{
    private Transform jogador;
    private NavMeshAgent agente;
    private Status statusChefe;
    private AnimacaoPersonagem animacaoChefe;
    private MovimentoPersonagem movimentoChefe;
    public GameObject kitMedico;
    private ControlaInterface scriptControlaInterface;
    public Slider sliderVidaChefe;
    public Image ImageSlider; //Pegando o slider
    public Color CorDaVIdaMaxima, CorDaVidaMinima;

    private void Start()
    {
        jogador = GameObject.FindWithTag("Jogador").transform;
        agente = GetComponent<NavMeshAgent>();
        statusChefe = GetComponent<Status>();
        agente.speed = statusChefe.Velocidade;
        animacaoChefe = GetComponent<AnimacaoPersonagem>();
        movimentoChefe = GetComponent<MovimentoPersonagem>();

        //Trabalhando com a slider de Vida
        sliderVidaChefe.maxValue = statusChefe.VidaInicial;
        AtualizarInterface();

        // Preenchendo Objeto com o FindObjectTypew
        scriptControlaInterface = GameObject.FindObjectOfType(typeof(ControlaInterface)) as ControlaInterface;
    }
    private void Update()
    {
        agente.SetDestination(jogador.position);
        animacaoChefe.Movimentar(agente.velocity.magnitude);

        if (agente.hasPath == true)
        {
            bool estouPerto = agente.remainingDistance <= agente.stoppingDistance;

            if (estouPerto)
            {
                animacaoChefe.Atacar(true);
                Vector3 direcao = jogador.position - transform.position;
                movimentoChefe.Rotacionar(direcao);
            }
            else
            {
                animacaoChefe.Atacar(false);
            }
        }
    }
    private void AtacaJogador() {
        int dano = Random.Range(30, 40);
        jogador.GetComponent<ControlaJogador>().TomarDano(dano);
    }

    public void TomarDano(int dano)
    {
        statusChefe.Vida -= dano;
        AtualizarInterface();
        if (statusChefe.Vida <= 0) {
            Morrer();
        }
        //throw new System.NotImplementedException();
    }

    public void Morrer()
    {
        animacaoChefe.Morrer();
        movimentoChefe.Morrer();
        this.enabled = false;
        agente.enabled = false;
        Instantiate(kitMedico, transform.position, Quaternion.identity);
        scriptControlaInterface.AtualizarQuantidadeDeZumbisMortos();
        Destroy(gameObject, 2);
        //throw new System.NotImplementedException();
    }

    void AtualizarInterface(){
        sliderVidaChefe.value = statusChefe.Vida;

        //calculando porcentagem da vida
        float porcentagemDaVida = (float)statusChefe.Vida / statusChefe.VidaInicial;
        //Interpolação Linear
        Color corDaVida = Color.Lerp(CorDaVidaMinima, CorDaVIdaMaxima, porcentagemDaVida);
        ImageSlider.color = corDaVida;
    }
}
