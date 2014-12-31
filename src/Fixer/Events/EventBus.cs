using System;
using System.Collections.Generic;

namespace Fixer.Events
{
    internal static class EventBus
    {
        private static List<Delegate> actions;

        public static void Register<T>(Action<T> callback) where T : IEvent
        {
            if (actions == null)
                actions = new List<Delegate>();

            actions.Add(callback);
        }

        public static void UnRegisterAll()
        {
            actions = null;
        }

        public static void Raise<T>(T args) where T : IEvent
        {
            if (actions != null)
                foreach (var action in actions)
                    if (action is Action<T>)
                        ((Action<T>)action)(args);
        }
    }
}
