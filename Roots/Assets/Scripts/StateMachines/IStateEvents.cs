namespace StateMachines {

    /// <summary>
    /// Interface used for methods that are only invoked by the <see cref="StateMachine"/>.  These should not be called otherwise.
    /// </summary>
    internal interface IStateEvents {

        void Initialize(StateMachine stateMachine);

        void OnRegistered();

        void OnBegin();

        void Update();

        void FixedUpdate();

        void LateUpdate();

        void OnEnd();
    }
}