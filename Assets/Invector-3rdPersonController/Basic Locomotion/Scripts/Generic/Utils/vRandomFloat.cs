﻿using UnityEngine;
using UnityEngine.UI;

namespace Invector.Utils
{
    public class vRandomFloat : MonoBehaviour
    {
        public bool randomValue = true;
        [vHideInInspector("randomValue")]
        public float min;
        public float max;
        public bool setOnStart;

        public Slider.SliderEvent onSet;
        private void Start()
        {
            if (setOnStart) Set();
        }
        // Start is called before the first frame update
        public void Set()
        {
            if (randomValue) onSet.Invoke(Random.Range(min, max));
            else onSet.Invoke(max);
        }
    }
}