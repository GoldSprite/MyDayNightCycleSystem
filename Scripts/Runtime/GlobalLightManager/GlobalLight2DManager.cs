using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace GoldSprite.UnityPlugins.DayNightCycleSystem {
    /// <summary>
    /// 根据系统时间从给定的GameTimeLoop单位(默认3分钟=24游戏时)返回归一化光照强度
    /// </summary>
    public class GlobalLight2DManager : MonoBehaviour {
        [Header("引用")]
        public Light2D light2D;

        [Header("配置")]
        [Tooltip("gameMinPerDay表示每现实时间{0}分钟为游戏内24h.")]
        public float gameMinPerDay = 5;
        [Range(0, 1)]
        public float lightingRange_min = 0.2f;
        [Range(0, 1)]
        public float lightingRange_max = 1;
        public bool manualMode;
        [Range(0, 1)]
        public float manualLighting;

        [Header("实时")]
        public double SystemTimeSeconds;
        [Header("系统当前时间")]
        public string SystemTime;
        public double currentSystemMins;
        [Header("游戏归一化当前时间")]
        public double gameTimeNormalized;
        public float GameTimeSeconds;
        [Header("游戏时间")]
        public string GameTime;
        [Range(0, 1)]
        [Header("当前归一化日照强度")]
        public float currentLightingNormalized;
        [Range(0, 1)]
        [Header("当前日照强度")]
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
