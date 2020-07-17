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
        
        if (objetoDeColisao.tag == "Inimigo")
        {
            //Reescrever!
            try{
                objetoDeColisao.GetComponent<ControlaInimigo>().TomarDano(50);
            }
            catch (Exception e) {
                objetoDeColisao.GetComponent<ControlaLich>().TomarDano(50);
            }
            
        }

        Destroy(gameObject);
    }
}
