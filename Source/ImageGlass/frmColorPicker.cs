﻿using ImageGlass.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ImageGlass.Theme;
using System.Runtime.InteropServices;

namespace ImageGlass
{
    public partial class frmColorPicker : Form
    {

        // opacity when the mouse is out of this form
        private const double FormOpacity = 0.9;
        // default location offset on the parent form
        private static Point DefaultLocationOffset = new Point(20, 80);

        private Form _currentOwner = null;
        private ImageBox _imgBox = null;
        private BitmapBooster _bmpBooster = null;
        private Point _locationOffset = DefaultLocationOffset;
        private Point _cursorPos = new Point();


        public frmColorPicker()
        {
            InitializeComponent();

            //apply current theme
            this.BackColor = txtRGB.BackColor = txtHEX.BackColor = LocalSetting.Theme.BackgroundColor;
            lblPixel.ForeColor = lblRgb.ForeColor = lblHex.ForeColor = LocalSetting.Theme.TextInfoColor;


            // set the opacity and events to manage it
            Opacity = FormOpacity;
            foreach (var child in Controls)
            {
                ((Control)child).MouseEnter += frmColorPicker_MouseEnter;
            }

            DefaultLocationOffset = new Point((int)(DefaultLocationOffset.X * DPIScaling.GetDPIScaleFactor()), (int)(DefaultLocationOffset.Y * DPIScaling.GetDPIScaleFactor()));
        }

        public void SetImageBox(ImageBox imgBox)
        {
            if (_imgBox != null)
            {
                _imgBox.MouseMove -= _imgBox_MouseMove;
                _imgBox.ImageChanged -= _imgBox_ImageChanged;
            }

            _imgBox = imgBox;
            _imgBox_ImageChanged(this, EventArgs.Empty);

            _imgBox.MouseMove += _imgBox_MouseMove;
            _imgBox.ImageChanged += _imgBox_ImageChanged;
            _imgBox.Click += _imgBox_Click;
        }


        #region Borderless form moving
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();


        private void frmColorPicker_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        #endregion


        #region Create shadow for borderless form

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(
            int nLeftRect, // x-coordinate of upper-left corner
            int nTopRect, // y-coordinate of upper-left corner
            int nRightRect, // x-coordinate of lower-right corner
            int nBottomRect, // y-coordinate of lower-right corner
            int nWidthEllipse, // height of ellipse
            int nHeightEllipse // width of ellipse
        );

        [DllImport("dwmapi.dll")]
        public static extern int DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS pMarInset);

        [DllImport("dwmapi.dll")]
        public static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);

        [DllImport("dwmapi.dll")]
        public static extern int DwmIsCompositionEnabled(ref int pfEnabled);

        private bool m_aeroEnabled = false;              // variables for box shadow
        private const int CS_DROPSHADOW = 0x00020000;
        private const int WM_NCPAINT = 0x0085;
        private const int WM_ACTIVATEAPP = 0x001C;

        public struct MARGINS                           // struct for box shadow
        {
            public int leftWidth;
            public int rightWidth;
            public int topHeight;
            public int bottomHeight;
        }

        private bool CheckAeroEnabled()
        {
            if (Environment.OSVersion.Version.Major >= 6)
            {
                int enabled = 0;
                DwmIsCompositionEnabled(ref enabled);
                return (enabled == 1) ? true : false;
            }
            return false;
        }

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_NCPAINT: // box shadow
                    if (m_aeroEnabled)
                    {
                        var v = 2;
                        DwmSetWindowAttribute(this.Handle, 2, ref v, 4);

                        MARGINS margins = new MARGINS()
                        {
                            bottomHeight = 1,
                            leftWidth = 1,
                            rightWidth = 1,
                            topHeight = 1
                        };

                        DwmExtendFrameIntoClientArea(this.Handle, ref margins);
                    }
                    break;
                default:
                    break;
            }

            base.WndProc(ref m);
        }
        #endregion
        

        #region Properties to make a tool window

        protected override bool ShowWithoutActivation
        {
            get { return true; }
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams baseParams = base.CreateParams;
                baseParams.ExStyle |= 0x8000000 // WS_EX_NOACTIVATE
                    | 0x00000080;   // WS_EX_TOOLWINDOW


                #region Shadow for Borderless form
                m_aeroEnabled = CheckAeroEnabled();

                if (!m_aeroEnabled)
                    baseParams.ClassStyle |= CS_DROPSHADOW;
                #endregion


                return baseParams;
            }
        }

        #endregion


        #region Events to manage the form location
        
        private Point parentLocation = Point.Empty;
        private Point parentOffset = Point.Empty;
        private bool formOwnerMoving = false;



        private void _AttachEventsToParent(Form frmOwner)
        {
            if (frmOwner == null)
                return;

            parentLocation = this.Owner.Location;

            frmOwner.Move += Owner_Move;
            frmOwner.SizeChanged += Owner_Move;
            frmOwner.VisibleChanged += Owner_Move;
            frmOwner.Deactivate += FrmOwner_Deactivate;
            frmOwner.LocationChanged += FrmOwner_LocationChanged;
        }

        private void FrmOwner_LocationChanged(object sender, EventArgs e)
        {
            formOwnerMoving = false;
        }

        private void _DetachEventsFromParent(Form frmOwner)
        {
            if (frmOwner == null)
                return;

            frmOwner.Move -= Owner_Move;
            frmOwner.SizeChanged -= Owner_Move;
            frmOwner.VisibleChanged -= Owner_Move;
            frmOwner.Deactivate -= FrmOwner_Deactivate;
            frmOwner.LocationChanged -= FrmOwner_LocationChanged;
        }

        private void FrmOwner_Deactivate(object sender, EventArgs e)
        {
            this.TopMost = false;
        }

        private void frmColorPicker_Move(object sender, EventArgs e)
        {
            if (!formOwnerMoving)
            {
                _locationOffset = new Point(this.Left - this.Owner.Left, this.Top - this.Owner.Top);
                parentOffset = _locationOffset;
            }
        }

        private void Owner_Move(object sender, EventArgs e)
        {
            formOwnerMoving = true;

            var parentOffset = new Point(this.Owner.Left - parentLocation.X, this.Owner.Top - parentLocation.Y);

            _SetLocationBasedOnParent();
            parentLocation = this.Owner.Location;
        }

        protected override void OnShown(EventArgs e)
        {
            if (Owner != _currentOwner)
            {
                _DetachEventsFromParent(_currentOwner);
                _currentOwner = Owner;
                _AttachEventsToParent(_currentOwner);
            }
            

            _locationOffset = DefaultLocationOffset;
            parentOffset = _locationOffset;

            _SetLocationBasedOnParent();
            _ResetColor();
            
            
            base.OnShown(e);
        }


        private void _SetLocationBasedOnParent()
        {
            if (Owner == null)
                return;

            if (Owner.WindowState == FormWindowState.Minimized
                || !Owner.Visible)
            {
                Visible = false;
                return;
            }

            // set location based on the main form
            Point ownerLocation = Owner.Location;
            ownerLocation.Offset(parentOffset);

            this.Location = ownerLocation;
        }

        #endregion


        #region Events to manage ImageBox

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            if (_imgBox != null)
            {
                _imgBox.MouseMove -= _imgBox_MouseMove;
                _imgBox.ImageChanged -= _imgBox_ImageChanged;
            }
        }

        private void _imgBox_ImageChanged(object sender, EventArgs e)
        {
            if (_bmpBooster != null)
            {
                _bmpBooster.Dispose();
            }

            if (_imgBox.Image != null)
            {
                _bmpBooster = new BitmapBooster(new Bitmap(_imgBox.Image));
            }
        }

        private void _imgBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (_imgBox.Image == null || _bmpBooster == null)
            {
                return;
            }
            _imgBox.Cursor = Cursors.Cross;

            _cursorPos = _imgBox.PointToImage(e.Location);
            if (_cursorPos.X >= 0 && _cursorPos.Y >= 0 && _cursorPos.X < _imgBox.Image.Width
                && _cursorPos.Y < _imgBox.Image.Height)
            {
                Color color = _bmpBooster.get(_cursorPos.X, _cursorPos.Y);
                //_DisplayColor(color, _cursorPos);
            }
        }

        private void _imgBox_Click(object sender, EventArgs e)
        {
            if (_imgBox.Image == null || _bmpBooster == null)
            {
                return;
            }

            if (_cursorPos.X >= 0 && _cursorPos.Y >= 0 && _cursorPos.X < _imgBox.Image.Width
                && _cursorPos.Y < _imgBox.Image.Height)
            {
                Color color = _bmpBooster.get(_cursorPos.X, _cursorPos.Y);
                _DisplayColor(color, _cursorPos);
            }
        }

        #endregion


        #region Opacity events

        private void frmColorPicker_MouseLeave(object sender, EventArgs e)
        {
            Opacity = FormOpacity;
        }

        private void frmColorPicker_MouseEnter(object sender, EventArgs e)
        {
            Opacity = 1;
        }

        #endregion


        #region Display data

        private void _DisplayColor(Color color, Point pixel)
        {
            panelColor.BackColor = color;
            lblPixel.Text = string.Format("Pixel: ({0}, {1})", pixel.X, pixel.Y);
            txtRGB.Text = string.Format("{0}, {1}, {2}", color.R, color.G, color.B);
            txtHEX.Text = string.Format("#{0:X2}{1:X2}{2:X2}", color.R, color.G, color.B);
        }

        private void _ResetColor()
        {
            lblPixel.Text = string.Empty;
            txtRGB.Text = string.Empty;
            txtHEX.Text = string.Empty;
        }


        private void ColorTextbox_Click(object sender, EventArgs e)
        {
            var txt = (TextBox)sender;
            txt.SelectAll();
        }
        
        #endregion



    }
}
