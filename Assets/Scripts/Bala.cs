using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour {

    public float Velocidade = 20;
    private Rigidbody rigibodyBala;
    public AudioClip AudioMorte;

    void Start()
    {
        rigibodyBala = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate () {
        rigibodyBala.MovePosition
            (rigibodyBala.position + 
            transform.forward * Velocidade * Time.deltaTime);
	}

    void OnTriggerEnter(Collider objetoDeColisao){
        
        switch(objetoDeColisao.tag)
        {
            case "Inimigo":
                objetoDeColisao.GetComponent<ControlaInimigo>().TomarDano(1);
            break;
            case "Lich":
                objetoDeColisao.GetComponent<ControlaLich>().TomarDano(1);
            break;
            case "ChefeDeFase":
                objetoDeColisao.GetComponent<ControlaChefe>().TomarDano(1);
            break;
        }
        Destroy(gameObject);
    }
}
