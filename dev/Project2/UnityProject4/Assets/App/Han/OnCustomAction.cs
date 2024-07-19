using System.Collections;
using UnityEngine;

namespace Assets.App.Han
{
    public class OnCustomAction : MonoBehaviour, IBaseButtonAction
    {
        public bool onStart;
        public bool onAwake;
        public bool onUpdate;
        
        private void Awake()
        {
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

        public bool isCustomCall()
        {
            return onStart || onAwake || onUpdate;
        }

        public virtual IEnumerator Perform()
        {
            throw new System.NotImplementedException();
        }
    }
}