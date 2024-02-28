using BoardDefenceGame.Events;

namespace BoardDefenceGame.UI.Panel.Controllers
{
    public class WinPanelController : EndGamePanelControllerBase
    {
        #region Protected Override Methods
        protected override void RegisterGameEvent()
        {
            GameEvents.OnAllEnemiesDied += EnablePanel;
        }

        protected override void UnRegisterGameEvent()
        {
            GameEvents.OnAllEnemiesDied -= EnablePanel;
        }
        #endregion
    }

}
