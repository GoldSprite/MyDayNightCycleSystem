using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GoldSprite.UnityPlugins.DayNightCycleSystem {
    public class EnvironmentColor2D : MonoBehaviour {
        [Header("“˝”√")]
        public GlobalLight2DManager lightManager;

        [Header("≈‰÷√")]
        public List<SpriteRenderer> cycleSpriteRenders;


        void Start()
        {
            lightManager = GetComponent<GlobalLight2DManager>();
        }

        void Update()
        {
            foreach(var render in cycleSpriteRenders) {
                var color = render.color;
                var lighting = lightManager.CurrentLighting;
                color.r = color.g = color.b = lighting;
                render.color = color;
            }
        }
    }
}
