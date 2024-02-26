using System;
using System.Collections.Generic;
using Qw1nt.FiniteStateMachine.Runtime.Interfaces;

namespace Qw1nt.FiniteStateMachine.Runtime.Common
{
    public struct StateMachineBuilder : IDisposable
    {
        private Dictionary<ulong, IState> _map;
        private HashSet<ulong> _availableTransitionsHashes;

        private ulong _initialStateId;
        private bool _hasInitialState;

        private Dictionary<ulong, IState> Map
        {
            get
            {
                if (_map == null)
                    _map = new Dictionary<ulong, IState>();

                return _map;
            }
        }

        private HashSet<ulong> AvailableTransitionsHashes
        {
            get
            {
                if (_availableTransitionsHashes == null)
                    _availableTransitionsHashes = new HashSet<ulong>(8);

                return _availableTransitionsHashes;
            }
        }

        public bool IsDisposed { get; private set; }

        public void AddState<T>(T instance) where T : class, IState
        {
#if UNITY_EDITOR
            if (Map.ContainsKey(instance.Id) == true)
                throw new ArgumentException($"State with ID {instance.Id} already registered");
#endif

            Map.Add(instance.Id, instance);
        }

        public void AddTransition(ulong from, ulong to)
        {
            var transition = new Transition(from, to);

#if UNITY_EDITOR
            if (AvailableTransitionsHashes.Contains(transition) == true)
                throw new ArgumentException($"Transition from {from} to {to} already added");
#endif

            AvailableTransitionsHashes.Add(transition.Hash);
        }
        
        public void SetInitialState(ulong stateId)
        {
#if UNITY_EDITOR
            if (Map.ContainsKey(stateId) == false)
                throw new ArgumentException($"State with ID {stateId} are not registered");

            if (_hasInitialState == true)
                throw new ArgumentException("Initial state already set");
#endif

            _initialStateId = stateId;
            _hasInitialState = true;
        }

        public void SetInitialState<TState>(TState state)
            where TState : IState
        {
            SetInitialState(state.Id);
        }
        
        public StateMachine Build()
        {
#if UNITY_EDITOR
            if (IsDisposed == true)
                throw new ObjectDisposedException(nameof(StateMachineBuilder));

            if (_hasInitialState == false)
                throw new ArgumentException("Initial state not set");
#endif

            Dispose();
            return new StateMachine(Map, AvailableTransitionsHashes, _initialStateId);
        }

        internal IState FindState<TState>()
            where TState : IState
        {
            foreach (var pair in Map)
            {
                if (pair.Value is TState)
                    return pair.Value;
            }

#if UNITY_EDITOR
            throw new ArgumentException($"State of type {typeof(TState)} not registered");
#endif
        }
        
        public void Dispose()
        {
            IsDisposed = true;
        }
    }
}