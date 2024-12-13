using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Character
{
    public abstract class CharacterAbility : ScriptableObject
    {
        public abstract void UseAbility(object data);
    }
}