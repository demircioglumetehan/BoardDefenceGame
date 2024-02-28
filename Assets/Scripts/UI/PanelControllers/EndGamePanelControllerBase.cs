using UnityEngine;

namespace BoardDefenceGame.UI.Panel.Controllers
{
    public abstract class EndGamePanelControllerBase : MonoBehaviour
    {
        #region Fields
        [SerializeField] protected EndGamePanelBase panel;
        #endregion

        #region Protected Methods
        protected virtual void OnEnable()
        {
            RegisterGameEvent();
        }

        private void OnDisable()
        {
            UnRegisterGameEvent();
        }

        protected abstract void RegisterGameEvent();

        protected abstract void UnRegisterGameEvent();

        protected virtual void EnablePanel()
        {
            panel.gameObject.SetActive(true);
        }
        #endregion

    }

}
