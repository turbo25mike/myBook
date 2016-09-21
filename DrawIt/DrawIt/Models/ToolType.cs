using System;

namespace DrawIt
{
    public enum ToolType
    {
        Bucket,
        Brush,
        Pen,
        Eraser
    }

    public class ToolTypeEventArgs : EventArgs
    {
        public ToolType Tool { get; set; }

        public ToolTypeEventArgs(ToolType tool) : base()
        { Tool = tool; }
    }
}
