﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[SerializeField]
public class EscolheArma {

    public int posicao;
    public float proximoTiro;
    public float tempRecoil;
    public int minDanoCausado;
    public int maxDanoCausado;

    public int PosicaoArma { get; set; }
    public float ProximoTiro { get; set; }
    public float TempoResfriamento { get; set; }
    public float danoArma { get; set; }
    //public int geraArma { get; set; }

    public EscolheArma(int posicao, float proximoShoot, float tempoResfr)
    {
        PosicaoArma = posicao;
        ProximoTiro = proximoShoot;
        TempoResfriamento = tempoResfr;
    }

    public enum Armas
    {
        Pistola = 0,
        Prego = 1,
        Desert = 2,
        MP = 3,
        Pistola_USP = 4,
        PistoAgua = 5,
        Revolver01 = 6,
        Revolver02 = 7
    }

}
