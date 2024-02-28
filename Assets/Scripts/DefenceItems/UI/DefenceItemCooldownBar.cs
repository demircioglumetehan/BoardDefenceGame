using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
namespace BoardDefenceGame.DefenceItems.UI
{
    public class DefenceItemCooldownBar : MonoBehaviour
    {
        #region Fields
        [SerializeField] private DefenceItemAttackController defenceItemAttackController;
        [SerializeField] private Image cooldownFillingImage;
        #endregion

        #region Unity Methods
        private void OnEnable()
        {
            defenceItemAttackController.OnDefenceItemAttacked += RefillCooldownBar;
            cooldownFillingImage.fillAmount = 1f;
        }
        private void OnDisable()
        {
            defenceItemAttackController.OnDefenceItemAttacked -= RefillCooldownBar;
        }
        #endregion

        #region Private Methods
        private void RefillCooldownBar(float cooldownTime)
        {
            cooldownFillingImage.fillAmount = 0f;
            cooldownFillingImage.DOFillAmount(1f, cooldownTime).SetEase(Ease.Linear);
        }
        #endregion
    }

}
