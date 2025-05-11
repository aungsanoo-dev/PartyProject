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
        public Views.frm_LogIn frmLogIn; // Declare the View

        public CtrlFrmLogIn(Views.frm_LogIn logInForm)
        {
            frmLogIn = logInForm; // Create the View
        }

        bool isPasswordShown = false;

        //public void EyeToggle()
        //{
        //    // Set initial icon and masking state
        //    frmLogIn.btnEye.Text = "👁️";
        //    frmLogIn.txtPassword.UseSystemPasswordChar = true;

        //    // Attach the toggle event
        //    frmLogIn.btnEye.Click += (s, e) =>
        //    {
        //        isPasswordShown = !isPasswordShown;

        //        frmLogIn.txtPassword.UseSystemPasswordChar = !isPasswordShown;

        //        frmLogIn.btnEye.Text = isPasswordShown ? "🚫" : "👁️";
        //    };
        //}

        public void EyeToggle()
        {
            if (frmLogIn.btnEye.Text == "👁️")
            {
                frmLogIn.txtPassword.PasswordChar = '\0';
                frmLogIn.btnEye.Text = "🚫";
            }
            else
            {
                frmLogIn.txtPassword.PasswordChar = '*';
                frmLogIn.btnEye.Text = "👁️";
            }
        }
    }
}
