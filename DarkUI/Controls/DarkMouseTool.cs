using DarkUI.Config;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace DarkUI.Controls
{
    [ToolboxBitmap(typeof(Button))]
    [DefaultEvent("Click")]
    public class DarkMouseTool : Button
    {
        #region Field Region

        private bool _useGenericBackColor = true;
        private DarkButtonStyle _style = DarkButtonStyle.Normal;
        private DarkControlState _buttonState = DarkControlState.Normal;

        private bool _isDefault;
        private bool _spacePressed;

        private const int _padding = Consts.Padding / 2;
        private DarkComboBox darkComboBox1;
        private int _imagePadding = 5; // Consts.Padding / 2

        #endregion

        #region Designer Property Region

        public bool Gradient { get; set; }

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

        public Color LeftColor { get; set; }
        public Color RightColor { get; set; }

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

        public DarkMouseTool()
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

                Pen pp = new Pen(Color.Black);

                int xx = rect.X;
                int yy = rect.Y + 16;
                int xo = xx;
                int size = 48;
                int index = 0;

                Brush bb = new SolidBrush(LeftColor);
                g.FillRectangle(bb, new Rectangle(xx, yy, 48, 48));
                g.DrawRectangle(pp, new Rectangle(xx, yy, 48, 48));
                bb = new SolidBrush(RightColor);
                g.FillRectangle(bb, new Rectangle(xx + 50, yy, 48, 48));
                g.DrawRectangle(pp, new Rectangle(xx + 50, yy, 48, 48));

                Brush cbb = new SolidBrush(Colors.LightText);
                g.DrawString("Left:", DefaultFont, cbb, new PointF(xx, yy - 16));
                g.DrawString("Right:", DefaultFont, cbb, new PointF(xx + 50, yy - 16));
        }

        #endregion

        private void InitializeComponent()
        {
            this.darkComboBox1 = new DarkUI.Controls.DarkComboBox();
            this.SuspendLayout();
            // 
            // darkComboBox1
            // 
            this.darkComboBox1.FormattingEnabled = true;
            this.darkComboBox1.Location = new System.Drawing.Point(0, 0);
            this.darkComboBox1.Name = "darkComboBox1";
            this.darkComboBox1.Size = new System.Drawing.Size(121, 21);
            this.darkComboBox1.TabIndex = 0;
            this.ResumeLayout(false);

        }
    }
}
