namespace StateMachines {

    [System.Serializable]
    public class State : IStateEvents {

        #region Public

        /// <summary>
        /// Id of the null state.
        /// </summary>
        public const int NullStateId = 0;

        /// <summary>
        /// Uniquely identifies this state in its state machine.
        /// </summary>
        public int Id {
            get {
                if (_id == 0) {
                    _id = this.GetId();
                    if (_id == NullStateId)
                        throw new System.Exception($"Id for state {this} cannot be {NullStateId} because that's reserved for the null state");
                }
                return _id;
            }
        }

        /// <summary>
        /// Gets the name of this state.
        /// The <see cref="Id"/> uniquely identifies this state, so this is mostly used for debugging.
        /// </summary>
        public string Name {
            get {
                if (_name == null) {
                    _name = this.GetName();
                    if (_name == null)
                        throw new System.Exception($"Name for this state cannot be null.");
                }
                return _name;
            }
        }

        /// <summary>
        /// Reference to the owner of the state machine.
        /// </summary>
        public object Owner { get; private set; }

        /// <summary>
        /// Reference to the the state machine.
        /// </summary>
        public StateMachine StateMachine { get; private set; }

        /// <summary>
        /// Gets if this state is the current state in the state machine.
        /// </summary>
        public bool IsCurrentState => this.StateMachine.CurrentState == this;

        public override string ToString() {
            return this.Name;
        }

        #endregion

        #region Protected Virtual Methods

        /// <summary>
        /// Called immediately after all states in the state machine are registered and the owner is available.
        /// This will only be called once for each state.
        /// </summary>
        protected virtual void OnRegistered() { }

        /// <summary>
        /// Called when the state machine switches to this state.  This will only be called when changing to a different state.
        /// </summary>
        protected virtual void OnBegin() { }

        /// <summary>
        /// Called by the <see cref="StateMachine"/> during Update() method when this is the current state.
        /// </summary>
        protected virtual void Update() { }

        /// <summary>
        /// Called by the <see cref="StateMachine"/> during FixedUpdate() method when this is the current state.
        /// </summary>
        protected virtual void FixedUpdate() { }

        /// <summary>
        /// Called by the <see cref="StateMachine"/> during LateUpdate() method when this is the current state.
        /// </summary>
        protected virtual void LateUpdate() { }

        /// <summary>
        /// Called when the state machine switches away from this state to a different state.
        /// </summary>
        protected virtual void OnEnd() { }

        /// <summary>
        /// Gets the id for this state.  Cannot be 0, as that's reserved for the null state.
        /// By default, the id is a deterministic hash of the state's type.
        /// </summary>
        protected virtual int GetId() {
            int id = GetDeterministicHash(this.GetType().ToString());
            return id == 0 ? 1 : id;
        }

        /// <summary>
        /// Gets the name for this state.  Internally <see cref="Id"/> is used instead, so this is really just for debugging.
        /// </summary>
        protected virtual string GetName() {
            return this.GetType().Name;
        }

        #endregion

        #region Protected Helper Methods

        /// <summary>
        /// Gets the registered state by its id.
        /// </summary>
        protected State GetState(int id) {
            return this.StateMachine.GetState(id);
        }
        /// <summary>
        /// Gets the registered state of the given type <typeparamref name="T"/>.
        /// </summary>
        protected T GetState<T>() where T : State {
            return this.StateMachine.GetState<T>();
        }

        /// <summary>
        /// Changes the current state to this state, invoking events as necessary.
        /// Nothing happens if this state is already the current state.
        /// </summary>
        /// <returns>If the state changed</returns>
        protected bool ChangeStateToSelf() {
            return this.StateMachine.ChangeState(this);
        }
        /// <summary>
        /// Changes the current state to this state, invoking events as necessary.
        /// If the given state is the current state, the null state is changed to first.
        /// </summary>
        protected void ChangeStateToSelfForce() {
            this.StateMachine.ChangeStateForce(this);
        }

        #endregion

        #region IStateEvents Implementation

        void IStateEvents.Initialize(StateMachine stateMachine) {
            if (stateMachine == null)
                throw new System.ArgumentNullException(nameof(stateMachine));
            if (this.StateMachine != null)
                throw new System.Exception("State cannot be initialized twice.");
            if (stateMachine.Owner == null)
                throw new System.ArgumentException($"State machine's owner must not be null.");

            this.StateMachine = stateMachine;
            this.Owner = this.StateMachine.Owner;
        }

        void IStateEvents.OnRegistered() {
            this.OnRegistered();
        }

        void IStateEvents.OnBegin() {
            this.OnBegin();
        }

        void IStateEvents.Update() {
            this.Update();
        }

        void IStateEvents.FixedUpdate() {
            this.FixedUpdate();
        }

        void IStateEvents.LateUpdate() {
            this.LateUpdate();
        }

        void IStateEvents.OnEnd() {
            this.OnEnd();
        }

        #endregion

        #region Private

        /// <summary>
        /// Gets an integer hash code for the given string.
        /// </summary>
        /// <remarks>From https://andrewlock.net/why-is-string-gethashcode-different-each-time-i-run-my-program-in-net-core/#a-deterministic-gethashcode-implementation </remarks>
        private static int GetDeterministicHash(string str) {
            unchecked {
                int hash1 = (5381 << 16) + 5381;
                int hash2 = hash1;

                for (int i = 0; i < str.Length; i += 2) {
                    hash1 = ((hash1 << 5) + hash1) ^ str[i];
                    if (i == str.Length - 1)
                        break;
                    hash2 = ((hash2 << 5) + hash2) ^ str[i + 1];
                }

                return hash1 + (hash2 * 1566083941);
            }
        }

        [System.NonSerialized]
        private int _id = 0;
        [System.NonSerialized]
        private string _name = null;

        #endregion

    }
}