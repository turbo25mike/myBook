using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace DrawIt
{
    public class ToolManager : ViewModelBase
    {


        private bool _AntiAlias = true;
        public bool AntiAlias
        {
            get
            {
                return _AntiAlias;
            }
            set
            {
                if (!_AntiAlias.Equals(value))
                {
                    _AntiAlias = value;
                    OnPropertyChanged(nameof(AntiAlias));
                }
            }
        }

        private int _Alpha = 30;
        public int Alpha
        {
            get
            {
                return _Alpha;
            }
            set
            {
                if (!_Alpha.Equals(value))
                {
                    _Alpha = value;
                    OnPropertyChanged(nameof(Alpha));
                }
            }
        }

        private string _ForegroundColor = "00000000";
        public string ForegroundColor
        {
            get
            {
                return _ForegroundColor;
            }
            set
            {
                if (!_ForegroundColor.Equals(value))
                {
                    _ForegroundColor = value;
                    OnPropertyChanged(nameof(ForegroundColor));
                }
            }
        }

        private double _BlurRadius = 5.0;
        public double BlurRadius
        {
            get
            {
                return _BlurRadius;
            }
            set
            {
                if (!_BlurRadius.Equals(value))
                {
                    _BlurRadius = value;
                    OnPropertyChanged(nameof(BlurRadius));
                }
            }
        }

        private double _BrushSize = 10.0;
        public double BrushSize
        {
            get
            {
                return _BrushSize;
            }
            set
            {
                if (!_BrushSize.Equals(value))
                {
                    _BrushSize = value;
                    OnPropertyChanged(nameof(BrushSize));
                }
            }
        }

        private ToolType _Tool = ToolType.Brush;
        public ToolType Tool
        {
            get
            {
                return _Tool;
            }
            set
            {
                if (!_Tool.Equals(value))
                {
                    _Tool = value;
                    OnPropertyChanged(nameof(Tool));
                }
            }
        }

        private static ToolManager _Instance;
        public static ToolManager Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new ToolManager();
                }
                return _Instance;
            }
        }

        protected ToolManager()
        {
        }
    }
}
