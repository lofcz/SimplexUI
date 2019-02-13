using DarkUI.Config;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace DarkUI.Controls
{
    [ToolboxBitmap(typeof(Button))]
    [DefaultEvent("Click")]
    public class DarkColorPallete : Button
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

        public Color[] colors = new Color[50];
        

        #endregion

        #region Designer Property Region

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

        public DarkColorPallete()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw |
                     ControlStyles.UserPaint, true);

            base.UseVisualStyleBackColor = false;
            base.UseCompatibleTextRendering = false;

            SetButtonState(DarkControlState.Normal);
            Padding = new Padding(_padding);

            // row 1
            colors[0] = Color.Red;
            colors[1] = Color.Yellow;
            colors[2] = Color.Lime;
            colors[3] = Color.Aqua;
            colors[4] = Color.Blue;
            colors[5] = Color.Fuchsia;

            // row 2
            colors[6] = Color.FromArgb(64, 0 ,0);
            colors[7] = Color.FromArgb(64, 64, 0);
            colors[8] = Color.FromArgb(0, 64, 0);
            colors[9] = Color.FromArgb(0, 64, 64);
            colors[10] = Color.FromArgb(0, 0, 64);
            colors[11] = Color.FromArgb(64, 0, 64);

            // row 3
            colors[12] = Color.FromArgb(128, 0, 0);
            colors[13] = Color.FromArgb(128, 128, 0);
            colors[14] = Color.FromArgb(0, 128, 0);
            colors[15] = Color.FromArgb(0, 128, 128);
            colors[16] = Color.FromArgb(0, 0, 128);
            colors[17] = Color.FromArgb(128, 0, 128);

            // row 4
            colors[18] = Color.FromArgb(192, 0, 0);
            colors[19] = Color.FromArgb(192, 192, 0);
            colors[20] = Color.FromArgb(0, 192, 0);
            colors[21] = Color.FromArgb(0, 192, 192);
            colors[22] = Color.FromArgb(0, 0, 192);
            colors[23] = Color.FromArgb(192, 0, 192);

            // row 5
            colors[24] = Color.FromArgb(255, 64, 64);
            colors[25] = Color.FromArgb(255, 255, 64);
            colors[26] = Color.FromArgb(64, 255, 64);
            colors[27] = Color.FromArgb(64, 255, 255);
            colors[28] = Color.FromArgb(64, 64, 255);
            colors[29] = Color.FromArgb(255, 64, 255);

            // row 6
            colors[30] = Color.FromArgb(255, 128, 128);
            colors[31] = Color.FromArgb(255, 255, 128);
            colors[32] = Color.FromArgb(128, 255, 128);
            colors[33] = Color.FromArgb(128, 255, 255);
            colors[34] = Color.FromArgb(128, 128, 255);
            colors[35] = Color.FromArgb(255, 128, 255);

            // row 7
            colors[36] = Color.FromArgb(255, 192, 192);
            colors[37] = Color.FromArgb(255, 255, 192);
            colors[38] = Color.FromArgb(192, 255, 192);
            colors[39] = Color.FromArgb(192, 255, 255);
            colors[40] = Color.FromArgb(192, 192, 255);
            colors[41] = Color.FromArgb(255, 192, 255);

            // row 8
            colors[42] = Color.FromArgb(0, 0, 0);
            colors[43] = Color.FromArgb(32, 32, 32);
            colors[44] = Color.FromArgb(64, 64, 64);
            colors[45] = Color.FromArgb(128, 128, 128);
            colors[46] = Color.FromArgb(192, 192, 192);
            colors[47] = Color.FromArgb(255, 255, 255);
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

            using (var b = new SolidBrush(Color.Red))
            {
                Pen pp = new Pen(Color.Black);

                int xx = rect.X;
                int yy = rect.Y;
                int xo = xx;
                int size = 16;
                int index = 0;

                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 6; j++)
                    {
                        rect.X = xx;
                        rect.Y = yy;
                        rect.Width = size;
                        rect.Height = size;

                        b.Color = colors[index];
                        g.FillRectangle(b, rect);
                        g.DrawRectangle(pp, rect);

                        //Color.FromArgb(i * 20, j * 30, i * 25);
                        index++;
                        xx += size;
                    }

                    yy += size;
                    xx = xo;
                }
               
            }
           /*
            if (ButtonStyle == DarkButtonStyle.Normal)
            {
                using (var p = new Pen(borderColor, 1))
                {
                    var modRect = new Rectangle(rect.Left, rect.Top, rect.Width - 1, rect.Height - 1);

                    g.DrawRectangle(p, modRect);
                }
            }

            var textOffsetX = 0;
            var textOffsetY = 0;

            if (Image != null)
            {
                var stringSize = g.MeasureString(Text, Font, rect.Size);

                var x = ClientSize.Width / 2 - Image.Size.Width / 2;
                var y = ClientSize.Height / 2 - Image.Size.Height / 2;

                switch (TextImageRelation)
                {
                    case TextImageRelation.ImageAboveText:
                        textOffsetY = Image.Size.Height / 2 + ImagePadding / 2;
                        y = y - ((int)(stringSize.Height / 2) + ImagePadding / 2);
                        break;
                    case TextImageRelation.TextAboveImage:
                        textOffsetY = (Image.Size.Height / 2 + ImagePadding / 2) * -1;
                        y = y + (int)(stringSize.Height / 2) + ImagePadding / 2;
                        break;
                    case TextImageRelation.ImageBeforeText:
                        textOffsetX = Image.Size.Width + ImagePadding / 2;
                        x = ImagePadding;
                        break;
                    case TextImageRelation.TextBeforeImage:
                        x = x + (int)stringSize.Width;
                        break;
                }

                //g.DrawImage(Image, x, y);
                g.DrawImage(Image, new Rectangle(x, y, Image.Width, Image.Height));
            }

            using (var b = new SolidBrush(textColor))
            {
                var modRect = new Rectangle(rect.Left + textOffsetX + Padding.Left,
                                            rect.Top + textOffsetY + Padding.Top, rect.Width - Padding.Horizontal,
                                            rect.Height - Padding.Vertical);

                if (Image != null)
                {
                    var stringFormat = new StringFormat
                    {
                        LineAlignment = StringAlignment.Center,
                        Alignment = StringAlignment.Near,
                        Trimming = StringTrimming.EllipsisCharacter
                    };

                    g.DrawString(Text, Font, b, modRect, stringFormat);
                }
                else
                {
                    var stringFormat = new StringFormat
                    {
                        LineAlignment = StringAlignment.Center,
                        Alignment = StringAlignment.Center,
                        Trimming = StringTrimming.EllipsisCharacter
                    };

                    g.DrawString(Text, Font, b, modRect, stringFormat);
                }
            }*/
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
