using System;
using System.Collections.Generic;
using Qw1nt.FiniteStateMachine.Runtime.Interfaces;
using R3;

namespace Qw1nt.FiniteStateMachine.Runtime.Common
{
    public class StateMachine : IStateSwitcher, IUpdatableStateSwitcher
    {
        private readonly ReactiveProperty<IState> _activeState = new();
        private readonly Dictionary<ulong, IState> _map;
        private readonly HashSet<ulong> _transitionsHashes;

        private IUpdatableState _updatableState;

        internal StateMachine(Dictionary<ulong, IState> map, HashSet<ulong> transitionsHashes, ulong initialState)
        {
            _map = map;
            _transitionsHashes = transitionsHashes;
            _activeState.Value = _map[initialState];
        }
        
        public ReadOnlyReactiveProperty<IState> ActiveState => _activeState;

        public void Switch(ulong stateId)
        {
#if UNITY_EDITOR
            if (_map.ContainsKey(stateId) == false)
                throw new ArgumentException($"State with ID {stateId} not register");

            var transition = new Transition(_activeState.Value.Id, stateId);
            
            if (_activeState.Value != null &&
                _transitionsHashes.Contains(transition) == false)
                throw new ArgumentException($"No transition from state {_activeState.Value.Id} to state {stateId} was registered");
#endif
            
            _activeState.Value?.Exit();
            _activeState.Value = _map[stateId].Enter();

            if (_activeState.Value is IUpdatableState updatableState)
                _updatableState = updatableState;
        }

        public void Update(float deltaTime)
        {
            _updatableState?.Update(deltaTime);
        }
    }
}