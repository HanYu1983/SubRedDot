using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.App.Han
{
    public class BaseButton : Base
    {
        void Start()
        {
            AddEventListener();
        }

        private void AddEventListener()
        {
            var button = GetComponent<Button>();
            if(button != null)
            {
                button.onClick.AddListener(() =>
                {
                    StartCoroutine(PerformAction());
                });
            }
            var slider = GetComponent<Slider>();
            if(slider != null)
            {
                slider.onValueChanged.AddListener(value =>
                {
                    StartCoroutine(PerformAction());
                });
            }
        }
    }
}