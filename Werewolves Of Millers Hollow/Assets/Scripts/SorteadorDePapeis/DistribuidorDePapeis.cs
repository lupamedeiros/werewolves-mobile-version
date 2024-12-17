using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SorteadorDePapeis
{
    public class DistribuidorDePapeis : MonoBehaviour
    {
        public enum Papel
        {
            Aldeao,
            Lobisomem,
            Vidente
        }

        public static List<Papel> DistribuirPapeis(int numJogadores, int numLobisomens, int numVidentes)
        {
            if (numJogadores < numLobisomens + numVidentes)
            {
                throw new ArgumentException("O número de jogadores deve ser maior ou igual à soma de lobisomens e videntes.");
            }

            List<Papel> papeis = new List<Papel>();

            // Adicionar o número especificado de lobisomens
            papeis.AddRange(Enumerable.Repeat(Papel.Lobisomem, numLobisomens));

            // Adicionar o número especificado de videntes
            papeis.AddRange(Enumerable.Repeat(Papel.Vidente, numVidentes));

            // Preencher os papéis restantes como aldeões
            papeis.AddRange(Enumerable.Repeat(Papel.Aldeao, numJogadores - papeis.Count));

            // Embaralhar os papéis aleatoriamente
            System.Random random = new System.Random();
            papeis = papeis.OrderBy(_ => random.Next()).ToList();

            return papeis;
        }

        public static void Main(string[] args)
        {
            // Definir número de jogadores, lobisomens e videntes
            int numJogadores = 6;
            int numLobisomens = 2;
            int numVidentes = 1;

            try
            {
                // Distribuir papéis
                List<Papel> papeis = DistribuirPapeis(numJogadores, numLobisomens, numVidentes);

                // Exibir os papéis atribuídos
                Console.WriteLine("Papéis Atribuídos:");
                for (int i = 0; i < papeis.Count; i++)
                {
                    Console.WriteLine($"Jogador {i + 1}: {papeis[i]}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
            }
        }
    }
}
