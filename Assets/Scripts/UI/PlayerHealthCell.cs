using UnityEngine;
using UnityEngine.UI;

namespace BoardDefenceGame.UI
{
    public class PlayerHealthCell : MonoBehaviour
    {
        #region Fields
        [Header("Child Injections")]
        [SerializeField] private Image healthImage;
        [Header("Variables")]
        [SerializeField] private Color enableColor = Color.white;
        [SerializeField] private Color disableColor = Color.black;
        #endregion

        #region Public Methods
        public void MakeEnable()
        {
            healthImage.color = enableColor;
        }

        public void MakeDisable()
        {
            healthImage.color = disableColor;
        }
        #endregion

    }
}
