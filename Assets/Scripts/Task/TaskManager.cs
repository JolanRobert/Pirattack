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
        [SerializeField] private List<Task> tasks;

        private Queue<Task> nextTasks;
        public List<Task> currentTasks = new List<Task>();

        public bool isCycling;

        private void Awake()
        {
            RefreshNextTasks();
            StartCoroutine(TaskCycle());
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
            List<Task> tasksToAdd = new List<Task>(tasks);
            tasksToAdd.Shuffle();
            Debug.Log(tasksToAdd.RemoveAll(task => currentTasks.Contains(task)));
            nextTasks = new Queue<Task>(tasksToAdd);
        }
        
        [ContextMenu("Add Task")]
        private void AddTask()
        {
            if (nextTasks.Count == 0) RefreshNextTasks();
            
            Task newTask = nextTasks.Peek();
            if (currentTasks.Contains(newTask)) return;
            
            currentTasks.Add(nextTasks.Dequeue());
            
            newTask.OnComplete = CompleteTask;
            newTask.gameObject.SetActive(true);
            newTask.Init();
        }

        private void CompleteTask(Task task)
        {
            currentTasks.Remove(task);
            task.gameObject.SetActive(false);
            if (!isCycling) StartCoroutine(TaskCycle());
        }
    }
}
