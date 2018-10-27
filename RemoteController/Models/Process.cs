﻿namespace RemoteController.Models
{
    public class Process
    {
        public string Name { get; }
        public string ExeName { get; }

        public int Pid { get; }
        public Process(string name, string exeName, int pid)
        {
            Name = name;
            ExeName = exeName;
            Pid = pid;
        }
    }
}
