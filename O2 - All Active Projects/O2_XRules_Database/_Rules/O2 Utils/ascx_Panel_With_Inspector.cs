﻿using System;
using System.Windows.Forms;
using System.Collections.Generic;
using O2.Kernel.ExtensionMethods;
using O2.DotNetWrappers.ExtensionMethods;
using O2.Views.ASCX.classes.MainGUI;
using O2.XRules.Database.O2Utils;
using O2.XRules.Database.ExtensionMethods;

namespace O2.XRules.Database._Rules.O2_Utils
{
    public class ascx_Panel_With_Inspector : Control
    {
        public Panel panel;
        public ascx_Simple_Script_Editor inspector;

        public static void runControl()
		{		
		 	O2Gui.load<ascx_Panel_With_Inspector>("Panel With Inspector"); 		 
		}

        public ascx_Panel_With_Inspector()
		{						
			this.Width = 600;
			this.Height = 400;
		
			var controls = this.add_1x1("Panel","Inspector",true,200);
			
			panel = controls[0].add_Panel();
			
			//graph.testGraph();			
            inspector = controls[1].add_Script();

            inspector.Code = "panel.clear();".line() + 
                             "var textBox = panel.add_TextBox(true);".line() + 
                             "textBox.set_Text(\"hello world\");";
            inspector.InvocationParameters.Add("panel", panel);
            inspector.InvocationParameters.Add("inspector", inspector);
            inspector.enableCodeComplete();
        }
    }

}
