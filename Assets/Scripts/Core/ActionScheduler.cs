using System.Collections;
using UnityEngine;

namespace RPG.Core
{
    public class ActionScheduler : MonoBehaviour
    {
        private IAction currentAction;

        public void StartAction(IAction action)
        {
            if (currentAction != action)
            {
                currentAction?.Cancel();
                currentAction = action;
            }
        }

        public void CancelCurrentAction()
        {
            StartAction(null);
        }
    }
}