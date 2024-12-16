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

        public class Jogador
        {
            public string Nome { get; set; }
            public Papel PapelAtribuido { get; set; }

            public override string ToString()
            {
                return $"{Nome} - {PapelAtribuido}";
            }
        }

        public static List<Jogador> DistribuirPapeis(List<string> nomesJogadores, int numLobisomens, int numVidentes)
        {
            if (nomesJogadores == null || nomesJogadores.Count < numLobisomens + numVidentes)
            {
                throw new ArgumentException("O número de jogadores deve ser maior ou igual à soma de lobisomens e videntes.");
            }

            List<Papel> papeis = new List<Papel>();

            // Adicionar o número especificado de lobisomens
            papeis.AddRange(Enumerable.Repeat(Papel.Lobisomem, numLobisomens));

            // Adicionar o número especificado de videntes
            papeis.AddRange(Enumerable.Repeat(Papel.Vidente, numVidentes));

            // Preencher os papéis restantes como aldeões
            papeis.AddRange(Enumerable.Repeat(Papel.Aldeao, nomesJogadores.Count - papeis.Count));

            // Embaralhar os papéis aleatoriamente
            System.Random random = new System.Random(); // Uso explícito de System.Random
            papeis = papeis.OrderBy(_ => random.Next()).ToList();

            // Atribuir os papéis aos jogadores
            List<Jogador> jogadores = new List<Jogador>();
            for (int i = 0; i < nomesJogadores.Count; i++)
            {
                jogadores.Add(new Jogador { Nome = nomesJogadores[i], PapelAtribuido = papeis[i] });
            }

            return jogadores;
        }

        public static void Main(string[] args)
        {
            // Lista de jogadores de exemplo
            List<string> nomesJogadores = new List<string> { "Alice", "Bob", "Charlie", "Diana", "Eve", "Frank" };

            // Definir número de lobisomens e videntes
            int numLobisomens = 2;
            int numVidentes = 1;

            // Distribuir papéis
            try
            {
                List<Jogador> jogadores = DistribuirPapeis(nomesJogadores, numLobisomens, numVidentes);

                // Exibir os papéis atribuídos
                Console.WriteLine("Papéis Atribuídos:");
                foreach (var jogador in jogadores)
                {
                    Console.WriteLine(jogador);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
            }
        }
    }
}


