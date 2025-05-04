using System;
using System.Collections.Generic;

namespace Snake
{
    class Snake
    {
        public List<SnakeNode> nodes;
        public Vector2 pos;
        public Vector2 vel;
        public int speed;

        public Snake()
        {
            nodes = new List<SnakeNode>();
            pos = Vector2.ZERO;
            vel = Vector2.DOWN;
            speed = 1;
        }

        public void Turn(Vector2 direction)
        {
            if (Vector2.Equals(direction, vel.Inverse())) return;
            vel = direction;
        }

        public void Move()
        {
            for (int i = nodes.Count - 1; i >= 0; --i)
            {
                if (i == 0)
                    nodes[i].pos = pos;
                else
                    nodes[i].pos = nodes[i-1].pos;
            }

            pos = Vector2.Add(pos, Vector2.Mul(vel, speed));
            pos.x = (pos.x + Program.VIEW_WIDTH) % Program.VIEW_WIDTH;
            pos.y = (pos.y + Program.VIEW_HEIGHT) % Program.VIEW_HEIGHT;

        }

        public void AddNode()
        {
            Vector2 node_pos = Vector2.ZERO;


            if (nodes.Count > 1)
            {
                node_pos = Vector2.Sub(
                    nodes[nodes.Count-1].pos,
                    Vector2.Sub(
                        nodes[nodes.Count-2].pos,
                        nodes[nodes.Count-1].pos
                    )
                );
            }
            else if (nodes.Count > 0) 
            {
                node_pos = Vector2.Sub(
                    nodes[nodes.Count-1].pos,
                    vel
                );
            }
            else
            {
                node_pos = Vector2.Sub(pos, vel);
            }

            node_pos.x = (node_pos.x + Program.VIEW_WIDTH) % Program.VIEW_WIDTH;
            node_pos.y = (node_pos.y + Program.VIEW_HEIGHT) % Program.VIEW_HEIGHT;

            nodes.Add(new SnakeNode(node_pos));
        }
    }
}
