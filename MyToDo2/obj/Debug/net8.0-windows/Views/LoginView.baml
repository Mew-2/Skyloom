   M S B A M L   `   `   `  ����B  >MyToDo2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:MyToDo2.ViewsMyToDo2.Views  5          MyToDo2.Views.LoginView   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a3i+http://schemas.microsoft.com/xaml/behaviors  5      'localclr-namespace:MyToDo2.Views  5      _ [MaterialDesignThemes.Wpf, Version=5.2.1.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  5	      ] YPrism.DryIoc.Wpf, Version=9.0.537.60525, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59V	 RPrism.Wpf, Version=9.0.537.60525, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59&prismhttp://prismlibrary.com/  	 5
      $	��728��5      $	��450��5       	 Prism.Dialogs.Dialog    WindowStyle  5      �� 5   
    �� 
TargetType" �6   .�5      ��  �� Property � �� �� Value$	 600��6&   �� 5       � ��$	 350��6'   �� 5       �@�SizeToContent$ WidthAndHeight=�6.   �� 5       �@�
ResizeMode$ NoResize=�6+   �� 5       �@ WindowStartupLocation$ CenterScreen=�6C   5      .��5      � 
������� � ColumnDefinitions 5   
   �� 5      $
��1.5* �6   �� 5      .+�5   
   �� $��50q�6   $��/Images/Login.png��6   '  !MaterialDesignThemes.Wpf.Snackbar  5!   
   -   5          Name$ LoginSnackBar��5"      $��2��5#         MessageQueue 5$      4  .MaterialDesignThemes.Wpf.MessageQueueExtension  7  1MaterialDesignThemes.Wpf.Transitions.Transitioner  5&   
   $��1��6&    �� SelectedIndex 66   �� *SelectedIndex+.@�5'      <  6MaterialDesignThemes.Wpf.Transitions.TransitionerSlide  .��5(      ]� $��15q�6   $��Center=�6(   .+�5)      �� $
��0,10q�5*      $	��Top=�5+      $��22(�5,      $
��Bold"�5-      $��欢迎使用��5.      �� 50      $
��0,10q�51      )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ 请输入账号��52      $	��Top=�53      ��54      �� *	Account+	 �� UpdateSourceTrigger$	 PropertyChanged=�G� 55      $
��0,10q�56      $ 请输入密码��57      $	��Top=�59      0  *MaterialDesignThemes.Wpf.PasswordBoxAssist
   Password
 58      �� *
Password+$	 PropertyChanged=�59      �� 5;      $��Login��5=      $��登录系统��5>      $	��Top=�5?       �� 	IsDefault$
 True��5@      ��5<      �� *ExcuteCommand+5@      ]� 5B      $	��0,5q�6    $��False��6-   .+�5C      �� $��注册账号��6$   *  $Microsoft.Xaml.Behaviors.Interaction   Triggers 5D      +	  %Microsoft.Xaml.Behaviors.EventTrigger	  5E   "    	  	EventName$ MouseLeftButtonDown��61   *
  $Microsoft.Xaml.Behaviors.TriggerBase 
  Actions. 5F   &   2  ,Microsoft.Xaml.Behaviors.InvokeCommandAction     CommandParameter$ Go��6^      Command 6<   �� *ExcuteCommand+6^   5G   #   5I      �� 5J      $��Right=�6$   $��忘记密码？��6;   5K      5L      5M        5O      .��5P      ]� $��15q�6   $��Center=�6(   .+�5Q      �� $
��0,10q�5R      $	��Top=�5S      $��22(�5T      $
��Bold"�5U      $��注册账号��5V      �� 5X      $
��0,10q�5Y      $ 请输入账号��5Z      $	��Top=�5[      ��5\      �� *RegisterUser.Account+�� 5^      $
��0,10q�5_      $ 请输入用户名��5`      $	��Top=�5a      ��5b      �� *RegisterUser.Username+G� 5d      $
��0,10q�5e      $ 请输入密码��5f      $	��Top=�5h      
 5g      �� *RegisterUser.Password+5h      G� 5j      $
��0,10q�5k      $ 请再次输入密码��5l      $	��Top=�5n      
 5m      �� *RegisterUser.ConfirmPassword+5n      �� 5p      $
��0,10q�5q      $��Register��5s      $��注册账号��5t      $	��Top=�5u      ��5r      �� *ExcuteCommand+5u      �� 5w      $��Return��5y      $��返回登录��5z      $	��Top=�5{      ��5x      �� *ExcuteCommand+5{      5|      5}      5~      5      5�      