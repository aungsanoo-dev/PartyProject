using F21Party.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F21Party.Controllers
{
    internal class CtrlFrmLogIn
    {
        public Views.frm_LogIn obj_frmLogIn; // Declare the View

        public CtrlFrmLogIn(Views.frm_LogIn _LogIn)
        {
            obj_frmLogIn = _LogIn; // Create the View
        }

        bool isPasswordShown = false;

        public void EyeToggle()
        {
            // Set initial icon and masking state
            obj_frmLogIn.btnEye.Text = "👁️";
            obj_frmLogIn.txtPassword.UseSystemPasswordChar = true;

            // Attach the toggle event
            obj_frmLogIn.btnEye.Click += (s, e) =>
            {
                isPasswordShown = !isPasswordShown;

                obj_frmLogIn.txtPassword.UseSystemPasswordChar = !isPasswordShown;

                obj_frmLogIn.btnEye.Text = isPasswordShown ? "🚫" : "👁️";
            };
        }
    }
}
