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
    public class GutterBookEffect: ShaderEffect
    {
        #region Default properties
        //trong file hlsl la2 float nhung muon dung nhung bien binh thuong trong axml luc runtime dc 
        //thi cac bien o day phai la DOUBLE!!!
        //float center = 0.5;
        //
        //float dark = 0.2;
        //float darker = 0.15;
        //float darkest = 0.4;

        public static readonly DependencyProperty CenterProperty =
            DependencyProperty.Register("center", typeof(double), typeof(GutterBookEffect),
            new PropertyMetadata(0.5, ShaderEffect.PixelShaderConstantCallback(0)));

        public static readonly DependencyProperty DarkProperty =
            DependencyProperty.Register("dark", typeof(double), typeof(GutterBookEffect),
            new PropertyMetadata(0.2, ShaderEffect.PixelShaderConstantCallback(1)));

        public static readonly DependencyProperty DarkerProperty =
            DependencyProperty.Register("darker", typeof(double), typeof(GutterBookEffect),
            new PropertyMetadata(0.15, ShaderEffect.PixelShaderConstantCallback(2)));

        public static readonly DependencyProperty DarkestProperty =
            DependencyProperty.Register("darkest", typeof(double), typeof(GutterBookEffect),
            new PropertyMetadata(0.4, ShaderEffect.PixelShaderConstantCallback(3)));

        public double Center
        {
            get
            {
                return (double)base.GetValue(CenterProperty);
            }
            set
            {
                base.SetValue(CenterProperty, value);
            }
        }

        public double Dark
        {
            get
            {
                return (double)base.GetValue(DarkProperty);
            }
            set
            {
                base.SetValue(DarkProperty, value);
            }
        }

        public double Darker
        {
            get
            {
                return (double)base.GetValue(DarkerProperty);
            }
            set
            {
                base.SetValue(DarkerProperty, value);
            }
        }

        public double Darkest
        {
            get
            {
                return (double)base.GetValue(DarkestProperty);
            }
            set
            {
                base.SetValue(DarkestProperty, value);
            }
        }

        #endregion

        public GutterBookEffect()
        {
            Uri u = new Uri(@"/CustomPixelShader;component/Effects/GutterBook.ps",
                        UriKind.Relative);
            PixelShader psCustom = new PixelShader();
            psCustom.UriSource = u;
            PixelShader = psCustom;

            base.UpdateShaderValue(CenterProperty);
            base.UpdateShaderValue(DarkProperty);
            base.UpdateShaderValue(DarkerProperty);
            base.UpdateShaderValue(DarkestProperty);
        }


    }
}
