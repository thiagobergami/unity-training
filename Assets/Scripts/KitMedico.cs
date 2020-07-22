﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitMedico : MonoBehaviour {
    public int quantidadeDeCura;

    private void OnTriggerEnter(Collider objetoDeColisao)
    {
        quantidadeDeCura = Random.Range(15, 30);
        if (objetoDeColisao.tag == "Jogador") {
            objetoDeColisao.GetComponent<ControlaJogador>().CurarVida(quantidadeDeCura);
            Destroy(gameObject);
        }
    }
}