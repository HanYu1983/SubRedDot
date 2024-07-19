using System.Collections;
using UnityEngine;

namespace Assets.App.Han
{
    public class AbstractBaseButtonAction : MonoBehaviour, IBaseButtonAction
    {
        public GameState GetGameState()
        {
            var gameState = FindObjectOfType<GameState>();
            if(gameState == null)
            {
                throw new UnityException("gameState not found");
            }
            return gameState;
        }
        public virtual IEnumerator Perform()
        {
            yield return null;
        }

        public virtual IEnumerator PerformFloat(float v)
        {
            yield return null;
        }

        public virtual IEnumerator PerformInt(int v)
        {
            yield return null;
        }

        public virtual IEnumerator PerformString(string v)
        {
            yield return null;
        }
    }
}