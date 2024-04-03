﻿using System;
using UnityEngine;

namespace Invector
{
    [Serializable]
    public class vDamage
    {
        [Tooltip("Apply damage to the Character Health")]
        public float damageValue = 15;
        [Tooltip("How much stamina the target will lost when blocking this attack")]
        public float staminaBlockCost = 5;
        [Tooltip("How much time the stamina of the target will wait to recovery")]
        public float staminaRecoveryDelay = 1;
        [Tooltip("Apply damage even if the Character is blocking")]
        public bool ignoreDefense;
        [Tooltip("Activated Ragdoll when hit the Character")]
        public bool activeRagdoll;
        [vHideInInspector("activeRagdoll"),Tooltip("Time to keep Ragdoll active")]
        public float senselessTime;
        [HideInInspector]
        public Transform sender;
        [HideInInspector]
        public Transform receiver;
        [HideInInspector]
        public Vector3 hitPosition;
        public bool hitReaction = true;
        [HideInInspector]
        public int recoil_id;
        [HideInInspector]
        public int reaction_id;
        public string damageType;
        [HideInInspector] public Vector3 force;

        public vDamage()
        {
            damageValue = 15;
            staminaBlockCost = 5;
            staminaRecoveryDelay = 1;
            hitReaction = true;
        }

        public vDamage(int value)
        {
            damageValue = value;
            hitReaction = true;
        }

        public vDamage(int value, bool ignoreReaction)
        {
            damageValue = value;
            hitReaction = !ignoreReaction;
            if (ignoreReaction)
            {
                recoil_id = -1;
                reaction_id = -1;
            }                
        }

        public vDamage(vDamage damage)
        {
            damageValue = damage.damageValue;
            staminaBlockCost = damage.staminaBlockCost;
            staminaRecoveryDelay = damage.staminaRecoveryDelay;
            ignoreDefense = damage.ignoreDefense;
            activeRagdoll = damage.activeRagdoll;
            sender = damage.sender;
            receiver = damage.receiver;
            recoil_id = damage.recoil_id;
            reaction_id = damage.reaction_id;
            damageType = damage.damageType;
            hitPosition = damage.hitPosition;
            senselessTime = damage.senselessTime;
            force = damage.force;
        }

        /// <summary>
        /// Calc damage Resuction percentage
        /// </summary>
        /// <param name="damageReduction"></param>
        public void ReduceDamage(float damageReduction)
        {
            int result = (int)(damageValue - ((damageValue * damageReduction) / 100));
            damageValue = result;
        }
    }
}