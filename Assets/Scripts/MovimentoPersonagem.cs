using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentoPersonagem : MonoBehaviour {

    private Rigidbody meuRigibody;

    void Awake()
    {
        meuRigibody = GetComponent<Rigidbody>();
    }

    public void Movimentar(Vector3 direcao, float velocidade)
    {
        meuRigibody.MovePosition(meuRigibody.position + direcao.normalized * velocidade * Time.deltaTime);
    }
    public void Rotacionar(Vector3 direcao) {
        Quaternion novaRotacao = Quaternion.LookRotation(direcao);
        meuRigibody.MoveRotation(novaRotacao);
    }
    //Método que faz o zumbi desaparecer
    public void Morrer() {
        meuRigibody.constraints = RigidbodyConstraints.None;
        meuRigibody.velocity = Vector3.zero;
        GetComponent<Collider>().enabled = false;
    }
}
