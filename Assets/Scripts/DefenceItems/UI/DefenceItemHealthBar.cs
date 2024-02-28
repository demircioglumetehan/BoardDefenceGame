using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
namespace BoardDefenceGame.DefenceItems.UI
{
    public class DefenceItemHealthBar : MonoBehaviour
    {
        [SerializeField] private DefenceItemHealthController defenceItemHealthController;
        [SerializeField] private Image healthBarFillingImage;
        [SerializeField] private GameObject healthBarBackGround;
        private void OnEnable()
        {
            defenceItemHealthController.OnCurrentHealthChanged += UpdateHealthBar;
            healthBarFillingImage.fillAmount = 1f;
            healthBarBackGround.SetActive(false);
        }

        private void OnDisable()
        {
            if (!defenceItemHealthController)
                return;
            defenceItemHealthController.OnCurrentHealthChanged -= UpdateHealthBar;

        }
        private void UpdateHealthBar(float currentHealth, float maximumHealth)
        {
            if (!healthBarBackGround.activeInHierarchy)
            {
                healthBarBackGround.SetActive(true);

            }
            var fillAmount = currentHealth / maximumHealth;

            healthBarFillingImage.DOKill();
            healthBarFillingImage.DOFillAmount(fillAmount, .1f).SetEase(Ease.Linear);
        }

    }

}
