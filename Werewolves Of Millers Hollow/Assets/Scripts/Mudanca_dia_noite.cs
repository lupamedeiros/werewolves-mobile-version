using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Mudanca_dia_noite : MonoBehaviour
{
    public int tempoInicialSegundos = 10;
    public Image backgroundDia;
    public Image backgroundNoite;
    
    private bool tempoAcabou;

    void Start()
    {

        backgroundDia.gameObject.SetActive(true);
        backgroundNoite.gameObject.SetActive(false);

        StartCoroutine(ContagemRegressiva());
    }

    IEnumerator ContagemRegressiva()
    {
        while (true)
        {
            yield return new WaitForSeconds(tempoInicialSegundos);
            
            yield return StartCoroutine(AlterarImagemPorTempo(backgroundDia, backgroundNoite));

            yield return new WaitForSeconds(tempoInicialSegundos);
            
            yield return StartCoroutine(AlterarImagemPorTempo(backgroundNoite, backgroundDia));
        }
    }

    IEnumerator AlterarImagemPorTempo(Image imagemAtual, Image imagemProxima)
    {
        imagemAtual.gameObject.SetActive(false);
        imagemProxima.gameObject.SetActive(true);

        yield return new WaitForSeconds(tempoInicialSegundos);
    }
}
