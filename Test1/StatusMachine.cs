﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Test1.Command;

namespace Test1
{
    //begin: initial status
    //Setup: setup testset
    //Ready: setup done, can be exec/update testset
    //Dirty: setup failed and some resources are created, need cleanup
    //Running: running testset/updating testset
    //Cleanup: cleaning resource
    //Terminated: process is terminated
    public enum ProcessState
    {
        Begin,
        Setup,
        Ready,
        Dirty,
        Running,
        Cleanup,
        Terminated
    }

    //Setup: get setup command 
    //SetupFailWithResource: setup FAILed, some resources are already created
    //SetupFailWithoutResource: setup FAILed, no resource is created
    //SetupPass: setup PASSed
    //Exec: get exec testset command
    //Update: get update testset command
    //RunPass: exec/update/release PASSed
    //RunFail: exec/update/release FAILed
    //Release: get release command
    public enum ChangeCondition
    {
        Setup,
        SetupFailWithResource,
        SetupFailWithoutResource,
        SetupPass,
        Exec,
        Update,
        RunPass,
        RunFail,
        Release
    }

    public class StatusMachine
    {        
        class StateTransition
        {
            readonly ProcessState CurrentState;
            readonly ChangeCondition Condition;

            public StateTransition(ProcessState currentState, ChangeCondition condition)
            {
                CurrentState = currentState;
                Condition = condition;
            }
        }

        //status change rules
        Dictionary<StateTransition, ProcessState> transitions;
        public ProcessState CurrentState { get; private set; }

        //if current status need run command
        Dictionary<ProcessState, bool> StateNeedRun;

        #region construction

        public StatusMachine()
        {
            CurrentState = ProcessState.Begin;
            transitions = new Dictionary<StateTransition, ProcessState>
            {
                //begin only accept Setup command, other commands will not be executed
                {new StateTransition(ProcessState.Begin,ChangeCondition.Setup),ProcessState.Setup}, 
                {new StateTransition(ProcessState.Begin,ChangeCondition.Exec),ProcessState.Terminated},
                {new StateTransition(ProcessState.Begin,ChangeCondition.Release),ProcessState.Terminated},
                {new StateTransition(ProcessState.Begin,ChangeCondition.Update),ProcessState.Terminated},
                
                //setup only accept setup result&resouce status
                {new StateTransition(ProcessState.Setup,ChangeCondition.SetupPass),ProcessState.Ready},
                {new StateTransition(ProcessState.Setup,ChangeCondition.SetupFailWithoutResource),ProcessState.Terminated},
                {new StateTransition(ProcessState.Setup,ChangeCondition.SetupFailWithResource),ProcessState.Dirty},
                
                //ready:
                {new StateTransition(ProcessState.Ready,ChangeCondition.Update),ProcessState.Running},
                {new StateTransition(ProcessState.Ready,ChangeCondition.Exec),ProcessState.Running},
                {new StateTransition(ProcessState.Ready,ChangeCondition.Setup),ProcessState.Ready},
                {new StateTransition(ProcessState.Ready,ChangeCondition.Release),ProcessState.Cleanup},

                //running: can also run it again no matter run pass or fail
                {new StateTransition(ProcessState.Running,ChangeCondition.RunFail),ProcessState.Ready},
                {new StateTransition(ProcessState.Running,ChangeCondition.RunPass),ProcessState.Ready},
                
                //Dirty: only accept release command to do cleanup
                {new StateTransition(ProcessState.Dirty,ChangeCondition.Release),ProcessState.Cleanup},
                {new StateTransition(ProcessState.Dirty,ChangeCondition.Setup),ProcessState.Dirty},
                {new StateTransition(ProcessState.Dirty,ChangeCondition.Exec),ProcessState.Dirty},
                {new StateTransition(ProcessState.Dirty,ChangeCondition.Update),ProcessState.Dirty},

                //Cleanup: clean up fail will cause status dirty, pass will terminate the process
                {new StateTransition(ProcessState.Cleanup,ChangeCondition.RunPass),ProcessState.Terminated},
                {new StateTransition(ProcessState.Dirty,ChangeCondition.RunFail),ProcessState.Dirty}
            };


        }
        #endregion construction

        public ProcessState getNext(ChangeCondition condition)
        {
            StateTransition transition = new StateTransition(CurrentState, condition);
            ProcessState nextState;
            if (!transitions.TryGetValue(transition,out nextState))
            {
                throw new Exception("Invalid transition: " + CurrentState + " -> " + condition);
            }
            return nextState;
        }

        public ProcessState MoveNext(ChangeCondition condition)
        {
            CurrentState = getNext(condition);            
            return CurrentState;
        }

        //it will be called to map run result condition
        //only setup run result need check resource existance
        public ChangeCondition mapToCondition(ICommand command, bool isPassed, bool hasResource)
        {
            if (command == null) throw new Exception ("Cannot map condition. Command is null.");
            if (isPassed == null) throw new Exception("Cannot map condition. Run result is null.");
            //setup condition
            if (command.GetType() == typeof(SetupCommand))
            {
                if (hasResource == null) throw new Exception("Cannot map condition. Resource existance is null.");
                if (isPassed == true) return ChangeCondition.SetupPass;
                if (hasResource == true) return ChangeCondition.SetupFailWithResource;
                return ChangeCondition.SetupFailWithoutResource;
            }
            //other command contition
            if (isPassed == true)
            {
                return ChangeCondition.RunPass;
            } else {
                return ChangeCondition.RunFail;
            }
        }
        
        //it will be called to map input command condition
        //Release command -> Release
        //Setup command -> Setup
        //Update command -> Update
        //Exec command - > Exec
        public ChangeCondition mapToCondition(ICommand command)
        {
            if (command == null) throw new Exception("Cannot map condition. Command is null.");
            Type commandType = command.GetType();
            if (commandType == typeof(SetupCommand))
            {
                return ChangeCondition.Setup;
            }
            if (commandType == typeof(ReleaseCommand))
            {
                return ChangeCondition.Release;
            }

            if (commandType == typeof(UpdateCommand))
            {
                return ChangeCondition.Update;
            }
            else 
            {
                //exec command
                return ChangeCondition.Exec;
            }

        }

        

       

    }
}