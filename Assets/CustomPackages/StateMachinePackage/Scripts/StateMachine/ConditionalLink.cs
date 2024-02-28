using System;


namespace OddieGames.StateMachine
{
    public class ConditionalLink : ILink
    {
        IState nextState;
        Func<bool> condition;
        public ConditionalLink(IState nextState, Func<bool> func)
        {
            this.nextState = nextState;
            this.condition = func;
        }

        public bool Validate(out IState nextState)
        {
            nextState = null;
            bool result = false;
            if (condition())
            {
                nextState = this.nextState;
                result = true;
            }
            return result;
        }


    }

}
