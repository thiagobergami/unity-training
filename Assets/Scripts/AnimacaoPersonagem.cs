using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimacaoPersonagem : MonoBehaviour {

    private Animator meuAnimator;

    void Awake()
    {
        meuAnimator = GetComponent<Animator>();
    }

    public void Atacar(bool estado) {

        meuAnimator.SetBool("Atacando", estado);
    }

    public void AletorizarZumbi() {
        int geraTipoZumbi = Random.Range(1, transform.childCount);
        transform.GetChild(geraTipoZumbi).gameObject.SetActive(true);
    }

    public void Movimentar(float valorDeMovimento) {
        meuAnimator.SetFloat("Movendo", valorDeMovimento);
    }
    public void Morrer() {
        meuAnimator.SetTrigger("Morrer");
    }

}
