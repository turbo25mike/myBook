using Android.Views;
using Android.Graphics;
using Android.Content;
using Android.Graphics.Drawables;
using Android.Graphics.Drawables.Shapes;
using System;

namespace DrawIt
{
    public class DrawView : View
    {
        public DrawView(Context context)
            : base(context)
        {
            Start();
        }
        
        public bool InFocus { get; set; }

        private Path DrawPath;
        private Paint DrawPaint;
        private Paint CanvasPaint;
        private Canvas DrawCanvas;
        private Bitmap CanvasBitmap;
        private Bitmap BrushBitmap;
        private float _PreviousX;
        private float _PreviousY;

        private void Start()
		{
            InFocus = true;
            DrawPath = new Path ();

            //ShapeDrawable mBrush = new ShapeDrawable(new OvalShape());

            //Paint paint = mBrush.Paint;
            CanvasPaint = new Paint();

            DrawPaint = new Paint
            {
                Dither = true
            };

            DrawPaint.SetStyle (Paint.Style.Stroke);
			DrawPaint.StrokeJoin = Paint.Join.Round;
			DrawPaint.StrokeCap = Paint.Cap.Round;
            //DrawPaint.SetPathEffect(new DashPathEffect(new[] { 15f, 5f }, 0));
            //DrawPaint.SetPathEffect(new CornerPathEffect(3));
            //DrawPaint.SetPathEffect(new DiscretePathEffect(10,10));

            //Path path = new Path();
            //path.AddRect(0, 0, 8, 8, Path.Direction.Ccw);
            //DrawPaint.SetPathEffect(new PathDashPathEffect(path, 20, 20, PathDashPathEffect.Style.Morph));

            //Shader shader = new RadialGradient(10, 10, 10, Color.Black, Color.Black, Shader.TileMode.Clamp);

            

            //DrawPaint.Alpha = 10;

            

            //var originalBitmap = await handler.LoadImageAsync(imageSource, context);
            //// load your brush here
            BrushBitmap = BitmapFactory.DecodeResource(Xamarin.Forms.Forms.Context.Resources, 2130837579); //brush1.jpg
            
            //BitmapShader b2 = new BitmapShader(BrushBitmap, Shader.TileMode.Repeat, Shader.TileMode.Repeat);
            //DrawPaint.SetShader(b2);
            //mBitmapBrushDimensions = new Vector2(mBitmapBrush.getWidth(), mBitmapBrush.getHeight());

        }

        private Bitmap ScaleBitmap(Bitmap bm, int newWidth, int newHeight)
        {
            int width = bm.Width;
            int height = bm.Height;
            float scaleWidth = ((float)newWidth) / width;
            float scaleHeight = ((float)newHeight) / height;
            // CREATE A MATRIX FOR THE MANIPULATION
            Matrix matrix = new Matrix();
            // RESIZE THE BIT MAP
            matrix.PostScale(scaleWidth, scaleHeight);

            // "RECREATE" THE NEW BITMAP
            Bitmap resizedBitmap = Bitmap.CreateBitmap(
                bm, 0, 0, width, height, matrix, false);
            //bm.Recycle();
            return resizedBitmap;
        }

        protected override void OnSizeChanged(int w, int h, int oldw, int oldh)
        {
            if(w < 1 || h < 1)
            {
                w = 300;
                h = 300;
            }
            base.OnSizeChanged(w, h, oldw, oldh);
            CanvasBitmap = Bitmap.CreateBitmap(w, h, Bitmap.Config.Argb8888);
            DrawCanvas = new Canvas(CanvasBitmap);
            
        }

        protected override void OnDraw(Canvas canvas)
        {
            base.OnDraw(canvas);

            DrawPaint.Alpha = ToolManager.Instance.Alpha;
            DrawPaint.AntiAlias = ToolManager.Instance.AntiAlias;
            DrawPaint.Color = Color.ParseColor("#" + ToolManager.Instance.ForegroundColor);
            DrawPaint.StrokeWidth = (float)ToolManager.Instance.BrushSize;
            canvas.DrawBitmap(CanvasBitmap, 0, 0, CanvasPaint);
            canvas.DrawPath(DrawPath, DrawPaint);
        }

        private void DrawBrush(float x, float y)
        {
            // get vector from previous to current position
            float xdist = x - _PreviousX;
            float ydist = y - _PreviousY;

            // get the length
            float segmentLength = (float)Math.Sqrt(xdist * xdist + ydist * ydist);

            // derive a suitable step size from stroke width
            float stepSize = Math.Max((float)(ToolManager.Instance.BrushSize / 10), 1f);

            // calculate the number of steps we need to take
            // NOTE: this draws a bunch of evenly spaced splashes from the start point
            // to JUST BEFORE the end point. The end point will be drawn by the start point of the
            // next stroke, or by the touch_up method. If we drew both the start and
            // end point there it would be doubled up
            int steps = (int)Math.Max(Math.Round((double)(segmentLength / stepSize)), 2);

            Bitmap b = ScaleBitmap(BrushBitmap, (int)ToolManager.Instance.BrushSize, (int)ToolManager.Instance.BrushSize);
            Paint p = new Paint();
            p.Alpha = ToolManager.Instance.Alpha;
            p.AntiAlias = ToolManager.Instance.AntiAlias;
            p.SetColorFilter(new LightingColorFilter(Color.ParseColor("#" + ToolManager.Instance.ForegroundColor), 0));
            BlurMaskFilter filter = new BlurMaskFilter((float)ToolManager.Instance.BlurRadius, BlurMaskFilter.Blur.Normal);
            p.SetMaskFilter(filter);

            for (int i = 0; i < steps; i++)
            {
                int currentX = (int)(_PreviousX + xdist * i / steps);
                int currentY = (int)(_PreviousY + ydist * i / steps);
                DrawCanvas.DrawBitmap(b, x - (int)ToolManager.Instance.BrushSize / 2, y - (int)ToolManager.Instance.BrushSize / 2, p);
            }

            // update the previous position
            _PreviousX = x;
            _PreviousY = y;
        }

        public override bool OnTouchEvent(MotionEvent e)
        {
            if (InFocus)
            {
                var touchX = e.GetX();
                var touchY = e.GetY();

                switch (e.Action)
                {
                    case MotionEventActions.Down:
                        _PreviousX = touchX;
                        _PreviousY = touchY;
                        switch (ToolManager.Instance.Tool)
                        {
                            case ToolType.Pen:
                                DrawPath.MoveTo(touchX, touchY);
                                break;
                            case ToolType.Bucket:
                                DrawCanvas.DrawPaint(DrawPaint);
                                break;
                        }
                        break;
                    case MotionEventActions.Move:
                        switch (ToolManager.Instance.Tool)
                        {
                            case ToolType.Pen:
                                DrawPath.LineTo(touchX, touchY);
                                break;
                            case ToolType.Brush:
                                DrawBrush(touchX, touchY);
                                break;
                        }
                        break;
                    case MotionEventActions.Up:
                        switch (ToolManager.Instance.Tool)
                        {
                            case ToolType.Pen:
                                DrawCanvas.DrawPath(DrawPath, DrawPaint);
                                DrawPath.Reset();
                                break;
                            case ToolType.Bucket:
                                break;
                        }
                        break;
                    default:
                        return false;
                }

                Invalidate();
                return true;
            }
            return false;
        }
    }
}

