using System;
using System.Collections.Generic;
using UnityEngine;
using SimpleAI.Logger;

namespace SimpleAI.Spatial
{
    public class SPAABB
    {
        public float X = 0.0f;

        public float Y = 0.0f;

        public float Width = 0.0f;

        public float Height = 0.0f;

        public SPAABB(float x, float y, float width, float height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public bool IsIntersect(ref SPAABB val)
        {
            bool result = !(Math.Abs(X - val.X) >= (Width + val.Width) * 0.5f ||
                Math.Abs(Y - val.Y) >= (Height + val.Height) * 0.5f);

            return result;
        }

        public bool ContainPoint(float x, float y)
        {
            return !(x < X - Width * 0.5f ||
                x > X + Width * 0.5f ||
                y < Y - Height * 0.5f ||
                y > Y + Height * 0.5f);
        }

        public bool ContainPoint(ref Vector3 pos)
        {
            return this.ContainPoint(pos.x, pos.y);
        }
    }

    /// <summary>
    /// Single spatial structure at one time.
    /// </summary>
    public sealed class SpatialManager
    {
        private static readonly SpatialManager TheInstance = 
            new SpatialManager();

        public static SpatialManager Instance
        {
            get
            {
                return TheInstance;
            }
        }

        private SpatialManager() 
        {
            Init(0, 0, 100, 100); // test
        }

        static SpatialManager() { }

        private QuadTree QTree = null;

        Dictionary<int, QuadTree> SubNodes = new Dictionary<int, QuadTree>();

        public void Init(float x, float y, float width, float height)
        {
            QTree = new QuadTree(x, y, width, height);
            AddTree(QTree);
        }

        public void AddTree(QuadTree tree)
        {
            SubNodes.Add(tree.ID, tree);
        }

        public void RemoveNode(ref SpatialFruitNode node)
        {
            if (!System.Object.ReferenceEquals(node, null))
            {
                QuadTree tree = GetTreeByID(node.SpatialNodeID);
                if (tree != null) tree.RemoveNode(node);
            }
        }

        public QuadTree GetTreeByID(int id)
        {
            if (SubNodes.ContainsKey(id))
            {
                return SubNodes[id];
            }

            return null;
        }

        public bool AddNode(SpatialFruitNode node)
        {
            if (!System.Object.ReferenceEquals(node, null))
                return QTree.AddNode(node);

            return false;
        }

        public void QueryRange(ref SPAABB range, 
                               ref List<SpatialFruitNode> nodes)
        {
            QTree.QueryRange(ref range, ref nodes);
        }

        public void HandleNodePosChanged(SpatialFruitNode node)
        {
            QuadTree tree = GetTreeByID(node.SpatialNodeID);
            if (!System.Object.ReferenceEquals(tree, null))
            {
                if (!tree.IsNodeInrange(ref node))
                {
                    tree.RemoveNode(node);
                    AddNode(node);
                }
            }
        }
    }

    public class QuadTree
    {
        private static int NextID = 0;

        public int ID
        {
            set;get;
        }

        SPAABB Boundary = null;

        /// <summary>
        /// The max capacity of the spatial nodes this tree node can hold if
        /// it's not arrive the minimum bound size.
        /// </summary>
        private int MaxCapacity = 1;

        public int Capacity
        {
            get
            {
                return MaxCapacity;
            }
            set
            {
                MaxCapacity = value;
            }
        }

        public float MinRadius = 10.0f;

        private int CurNodeCount = 0;

        private bool IsSubOpened = false;

        private List<SpatialFruitNode> Nodes = new List<SpatialFruitNode>();

        private List<QuadTree> SubTrees = new List<QuadTree>();

        private readonly int SubTreeCount = 4;

        public QuadTree(float x, float y, float width, float height)
        {
            Boundary = new SPAABB(x, y , width, height);
            ID = NextID;
            NextID++;
        }

        /// <summary>
        /// Attach this instance.
        /// </summary>
        public void Attach()
        {
            SpatialManager.Instance.AddTree(this);
        }

        /// <summary>
        /// Subs the divide.
        /// </summary>
        private void SubDivide()
        {
            float subwidth = Boundary.Width * 0.5f;
            float subheight = Boundary.Height * 0.5f;
            float subwoffset = subwidth * 0.5f;
            float subhoffset = subheight * 0.5f;
            var sub1 = new QuadTree(Boundary.X - subwoffset, 
                Boundary.Y - subhoffset, subwidth, subheight);
            SubTrees.Add(sub1);

            var sub2 = new QuadTree(Boundary.X - subwoffset, 
                Boundary.Y + subhoffset, subwidth, subheight);
            SubTrees.Add(sub2);

            var sub3 = new QuadTree(Boundary.X + subwoffset, 
                Boundary.Y - subhoffset, subwidth, subheight);
            SubTrees.Add(sub3);

            var sub4 = new QuadTree(Boundary.X + subwoffset, 
                Boundary.Y + subhoffset, subwidth, subheight);
            SubTrees.Add(sub4);

            sub1.Attach();
            sub2.Attach();
            sub3.Attach();
            sub4.Attach();

            TinyLogger.Instance.DebugLog("$$$ SubDivide ->");
            for (var i = 0; i < SubTreeCount; i++)
            {
                TinyLogger.Instance.DebugLog(
                    String.Format("$$$ {0}, {1}, {2}, {3}", 
                    SubTrees[i].Boundary.X,
                    SubTrees[i].Boundary.Y,
                    SubTrees[i].Boundary.Width,
                    SubTrees[i].Boundary.Height));
            }

            IsSubOpened = true;
        }

        /// <summary>
        /// Clear this instance.
        /// </summary>
        public void Clear()
        {
            Nodes.Clear();
        }

        /// <summary>
        /// Ises the node inrange.
        /// </summary>
        /// <returns><c>true</c>, if node inrange was ised, 
        /// <c>false</c> otherwise.</returns>
        /// <param name="node">Node.</param>
        public bool IsNodeInrange(ref SpatialFruitNode node)
        {
            return Boundary.ContainPoint(ref node.Position);
        }

        /// <summary>
        /// Nots the should divide.
        /// </summary>
        /// <returns><c>true</c>, if should divide was noted, 
        /// <c>false</c> otherwise.</returns>
        private bool NotShouldDivide()
        {
            return (Boundary.Width <= MinRadius && 
                    Boundary.Height <= MinRadius) ||
                    CurNodeCount < MaxCapacity;
        }

        /// <summary>
        /// Adds the node.
        /// </summary>
        /// <returns><c>true</c>, if node was added, 
        /// <c>false</c> otherwise.</returns>
        /// <param name="node">Node.</param>
        public bool AddNode(SpatialFruitNode node)
        {
            if (!System.Object.ReferenceEquals(node, null) && IsNodeInrange(ref node))
            {
                if (NotShouldDivide() && !IsSubOpened)
                {
                    Nodes.Add(node);
                    node.SpatialNodeID = ID;
                    CurNodeCount++;

                    TinyLogger.Instance.DebugLog(
                        String.Format("$ add leaf node {0}, {1}, " +
                        "count: {2}, MaxCapacity {3}",
                        node.Position.x, node.Position.y, 
                        CurNodeCount, MaxCapacity));

                    TinyLogger.Instance.DebugLog(
                        String.Format("$ in leaf range {0}" + 
                        ", {1}, {2}, {3}, {4}",
                        Boundary.X,
                        Boundary.Y,
                        Boundary.Width,
                        Boundary.Height,
                        Nodes.Count));

                    return true;
                }

                if (!IsSubOpened)
                {
                    SubDivide();

                    for (int j = 0; j < Nodes.Count; j++)
                    {
                        var qtnode = Nodes[j];
                        for (var i = 0; i < SubTrees.Count; i++)
                        {
                            var result = SubTrees[i].AddNode(qtnode);

                            if (result)
                            {
                                TinyLogger.Instance.DebugLog(
                                    String.Format("$$$ add leaf node pos {0}, {1}", 
                                    qtnode.Position.x, qtnode.Position.y));

                                TinyLogger.Instance.DebugLog(
                                    String.Format("$$$ add leaf range {0}, " + 
                                    "{1}, {2}, {3}, {4}",
                                    SubTrees[i].Boundary.X,
                                    SubTrees[i].Boundary.Y,
                                    SubTrees[i].Boundary.Width,
                                    SubTrees[i].Boundary.Height,
                                    SubTrees[i].Nodes.Count));

                                break;
                            }
                        }
                    }

                    Nodes.Clear();
                    CurNodeCount = 0;
                }

                for (var i = 0; i < SubTrees.Count; i++)
                {
                    if (SubTrees[i].AddNode(node))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Removes the node.
        /// </summary>
        /// <param name="node">Node.</param>
        public void RemoveNode(SpatialFruitNode node)
        {
            if (!System.Object.ReferenceEquals(node, null))
            {
                Nodes.Remove(node);
            }
        }

        /// <summary>
        /// Queries the spatial nodes in the given range.
        /// </summary>
        /// <param name="range">Range.</param>
        /// <param name="nodes">Nodes.</param>
        public void QueryRange(ref SPAABB range, ref List<SpatialFruitNode> nodes)
        {
            if (nodes != null && !Boundary.IsIntersect(ref range))
            {
                return;
            }

            if (!IsSubOpened)
            {
                for (int i = 0; i < Nodes.Count; i ++)
                {
                    nodes.Add(Nodes[i]);
                    TinyLogger.Instance.DebugLog(
                        string.Format("$$$ leaf node pos {0}, {1}", 
                            Nodes[i].Position.x, Nodes[i].Position.y));
                }

                TinyLogger.Instance.DebugLog(
                        string.Format("$$$$$$$$$$$$$$$$ leaf range {0}, {1}, " + 
                        "{2}, {3}, {4} , {5} $$$$$$$$$$$$$$$$", 
                        ID, Boundary.X, Boundary.Y, 
                        Boundary.Width, Boundary.Height, Nodes.Count));
                                     
                return;
            }

            for (int i = 0; i < SubTreeCount; i++)
            {
                SubTrees[i].QueryRange(ref range, ref nodes);
            }
        }
    }
}