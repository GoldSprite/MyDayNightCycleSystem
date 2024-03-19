using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace GoldSprite.UnityPlugins.DayNightCycleSystem {
    /// <summary>
    /// ����ϵͳʱ��Ӹ�����GameTimeLoop��λ(Ĭ��3����=24��Ϸʱ)���ع�һ������ǿ��
    /// </summary>
    public class GlobalLight2DManager : MonoBehaviour {
        [Header("����")]
        public Light2D light2D;

        [Header("����")]
        [Tooltip("gameMinPerDay��ʾÿ��ʵʱ��{0}����Ϊ��Ϸ��24h.")]
        public float gameMinPerDay = 5;
        [Range(0, 1)]
        public float lightingRange_min = 0.2f;
        [Range(0, 1)]
        public float lightingRange_max = 1;
        public bool manualMode;
        [Range(0, 1)]
        public float manualLighting;

        [Header("ʵʱ")]
        public double SystemTimeSeconds;
        [Header("ϵͳ��ǰʱ��")]
        public string SystemTime;
        public double currentSystemMins;
        [Header("��Ϸ��һ����ǰʱ��")]
        public double gameTimeNormalized;
        public float GameTimeSeconds;
        [Header("��Ϸʱ��")]
        public string GameTime;
        [Range(0, 1)]
        [Header("��ǰ��һ������ǿ��")]
        public float currentLightingNormalized;
        [Range(0, 1)]
        [Header("��ǰ����ǿ��")]
        public float currentLighting;
        public float CurrentLighting => manualMode ? manualLighting : currentLighting;


        void Start()
        {
            light2D = GetComponent<Light2D>();
        }

        void Update()
        {
            SystemTimeSeconds = DateTime.Now.TimeOfDay.TotalSeconds;
            SystemTime = new DateTime((long)(SystemTimeSeconds * TimeSpan.TicksPerSecond)).ToString("HH:mm:ss");
            currentSystemMins = SystemTimeSeconds / 60f;

            gameTimeNormalized = currentSystemMins % gameMinPerDay / gameMinPerDay;
            GameTimeSeconds = (float)(gameTimeNormalized * 60 * 60 * 24);
            GameTime = new DateTime((long)(GameTimeSeconds * TimeSpan.TicksPerSecond)).ToString("HH:mm:ss");

            currentLightingNormalized = GetDayTimeLighting(gameTimeNormalized);
            currentLighting = LimitLightingRange(currentLightingNormalized);
            light2D.intensity = currentLighting;

            if (manualMode)
                light2D.intensity = manualLighting;
        }


        public float GetDayTimeLighting(double pams)
        {
            var rad = pams * Math.PI * 2;
            var cos = Math.Cos(rad);
            var result = (float)(-cos / 2f + 0.5f);
            return result;
        }
        public float LimitLightingRange(double lighting)
        {
            var range = lightingRange_max - lightingRange_min;
            var result = (float)(lightingRange_min + lighting * range);
            return result;
        }
    }
}
