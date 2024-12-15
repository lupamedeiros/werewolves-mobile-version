using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Character
{
    [CreateAssetMenu(fileName = "Werewolf Ability", menuName = "Scriptable Objects/Characters/Abilities/Werewolf Ability")]
    public class WerewolfAbility : CharacterAbility
    {
        public override void UseAbility(object data)
        {
            Debug.Log("Raw");
        }
    }
}