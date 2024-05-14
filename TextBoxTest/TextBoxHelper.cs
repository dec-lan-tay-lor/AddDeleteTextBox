using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace TextBoxTest
{
    /// <summary>
    /// 测试附加属性
    /// </summary>
    public class TextBoxHelper
    {

        #region CornerRadius
        public static CornerRadius GetCornerRadius(DependencyObject obj)
        {
            return (CornerRadius)obj.GetValue(CornerRadiusProperty);
        }

        public static void SetCornerRadius(DependencyObject obj, CornerRadius value)
        {
            obj.SetValue(CornerRadiusProperty, value);
        }

        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.RegisterAttached("CornerRadius", typeof(CornerRadius), typeof(TextBoxHelper));
        #endregion


        #region IsClearButtonVisible
        public static bool GetIsClearButtonVisible(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsClearButtonVisibleProperty);
        }

        public static void SetIsClearButtonVisible(DependencyObject obj, bool value)
        {
            obj.SetValue(IsClearButtonVisibleProperty, value);
        }

        public static readonly DependencyProperty IsClearButtonVisibleProperty =
            DependencyProperty.RegisterAttached("IsClearButtonVisible", typeof(bool), typeof(TextBoxHelper), new PropertyMetadata(OnTextBoxHookChanged));
       

        private static void OnTextBoxHookChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var textbox = d as TextBox;
            textbox.RemoveHandler(Button.ClickEvent, new RoutedEventHandler(ClearButtonClicked));
            textbox.AddHandler(Button.ClickEvent, new RoutedEventHandler(ClearButtonClicked));
        }

        private static void ClearButtonClicked(object sender, RoutedEventArgs e)
        {
            var button = e.OriginalSource as Button;

            if (button == null || button.Name != "PART_BtnClear")
                return;

            var textbox = sender as TextBox;

            if (textbox == null)
                return;

            textbox.Text = null;
        }

        #endregion




        public static bool GetIsAddButtonVisible(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsAddButtonVisibleProperty);
        }

        public static void SetIsAddButtonVisible(DependencyObject obj, bool value)
        {
            obj.SetValue(IsAddButtonVisibleProperty, value);
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsAddButtonVisibleProperty =
            DependencyProperty.RegisterAttached("IsAddButtonVisible", typeof(bool), typeof(TextBoxHelper), new PropertyMetadata(OnTextChanged));





        public static bool GetIsRemoveButtonVisible(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsRemoveButtonVisibleProperty);
        }

        public static void SetIsRemoveButtonVisible(DependencyObject obj, bool value)
        {
            obj.SetValue(IsRemoveButtonVisibleProperty, value);
        }

      
        public static readonly DependencyProperty IsRemoveButtonVisibleProperty =
            DependencyProperty.RegisterAttached("IsRemoveButtonVisible", typeof(bool), typeof(TextBoxHelper), new PropertyMetadata(OnTextChanged));



        private static void OnTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var textbox = d as TextBox;

            if ((bool)e.NewValue == true)
            {
                textbox.MouseWheel += Textbox_MouseWheel; 

            }
            else
            {
                textbox.MouseWheel -= Textbox_MouseWheel; 

            }

            textbox.RemoveHandler(Button.ClickEvent, new RoutedEventHandler(ButtonClicked));
            textbox.AddHandler(Button.ClickEvent, new RoutedEventHandler(ButtonClicked));
        }

        /// <summary>
        /// 鼠标滚轮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Textbox_MouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            var textbox = sender as TextBox;
            double temp;
            bool result = double.TryParse(textbox.Text, out temp);

            if (result == true)
            {
                if (e.Delta > 0)
                {
                    textbox.Text = (temp + 1).ToString();

                }
                else
                {
                    textbox.Text = (temp - 1).ToString();
                }
                textbox.Select(textbox.Text.Length, 0);//光标设置到文本尾部
            }

        }

        private static void ButtonClicked(object sender, RoutedEventArgs e)
        {
            var button = e.OriginalSource as RepeatButton;

            if (button == null)
                return;

            var textbox = sender as TextBox;

            if (textbox == null)
                return;

            double temp;
            bool result = double.TryParse(textbox.Text, out temp);
            if (result == true)
            {
                if (button.Name == "PART_BtnAdd")
                {
                    textbox.Text = (temp + 1).ToString();
                }
                else
                {
                    textbox.Text = (temp - 1).ToString();
                }
                textbox.Focus();
                textbox.Select(textbox.Text.Length, 0);
            }

        }





    }
}
