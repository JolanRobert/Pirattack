using System.Collections.Generic;
using MyBox;
using UnityEngine;

namespace Task
{
    public class TaskManager : MonoBehaviour
    {
        [SerializeField] private List<Task> tasks;

        private Queue<Task> nextTasks;
        private List<Task> currentTasks = new List<Task>();

        private void Awake()
        {
            RefreshNextTasks();
        }

        private void RefreshNextTasks()
        {
            nextTasks = new Queue<Task>(tasks.Shuffle());
        }
        
        [ContextMenu("Add Task")]
        private void AddTask()
        {
            Task newTask = nextTasks.Peek();
            
            if (currentTasks.Contains(newTask)) return;
            currentTasks.Add(nextTasks.Dequeue());

            newTask.gameObject.SetActive(true);
            newTask.Init();
            Debug.Log($"New Task {newTask.gameObject.name}");
            
            if (nextTasks.Count == 0) RefreshNextTasks();
        }
    }
}
