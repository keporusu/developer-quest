using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Collections.Generic;
using System;
using System.Collections.Generic;

namespace Code
{
    

    

    public class ActionManager
    {
        private List<Action> actions = new List<Action>();

        public void AddAction(Action action)
        {
            if (action != null)
            {
                actions.Add(action);
            }
        }

        public bool RemoveOldestAction()
        {
            if (actions.Count > 0)
            {
                actions.RemoveAt(0);
                return true;
            }
            return false;
        }

        public void InvokeActions()
        {
            foreach (var action in actions.ToArray())  // ToArray() を使用して安全に反復
            {
                action?.Invoke();
            }
        }

        public bool RemoveSpecificAction(Action action)
        {
            return actions.Remove(action);
        }

        public void ClearAllActions()
        {
            actions.Clear();
        }

        public int ActionCount => actions.Count;

        public bool HasActions => actions.Count > 0;
    }
}
