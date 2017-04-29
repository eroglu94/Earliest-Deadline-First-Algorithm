using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;
using System.Windows.Shapes;

namespace Operating_Project
{
    class Scheduling
    {
        // PROCESS STRUCT
        public struct Process
        {
            public string Name { get; set; }
            public int ArrivalTime { get; set; }
            public int BurstTime { get; set; }
            public int Deadline { get; set; }
            public int TotalExecutionTime { get; set; }
            public bool isFinished { get; set; }       //default false
            public bool isDeadlineFailed { get; set; } //default false
            public Brush Color { get; set; }
            public int TotalProcess;
        }

        //TIMELINE OF PROCESSES
        public struct Timeline
        {
            public Process Process { get; set; }
            public int TotalBurstTime { get; set; }
        }



        public void SetColors()
        {

        }

        public Process FindNextProcessToExecute(Process[] _process, int currentTime)
        {
            int earliestDeadlineValue = 1000; //Referance value. Value is not important. Its just need to be a big number
            var nextProcess = new Process();  //Process has earliest deadline

            foreach (var item in _process)
            {
                if (earliestDeadlineValue > item.Deadline - currentTime && item.isFinished == false && item.Deadline != 0)
                {
                    if (item.ArrivalTime <= currentTime)
                    {
                        earliestDeadlineValue = item.Deadline - currentTime;
                        nextProcess = item;

                        if (item.Deadline < currentTime) // Check and Mark for Deadline Failure
                        {
                            nextProcess.isDeadlineFailed = true;
                        }
                    }
                }
            }

            //Marking The Last Item of Timeline
            var lastItemChecker = 0;
            foreach (var item in _process)
            {
                if (item.isFinished == true && item.Name != null)
                {
                    lastItemChecker++;
                }
            }

            if (lastItemChecker == _process[0].TotalProcess)
            {
                nextProcess.Name = "Finished";
            }



            return nextProcess;
        }

        public Process[] CheckandMarkProcesses(Process[] _process, Timeline[] timeline)
        {
            //Update TotalExecution Times of Procesesses
            foreach (var item in _process)
            {
                if (item.Name != null && item.isFinished != true)
                {
                    int index = Array.FindIndex(_process, xx => xx.Name == item.Name);

                    _process[index].TotalExecutionTime = timeline.Count(p => p.Process.Name == item.Name);
                    if (_process[index].TotalExecutionTime >= _process[index].BurstTime)
                    {
                        _process[index].isFinished = true;
                    }

                }
            }
            return _process;
        }

        public Timeline[] CalculateTimeLine(Process[] _process)
        {
            int totalBurstTime = 0;
            foreach (var item in _process) // Creating Timeline with reference of BurstTime
            {
                totalBurstTime += item.BurstTime;
                
            }
            totalBurstTime = 50; //***BOŞ GEÇİRİLEN ZAMANIDA HESABA KATMAK İÇİN ELLE DEĞER VERDİK******

            var timeLine = new Scheduling.Timeline[totalBurstTime]; //Timeline Limit should be equal to TotalBurstTime
            timeLine[0].TotalBurstTime = totalBurstTime;            //Record Total Burst Time

            for (int currentTime = 0; currentTime < totalBurstTime; currentTime++)
            {

                timeLine[currentTime].Process = FindNextProcessToExecute(_process, currentTime);
                _process = CheckandMarkProcesses(_process, timeLine);
            }

            //TRY
            //string sıra = "";
            //int count = 0;
            //foreach (var item in timeLine)
            //{
            //    sıra = sıra + "  " + "(" + count + ")" + item.Process.Name;
            //    count++;
            //}


            //MessageBox.Show(sıra);

            return timeLine;

        }



    }
}
