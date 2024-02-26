using Qw1nt.FiniteStateMachine.Runtime.Common;
using Qw1nt.FiniteStateMachine.Runtime.Interfaces;

namespace Qw1nt.FiniteStateMachine.Runtime.Extensions
{
    public static class StateSwitcherBuilderExtensions
    {
        public static ref StateMachineBuilder AddState<T>(ref this StateMachineBuilder builder) where T : class, IState, new()
        {
            builder.AddState(new T());
            return ref builder;
        }
        
        public static ref StateMachineBuilder AddTransition<TFrom, TTo>(ref this StateMachineBuilder builder)
            where TFrom : class, IState
            where TTo : class, IState
        {
            var from = builder.FindState<TFrom>();
            var to = builder.FindState<TTo>();

            builder.AddTransition(from.Id, to.Id);
            return ref builder;
        }

        public static ref StateMachineBuilder AddTransition<TFrom, TTo>(ref this StateMachineBuilder builder, TFrom from, TTo to)
            where TFrom : class, IState
            where TTo : class, IState
        {
            builder.AddTransition(from.Id, to.Id);
            return ref builder;
        }
        
        public static ref StateMachineBuilder AddTwoWayTransition<TFrom, TTo>(ref this StateMachineBuilder builder)
            where TFrom : class, IState
            where TTo : class, IState
        {
            var from = builder.FindState<TFrom>();
            var to = builder.FindState<TTo>();
            
            builder.AddTransition(from, to);
            builder.AddTransition(to, from);

            return ref builder;
        }
        
        public static ref StateMachineBuilder SetInitialState<TState>(ref this StateMachineBuilder builder)
            where TState : IState
        {
            builder.SetInitialState(builder.FindState<TState>().Id);
            return ref builder;
        }
    }
}