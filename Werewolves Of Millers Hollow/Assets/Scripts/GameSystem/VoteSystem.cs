using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VoteSystem : MonoBehaviour
{
    [SerializeField] private GameObject button1;
    [SerializeField] private GameObject button2;
    [SerializeField] private TextMeshProUGUI resultText; 
    
    private Dictionary<string, int> votos = new Dictionary<string, int>(); 
    private string[] jogadores = {"Jogador1"}; 

    void OnEnable()
    {
        foreach (var jogador in jogadores)
        {
            votos[jogador] = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActiveButtons()
    {
        button1.SetActive(true);
        button2.SetActive(true);
    }

    public void DisableButtons()
    {
        button1.SetActive(false);
        button2.SetActive(false);
    }

    public void Vote(string jogador)
    {
        if (votos.ContainsKey(jogador))
        {
            votos[jogador]++;
        }
        
        UpdateResults();
    }
    private void UpdateResults()
    {
        string resultados = "Resultados da votação:\n";
        foreach (var kvp in votos)
        {
            resultados += $"{kvp.Key}: {kvp.Value} votos\n";
        }

        resultText.text = resultados;
    }

    public void FinalizeVote()
    {
        
        string maisVotado = null;
        int maiorVoto = 0;
        foreach (var kvp in votos)
        {
            if (kvp.Value > maiorVoto)
            {
                maisVotado = kvp.Key;
                maiorVoto = kvp.Value;
            }
        }
        
        if (maisVotado != null)
        {
            resultText.text = $"Jogador eliminado: {maisVotado} com {maiorVoto} votos!";
        }
        else
        {
            resultText.text = "Nenhum jogador foi votado.";
        }
        
        ResetVotes();
    }

    private void ResetVotes()
    {
        foreach (var jogador in jogadores)
        {
            votos[jogador] = 0;
        }
    }
}
