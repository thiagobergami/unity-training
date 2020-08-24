using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeradorChefe : MonoBehaviour {

    private float tempoParaProximaGeracao = 0;
    public float tempoEntreGeracoes = 60;
    public GameObject ChegePrefeb;

    private void Start()
    {
        tempoParaProximaGeracao = tempoEntreGeracoes;
    }

    private void Update()
    {
        if (Time.timeSinceLevelLoad > tempoParaProximaGeracao) {
            Instantiate(ChegePrefeb, transform.position, Quaternion.identity);
            tempoParaProximaGeracao = Time.timeSinceLevelLoad + tempoEntreGeracoes;
        }
    }
}
