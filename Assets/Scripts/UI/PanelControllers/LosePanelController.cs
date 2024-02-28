using BoardDefenceGame.Events;

namespace BoardDefenceGame.UI.Panel.Controllers
{
    public class LosePanelController : EndGamePanelControllerBase
    {
        #region Protected Override Methods
        protected override void RegisterGameEvent()
        {
            GameEvents.OnPlayerBaseDestroyed += EnablePanel;
        }

        protected override void UnRegisterGameEvent()
        {
            GameEvents.OnPlayerBaseDestroyed -= EnablePanel;
        }
        #endregion

    }

}
