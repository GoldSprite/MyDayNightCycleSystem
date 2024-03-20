using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace GoldSprite.UnityPlugins.DayNightCycleSystem {
    public class EnvironmentColor2D : MonoBehaviour {
        [Header("引用")]
        public GlobalLight2DManager lightManager;

        [Header("配置")]
        public List<SpriteRenderer> cycleSpriteRenders;


        void Start()
        {
            lightManager = GetComponent<GlobalLight2DManager>();
        }

        void Update()
        {
            ////受光照红蓝光影响物体 (效果不是很好)
            //foreach (var render in cycleSpriteRenders) {
            //    var color = lightManager.clightColor;
            //    render.color = color;
            //}
        }
    }
}
