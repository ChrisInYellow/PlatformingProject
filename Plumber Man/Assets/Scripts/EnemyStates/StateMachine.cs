using System; 
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices; 
using UnityEngine;
using Object = System.Object;


namespace EnemyStates.StateMachine
{
    public enum StateTransition
    {
        Safe,
        Overwrite,
    }

    public interface IStateMachine
    {
        MonoBehaviour Component { get; }
        StateMapping CurrentStateMap { get; }
        bool IsInTransition { get; }
    }

    public class StateMachine<T> : IStateMachine where T : struct, IConvertible, IComparable
    {
        public event Action<T> Changed;

        private StateMachineRunner engine;
        private MonoBehaviour component;

        private StateMapping lastState;
        private StateMapping currentState;
        private StateMapping destinationState;

        private Dictionary<object, StateMapping> stateObserve;

        private readonly string[] ignoredNames = new[] { "add", "remove", "get", "set" };

        private bool isInTransition = false;
        private IEnumerator currentTransition;
        private IEnumerator exitAction;
        private IEnumerator enterAction;
        private IEnumerator queuedTransition;

        public StateMachine(StateMachineRunner engine, MonoBehaviour component)
        {
            this.engine = engine;
            this.component = component;

            //Define States
            var values = Enum.GetValues(typeof(T));
            if (values.Length < 1)
            {
                throw new ArgumentException
                    ("Provided Enum must have at least one definition. ");
            }

            stateObserve = new Dictionary<Object, StateMapping>();
            for (int i = 0; i < values.Length; i++)
            {
                var mapping = new StateMapping((Enum)values.GetValue(i));
                stateObserve.Add(mapping.state, mapping);
            }

            //Reflect functions
            var functions = component.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            //Bind functions to states
            var seperator = "_".ToCharArray();
            for (int i = 0; i < functions.Length; i++)
            {
                if (functions[i].GetCustomAttributes(typeof(CompilerGeneratedAttribute), true).Length != 0)
                {
                    continue;
                }

                var names = functions[i].Name.Split(seperator);

                //Ignore functions without an "_"
                if (names.Length <= 1)
                {
                    continue;
                }

                Enum key;
                try
                {
                    key = (Enum)Enum.Parse(typeof(T), names[0]);
                }
                catch (ArgumentException)
                {
                    //If it isn't a function listed in the state enum.
                    continue;
                }

                var targetState = stateObserve[key];

                switch (names[1])
                {
                    case "Enter":
                        if (functions[i].ReturnType == typeof(IEnumerator))
                        {
                            targetState.hasEnterAction = true;
                            targetState.EnterAction = CreateDelegate<Func<IEnumerator>>(functions[i], component);
                        }
                        else
                        {
                            targetState.hasEnterAction = false;
                            targetState.EnterCall = CreateDelegate<Action>(functions[i], component);

                        }
                        break;
                    case "Exit":
                        if (functions[i].ReturnType == typeof(IEnumerator))
                        {
                            targetState.hasExitAction = true;
                            targetState.ExitAction = CreateDelegate<Func<IEnumerator>>(functions[i], component);
                        }
                        else
                        {
                            targetState.hasExitAction = false;
                            targetState.ExitCall = CreateDelegate<Action>(functions[i], component);
                        }
                        break;
                    case "Finally":
                        targetState.Finally = CreateDelegate<Action>(functions[i], component);
                        break;
                    case "Update":
                        targetState.Update = CreateDelegate<Action>(functions[i], component);
                        break;
                    case "LateUpdate":
                        targetState.LateUpdate = CreateDelegate<Action>(functions[i], component);
                        break;
                    case "FixedUpdate":
                        targetState.FixedUpdate = CreateDelegate<Action>(functions[i], component);
                        break;
                    case "OnCollisionEnter":
                        targetState.OnCollisionEnter = CreateDelegate<Action<Collision2D>>(functions[i], component);
                        break;
                }
            }
            //Create nil State Mapping
            currentState = new StateMapping(null);

        }
        private V CreateDelegate<V>(MethodInfo function, Object target) where V : class
        {
            var ret = (Delegate.CreateDelegate(typeof(V), target, function) as V);

            if (ret == null)
            {
                throw new ArgumentException("Unable to create delegate for called function" + function.Name);
            }
            return ret;
        }

        public void ChangeState(T newState)
        {
            ChangeState(newState, StateTransition.Safe);
        }

        public void ChangeState(T newState, StateTransition transition)
        {
            if (stateObserve == null)
            {
                throw new Exception("No state with the name " + newState.ToString() + " can be found! Make sure you called the correct type the statemachine was initialized with");

            }

            var nextState = stateObserve[newState];

            if (currentState == nextState) return;

            //Cancel any queued transitions
            if (queuedTransition != null)
            {
                engine.StopCoroutine(queuedTransition);
                queuedTransition = null;
            }

            switch (transition)
            {
                //case StateMachineTransition.Blend.

                case StateTransition.Safe:
                    if (isInTransition)
                    {
                        if (exitAction != null) //If already exiting current state on our way to previous target state
                        {
                            //Overwrite with new target
                            destinationState = nextState;
                            return;
                        }
                        if (enterAction != null) //Already entering previous target state. Wait and Call the exit routine
                        {
                            queuedTransition = WaitForPreviousTransition(nextState);
                            engine.StartCoroutine(queuedTransition);
                            return;
                        }
                    }
                    break;
                case StateTransition.Overwrite:
                    if (currentTransition != null)
                    {
                        engine.StopCoroutine(currentTransition);
                    }
                    if (currentTransition != null)
                    {
                        engine.StopCoroutine(exitAction);
                    }
                    if (enterAction != null)
                    {
                        engine.StopCoroutine(enterAction);
                    }
                    //If we are currently in EnterAction and Exit is also a action, this will be skipped in ChangeToNewStateRoutine()
                    break;
            }

            if ((currentState != null && currentState.hasExitAction) || nextState.hasEnterAction)
            {
                isInTransition = true;
                currentTransition = ChangeToNewStateAction(nextState, transition);
                engine.StartCoroutine(currentTransition);
            }
            else { // sSame frame transition (no coroutines).
                if (currentState != null)
                {
                    currentState.ExitCall();
                    currentState.Finally();
                }

                lastState = currentState;
                currentState = nextState;
                if (currentState != null)
                {
                    currentState.EnterCall();
                    if (Changed != null)
                    {
                        Changed((T)currentState.state);
                    }
                }
                isInTransition = false;

            }
        }

        private IEnumerator ChangeToNewStateAction(StateMapping newState, StateTransition transition)
        {
            destinationState = newState; //Chache this so that we can overwrite it and hijack a transition

            if (currentState != null)
            {
                if (currentState.hasExitAction)
                {
                    exitAction = currentState.ExitAction();

                    if (exitAction != null && transition != StateTransition.Overwrite) //Don't wait for exit if we are overwriting
                    {
                        yield return engine.StartCoroutine(exitAction);
                    }

                    exitAction = null;
                }
                else
                {
                    currentState.ExitCall();
                }

                currentState.Finally();
            }

            lastState = currentState;
            currentState = destinationState;

            if (currentState != null)
            {
                if (currentState.hasEnterAction)
                {
                    enterAction = currentState.EnterAction();

                    if (enterAction != null)
                    {
                        yield return engine.StartCoroutine(enterAction);
                    }
                    enterAction = null;
                }
                else
                {
                    currentState.EnterCall();
                }
                //Broadcast change only after transition has begun

                if (Changed != null)
                {
                    Changed((T)currentState.state);
                }
            }
            isInTransition = false;
        }

        IEnumerator WaitForPreviousTransition(StateMapping nextState)
        {
            while (isInTransition)
            {
                yield return null;
            }

            ChangeState((T)nextState.state);
        }

        public T LastState
        {
            get
            {
                if (lastState == null) return default(T);
                return (T)lastState.state;
            }
        }

        public T State
        {
            get { return (T)currentState.state; }
        }

        public bool IsInTransition
        {
            get { return isInTransition; }
        }

        public StateMapping CurrentStateMap
        {
            get { return currentState; }
        }

        public MonoBehaviour Component
        {
            get { return component; }
        }

        public static StateMachine<T> Initialize(MonoBehaviour component)
        {
            var engine = component.GetComponent<StateMachineRunner>();
            if (engine == null) engine = component.gameObject.AddComponent<StateMachineRunner>();

            return engine.Initialize<T>(component);
        }

        public static StateMachine<T> Initalize(MonoBehaviour component, T startState)
        {
            var engine = component.GetComponent<StateMachineRunner>();
            if (engine == null) engine = component.gameObject.AddComponent<StateMachineRunner>();

            return engine.Initialize<T>(component, startState);
        }
    }
}