using System;
using System.Windows.Media.Effects;
using System.Windows;

namespace PageFlip
{
  public class ReflectionShader : ShaderEffect
  {
    //static PropertyChangedCallback ElementHeightRegisterCallback;

    //static ReflectionShader()
    //{
    //    ElementHeightRegisterCallback = ShaderEffect.PixelShaderConstantCallback(1);
    //}

    public ReflectionShader()
    {
        try
        {
            //Uri u = new Uri(@"/NavigationWithTransitions;component/Reflection.ps", UriKind.Relative);
            Uri u = new Uri(@"/PageFlip;component/Effects/Reflection.ps", UriKind.Relative);
            //Uri u = new Uri(@"Reflection.ps", UriKind.RelativeOrAbsolute);
            PixelShader = new PixelShader() { UriSource = u };
            //base.UpdateShaderValue(ElementHeightProperty);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    //public static readonly DependencyProperty ElementHeightProperty =
    //        DependencyProperty.Register("ElementHeight",
    //        typeof(int),
    //        typeof(ReflectionShader),
    //        new PropertyMetadata(100, OnElementHeightChanged));

    //static void OnElementHeightChanged(DependencyObject d, 
    //  DependencyPropertyChangedEventArgs e)
    //{
    //  ElementHeightRegisterCallback(d, e);
    //  (d as ReflectionShader).OnElementHeightChanged(
    //    (double)e.OldValue, 
    //    (double)e.NewValue);
    //}

    //protected virtual void OnElementHeightChanged(double oldValue, double newValue)
    //{
    //  PaddingBottom = newValue;
    //}

    //public double ElementHeight
    //{
    //  get { return (double)base.GetValue(ElementHeightProperty); }
    //  set { base.SetValue(ElementHeightProperty, value); }
    //}
  }
}