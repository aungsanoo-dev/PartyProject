using F21Party.Views;
using F21Party.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F21Party.Controllers
{
    internal class CtrlFrmLogIn
    {
        private readonly frm_LogIn _frmLogIn; // Declare the View
        private readonly Image _imageEye = new Bitmap(Properties.Resources.eye, new Size(16, 16));
        private readonly Image _imageEyeSlash = new Bitmap(Properties.Resources.eye_slash, new Size(16, 16));
        private bool _isPasswordVisible = false;

        public CtrlFrmLogIn(frm_LogIn logInForm)
        {
            _frmLogIn = logInForm; // Create the View
            _frmLogIn.btnEye.Image = _imageEye; // Set the initial eye icon
        }

        public void EyeToggle()
        {
            if (_isPasswordVisible)
            {
                _frmLogIn.txtPassword.UseSystemPasswordChar = true;
                _frmLogIn.btnEye.Image = _imageEye;
            }
            else
            {
                _frmLogIn.txtPassword.UseSystemPasswordChar = false;
                _frmLogIn.btnEye.Image = _imageEyeSlash;
            }
            _isPasswordVisible = !_isPasswordVisible;
        }
    }
}
