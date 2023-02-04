using System.Collections.Generic;
using UnityEngine;

namespace StateMachines {

    /// <summary>
    /// Abstract base class of a state machine.
    /// </summary>
    [System.Serializable]
    public class StateMachine {

        #region Public

        /// <summary>
        /// Gets the owner of this state machine.
        /// </summary>
        public object Owner { get; private set; }

        /// <summary>
        /// Gets the current state.
        /// </summary>
        public State CurrentState => _currentState;

        /// <summary>
        /// Gets the id of the current state.
        /// </summary>
        public int CurrentStateId => _currentStateId;

        /// <summary>
        /// Gets the state that was set previously.
        /// </summary>
        public State PreviousState => _previousState;

        /// <summary>
        /// Gets the id of the previous state.
        /// </summary>
        public int PreviousStateId => _previousStateId;

        /// <summary>
        /// Gets the registered state by its id.
        /// </summary>
        public State GetState(int id) {
            if (id == State.NullStateId)
                return null;
            if (_states.TryGetValue(id, out State state)) {
                return state;
            }

            Debug.LogError($"No registered state with id {id} could be found");
            return null;
        }
        /// <summary>
        /// Gets a registered state of the given type <typeparamref name="T"/>.
        /// </summary>
        public T GetState<T>() {
            foreach (State state in _states.Values) {
                if (state is T stateT) {
                    return stateT;
                }
            }

            Debug.LogError($"No registered state of type {typeof(T).Name} could be found");
            return default;
        }

        /// <summary>
        /// Changes state, invoking events as necessary.  Nothing happens if the given state is the current state.
        /// null can be given to change the state to the null state.
        /// 
        /// It's not recommended to call this directly.  A better approach is to have each state define a custom public Start() method (with its own arguments),
        /// that changes the state to itself.  Call the state's Start() method instead.
        /// </summary>
        /// <returns>If the state changed</returns>
        public bool ChangeState(State state) {
            return this.ChangeState(state, false);
        }
        /// <summary>
        /// Changes state, invoking events as necessary.  If the given state is the current state, the null state is changed to first.
        /// null can be given to change the state to the null state.
        /// 
        /// It's not recommended to call this directly.  A better approach is to have each state define a custom public Start() method (with its own arguments),
        /// that changes the state to itself.  Call the state's Start() method instead.
        /// </summary>
        /// <returns>If the state changed</returns>
        public void ChangeStateForce(State state) {
            this.ChangeState(state, true);
        }

        #endregion

        #region Methods to be called by the Owner

        /// <summary>
        /// To be called by the owner in the Awake() method.  Sets this state machine's owner and registers the states.
        /// After called, the null state is the current state.
        /// </summary>
        /// <param name="owner">Reference to the owner of this state machine.</param>
        public void Awake(object owner) {
            this.Awake(owner, null);
        }
        /// <summary>
        /// To be called by the owner in the Awake() method.  Sets this state machine's owner and registers the states.
        /// After called, the null state is the current state.
        /// </summary>
        /// <param name="owner">Reference to the owner of this state machine.</param>
        /// <param name="states">Additional states to register.</param>
        public void Awake(object owner, params State[] states) {
            if (owner == null)
                throw new System.ArgumentNullException(nameof(owner));
            if (_awakeCalled) {
                throw new System.Exception($"Cannot call {nameof(this.Awake)}() more than once.");
            }
            _awakeCalled = true;

            this.Owner = owner;
            this.RegisterStates();
            if (states != null) {
                this.Register(states);
            }
            foreach (State state in _states.Values) {
                ((IStateEvents)state).OnRegistered();
            }
        }

        /// <summary>
        /// To be called by the owner in the Update() method.  Calls Update() of the current state.
        /// </summary>
        public void Update() {
            _currentStateEvents?.Update();
        }

        /// <summary>
        /// To be called by the owner in the FixedUpdate() method.  Calls FixedUpdate() of the current state.
        /// </summary>
        public void FixedUpdate() {
            _currentStateEvents?.FixedUpdate();
        }

        /// <summary>
        /// To be called by the owner in the LateUpdate() method.  Calls LateUpdate() of the current state.
        /// </summary>
        public void LateUpdate() {
            _currentStateEvents?.LateUpdate();
        }

        #endregion

        #region Protected Virtual Methods

        /// <summary>
        /// Registers the states of this state machine.  To be overridden.
        /// </summary>
        protected virtual void RegisterStates() { }

        #endregion

        #region Protected Helper Methods

        /// <summary>
        /// To be called by a derived class's implementation of <see cref="RegisterStates()"/>.
        /// </summary>
        protected void Register(State state) {
            if (state == null)
                throw new System.ArgumentNullException(nameof(state));
            if (state.Id == State.NullStateId)
                throw new System.ArgumentException($"Cannot register state {state} because its id value is {State.NullStateId}.  This is reserved for the null state.", nameof(state));
            if (_states.ContainsKey(state.Id))
                throw new System.ArgumentException($"Cannot register state {state} because a state with its id {state.Id} was already added.", nameof(state));

            ((IStateEvents)state).Initialize(this);

            _states.Add(state.Id, state);
        }

        /// <summary>
        /// To be called by a derived class's implementation of <see cref="RegisterStates()"/>.  Registers several states at once
        /// </summary>
        protected void Register(params State[] states) {
            foreach (State state in states) {
                this.Register(state);
            }
        }

        #endregion

        #region Private

        /// <summary>
        /// Changes state, invoking events as necessary.
        /// If forced, state will change event if the given state is the same.  This is done by switching to the null state first.
        /// </summary>
        /// <returns>If the state changed</returns>
        private bool ChangeState(State state, bool force) {
            if (state != null) {
                // ensure state machine has been initialized first
                if (!_awakeCalled) {
                    Debug.LogError($"Cannot use this state machine until {nameof(this.Awake)}() is called.");
                    return false;
                }

                // validate that state has the same owner (which also validates that it's in this state machine)
                if (!ReferenceEquals(state.Owner, this.Owner)) {
                    Debug.LogError($"Cannot change to state {state} because it does not belong to the same owner.");
                    return false;
                }
            }

            if (this.CurrentState == state) {
                if (force) {
                    this.ChangeState(null, false);
                    return this.ChangeState(state, false);
                } else {
                    return false;
                }
            }

            // OnEnd event
            _currentStateEvents?.OnEnd();

            // change previous state
            _previousState = this.CurrentState;
            _previousStateId = this.CurrentStateId;

            // change current state
            _currentState = state;
            _currentStateId = this.CurrentState == null ? State.NullStateId : this.CurrentState.Id;
            _currentStateEvents = this.CurrentState;

            // OnBegin event
            _currentStateEvents?.OnBegin();

            return true;
        }

        [System.NonSerialized]
        private bool _awakeCalled = false;
        [System.NonSerialized]
        private Dictionary<int, State> _states = new Dictionary<int, State>();
        [System.NonSerialized]
        private State _currentState;
        [System.NonSerialized]
        private int _currentStateId;
        [System.NonSerialized]
        private IStateEvents _currentStateEvents;
        [System.NonSerialized]
        private State _previousState;
        [System.NonSerialized]
        private int _previousStateId;

        #endregion
    }
}