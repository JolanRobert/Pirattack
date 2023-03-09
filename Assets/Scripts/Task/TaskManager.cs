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

        [SerializeField, ReadOnly] private List<Task> nextTasks;
        [SerializeField, ReadOnly] private List<Task> currentTasks = new List<Task>();

        private bool isCycling = true;

        private void Awake()
        {
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
            nextTasks = new List<Task>(tasks);
            nextTasks.Shuffle();
        }
        
        [ContextMenu("Add Task")]
        private void AddTask()
        {
            Task newTask = nextTasks[0];
            if (currentTasks.Contains(newTask)) return;
            
            currentTasks.Add(newTask);
            nextTasks.RemoveAt(0);
            
            newTask.OnComplete = CompleteTask;
            newTask.gameObject.SetActive(true);
            newTask.Init();
            Debug.Log($"New Task {newTask.gameObject.name}");
            
            if (nextTasks.Count == 0) RefreshNextTasks();
        }

        private void CompleteTask(Task task)
        {
            currentTasks.Remove(task);
            task.gameObject.SetActive(false);
            //if (!isCycling) StartCoroutine(TaskCycle());
        }
    }
}
