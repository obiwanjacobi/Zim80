using Jacobi.Zim80.Diagnostics.DgmlModel;
using System.Collections.Generic;

namespace Jacobi.Zim80.Diagnostics
{
    internal class DgmlModelBuilder
    {
        public DgmlModelBuilder(string title)
        {
            New(title);
        }

        public DgmlModelBuilder(DirectedGraph graph)
        {
            DirectedGraph = graph;
            EnsureValid(DirectedGraph);
        }

        public DirectedGraph DirectedGraph { get; protected set; }

        public void New(string title)
        {
            DirectedGraph = new DirectedGraph()
            {
                Title = title
            };

            EnsureValid(DirectedGraph);
        }

        protected void EnsureValid(DirectedGraph graph)
        {
            if (graph.Categories == null) graph.Categories = new DirectedGraphCategory[0];
            if (graph.IdentifierAliases == null) graph.IdentifierAliases = new DirectedGraphAlias[0];
            if (graph.Nodes == null) graph.Nodes = new DirectedGraphNode[0];
            if (graph.Links == null) graph.Links = new DirectedGraphLink[0];
            if (graph.Paths == null) graph.Paths = new DirectedGraphPath[0];
            if (graph.Properties == null) graph.Properties = new DirectedGraphProperty[0];
            if (graph.QualifiedNames == null) graph.QualifiedNames = new DirectedGraphName[0];
            if (graph.Styles == null) graph.Styles = new DirectedGraphStyle[0];
        }

        public DirectedGraphNode AddNode(string id)
        {
            var node = new DirectedGraphNode()
            {
                Id = id
            };

            AddNode(node);

            return node;
        }

        public void AddNode(DirectedGraphNode node)
        {
            var nodes = new List<DirectedGraphNode>(DirectedGraph.Nodes);

            nodes.Add(node);

            DirectedGraph.Nodes = nodes.ToArray();
        }

        public DirectedGraphLink AddLink(DirectedGraphNode source, DirectedGraphNode target)
        {
            if (source == null || target == null) return null;

            return AddLink(source.Id, target.Id);
        }

        public DirectedGraphLink AddLink(string source, string target)
        {
            var link = new DirectedGraphLink()
            {
                Source = source,
                Target = target,
            };

            AddLink(link);

            return link;
        }

        public void AddLink(DirectedGraphLink link)
        {
            var links = new List<DirectedGraphLink>(DirectedGraph.Links);

            links.Add(link);

            DirectedGraph.Links = links.ToArray();
        }

        public DirectedGraphCategory AddCategory(string id)
        {
            var cat = new DirectedGraphCategory()
            {
                Id = id
            };

            AddCategory(cat);

            return cat;
        }

        public void AddCategory(DirectedGraphCategory category)
        {
            var cats = new List<DirectedGraphCategory>(DirectedGraph.Categories);

            cats.Add(category);

            DirectedGraph.Categories = cats.ToArray();
        }
    }
}
