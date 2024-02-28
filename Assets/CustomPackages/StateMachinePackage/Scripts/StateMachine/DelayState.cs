using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OddieGames.StateMachine
{
    /// <summary>
    /// Delays the state-machine for the set amount
    /// </summary>
    public class DelayState : AbstractState
    {
        public override string Name => nameof(DelayState);

        readonly float m_DelayInSeconds;
        
        /// <param name="delayInSeconds">delay in seconds</param>
        public DelayState(float delayInSeconds)
        {
            m_DelayInSeconds = delayInSeconds;
        }

        public override IEnumerator Execute()
        {
            Debug.Log("DelaySate Entered" + m_DelayInSeconds);
            var startTime = Time.time;
            while (Time.time - startTime < m_DelayInSeconds)
            {
                yield return null;
            }
            Debug.Log("DelaySate Exited" + m_DelayInSeconds);
        }
    }
}
