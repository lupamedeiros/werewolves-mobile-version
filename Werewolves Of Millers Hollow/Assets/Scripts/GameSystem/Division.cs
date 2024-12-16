using Game.Multiplayer;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Character
{
    public class Division : MonoBehaviour
    {
        [SerializeField, Range(0, 100)] float m_werewolfPercentage = 20f;
        [SerializeField] CharacterAbility m_currentAbility;
        [SerializeField] CharacterAbility m_humanAbility;
        [SerializeField] CharacterAbility m_werewolfAbility;
        public Dictionary<Photon.Realtime.Player, CharacterAbility> m_playerWithAbility { get; private set; }

        private void Start()
        {
            (m_playerWithAbility ??= new())?.Clear();
            PhotonNetwork.AutomaticallySyncScene = false;
            HandleClient();
        }
        
        void HandleClient()
        {
            if (PhotonNetwork.IsMasterClient)
            {
                DividePlayers();
            }

            StartCoroutine(WaitSetup());
        }

        int GetCountOfWerewolves()
        {
            int playerCount = PhotonNetwork.CurrentRoom.PlayerCount;
            float percentage = m_werewolfPercentage * 0.01f;
            float werewolvesPercentage = playerCount * percentage;
            int werewolvesCount = Mathf.RoundToInt(werewolvesPercentage);
            werewolvesCount = Mathf.Clamp(werewolvesCount, 1, playerCount);
            return werewolvesCount;
        }

        void DividePlayers()
        {
           
            Dictionary<int, Photon.Realtime.Player> playersToDivide = new(PhotonNetwork.CurrentRoom.Players);
            int werewolvesCount = GetCountOfWerewolves();

            foreach (KeyValuePair<int, Photon.Realtime.Player> player in PhotonNetwork.CurrentRoom.Players)
            {
                int randomPhotonPlayerKey = playersToDivide.ElementAt(Random.Range(0, playersToDivide.Count)).Key;
                Photon.Realtime.Player photonPlayer = playersToDivide[randomPhotonPlayerKey];
                CharacterAbility ability = GetNewPlayerAbility();

                SetProps(photonPlayer, ability);
                m_currentAbility = ability;
                m_playerWithAbility.Add(photonPlayer, ability);
                playersToDivide.Remove(randomPhotonPlayerKey);

                Debug.Log($"{player.Value.NickName} definido!\nHabilidade: {PropertiesHandler.GetPlayerPropertyValue<string>(player.Value, PropertiesHandler.PROP_PLAYER_ABILITY)}");
            }

            CharacterAbility GetNewPlayerAbility()
            {
                if (werewolvesCount > 0)
                {
                    werewolvesCount--;
                    return m_werewolfAbility;
                }

                return m_humanAbility;
            }
        }

        void SetProps(Photon.Realtime.Player player, CharacterAbility characterAbility)
        {
            Debug.Log($"Definindo Player: {player.NickName}\n" +
                $"Habilidade: {characterAbility.name}");
            ExitGames.Client.Photon.Hashtable hashtable = new()
            {
                { PropertiesHandler.PROP_PLAYER_STATE, CharacterState.Alive},
                { PropertiesHandler.PROP_PLAYER_ABILITY, characterAbility.name}
            };

            player.SetCustomProperties(hashtable);
        }

        IEnumerator WaitSetup()
        {
            while (!PropertiesHandler.PlayersPropsSet())
            {
                yield return null;
            }

            foreach (KeyValuePair<int, Photon.Realtime.Player> player in PhotonNetwork.CurrentRoom.Players)
            {

            }

            Debug.Log($"Minha habilidade é: {PropertiesHandler.GetPlayerPropertyValue<string>(PhotonNetwork.LocalPlayer, PropertiesHandler.PROP_PLAYER_ABILITY)}");
            Debug.Log("Todos os players foram setados!");
        }
    }
}