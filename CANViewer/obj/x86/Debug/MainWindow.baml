   M S B A M L   `   `   `  ����D  @CANViewer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:CANViewer	CANViewer  5   	       CANViewer.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   )localclr-namespace:CANViewer     5   	     � Title$  
MainWindow��5   	   $	��350��6   $	��525��6)    � 	Resources 5      %   CANViewer.BoolToStringConverter   FlagConverter�   ~CANViewer.BoolToValueConverter`1[[System.String, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]   
FalseValue   	TrueValue#   CANViewer.BoolToBoolConverter  InverseConverter�   CANViewer.BoolToValueConverter`1[[System.Boolean, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]   
FalseValue   	TrueValue��%\   &	        &	 '       5   
   $  ��6<   $ +��6J     5   
    .  .  6O   5	      .��5
      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   6,   �� 5      $��* �6   6)    � ColumnDefinitions 5   
   �� 5      $��* �6   6+   .+�5   
   o� .@�5      m� $
��File��6   .@�5      m� -   5        $��Export��5      6+   5      m� 5      $	��CAN��6   .@�5      m� -   5        $��Start��5      {�6*   �� *	IsWorking+ �� 	Converter# [ 6w   m� 5      -   5        $
��Stop��5      {�6)   �� *	IsWorking+6I   5      5      &   System.Windows.Controls.DataGrid  5   
   -   5        $��messageGrid��5      $��1��6&   $��0��63   	   AutoGenerateColumns	 .  ��6C   �� *Data+
   Columns
 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$
 Time��6%   1  +System.Windows.Controls.DataGridBoundColumn   Binding 63   �� *
DateTime+ �� StringFormat$! {0:yyyy.MM.dd hh:mm:ss.fff}��  5      $ Message��6%    66   �� *	Message+  5      $ Source��6%    65   �� *Parsed.Source+  5       $ Target��6%    65   �� *Parsed.Target+  5!      $ R��6%    60   �� *Parsed.Response+# [    5"      $ E��6%    60   �� *Parsed.Exception+# [    5#      $ F��6%    60   �� *Parsed.Reserved+  5$      $
 Type��6%    63   �� *Parsed.Type+  5%      $ Function��6%    67   �� *Parsed.Function+  5&      $ Temperature��6%    6:   �� *Parsed.Temperature+  5'      $ Priority��6%    67   �� *Parsed.Priority+  5(      $ Manager��6%    66   �� *Parsed.Manager+  5)      $ State��6%    64   �� *Parsed.State+  5*      $ Boiler Type��6%    6:   �� *Parsed.BoilerType+  5+      $ Power��6%    64   �� *Parsed.Power+  5,      $ 	Work Time��6%    68   �� *Parsed.WorkTime+  5-      $ Datetime��6%    67   �� *Parsed.Datetime+$! {0:yyyy.MM.dd hh:mm:ss.fff}��  5.      $ Error��6%    64   �� *Parsed.Error+50      51      52      