using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace BoardDefenceGame.UI.Panel
{
    public abstract class EndGamePanelBase : MonoBehaviour
    {
        #region Fields
        [Header("Child Injections")]
        [SerializeField] private Button continueButton;
        #endregion

        #region Protected Methods
        protected virtual void OnEnable()
        {
            continueButton.onClick.AddListener(ContinueButtonPressed);
        }

        protected virtual void OnDisable()
        {
            continueButton.onClick.RemoveListener(ContinueButtonPressed);
        }

        protected virtual void ContinueButtonPressed()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        #endregion

    }
}
