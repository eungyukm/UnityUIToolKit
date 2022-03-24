using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BKK
{
    public class Demo : MonoBehaviour
    {
        public ParticleSystem first;
        public ParticleSystem last;

        public Button button;

        public float startDelay = 1;
        public float endDelay = 1;

        private bool startDelayed = false;
        private bool endDelayed = false;
        
        private void Awake()
        {
            if (button != null) button.onClick.AddListener(Boom);
        }

        public void Boom()
        {
            StartCoroutine(Co_First());
            StartCoroutine(Co_Last());
        }

        IEnumerator Co_First()
        {
            if(startDelayed) yield break;
            startDelayed = true;
            first.Play(true);
            yield return new WaitForSeconds(startDelay);
            startDelayed = false;
        }
        
        IEnumerator Co_Last()
        {
            if(endDelayed) yield break;
            endDelayed = true;
            yield return new WaitForSeconds(endDelay);
            last.Play(true);
            endDelayed = false;
        }
    }
}

