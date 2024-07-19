using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.App.Han
{
    public class OnCustomAction : AbstractBaseButtonAction
    {
        public bool onStart;
        public bool onAwake;
        public bool onUpdate;
        public bool onValueChanged;
        
        private void Awake()
        {
            AddValueChangeListenerIfNeeded();
            if (onAwake)
            {
                StartCoroutine(Perform());
            }
        }

        // Use this for initialization
        void Start()
        {
            if (onStart)
            {
                StartCoroutine(Perform());
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (onUpdate)
            {
                StartCoroutine(Perform());
            }
        }

        private void AddValueChangeListenerIfNeeded()
        {
            if (onValueChanged)
            {
                var slider = GetComponent<Slider>();
                if (slider != null)
                {
                    slider.onValueChanged.AddListener(value =>
                    {
                        StartCoroutine(PerformFloat(value));
                    });
                }
                var dropdown = GetComponent<Dropdown>();
                if (dropdown != null)
                {
                    dropdown.onValueChanged.AddListener(value =>
                    {
                        StartCoroutine(PerformInt(value));
                    });
                }
            }
        }

        public bool isCustomCall()
        {
            return onStart || onAwake || onUpdate || onValueChanged;
        }
    }
}