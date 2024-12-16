using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Character
{
    [CreateAssetMenu(fileName = "Human Ability", menuName = "Scriptable Objects/Characters/Abilities/Human Ability")]
    public class HumanAbility : CharacterAbility
    {
        public override void UseAbility(object data)
        {
            Debug.Log("faço nada não maluco");
        }
    }
}