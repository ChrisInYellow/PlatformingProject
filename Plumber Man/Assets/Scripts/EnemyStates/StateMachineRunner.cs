
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using UnityEngine;
using Object = System.Object;

namespace EnemyStates.StateMachine
{
    public class StateMachineRunner : MonoBehaviour
    {
        private List<IStateMachine> stateMachineList = new List<IStateMachine>();
        ///<summary>
        /// Creates a stateMachine token object which is used to managed to the state of a monobehaviour. 
        ///</summary>
        /// <typeparam name="T">An Enum listing different state transitions</typeparam>
        /// <param name="component">The component whose state will be managed</param>
        /// <returns></returns>
        public StateMachine<T> Initialize<T>(MonoBehaviour component) where T : struct, IConvertible, IComparable
        {
            var fsm = new StateMachine<T>(this, component);

            stateMachineList.Add(fsm);

            return fsm; 
        }

        /// <summary>
        /// Creates a stateMachine token object which is used to managed to the state of a monobehaviour. Will automatically transition the startState
        /// </summary>
        /// <typeparam name="T">An Enum listing different state transitions</typeparam>
        /// <param name="component">The component whose state will be managed</param>
        /// <param name="startState">The default start state</param>
        /// <returns></returns>
        public StateMachine<T> Initialize<T>(MonoBehaviour component, T startState) where T : struct, IConvertible, IComparable
        {
            var fsm = Initialize<T>(component);
            fsm.ChangeState(startState);

            return fsm;
        }

        void FixedUpdate()
        {
            for(int i = 0; i<stateMachineList.Count; i++)
            {
                var fsm = stateMachineList[i];
                if (!fsm.IsInTransition && fsm.Component.enabled) fsm.CurrentStateMap.FixedUpdate(); 
            }
        }

         void Update()
        {
            for(int i= 0; i<stateMachineList.Count; i++)
            {
                var fsm = stateMachineList[i]; 
                if(!fsm.IsInTransition && fsm.Component.enabled)
                {
                    fsm.CurrentStateMap.Update();
                }
            }
        }

         void LateUpdate()
        {
            for(int i = 0; i<stateMachineList.Count; i++)
            {
                var fsm = stateMachineList[i]; 
                if(!fsm.IsInTransition && fsm.Component.enabled)
                {
                    fsm.CurrentStateMap.LateUpdate();
                }
            }
        }

        public static void DoNothing()
        {

        }

        public static void DoNothingCollision(Collision2D other)
        {

        }

        public static void DoNothingTrigger(Collider2D other)
        {

        }

        public static IEnumerator DoNothingCoroutine()
        {
            yield break; 
        }
    }

    public class StateMapping
    {
        public object state;

        public bool hasEnterAction;
        public Action EnterCall = StateMachineRunner.DoNothing;
        public Func<IEnumerator> EnterAction = StateMachineRunner.DoNothingCoroutine;

        public bool hasExitAction;
        public Action ExitCall = StateMachineRunner.DoNothing;
        public Func<IEnumerator> ExitAction = StateMachineRunner.DoNothingCoroutine;

        public Action Finally = StateMachineRunner.DoNothing;
        public Action Update = StateMachineRunner.DoNothing;
        public Action LateUpdate = StateMachineRunner.DoNothing;
        public Action FixedUpdate = StateMachineRunner.DoNothing;
        public Action<Collision2D> OnCollisionEnter = StateMachineRunner.DoNothingCollision;
        public Action<Collider2D> OnTriggerEnter = StateMachineRunner.DoNothingTrigger; 

        public StateMapping(object state)
        {
            this.state = state; 
        }
    }
}
