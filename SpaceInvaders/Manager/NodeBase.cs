//-----------------------------------------------------------------------------
// Copyright 2025, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 

using System;
using System.Diagnostics;

namespace SE456
{
    abstract public class NodeBase
    {
		// --------------------------------------
		// Public contracts used in
		//      ManagerBase()
		// --------------------------------------
		abstract public Enum GetName();

		// --------------------------------------
		// Protected contracts used in
		//      Wash() 
		//      Detach()
		//      Dump()
		// --------------------------------------
		abstract public void Wash();
		abstract public void Dump();
		abstract public void Detach();

		// --------------------------------------
		// Default functionality...
		//      can be overrided in derived
		// --------------------------------------
		virtual protected void baseWash()
		{
			// top level
		}

		virtual protected void baseDump()
		{
			// top level
		}

		virtual public bool Compare(NodeBase _pNodeBaseB)
        {
            // This is used in baseFind() 
            Debug.Assert(_pNodeBaseB != null);
            bool status = false;

            // Why doesn't GetName() work without GetHashCode?
            // Debug.WriteLine("cmp {0} {1} \n", this.GetName().GetHashCode(), pNodeBaseB.GetName().GetHashCode());
            if (this.GetName().GetHashCode() == _pNodeBaseB.GetName().GetHashCode())
            {
                status = true;
            }

            return status;
        }

        virtual public bool LessThanByPriority(NodeBase _pNodeBaseB)
        {
            // With this virtual function, we can customize priority rules
            // in the concrete class by overriding it, allowing the use of
            // different data types as priority values.
            Debug.Assert(false);
            return false;
        }

    }
}

// --- End of File ---

