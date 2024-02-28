using System;
using System.Collections;
using UnityEngine;

namespace BoardDefenceGame.DefenceItems
{
    public class DefenceItemCooldownController : MonoBehaviour
    {
        #region Fields
        DefenceItemAttackController defenceItemAttackController;
        #endregion
        #region Unity Methods
        private void OnEnable()
        {
            defenceItemAttackController = GetComponent<DefenceItemAttackController>();
            defenceItemAttackController.OnDefenceItemAttacked += StartCoolDown;
        }

        private void OnDisable()
        {
            defenceItemAttackController.OnDefenceItemAttacked -= StartCoolDown;

        }
        #endregion

        #region Private Methods
        private void StartCoolDown(float coolDownTime)
        {
            defenceItemAttackController.DisableAttackingStatus();
            StartCoroutine(WaitForActionCor(coolDownTime, () =>
            {
                defenceItemAttackController.EnableAttackingStatus();
            }));
        }

        private IEnumerator WaitForActionCor(float waitTime, Action waitedAction)
        {
            yield return new WaitForSeconds(waitTime);
            waitedAction?.Invoke();

        }
        #endregion
    }


}
