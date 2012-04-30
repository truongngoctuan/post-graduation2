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

namespace CustomPixelShader.Effects
{
    public class AmbientEffect: ShaderEffect
    {
        
        public static readonly DependencyProperty AmplitudeProperty =
            DependencyProperty.Register("Amplitude", typeof(double), typeof(AmbientEffect), 
            new PropertyMetadata(0.1, ShaderEffect.PixelShaderConstantCallback(1)));

        public static readonly DependencyProperty CenterProperty =
            DependencyProperty.Register("Center", typeof(Point), typeof(AmbientEffect), 
            new PropertyMetadata(new Point(0.5, 0.5), ShaderEffect.PixelShaderConstantCallback(0)));


        public AmbientEffect()
        {
            Uri u = new Uri(@"/CustomPixelShader;component/Effects/Ambient.ps",
                        UriKind.Relative);
            PixelShader psCustom = new PixelShader();
            psCustom.UriSource = u;
            PixelShader = psCustom;

            base.UpdateShaderValue(CenterProperty);
            base.UpdateShaderValue(AmplitudeProperty);
        }

        public double Amplitude
        {
            get
            {
                return (double)base.GetValue(AmplitudeProperty);
            }
            set
            {
                base.SetValue(AmplitudeProperty, value);
            }
        }

        public Point Center
        {
            get
            {
                return (Point)base.GetValue(CenterProperty);
            }
            set
            {
                base.SetValue(CenterProperty, value);
            }
        }
    }
}
