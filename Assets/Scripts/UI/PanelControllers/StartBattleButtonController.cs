using BoardDefenceGame.Events;
using UnityEngine;
using UnityEngine.UI;

namespace BoardDefenceGame.UI.Panel.Controllers
{
    public class StartBattleButtonController : MonoBehaviour
    {
        #region Fields
        [SerializeField] private Button startBattleButton;
        #endregion

        #region Unity Methods
        private void OnEnable()
        {
            startBattleButton.onClick.AddListener(StartBattleButtonPressed);
        }

        private void OnDisable()
        {
            startBattleButton.onClick.RemoveListener(StartBattleButtonPressed);

        }
        #endregion

        #region Private Methods
        private void StartBattleButtonPressed()
        {
            GameEventsUI.OnStartBattleButtonPressed?.Invoke();
            startBattleButton.gameObject.SetActive(false);
        }
        #endregion

    }

}
