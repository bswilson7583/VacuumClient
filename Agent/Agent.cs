namespace IntelligentVacuum.Agent
{
    using System;
    using System.Collections.Generic;
    using Environments;
    using System.Runtime.ExceptionServices;

    public class Agent
    {
        public Agent()
        {
        }

        public AgentAction DecideAction(Room room)
        {

           
            AgentAction action;


            // SENSE
            bool isDirty = room.IsDirty;
            // looking for a random number 
            Random random = new Random();
            //the random number will be between 1-3 
            int randNumber = random.Next(1, 3);

            // PLAN 
            if (!isDirty)
            {
                switch (randNumber)
                {
                    case 1:
                        action = AgentAction.MOVE_DOWN;
                        break;
                    case 2:
                        action = AgentAction.MOVE_LEFT;
                        break;
                    case 3:
                        action = AgentAction.MOVE_RIGHT;
                        break;
                    
                    default:
                        action = AgentAction.NONE;
                        break;
                }

            }
            else
            {
                action = AgentAction.CLEAN;

            }
            // ACT
            return action;
        }
    }
}