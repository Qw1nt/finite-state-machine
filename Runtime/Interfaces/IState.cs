using System.Collections.Generic;

namespace Qw1nt.FiniteStateMachine.Runtime.Interfaces
{
    public interface IReadOnlyState
    {
        public ulong Id { get; }
    }

    public interface IState : IReadOnlyState, IEqualityComparer<IState>
    {
        IState Enter();

        void Exit();
    }

    public interface IUpdatableState : IState
    {
        void Update(float deltaTime);
    }
}