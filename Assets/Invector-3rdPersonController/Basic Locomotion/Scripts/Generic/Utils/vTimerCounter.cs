﻿using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Invector.Utils
{
    public class vTimerCounter : MonoBehaviour
    {
        public float targetTime;
        public bool normalizeResult;
        [SerializeField, vReadOnly]
        protected float timerResult;
        public bool startTimerOnStart;

        public UnityEvent onStart, onPause, onStop, onFinish;
        public Slider.SliderEvent onTimerUpdated;

        protected float currentTime;
        protected Coroutine timerRoutine;

        public void Start()
        {
            if (startTimerOnStart) StartTimer();
        }

        public virtual void StartTimer()
        {
            if (timerRoutine != null) StopCoroutine(timerRoutine);
            currentTime = 0;
            timerResult = 0;
            onTimerUpdated.Invoke(0);
            timerRoutine = StartCoroutine(TimerRoutiner());
        }

        public void StopTimer()
        {
            PauseTimer();
            currentTime = 0;
            onStop.Invoke();
            timerResult = 0;
            onTimerUpdated.Invoke(0);
        }

        public void PauseTimer()
        {
            if (timerRoutine != null) StopCoroutine(timerRoutine);
            timerRoutine = null;
            onPause.Invoke();
        }

        IEnumerator TimerRoutiner()
        {
            onStart.Invoke();

            while (currentTime < targetTime)
            {
                currentTime += Time.deltaTime;
                timerResult = normalizeResult ? currentTime / targetTime : currentTime;
                onTimerUpdated.Invoke(timerResult);
                yield return null;
            }
            timerRoutine = null;
            timerResult = normalizeResult ? 1 : targetTime;
            onTimerUpdated.Invoke(timerResult);
            onFinish.Invoke();

        }
    }
}