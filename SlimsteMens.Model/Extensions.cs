using System;
using System.Collections.Generic;
using System.Linq;

namespace SlimsteMens.Model
{
    public static class Extensions
    {
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
        {
            var rand = new Random((int)DateTime.Now.Ticks);
            return source.Select(t => new Pair(rand.Next(), t)).OrderBy(pair => (int)pair.First)
                .Select(pair => (T)pair.Second).ToList();
        }
    }

    public sealed class Pair
    {
        public Object First;
        public Object Second;

        public Pair() { }
        public Pair(Object first, Object second)
        {
            First = first;
            Second = second;
        }
    }
}
