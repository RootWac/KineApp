   M S B A M L   `   `   `  ����E  AMyKinéApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:MyKinéApp
MyKinéApp  5   	       MyKinéApp.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:MyKinéApp     5   	     � Title$
  Menu��5   	   $	��350��6   $	��525��6#    � WindowStyle$
 None=�6/    � WindowState$ 	Maximized=�6B    � Icon$ Resources/KineApp.ico��6Z    � Language$ en�6w    �� 
Attributes$ en��6�    � 	Resources 5	         MyButton �� 
TargetType �� Property	 �� Value
 �� 
TargetType �� Triggers P� Property P� Value��%�  &	        �� 5
   
   " ��6!   .�5      ��  �@�OverridesDefaultStyle	 . 66   �� 5       �@�Cursor$
	 Hand��6'   �� 5       � ��	 5      �� 5      "
 ��6&   .R�5      �� $��border��6!   $��0q�6/   
���   �#��z��6W   ._�5      �� $��Center=�6/   $��Center=�6L   5       5      P� 5       �@J�IsMouseOver . . �5   "   ��  �@J�Opacity$		 0.8Q�6<   5      5      5      5      5      ��5      �� 5   
    �� ImageSource$ Resources/Kine.jpg��6    �� Opacity$ 0.3                     Q�66   .��5!      � $y�Visible=�6    � RowDefinitions 5"   
   �� 5#      $��1* �6   �� 5$      $��1* �6   �� 5%      $��1* �6   .+�5'   
   � $��1��6   ���	#7FFF0C00 � ColumnDefinitions 5(      �� 5)      $��1* �6#   �� 5*      $��1* �6#   �� 5+      $��1* �6#   �� 5,      $��1* �6#   �� 5-      $��1* �6#   .+�5/      �� -   5         ��Name$ B_User��5/      $��1q�6%   #��[  60   ��50      �� 51      -   5        IB_User��$ Resources/UserConnect.png��51   2    j� Stretch$ Uniform=�6Z    J� Effect 53      3  -System.Windows.Media.Effects.DropShadowEffect  54         	Direction$	 225Q�6'      Color$ 	#FFB6C2CB��67      ShadowDepth$ 10Q�6I      
BlurRadius$ 14Q�6Z   56      �� 57      -   5        $ 	B_Patient��57      $��1q�6(   #��[  63   $��1��6U   ��58      �� 59      $ Resources/Stat.png��6!   $ Uniform=�6B    5;        5<      $	 225Q�6'   $ 	#FFB6C2CB��67   $ 10Q�6I   $ 14Q�6Z   5>      �� 5?      -   5        $ 
B_Calendar��5?      $��1q�6)   #��[  64   $��2��6V   ��5@      �� 5A      $ Resources/Calendar.png��6!   $ Uniform=�6F    5C        5D      $	 225Q�6'   $ 	#FFB6C2CB��67   $ 10Q�6I   $ 14Q�6Z   5F      �� 5H      -   5        $ 	B_Facture��5H      $��1q�6(   #��[  63   $��3��6U   ��5I      �� 5J      $ Resources/Facture.png��6!   $ Uniform=�6E    5L        5M      $	 225Q�6'   $ 	#FFB6C2CB��67   $ 10Q�6I   $ 14Q�6Z   5O      �� 5Q      -   5        $ B_Exit��5Q      $��1q�6%   #��[  60   $��4��6R   ��5R      �� 5S      $ Resources/Exit.png��6!   $ Uniform=�6B    5U        5V      $	 225Q�6'   $ 	#FFB6C2CB��67   $ 10Q�6I   $ 14Q�6Z   5X      5Z       � 5[   
   -   5          �Name$ 	F_Patient��5[        � Source$ P_Patient.xaml?�6#   $y�Hidden=�6;   $��7��6O   $��7��6c    � 5\   
   -   5        $ F_Connexion��5\      $ Connexion.xaml?�6%   $y�Hidden=�6=   $��7��6Q   $��7��6e    � 5]   
   -	   5        $ 
F_Calendar��5]      $ Calendar.xaml?�6$   $y�Hidden=�6;   $��7��6O   $��7��6c    � 5^   
   -
   5        $ 	F_Billing��5^      $ Billing.xaml?�6#   $y�Hidden=�69   $��7��6M   $��7��6a   5`      5a      