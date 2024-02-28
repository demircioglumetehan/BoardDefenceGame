using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace BoardDefenceGame.UI
{
    public class KilledEnemyCell : MonoBehaviour
    {
        #region Fields
        [Header("Child Injections")]
        [SerializeField] private Image enemyIconImage;
        [SerializeField] private TextMeshProUGUI killedAmountText;
        #endregion

        #region Public Methods
        public void Initialize(EnemyCounter killedEnemies)
        {
            killedAmountText.text = "x" + killedEnemies.EnemyAmount.ToString();
            enemyIconImage.sprite = killedEnemies.EnemyFeature.EnemyIcon;
        }
        #endregion

    }
}
