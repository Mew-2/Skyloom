   M S B A M L   `   `   `  ����B  >MyToDo2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:MyToDo2.ViewsMyToDo2.Views  5          MyToDo2.Views.IndexView   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a3i+http://schemas.microsoft.com/xaml/behaviors  5      'localclr-namespace:MyToDo2.Views  5      _ [MaterialDesignThemes.Wpf, Version=5.2.1.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  5	      .��5      �   � RowDefinitions  5   
   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      .+�5   
   �� $��15,10q�5      $��20(�5      ��5      �� *Title+�� 5   
   $��1��6   ��6$   �� *
TaskBars+��5      �� 5      .R�5      A�  A� Columns$ 4��6"   5      ��5!      �� 5"      .R�5#      #  MaterialDesignThemes.Wpf.Card  $��10q�6*   ��66   �� *Color+*  $Microsoft.Xaml.Behaviors.Interaction   Triggers 5%      +  %Microsoft.Xaml.Behaviors.EventTrigger  5&         	EventName$ MouseUp��6-   *  $Microsoft.Xaml.Behaviors.TriggerBase   Actions. 5'   "   2  ,Microsoft.Xaml.Behaviors.InvokeCommandAction     Command 68   �� *DataContext.NavigateCommand+ �� RelativeSource ��  �� Mode$ FindAncestor=� �� AncestorType ItemsControl	   CommandParameter	 6�   �� 5(      .��5+      � .+�5,      �� $��20,15q�6)   .+�5-   "   '  !MaterialDesignThemes.Wpf.PackIcon  $��50��5.   %   $��50��5/   %   
   Kind
 50   %   �� *Icon+�� 51   "   ��6,   �� *Title+�� 52   "   $��40(�53   %   $
��Bold"�54   %   ��55   %   �� *	Content+56      �� 58      $
}�True��6%   .+�59   "   �� 
 �� Top$ 10��5:   %    �� Right$	 -50��5;   %   $	��120��5<   %   $	��120��5=   %   
������� �� CornerRadius$	 100��5?   %    J� Opacity$	 0.1Q�5@   %   �� 5A   "   $ 80��5B   %   $	 -30��5C   %   $	��120��5D   %   $	��120��5E   %   
�������$	 100��5G   %   $	 0.1Q�5H   %   5I      5J      5K      5L      5N      � 5Q   
   $��2��6    � ColumnDefinitions 5R      �� 5S      �� 5T      .+�5X        $��10q�6"   $��5q�6.   .��5Y      �   5Z      �� 5[      $
��auto �6(   �� 5\      .+�5_      �� $��Center=�5`      $��20(�5a      $
��Bold"�5b      $��待办事项��5c      �� 5e      $��Right=�5f      $	��Top=�5g      $��添加待办：��5i       /  +MaterialDesignFloatingActionMiniLightButton#��[  5j       � ToolTip$1 +MaterialDesignFloatingActionMiniLightButton��5k      ��5h      �� *
AddCommand+.��5l        $	
 Add=�62   5m      �� 5o      $��1��5p      $
��0,10q�5q      $��Stretch=�5r      ��5s      �� *Summary.TodoList+��5t      �� 5u      .R�5v   "   ]� $��False��6,   .+�5w   &   �� .+�5x   *   �� $
��Bold"�64   ��6F   �� *Title+�� 5y   *   $	 0.5Q�64   ��6B   �� *	Content+5z   '   d� 5|   &   $��Right=�5   )   ��5}   )   �� *DataContext.CompletedCommand+ �� $ FindAncestor=� ItemsControl��5~   )   �� 5   )   5�   #   5�      5�      5�      5�        5�      $��1��5�      $��10q�5�      $��5q�5�      .��5�      �   5�      �� 5�      $
��auto �6(   �� 5�      .+�5�      �� $��Center=�5�      $��20(�5�      $
��Bold"�5�      $��	备忘录��5�      �� 5�      $��Right=�5�      $	��Top=�5�      $��添加备忘：��5�      #��[  5�      $1 +MaterialDesignFloatingActionMiniLightButton��5�      ��5�      �� *
AddCommand+.��5�        $	
 Add=�62   5�      �� 5�      $��1��5�      $
��0,10q�5�      ��5�      �� *Summary.MemoList+��5�      �� 5�      .R�5�   "   �� .+�5�   &   �� $
��Bold"�60   ��6B   �� *Title+�� 5�   &   $	 0.5Q�60   ��6>   �� *	Content+5�   #   5�      5�      5�      5�      5�      5�      5�      