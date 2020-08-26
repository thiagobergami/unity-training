using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeradorChefe : MonoBehaviour {

    private float tempoParaProximaGeracao = 0;
    public float tempoEntreGeracoes = 60;
    public GameObject ChegePrefeb;
    public ControlaInterface scripControlaInterface;
    public Transform[] PosicoesGeracao;
    private Transform jogador;

    private void Start()
    {
        tempoParaProximaGeracao = tempoEntreGeracoes;

        //Script para manipular texto do chefe
        scripControlaInterface = GameObject.FindObjectOfType(typeof(ControlaInterface)) as ControlaInterface;
        jogador = GameObject.FindWithTag("Jogador").transform;
    }

    private void Update()
    {
        if (Time.timeSinceLevelLoad > tempoParaProximaGeracao) {
            Vector3 posicaoDeCriacao = CalculaPosicaoMaisDistanteDoJogador();
            Instantiate(ChegePrefeb, posicaoDeCriacao, Quaternion.identity);
            scripControlaInterface.AparecerTextoChefeCriadao();
            tempoParaProximaGeracao = Time.timeSinceLevelLoad + tempoEntreGeracoes;
        }
    }

    Vector3 CalculaPosicaoMaisDistanteDoJogador()
    {
        Vector3 posicaoDeMaisDistancia = Vector3.zero;
        float maiorDistancia = 0;
        foreach (Transform posicao in PosicoesGeracao) {
            float distanciaEntreOJogador = Vector3.Distance(posicao.position, jogador.position);
            if (distanciaEntreOJogador > maiorDistancia) {

                maiorDistancia = distanciaEntreOJogador;
                posicaoDeMaisDistancia = posicao.position;
            }
        }

        return posicaoDeMaisDistancia;
    }
}
