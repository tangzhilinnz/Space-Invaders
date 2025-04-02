//-----------------------------------------------------------------------------
// Copyright 2025, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 

using System;
using System.Diagnostics;

namespace SE456
{
    abstract public class Iterator
    {
        // -------------------------------------------------------------------
        // Next() - Advances the iterator to the next item after current item
        //          If valid returns the next item
        //          If the item is not valid, return null
        //          This function advances the iterator
        // -------------------------------------------------------------------
        abstract public NodeBase Next();

        // -------------------------------------------------------------------
        // IsDone() - (sometimes called hasNext or hasMore)
        //      Returns false if an iterator has more elements, i.e. Current() is valid
        //      Returns true otherwise ( Note: Current() is undefined, when IsDone() is true )
        // -------------------------------------------------------------------
        abstract public bool IsDone();

        // -------------------------------------------------------------------
        // First() - Returns the first element
        //           Resets the iterator state
        //           Does not advance the iterator
        // -------------------------------------------------------------------
        abstract public NodeBase First();

        // -------------------------------------------------------------------
        // Current() - Returns the current item the iterator is pointing to
        //             Does not advance the iterator
        // -------------------------------------------------------------------
        abstract public NodeBase Current();

        // -------------------------------------------------------------------
        // Erase() - Remove a node while iterating
        // -------------------------------------------------------------------
        virtual public void Erase(ManBase _pMan)
        {
			Debug.Assert(false);
        }
    }

}

// --- End of File ---
