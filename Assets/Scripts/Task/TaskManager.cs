using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MyBox;
using Player;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Task
{
    public class TaskManager : MonoBehaviour
    {
        [SerializeField] private int maxTaskSimultaneously;
        [SerializeField, MinMaxRange(1, 30)] private RangedInt timeBeforeNextTask;
        [SerializeField] private List<ChaosTask> tasks;

        [Header("Debug")]
        [SerializeField] private bool isActive = true;

        [SerializeField, ReadOnly] private List<ChaosTask> nextTasks;
        [SerializeField, ReadOnly] private List<ChaosTask> currentTasks = new List<ChaosTask>();

        private bool isCycling;
        
        private void Awake()
        {
            if (tasks.Count == 0) return;
            
            RefreshNextTasks();
            if (isActive) StartCoroutine(TaskCycle());
        }
        
        private void RefreshNextTasks()
        {
            List<ChaosTask> tasksToAdd = new List<ChaosTask>(tasks);
            tasksToAdd.Shuffle();
            tasksToAdd.RemoveAll(task => currentTasks.Contains(task));
            nextTasks = new List<ChaosTask>(tasksToAdd);
        }

        private IEnumerator TaskCycle()
        {
            isCycling = true;
            int time = Random.Range(timeBeforeNextTask.Min, timeBeforeNextTask.Max+1);
            Debug.Log("Wait "+time+"s");
            yield return new WaitForSeconds(time);
            AddTask();

            if (currentTasks.Count < maxTaskSimultaneously) StartCoroutine(TaskCycle());
            else isCycling = false;
        }

        [ContextMenu("Add Task")]
        private void AddTask()
        {
            if (nextTasks.Count == 0) RefreshNextTasks();

            ChaosTask newChaosTask = nextTasks[0];
            if (currentTasks.Contains(newChaosTask)) return;

            Debug.Log("Add task");
            currentTasks.Add(newChaosTask);
            nextTasks.RemoveAt(0);

            newChaosTask.OnComplete = null;
            newChaosTask.OnComplete += CompleteTask;
            newChaosTask.gameObject.SetActive(true);
            newChaosTask.Init();
        }

        private void CompleteTask(ChaosTask chaosTask)
        {
            currentTasks.Remove(chaosTask);
            chaosTask.gameObject.SetActive(false);
            
            if (!isCycling) StartCoroutine(TaskCycle());
        }
    }
}
