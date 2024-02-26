using Qw1nt.FiniteStateMachine.Runtime.Interfaces;

namespace Qw1nt.FiniteStateMachine.Runtime.Base
{
    public abstract class StateBase : IState
    {
        public abstract ulong Id { get; }

        public abstract IState Enter();

        public virtual void Update(float deltaTime)
        {
        }

        public abstract void Exit();

        public bool Equals(IState x, IState y)
        {
            if (x == null && y == null)
                return true;
            
            if (ReferenceEquals(x, null)) 
                return false;
            
            if (ReferenceEquals(y, null)) 
                return false;
            
            return x.Id == y.Id;
        }

        public int GetHashCode(IState obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}