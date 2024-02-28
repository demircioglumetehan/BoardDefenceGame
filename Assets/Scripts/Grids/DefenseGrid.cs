using BoardDefenceGame.DefenceItems;

namespace BoardDefenceGame.Core.Grid
{
    public class DefenseGrid : GridBase
    {
        #region Fields
        public bool HasDefenceItem { get; private set; } = false;
        public DefenceItem HoldingDefenceItem { get; private set; }
        #endregion

        #region Public Methods
        public void PutDefenceItemToGrid(DefenceItem addingDefenceItem)
        {
            addingDefenceItem.transform.position = transform.position;
            this.HoldingDefenceItem = addingDefenceItem;
            addingDefenceItem.StartDefending(this);
            HasDefenceItem = true;
        }
        public void OnDefenceItemDestroyed()
        {
            HasDefenceItem = false;
            HoldingDefenceItem = null;
        }
        #endregion

    }

}
