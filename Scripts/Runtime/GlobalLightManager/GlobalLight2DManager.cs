using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace GoldSprite.UnityPlugins.DayNightCycleSystem {
    /// <summary>
    /// 根据系统时间从给定的GameTimeLoop单位(默认3分钟=24游戏时)返回归一化光照强度
    /// </summary>
    public class GlobalLight2DManager : MonoBehaviour {
        [Header("引用")]
        public Light2D globalLight;

        [Header("配置")]
        [Tooltip("gameMinPerDay表示每现实时间{0}分钟为游戏内24h.")]
        public float gameMinPerDay = 5;
        [Range(0, 1)]
        public float lightingRange_min = 0.2f;
        [Range(0, 1)]
        public float lightingRange_max = 1;
        public bool manualMode;
        [Range(0, 24)]
        public float manualGameTimeHours;
        public Color dayColor = Color.blue;
        public Color nightColor = Color.red;

        [Header("实时")]
        public double SystemTimeSeconds;
        [Header("系统当前时间")]
        public string SystemTime;
        public double currentSystemMins;
        [Header("游戏归一化当前时间")]
        public double gameTimeNormalized;
        public float gameTimeHours;
        [Header("游戏时间")]
        public string gameTime;
        [Range(0, 1)]
        [Header("当前归一化日照强度")]
        public float currentLightingNormalized;
        [Range(0, 1)]
        [Header("当前日照强度")]
        public float currentLighting;
        [Header("当前光照颜色偏移")]
        public Color clightColor;
        public Color cDayColor;
        public Color cNightColor;
        public List<Light2D> environmentLights => FindObjectsOfType<Light2D>().Where(p=>p!= globalLight).ToList();

        void Start()
        {
            globalLight = GetComponent<Light2D>();
        }

        void Update()
        {
            //计算当前系统分钟数
            SystemTimeSeconds = DateTime.Now.TimeOfDay.TotalSeconds;
            SystemTime = new DateTime((long)(SystemTimeSeconds * TimeSpan.TicksPerSecond)).ToString("HH:mm:ss");
            currentSystemMins = SystemTimeSeconds / 60f;

            //计算当前游戏时
            gameTimeNormalized = currentSystemMins % gameMinPerDay / gameMinPerDay;
            gameTimeHours = manualMode ? manualGameTimeHours : (float)(gameTimeNormalized * 24);
            gameTime = new DateTime((long)(gameTimeHours * TimeSpan.TicksPerHour)).ToString("HH:mm:ss");

            //计算光照/红蓝光强度
            currentLightingNormalized = GetDayTimeLighting(gameTimeHours);
            currentLighting = LimitLightingRange(currentLightingNormalized);
            clightColor = GetLightingColor(currentLightingNormalized);


            globalLight.intensity = currentLighting;
            globalLight.color = clightColor;

            //其他光源
            foreach(var light in environmentLights) {
                light.intensity = 1 - currentLighting;
            }
        }

        //0/24点红光弱蓝光强, 12点蓝光弱红光强
        private Color GetLightingColor(float lighting)
        {
            var sunIntensity = lighting;
            var moonIntensity = 1 - lighting;

            cDayColor = dayColor * sunIntensity; cDayColor.a = 1;
            cNightColor = nightColor * moonIntensity; cNightColor.a = 1;

            clightColor = Color.Lerp(cNightColor, cDayColor, lighting);
            return clightColor;
        }

        public float GetDayTimeLighting(float hours)
        {
            var normalizeHours = hours / 24f;
            var rad = normalizeHours * Math.PI * 2;
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
