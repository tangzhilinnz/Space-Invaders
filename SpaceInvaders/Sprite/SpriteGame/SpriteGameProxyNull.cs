//-----------------------------------------------------------------------------
// Copyright 2024, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 

using System;
using System.Diagnostics;

namespace SE456
{
    class SpriteGameProxyNull : SpriteGameProxy
    {
        public SpriteGameProxyNull()
            : base(SpriteGameProxy.Name.NullObject)
        {

        }

        public override void Render()
        {
            // do nothing
        }

        public override void Update()
        {
            // do nothing
        }
    }
}

// --- End of File ---
