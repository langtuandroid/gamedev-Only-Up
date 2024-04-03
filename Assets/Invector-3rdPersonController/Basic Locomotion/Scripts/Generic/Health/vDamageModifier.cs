using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Invector
{
    /// <summary>
    /// Damage Modifier. You can use this with <see cref="vIDamageReceiver.onStartReceiveDamage"/> to modificate the damage result
    /// </summary>   
    [Serializable]
    public class vDamageModifier
    {
        public enum FilterMethod
        {
            ApplyToAll,
            ApplyToAllInList,
            ApplyToAllOutList
        }

        [Serializable]
        public class DamageModifierEvent : UnityEvent<vDamageModifier> { }
        public string name = "MyModifier";
        public FilterMethod filterMethod;
        [Tooltip("List of Damage type that this can modify, keep empty if the filter will be applied to all types of damage")] public List<string> damageTypes = new List<string>();
        [Tooltip("Modifier value")] public int value;
        [Tooltip("true: Reduce a percentage of damage value\nfalse: Reduce da damage value directly")] public bool percentage;
        [Tooltip("The Filter will receive all damage and decrease your self resistance")] public bool destructible = true;
        public float resistance = 100;
        public float maxResistance = 100;
        public Slider.SliderEvent onChangeResistance;
        public DamageModifierEvent onBroken;

        public bool isBroken => destructible && resistance <= 0;
        /// <summary>
        /// Apply modifier to damage 
        /// </summary>
        /// <param name="damage">Damage to modify</param>
        public virtual void ApplyModifier(vDamage damage)
        {
            ///Apply modifier conditions            
            if (damage.damageValue > 0 && (CanFilterDamage(damage.damageType)) && (!isBroken))
            {
                float modifier = 0;

                if (percentage)
                {
                    float reduction = (damage.damageValue / 100f) * value;

                    modifier = reduction;  ///Calculate Percentage of the damage
                }
                else
                {
                    modifier = value;/// default value
                }
                ///apply damage to resistance
                if (destructible)
                {
                    resistance -= damage.damageValue;
                    onChangeResistance.Invoke(Mathf.Max(resistance, 0));
                    if (resistance <= 0) onBroken.Invoke(this);
                }
                ///apply modifier to damage value                
                if ((!destructible || resistance > 0)) damage.damageValue -= modifier;


            }
        }

        protected virtual bool CanFilterDamage(string damageType)
        {
            switch (filterMethod)
            {
                case FilterMethod.ApplyToAll:
                    return true;

                case FilterMethod.ApplyToAllInList:
                    return damageTypes.Contains(damageType);

                case FilterMethod.ApplyToAllOutList:
                    return !damageTypes.Contains(damageType);

            }
            return true;
        }
        public virtual void ResetModifier()
        {
            if (destructible)
            {
                resistance = maxResistance;
                onChangeResistance.Invoke(Mathf.Max(resistance, 0));
            }
        }
    }
}