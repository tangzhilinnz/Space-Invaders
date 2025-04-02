//-----------------------------------------------------------------------------
// Copyright 2025, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 

using System;
using System.Diagnostics;

namespace SE456
{
    public class InputTest
    {
        public static void KeyboardTest()
        {
	        // Quick and dirty test, if these work the rest do.
	        // ---> try a,s,d,<SPACE> keys
	        String a = "";
            String b = "";
            String c = "";
            String d = "";

	        if (Azul.Keyboard.KeyPressed( Azul.AZUL_KEY.KEY_RIGHT))
	        {
		        a = " RIGHT_ARROW";
	        }

	        if (Azul.Keyboard.KeyPressed( Azul.AZUL_KEY.KEY_LEFT))
	        {
		        b = " LEFT_ARROW";
	        }

	        if (Azul.Keyboard.KeyPressed( Azul.AZUL_KEY.KEY_Q))
	        {
		        c = " Q";
	        }

	        if (Azul.Keyboard.KeyPressed( Azul.AZUL_KEY.KEY_SPACE))
	        {
		      d = " <SPACE>";
	        }

	        //String total = a + b + c + d;
            Console.WriteLine("Key: {0}{1}{2}{3} ",a,b,c,d);
	
        }

    }
}

// --- End of File ---
