using Appccelerate.StateMachine.Machine;
using System;
using System.Collections.Generic;

namespace InterviewAssignment
{

    /// <summary>
    /// Provides way to customize state reporting.
    /// 
    /// By default you cannot get container of states from Appccelerate state machine.
    /// This class can be used for that. Also it has StateToString method to enable
    /// printing hierarchical states.
    /// </summary>
    /// <typeparam name="TState"></typeparam>
    /// <typeparam name="TEvent"></typeparam>
    public class CustomStateMachineReporter<TState, TEvent> : IStateMachineReport<TState, TEvent>
            where TState : IComparable
    where TEvent : IComparable

    {
        IEnumerable<IState<TState, TEvent>> myStates;

        public IEnumerable<IState<TState, TEvent>> States
        {
            get
            {
                return myStates;
            }
        }

        public void Report(string name, IEnumerable<IState<TState, TEvent>> states, Initializable<TState> initialStateId)
        {
            myStates = states;
        }

        public string StateToString(TState state, string separator = ".")
        {


            //Your assignment is here!
            //Tip: You find state machine hierarchy on States property (or myStates field). 
            //You should go through the states and print that on what hierarchy path the current state
            //is found. So if state is Initializing this method should return "Down.Initializing" because
            //"Initializing" is substate of "Down".

            //Recursion algorithm allows to scale the application adding robot states without having to rewrite foreach cycles, in ideal world we should not care how many cycles there could be nested

            string currentState = "";
            
            foreach (var robotState in States) //Loop through all available states
            {
                if (state.ToString() == robotState.Id.ToString() && robotState.Level != 1) //if current state is equal nested, there are SuperStates then we need to go deepere
                {
                    currentState += StateToString(robotState.SuperState.Id, separator) + separator; //this is how we go deeper, call the StateToString inside itself on the SuperState.Id, save result to currentState
                    //Console.WriteLine(currentState + state.ToString()); //debugging feature, uncomment to debug
                    return currentState + state.ToString(); //return the accumulated currentState variable with all SuperStates plus the current state that is of the lowest level
                }
                if (state.ToString() == robotState.Id.ToString() && robotState.Level == 1) //if the state that we are looking for is the highest level 
                {
                    currentState = state.ToString(); //get the current state
                    return currentState; //and return current state without separators or nested states
                }
            }

            throw new NotImplementedException();
        }
    }
}
