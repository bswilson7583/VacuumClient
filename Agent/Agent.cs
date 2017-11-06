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
            Random random = new Random();
            int randNumber = random.Next(1, 9);

            // PLAN 
            if (!isDirty)
            {
                switch (randNumber)
                {
                    case 1:
                        action = AgentAction.NONE;
                        break;
                    case 2:
                        action = AgentAction.LOOK_LEFT;
                        break;
                    case 3:
                        action = AgentAction.LOOK_RIGHT;
                        break;
                    case 4:
                        action = AgentAction.LOOK_UP;
                        break;
                    case 5:
                        action = AgentAction.MOVE_DOWN;
                        break;
                    case 6:
                        action = AgentAction.MOVE_LEFT;
                        break;
                    case 7:
                        action = AgentAction.MOVE_RIGHT;
                        break;
                    case 8:
                        action = AgentAction.MOVE_UP;
                        break;
                    case 9:
                        action = AgentAction.LOOK_DOWN;
                        break;

                    default:
                        action = AgentAction.MOVE_RIGHT;
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