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
        private Queue<Action> actions = new Queue<Action>();

        public void AddAction(Action action)
        {
            if (action != null)
            {
                actions.Enqueue(action);
            }
        }

        public void InvokeAction()
        {
            if (actions.Count > 0)
            {
                Action action = actions.Dequeue();
                action.Invoke();
            }
        }
    }
}
