using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace OddieGames.StateMachine
{
    /// <summary>
    /// A Generic state machine
    /// </summary>
    public class StateMachine
    {
        public IState CurrentState { get; private set; }
        private List<ILink> AnyStateLinks=new List<ILink>();
        /// <summary>
        /// Finalizes the previous state and then runs the new state
        /// </summary>
        /// <param name="state"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public virtual void SetCurrentState(IState state)
        {
            if (state == null)
                throw new ArgumentNullException(nameof(state));

            if (CurrentState != null && m_CurrentPlayCoroutine != null) 
            {
                //interrupt currently executing state
                Skip();
            }
            
            CurrentState = state;
            Coroutines.StartCoroutine(Play());
        }

        Coroutine m_CurrentPlayCoroutine;
        bool m_PlayLock;
        /// <summary>
        /// Runs the life cycle methods of the current state.
        /// </summary>
        /// <returns></returns>
        IEnumerator Play()
        {
            if (!m_PlayLock)
            {
                m_PlayLock = true;
                
                CurrentState.Enter();

                //keep a ref to execute coroutine of the current state
                //to support stopping it later.
                m_CurrentPlayCoroutine = Coroutines.StartCoroutine(CurrentState.Execute());
                yield return m_CurrentPlayCoroutine;
                
                m_CurrentPlayCoroutine = null;
            }
        }
        /// <summary>
        /// Adds Link which makes transition whatever current state is.
        /// </summary>
        /// <param name="link"></param>
        public void AddAnyStateLink(ILink link)
        {
            if (!AnyStateLinks.Contains(link))
            {
                AnyStateLinks.Add(link);
            }
        }
        public void RemoveAnyStateLink(ILink link, IState passingState)
        {
            if (!AnyStateLinks.Contains(link))
            {
                AnyStateLinks.Remove(link);
            }
        }
        /// <summary>
        /// Interrupts the execution of the current state and finalizes it.
        /// </summary>
        /// <exception cref="Exception"></exception>
        void Skip()
        {
            if (CurrentState == null)
                throw new Exception($"{nameof(CurrentState)} is null!");
            
            if (m_CurrentPlayCoroutine != null)
            {
                Coroutines.StopCoroutine(ref m_CurrentPlayCoroutine);
                //finalize current state
                CurrentState.Exit();
                m_CurrentPlayCoroutine = null;
                m_PlayLock = false;
            }
        }
        
        public virtual void Run(IState state)
        {
            SetCurrentState(state);
            Run();
        }
        
        Coroutine m_LoopCoroutine;
        /// <summary>
        /// Turns on the main loop of the StateMachine.
        /// This method does not resume previous state if called after Stop()
        /// and the client needs to set the state manually.
        /// </summary>
        public virtual void Run() 
        {
            if (m_LoopCoroutine != null) //already running
                return;
            
            m_LoopCoroutine = Coroutines.StartCoroutine(Loop());
        }

        /// <summary>
        /// Turns off the main loop of the StateMachine
        /// </summary>
        public void Stop()
        {
            if (m_LoopCoroutine == null) //already stopped
                return;
            
            if (CurrentState != null && m_CurrentPlayCoroutine != null) 
            {
                //interrupt currently executing state
                Skip();
            }
            
            Coroutines.StopCoroutine(ref m_LoopCoroutine);
            CurrentState = null;
        }

        /// <summary>
        /// The main update loop of the StateMachine.
        /// It checks the status of the current state and its link to provide state sequencing
        /// </summary>
        /// <returns></returns>
        protected virtual IEnumerator Loop()
        {
            while (true)
            {
                if(CurrentState != null && AnyStateLinks.Count > 0)
                {
                    if (HasAnyStateLinkConditionOccured(out var nextState))
                    {
                        if (m_PlayLock)
                        {
                            //finalize current state
                            CurrentState.Exit();
                            m_PlayLock = false;
                        }
                        CurrentState.DisableLinks();
                        SetCurrentState(nextState);
                        CurrentState.EnableLinks();
                    }
                }
                if (CurrentState != null && m_CurrentPlayCoroutine == null) //current state is done playing
                {
                    if (CurrentState.ValidateLinks(out var nextState))
                    {
                        if (m_PlayLock)
                        {
                            //finalize current state
                            CurrentState.Exit();
                            m_PlayLock = false;
                        }
                        CurrentState.DisableLinks();
                        SetCurrentState(nextState);
                        CurrentState.EnableLinks(); 
                    }
                }

                yield return null;
            }
        }

        private bool HasAnyStateLinkConditionOccured(out IState nextState2)
        {
            nextState2 = null;
            foreach (var link in AnyStateLinks)
            {
                if(link.Validate(out IState nextState))
                {
                    nextState2 = nextState;
                    return true;
                }
            }
            return false;
        }

        public bool IsRunning => m_LoopCoroutine != null;
    }
}
