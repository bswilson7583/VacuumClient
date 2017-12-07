namespace IntelligentVacuum.Agent
{
    using System;
    using System.Collections.Generic;
    using Environments;

    public class Agent
    {
        private Sensor _sensor;

        private Stack<AgentAction> _plan;

        public Agent(Sensor sensor)
        {
            this._sensor = sensor;
            this._plan = new Stack<AgentAction>();
        }

        public AgentAction DecideAction(Room room)
        {
            AgentAction action;
            //If the plan is empty
            if (this._plan.TryPop(out action))
            {
                BuildPlan(room);
                // if the plan is stil empty
                if (!this._plan.TryPop(out action))
                {
                    action = AgentAction.NONE;
                }
            }
            

            return AgentAction.NONE;
        }

        public void BuildPlan(Room room)
        {
              /*
            planning phase
            exploreed = []
            frontier = [currentNode]
            do
                if frontier is empty
                return Action.NONE
                node = frontier.dequeue
                newNodes = explore (node)
                remove any explored nodes from NewNodes
                explored.add(node)
                frontier.enqueue(newNodes)
                while (node is not dirty)
                plan = [Action.CLEAN]
                while node has a parent
                plan.push(node.action)
                node = node.parent
                return plan[0]
                */

             // Search for a goal node 
            var explored = new HashSet<Room>();
            var frontier = new Queue<GraphNode>();
            var node = new GraphNode(room, null, AgentAction.NONE);
            frontier.Enqueue(node);

            do 
            {   
                // if frontier is empty 
                if (!frontier.TryDequeue(out node))
                {
                    return;
                }
            List<GraphNode> newNodes = Explore(node);
            explored.Add(node.Room);
            newNodes.RemoveAll(n => explored.Contains(n.Room));
            foreach(var newNode in newNodes)
            {
                frontier.Enqueue(newNode);
            }
            
        // trace our steps from the goal node to build the plan check for if the the room is not 
            }while (!node.Room.IsDirty || !node.Room.IsLocked);
                _plan.Clear();
                _plan.Push(AgentAction.CLEAN);
                while (node.Parent != null)
                {
                    _plan.Push (node.Action);
                    node = node.Parent;
                }
        }

        private List<GraphNode> Explore(GraphNode node)
        {
            List<GraphNode> discovered = new List<GraphNode>();
            AgentAction[] moveAction = new AgentAction[]
             {AgentAction.MOVE_DOWN, AgentAction.MOVE_LEFT, AgentAction.MOVE_RIGHT, AgentAction.MOVE_UP};

             
        if (!node.Room.IsLocked)
        { foreach (var action in moveAction)
             {
                 GraphNode newNode = Transition(node, action);
                 if (newNode != null)
                 {
                     discovered.Add(newNode);
                 }
             }
        }else
        {
            // room is locked and can't enter 
        }
                    
             return discovered;
        }

    private GraphNode Transition(GraphNode node, AgentAction action)
    {
        Room newRoom = null;
        Room currentRoom = node.Room;

    
        switch(action)
        {
            case AgentAction.MOVE_DOWN:
                newRoom = _sensor.SenseRoom(currentRoom.XAxis, currentRoom.YAxis +1);
                break;
            case AgentAction.MOVE_UP:
                newRoom = _sensor.SenseRoom(currentRoom.XAxis, currentRoom.YAxis -1);
                break;
            case AgentAction.MOVE_RIGHT:
                newRoom = _sensor.SenseRoom(currentRoom.XAxis +1, currentRoom.YAxis );
                break;
            case AgentAction.MOVE_LEFT:
                newRoom = _sensor.SenseRoom(currentRoom.XAxis -1, currentRoom.YAxis );
                break;
           
        }
        GraphNode newNode = null;
        if (newNode != null)
        {
            newNode = new GraphNode(newRoom, node, action);
        }
        return newNode;
    }


    }
}