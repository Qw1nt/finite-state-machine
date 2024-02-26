using R3;

namespace Qw1nt.FiniteStateMachine.Runtime.Interfaces
{
    public interface IStateSwitcher
    {
        ReadOnlyReactiveProperty<IState> ActiveState { get; }
        
        void Switch(ulong stateId);
    }
}