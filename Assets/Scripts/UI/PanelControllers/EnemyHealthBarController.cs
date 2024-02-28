using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace BoardDefenceGame.Enemy.UI.Controllers
{
    public class EnemyHealthBarController : MonoBehaviour
    {
        #region Fields
        [SerializeField] private GameObject enemyHealthBar;
        [SerializeField] private Image enemyHealthBarFillingImage;
        [SerializeField] private EnemyHealthController enemyHealthController;
        [SerializeField] private BaseEnemy baseEnemy;
        #endregion

        #region Unity Methods
        private void OnEnable()
        {
            enemyHealthController.OnEnemyHealthChanged += UpdateHealthBar;
            baseEnemy.OnEnemyDied += HideHealthBar;
            InitiateHealthBar();
            HideHealthBar();
        }

        private void OnDisable()
        {
            enemyHealthController.OnEnemyHealthChanged -= UpdateHealthBar;
            baseEnemy.OnEnemyDied -= HideHealthBar;
        }
        #endregion

        #region Private Methods
        private void UpdateHealthBar(float currentHealth, float maximumHealth)
        {
            if (!enemyHealthBar.activeInHierarchy)
            {
                enemyHealthBar.gameObject.SetActive(true);
            }
            var fillAmount = currentHealth / maximumHealth;
            enemyHealthBarFillingImage.DOFillAmount(fillAmount, .4f);
        }
        private void InitiateHealthBar()
        {
            enemyHealthBarFillingImage.fillAmount = 1f;
        }

        private void HideHealthBar()
        {
            enemyHealthBar.SetActive(false);
        }
        #endregion

    }

}
