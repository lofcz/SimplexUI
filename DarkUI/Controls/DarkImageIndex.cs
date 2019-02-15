using DarkUI.Config;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DarkUI.Icons;

namespace DarkUI.Controls
{

    public class ImageIndex
    {
        public Bitmap bmp;

        public ImageIndex()
        {

        }
    }

    [ToolboxBitmap(typeof(Button))]
    [DefaultEvent("Click")]
    public class DarkImageIndex : Button
    {
        #region Field Region

        private bool _useGenericBackColor = true;
        private DarkButtonStyle _style = DarkButtonStyle.Normal;
        private DarkControlState _buttonState = DarkControlState.Normal;

        private bool _isDefault;
        private bool _spacePressed;

        private const int _padding = Consts.Padding / 2;
        private int _imagePadding = 5; // Consts.Padding / 2

        public List<ImageIndex> Frames = new List<ImageIndex>();
        public int SelectedFrame { get; set; }


        #endregion

        #region Designer Property Region

        public int CameraX = 0;

        public new string Text
        {
            get { return base.Text; }
            set
            {
                base.Text = value;
                Invalidate();
            }
        }

        public new bool Enabled
        {
            get { return base.Enabled; }
            set
            {
                base.Enabled = value;
                Invalidate();
            }
        }

        [Localizable(true)]
        [ReadOnly(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new Padding Padding
        {
            get { return base.Padding; }
            set { base.Padding = value; }
        }

        [Category("Appearance")]
        [Description("Determines if system BackColor should be used or not.")]
        [DefaultValue(true)]
        public bool BackColorUseGeneric
        {
            get { return _useGenericBackColor; }
            set
            {
                _useGenericBackColor = value;
                Invalidate();
            }
        }

        [Category("Appearance")]
        [Description("Determines the style of the button.")]
        [DefaultValue(DarkButtonStyle.Normal)]
        public DarkButtonStyle ButtonStyle
        {
            get { return _style; }
            set
            {
                _style = value;
                Invalidate();
            }
        }

        [Category("Appearance")]
        [Description("Determines the amount of padding between the image and text.")]
        [DefaultValue(5)]
        public int ImagePadding
        {
            get { return _imagePadding; }
            set
            {
                _imagePadding = value;
                Invalidate();
            }
        }

        #endregion

        #region Code Property Region

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new bool AutoEllipsis
        {
            get { return false; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DarkControlState ButtonState
        {
            get { return _buttonState; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new ContentAlignment ImageAlign
        {
            get { return base.ImageAlign; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new bool FlatAppearance
        {
            get { return false; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new FlatStyle FlatStyle
        {
            get { return base.FlatStyle; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new ContentAlignment TextAlign
        {
            get { return base.TextAlign; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new bool UseCompatibleTextRendering
        {
            get { return false; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new bool UseVisualStyleBackColor
        {
            get { return false; }
        }

        #endregion

        #region Constructor Region

        public DarkImageIndex()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw |
                     ControlStyles.UserPaint, true);

            base.UseVisualStyleBackColor = false;
            base.UseCompatibleTextRendering = false;

            SetButtonState(DarkControlState.Normal);
            Padding = new Padding(_padding);
        }

        #endregion

        #region Method Region

        private void SetButtonState(DarkControlState buttonState)
        {
            if (_buttonState != buttonState)
            {
                _buttonState = buttonState;
                Invalidate();
            }
        }

        #endregion

        #region Event Handler Region

        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            var form = FindForm();
            if (form != null)
            {
                if (form.AcceptButton == this)
                    _isDefault = true;
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (_spacePressed)
                return;

            if (e.Button == MouseButtons.Left)
            {
                if (ClientRectangle.Contains(e.Location))
                    SetButtonState(DarkControlState.Pressed);
                else
                    SetButtonState(DarkControlState.Hover);
            }
            else
            {
                SetButtonState(DarkControlState.Hover);
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (!ClientRectangle.Contains(e.Location))
                return;

            SetButtonState(DarkControlState.Pressed);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            if (_spacePressed)
                return;

            SetButtonState(DarkControlState.Normal);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);

            if (_spacePressed)
                return;

            SetButtonState(DarkControlState.Normal);
        }

        protected override void OnMouseCaptureChanged(EventArgs e)
        {
            base.OnMouseCaptureChanged(e);

            if (_spacePressed)
                return;

            var location = Cursor.Position;

            if (!ClientRectangle.Contains(location))
                SetButtonState(DarkControlState.Normal);
        }

        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);

            Invalidate();
        }

        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);

            _spacePressed = false;

            var location = Cursor.Position;

            if (!ClientRectangle.Contains(location))
                SetButtonState(DarkControlState.Normal);
            else
                SetButtonState(DarkControlState.Hover);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (e.KeyCode == Keys.Space)
            {
                _spacePressed = true;
                SetButtonState(DarkControlState.Pressed);
            }
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);

            if (e.KeyCode != Keys.Space)
                return;

            _spacePressed = false;

            var location = Cursor.Position;

            if (!ClientRectangle.Contains(location))
                SetButtonState(DarkControlState.Normal);
            else
                SetButtonState(DarkControlState.Hover);
        }

        public override void NotifyDefault(bool value)
        {
            base.NotifyDefault(value);

            if (!DesignMode)
                return;

            _isDefault = value;
            Invalidate();
        }

        #endregion

        #region Paint Region

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            var rect = new Rectangle(0, 0, ClientSize.Width, ClientSize.Height);

            var textColor = Colors.LightText;
            var borderColor = Colors.GreySelection;
            var fillColor = _useGenericBackColor ? (_isDefault ? Colors.DarkBlueBackground : Colors.LightBackground) : BackColor;
            var hoverColor = _useGenericBackColor ? (_isDefault ? Colors.BlueBackground : Colors.LighterBackground) : ControlPaint.Light(BackColor);

            Brush cb = new SolidBrush(Color.FromArgb(60, 63, 65));
            g.FillRectangle(cb, rect);

            if (Enabled)
            {
                switch (ButtonStyle)
                {
                    case DarkButtonStyle.Normal:
                        if (Focused && TabStop)
                            borderColor = Colors.BlueHighlight;

                        switch (ButtonState)
                        {
                            case DarkControlState.Hover:
                                fillColor = hoverColor;
                                break;
                            case DarkControlState.Pressed:
                                fillColor = Colors.DarkBackground;
                                break;
                        }
                        break;
                    case DarkButtonStyle.Flat:
                        switch (ButtonState)
                        {
                            case DarkControlState.Normal:
                                fillColor = Colors.GreyBackground;
                                break;
                            case DarkControlState.Hover:
                                fillColor = Colors.MediumBackground;
                                break;
                            case DarkControlState.Pressed:
                                fillColor = Colors.DarkBackground;
                                break;
                        }
                        break;
                }
            }
            else
            {
                textColor = Colors.DisabledText;
                fillColor = Colors.DarkGreySelection;
            }

            Pen black = new Pen(Color.Black);
            Pen orange = new Pen(Color.FromArgb(203, 165, 110));

            SolidBrush g0 = new SolidBrush(Color.FromArgb(51, 53, 54));

            Pen g1 = new Pen(Color.FromArgb(51, 53, 54));
            Pen g2 = new Pen(Color.FromArgb(60, 63, 65));
            Pen g3 = new Pen(Color.FromArgb(81, 81, 81));


            // draw slider
            SolidBrush solid = new SolidBrush(Colors.DarkBlueBackground);
            SolidBrush dark = new SolidBrush(Color.FromArgb(40, 43, 45));

            g.FillRectangle(solid, new Rectangle(0, rect.Height - 8, rect.Width, 6));

            int chunk = 8;
            int current = 4;
            bool flag = true;

            Point p1 = new Point();
            Point p2 = new Point();
            int x = rect.X;

            for (int i = 0; i < rect.Width; i++)
            {
                p1.X = x;
                p1.Y = rect.Height;

                p2.X = x + 8;
                p2.Y = rect.Height - 8;

                g.DrawLine(flag ? g1 : g2, p1, p2);

                current++;
                x++;
                if ((current >= 8 && flag) || (current >= 9 && !flag))
                {
                    current = 0;
                    flag = !flag;
                }
            }

            g.DrawRectangle(g3, new Rectangle(0, rect.Height - 8, rect.Width - 1, 7));

            //  g.DrawRectangle(black, new Rectangle(0, rect.Height - 8, rect.Width - 1, 6));
            //asdas
            // draw frames with respect to camera
            int frames = Frames.Count + 1;

            SolidBrush q1 = new SolidBrush(Color.FromArgb(68, 68, 68));
            SolidBrush q2 = new SolidBrush(Color.FromArgb(77, 77, 77));
            Pen q3 = new Pen(Color.FromArgb(128, 128, 128));

            bool br = false;
            bool toSwith = false;
            for (var i = 0; i < frames; i++)
            {
                // override this one for the + symbol
                if (i == 0)
                {
                    g.FillRectangle(g0, new Rectangle(80 * i + 16 * i + CameraX, 5, 80, 80));
                    g.DrawRectangle(q3, new Rectangle(80 * i + 16 * i + CameraX, 5, 80, 80));

                    // draw that +
                    g.DrawImageUnscaled(LofIcons.sign, new Point(80 * i + 16 * i + CameraX - 15, 5 - 15));

                    continue;
                }

                if (i == SelectedFrame)
                {
                    g.FillRectangle(dark, new Rectangle(80 * i + 16 * i + CameraX - 3,  0, 86, 90));

                    g.DrawRectangle(orange, new Rectangle(80 * i + 16 * i + CameraX - 3, 0, 86, 90));
                    g.DrawLine(orange, 80 * i + 16 * i + CameraX - 3, 1, 80 * i + 16 * i + CameraX - 3 + 86, 1);
                    g.DrawLine(orange, 80 * i + 16 * i + CameraX - 3, 2, 80 * i + 16 * i + CameraX - 3 + 86, 2);

                    g.DrawLine(orange, 80 * i + 16 * i + CameraX - 3, 89, 80 * i + 16 * i + CameraX - 3 + 86, 89);
                    g.DrawLine(orange, 80 * i + 16 * i + CameraX - 3, 88, 80 * i + 16 * i + CameraX - 3 + 86, 88);
                }


                // Draw blank texture then
                for (var yy = 0; yy < 80; yy += 10)
                {
                    for (var xx = 0; xx < 80; xx += 10)
                    {
                        g.FillRectangle(br ? q1 : q2, new Rectangle(80 * i + 16 * i + CameraX + xx, 5 + yy, 10, 10));
                        br = !br;
                    }

                    br = !br;
                }

                // draw image
                if (Frames.Count > i)
                {
                    if (Frames[i].bmp != null)
                    {
                        g.DrawImage(Frames[i].bmp, new Point(80 * (i) + 16 * (i) + CameraX + 20, 30));
                    }
                }
                //
                g.DrawRectangle(q3, new Rectangle(80 * i + 16 * i + CameraX, 5, 80, 80));
            }
        }

        #endregion
    }
}
