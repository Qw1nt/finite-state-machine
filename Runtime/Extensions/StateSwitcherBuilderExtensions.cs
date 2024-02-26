using Qw1nt.FiniteStateMachine.Runtime.Common;
using Qw1nt.FiniteStateMachine.Runtime.Interfaces;

namespace Qw1nt.FiniteStateMachine.Runtime.Extensions
{
    public static class StateSwitcherBuilderExtensions
    {
        public static void AddTwoWayTransition<TFrom, TTo>(ref this StateMachineBuilder builder)
            where TFrom : class, IState
            where TTo : class, IState
        {
            var from = builder.FindState<TFrom>();
            var to = builder.FindState<TTo>();
            
            builder.AddTransition(from, to);
            builder.AddTransition(to, from);
        }
    }
}