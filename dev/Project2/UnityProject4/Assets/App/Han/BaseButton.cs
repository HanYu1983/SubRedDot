using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.App.Han
{
    public class BaseButton : MonoBehaviour//, IPointerDownHandler, IPointerUpHandler
    {
        /*public void OnPointerDown(PointerEventData eventData)
        {
            StartCoroutine(PerformAction());
        }
        public void OnPointerUp(PointerEventData eventData)
        {
            StartCoroutine(PerformAction());
        }*/

        void Start()
        {
            AddEventListener();
        }

        void AddEventListener()
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

        IEnumerator PerformMiddleware()
        {
            yield return null;
        }

        IEnumerator PerformAction()
        {
            //Debug.Log("PerformAction");
            yield return PerformMiddleware();
            var actions = GetComponents<IBaseButtonAction>();
            for (var i = 0; i < actions.Length; ++i)
            {
                var action = actions[i];
                //Debug.Log(action.GetType());
                yield return action.Perform();
            }
        }
    }
}