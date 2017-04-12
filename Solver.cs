using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraphProject
{
    class Solver<T>
    {
        class Meta
        {
            public enum VisitState {undiscorcered, frontier, explored };
            public VisitState state;
            public Meta() { state = VisitState.undiscorcered; }
        };

        public Graph<T> graph;
        private Meta[] metadata;
        private Stack<Graph<T>.Node> frontier;

        public void init(T start, Graph<T>.FindDelegate searcher)
        {
            metadata = new Meta[graph.nodes.Count];
            frontier = new Stack<Graph<T>.Node>();

            var snode = graph.FindNode(start, searcher, 3);
            for (int i = 0; i < metadata.Length; ++i)
                metadata[i] = new Meta();

            metadata[snode.uid].state = Meta.VisitState.frontier;
            frontier.Push(snode);
        }

        public bool step()
        {
            var current = frontier.Pop();

            Console.Write(frontier.Peek().uid);
            return frontier.Count != 0;
        }
    }
}
