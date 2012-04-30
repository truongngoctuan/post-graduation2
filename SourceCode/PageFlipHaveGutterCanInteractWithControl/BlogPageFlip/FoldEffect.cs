using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Media.Effects;

namespace PageFlip
{
    internal class FoldEffect : ShaderEffect
    {
        // Fields
        private static readonly DependencyProperty ContentSizeProperty = DependencyProperty.Register("ContentSize", typeof(Size), typeof(FoldEffect), new PropertyMetadata(new Size(0.0, 0.0), ShaderEffect.PixelShaderConstantCallback(3)));
        private static readonly DependencyProperty InnerShadowsProperty = DependencyProperty.Register("InnerShadows", typeof(float), typeof(FoldEffect), new PropertyMetadata(150f, ShaderEffect.PixelShaderConstantCallback(6)));
        private static readonly DependencyProperty InputProperty = ShaderEffect.RegisterPixelShaderSamplerProperty("Input", typeof(ShaderEffect), 0);
        private static readonly DependencyProperty OuterShadowsProperty = DependencyProperty.Register("OuterShadows", typeof(float), typeof(FoldEffect), new PropertyMetadata(8f, ShaderEffect.PixelShaderConstantCallback(7)));
        private static readonly DependencyProperty PaddingProperty = DependencyProperty.Register("Padding", typeof(Size), typeof(FoldEffect), new PropertyMetadata(new Size(0.0, 0.0), ShaderEffect.PixelShaderConstantCallback(2)));
        private static readonly DependencyProperty PixelProperty = DependencyProperty.Register("Pixel", typeof(Size), typeof(FoldEffect), new PropertyMetadata(new Size(0.0, 0.0), ShaderEffect.PixelShaderConstantCallback(1)));
        private static readonly DependencyProperty RectABProperty = DependencyProperty.Register("RectAB", typeof(Point), typeof(FoldEffect), new PropertyMetadata(new Point(1.0, 0.0), ShaderEffect.PixelShaderConstantCallback(4)));
        private static readonly DependencyProperty RectCProperty = DependencyProperty.Register("RectC", typeof(double), typeof(FoldEffect), new PropertyMetadata(0.0, ShaderEffect.PixelShaderConstantCallback(5)));
        private static readonly DependencyProperty SizeProperty = DependencyProperty.Register("Size", typeof(Size), typeof(FoldEffect), new PropertyMetadata(new Size(0.0, 0.0), ShaderEffect.PixelShaderConstantCallback(0)));

        // Methods
        public FoldEffect()
        {
            PixelShader shader = new PixelShader();
            shader.UriSource = (new Uri("/PageFlip;component/Effects/FoldEffect.ps", UriKind.Relative));
            base.PixelShader=(shader);
            base.UpdateShaderValue(InputProperty);
            base.UpdateShaderValue(RectABProperty);
            base.UpdateShaderValue(RectCProperty);
            base.UpdateShaderValue(InnerShadowsProperty);
            base.UpdateShaderValue(OuterShadowsProperty);
        }

        private void UpdatePadding()
        {
            base.PaddingLeft = (this.Padding.Width);
            base.PaddingTop=(this.Padding.Height);
            base.PaddingRight=((this.Size.Width - this.Padding.Width) - this.ContentSize.Width);
            base.PaddingBottom=((this.Size.Height - this.Padding.Height) - this.ContentSize.Height);
        }

        // Properties
        public Size ContentSize
        {
            get
            {
                return (Size)base.GetValue(ContentSizeProperty);
            }
            set
            {
                base.SetValue(ContentSizeProperty, value);
                this.UpdatePadding();
            }
        }

        public float InnerShadows
        {
            get
            {
                return (float)base.GetValue(InnerShadowsProperty);
            }
            set
            {
                base.SetValue(InnerShadowsProperty, value);
            }
        }

        public float OuterShadows
        {
            get
            {
                return (float)base.GetValue(OuterShadowsProperty);
            }
            set
            {
                base.SetValue(OuterShadowsProperty, value);
            }
        }

        public Size Padding
        {
            get
            {
                return (Size)base.GetValue(PaddingProperty);
            }
            set
            {
                base.SetValue(PaddingProperty, value);
                this.UpdatePadding();
            }
        }

        public Point RectAB
        {
            get
            {
                return (Point)base.GetValue(RectABProperty);
            }
            set
            {
                base.SetValue(RectABProperty, value);
            }
        }

        public double RectC
        {
            get
            {
                return (double)base.GetValue(RectCProperty);
            }
            set
            {
                base.SetValue(RectCProperty, value);
            }
        }

        public Size Size
        {
            get
            {
                return (Size)base.GetValue(SizeProperty);
            }
            set
            {
                base.SetValue(SizeProperty, value);
                base.SetValue(PixelProperty, new Size(1.0 / value.Width, 1.0 / value.Height));
                this.UpdatePadding();
            }
        }
    }


}
