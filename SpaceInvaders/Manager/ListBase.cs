//-----------------------------------------------------------------------------
// Copyright 2025, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 

using System;
using System.Diagnostics;

namespace SE456
{
    public abstract class ListBase
    {
        abstract public void AddToFront(NodeBase pNode);
        abstract public void Remove(NodeBase pNode);
        abstract public NodeBase RemoveFromFront();
        abstract public Iterator GetIterator();

        virtual public void InsertWithPriority(NodeBase pNode)
        {
            Debug.Assert(false);
        }
    }
}

// --- End of File ---
