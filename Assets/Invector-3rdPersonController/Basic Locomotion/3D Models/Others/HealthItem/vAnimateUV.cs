﻿using UnityEngine;

namespace Invector
{
    public class vAnimateUV : MonoBehaviour
    {
        public Vector2 speed;
        public Renderer _renderer;
        public string[] textureParameters = { "_MainTex" };
        private Vector2 offSet;

        void Update()
        {
            offSet.x += speed.x * Time.deltaTime;
            offSet.y += speed.y * Time.deltaTime;
            for (int i = 0; i < textureParameters.Length; i++)
                _renderer.material.SetTextureOffset(textureParameters[i], offSet);
        }
    }
}
