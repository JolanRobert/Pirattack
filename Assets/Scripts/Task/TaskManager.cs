using System.Collections;
using System.Collections.Generic;
using MyBox;
using UnityEngine;

namespace Task
{
    public class TaskManager : MonoBehaviour
    {
        [SerializeField] private int maxTaskSimultaneously;
        [SerializeField, MinMaxRange(1, 10)] private RangedInt timeBeforeNextTask;
        [SerializeField] private List<ChaosTask> tasks;

        private Queue<ChaosTask> nextTasks;
        private List<ChaosTask> currentTasks = new List<ChaosTask>();

        private bool isCycling;

        private void Awake()
        {
            if (tasks.Count == 0) return;
            RefreshNextTasks();
            //StartCoroutine(TaskCycle());
        }

        private IEnumerator TaskCycle()
        {
            isCycling = true;
            int time = Random.Range(timeBeforeNextTask.Min, timeBeforeNextTask.Max+1);
            yield return new WaitForSeconds(time);
            AddTask();

            if (currentTasks.Count < maxTaskSimultaneously) StartCoroutine(TaskCycle());
            else isCycling = false;
        }

        private void RefreshNextTasks()
        {
            List<ChaosTask> tasksToAdd = new List<ChaosTask>(tasks);
            tasksToAdd.Shuffle();
            nextTasks = new Queue<ChaosTask>(tasksToAdd);
        }
        
        [ContextMenu("Add Task")]
        private void AddTask()
        {
            if (nextTasks.Count == 0) RefreshNextTasks();

            ChaosTask newChaosTask = nextTasks.Peek();
            if (currentTasks.Contains(newChaosTask)) return;
            
            currentTasks.Add(nextTasks.Dequeue());
            
            newChaosTask.OnComplete = CompleteTask;
            newChaosTask.gameObject.SetActive(true);
            newChaosTask.Init();
        }

        private void CompleteTask(ChaosTask chaosTask)
        {
            currentTasks.Remove(chaosTask);
            chaosTask.gameObject.SetActive(false);
            //if (!isCycling) StartCoroutine(TaskCycle());
        }
    }
}
