using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeradorZumbis : MonoBehaviour
{

    public GameObject Zumbi;
    private float contadorTempo = 0;
    public float TempoGerarZumbi = 5;
    public LayerMask LayerZumbi;
    private readonly float distanciaDeGeracao = 10;
    private float DistanciaDoJogadorParaGeracao = 30;
    private GameObject jogador;
    private int quantidadeMaximaDeZumbiVivos = 2;
    private int quantideZumbisVivosAtual;
    private float tempoProximoAumentoDeDificuldade = 15;
    private float contadorAumentarDificuldade;

    private void Start()
    {
        contadorAumentarDificuldade = tempoProximoAumentoDeDificuldade;
        jogador = GameObject.FindWithTag("Jogador");
        for (int i = 0; i < quantidadeMaximaDeZumbiVivos; i++) {
            StartCoroutine(GerarNovoZumbi());
        }
    }

    // Update is called once per frame
    void Update()
    {
        bool possoGerarZumbiPelaDistancia = Vector3.Distance(transform.position, jogador.transform.position) > DistanciaDoJogadorParaGeracao;

        if (possoGerarZumbiPelaDistancia == true && quantideZumbisVivosAtual <= quantidadeMaximaDeZumbiVivos) {
            contadorTempo += Time.deltaTime;

            if (contadorTempo >= TempoGerarZumbi)
            {
                StartCoroutine(GerarNovoZumbi());
                contadorTempo = 0;
            }
        }

        //Criando o aumentador de dificuldade

        if (Time.timeSinceLevelLoad > contadorAumentarDificuldade) {
            quantidadeMaximaDeZumbiVivos++;
            contadorAumentarDificuldade = Time.timeSinceLevelLoad + tempoProximoAumentoDeDificuldade;
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, distanciaDeGeracao);
    }

    IEnumerator GerarNovoZumbi()
    {
        Vector3 posicaoDeCriacao = AleatorizarPosicao();
        Collider[] colisores = Physics.OverlapSphere(posicaoDeCriacao, 1, LayerZumbi);

        while (colisores.Length > 0) {

            posicaoDeCriacao = AleatorizarPosicao();
            colisores = Physics.OverlapSphere(posicaoDeCriacao, 1, LayerZumbi);
            yield return null;
        }
        ControlaInimigo zumbi = Instantiate(Zumbi, posicaoDeCriacao, transform.rotation).GetComponent<ControlaInimigo>();
        zumbi.meuGerador = this;
        quantideZumbisVivosAtual++;
    }

    Vector3 AleatorizarPosicao() {
        Vector3 posicao = Random.insideUnitSphere * distanciaDeGeracao;
        posicao += transform.position;
        posicao.y = 0;

        return posicao;
    }

    public void DiminuirQuantidadeDeZumbisVivos()
    {
        quantideZumbisVivosAtual--;

    }
}
