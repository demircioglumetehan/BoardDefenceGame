using UnityEngine;
using TMPro;
using BoardDefenceGame.Events;

namespace BoardDefenceGame.UI.Panel.Controllers
{
    public class CurrentLevelTextController : MonoBehaviour
    {
        #region Fields
        [SerializeField] private TextMeshProUGUI currentLevelText;
        #endregion

        #region Unity Methods
        private void OnEnable()
        {
            GameEvents.OnCurrentLevelSet += UpdateLevelText;
        }
        private void OnDisable()
        {
            GameEvents.OnCurrentLevelSet -= UpdateLevelText;
        }
        #endregion

        #region Private Methods
        private void UpdateLevelText(LevelDataScriptableObject currentLevel)
        {
            currentLevelText.text = "LEVEL " + currentLevel.LevelNumber.ToString();
        }
        #endregion

    }

}

