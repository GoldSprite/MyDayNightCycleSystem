using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace GoldSprite.UnityPlugins.DayNightCycleSystem {
    public class EnvironmentColor2D : MonoBehaviour {
        [Header("����")]
        public GlobalLight2DManager lightManager;

        [Header("����")]
        public List<SpriteRenderer> cycleSpriteRenders;


        void Start()
        {
            lightManager = GetComponent<GlobalLight2DManager>();
        }

        void Update()
        {
            ////�ܹ��պ�����Ӱ������ (Ч�����Ǻܺ�)
            //foreach (var render in cycleSpriteRenders) {
            //    var color = lightManager.clightColor;
            //    render.color = color;
            //}
        }
    }
}
