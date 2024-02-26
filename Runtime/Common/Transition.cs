using System.Collections.Generic;
using Qw1nt.FiniteStateMachine.Runtime.Interfaces;

namespace Qw1nt.FiniteStateMachine.Runtime.Common
{
    internal readonly struct Transition : IEqualityComparer<Transition>, IEqualityComparer<ulong>
    {
        private const ulong Offset = 3074457345618258791ul;
        
        public Transition(ulong from, ulong to) : this()
        {
            From = from;
            To = to;
            Hash = CalculateHash(From, To);
        }
        
        public Transition(IReadOnlyState from, IReadOnlyState to) : this()
        {
            From = from.Id;
            To = to.Id;
            Hash = CalculateHash(From, To);
        }     
     
        public ulong Hash { get; }
        
        public ulong From { get; }
        
        public ulong To { get; }

        private ulong CalculateHash(ulong from, ulong to)
        {
            var hash = Offset;
            
            hash += from;
            hash *= Offset;

            hash += to;
            hash *= Offset;

            return hash;
        }
        
        public bool Equals(Transition x, Transition y)
        {
            return x.Hash == y.Hash;
        }

        public int GetHashCode(Transition obj)
        {
            return (int) Hash;
        }

        public bool Equals(ulong x, ulong y)
        {
            return x == y;
        }

        public int GetHashCode(ulong obj)
        {
            return (int) obj;
        }

        public static implicit operator ulong(Transition transition)
        {
            return transition.Hash;
        }
    }
}