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
    �� ImageSource$ Resources/Kine.jpg��6   .��5       � $y�Visible=�6    � RowDefinitions 5!   
   �� 5"      $��1* �6   �� 5#      $��1* �6   �� 5$      $��1* �6   .+�5&   
   � $��1��6   ���	#7FFF0C00 � ColumnDefinitions 5'      �� 5(      $��1* �6#   �� 5)      $��1* �6#   �� 5*      $��1* �6#   �� 5+      $��1* �6#   �� 5,      $��1* �6#   .+�5.      �� -   5         ��Name$ B_User��5.      $��1q�6%   #��[  60   ��5/      �� 50      -   5        IB_User��$ Resources/UserConnect.png��50   2    j� Stretch$ Uniform=�6Z    J� Effect 52      3  -System.Windows.Media.Effects.DropShadowEffect  53         	Direction$	 225Q�6'      Color$ 	#FFB6C2CB��67      ShadowDepth$ 10Q�6I      
BlurRadius$ 14Q�6Z   55      �� 56      -   5        $ 	B_Patient��56      $��1q�6(   #��[  63   $��1��6U   ��57      �� 58      $ Resources/Stat.png��6!   $ Uniform=�6B    5:        5;      $	 225Q�6'   $ 	#FFB6C2CB��67   $ 10Q�6I   $ 14Q�6Z   5=      �� 5>      -   5        $ 
B_Calendar��5>      $��1q�6)   #��[  64   $��2��6V   ��5?      �� 5@      $ Resources/Calendar.png��6!   $ Uniform=�6F    5B        5C      $	 225Q�6'   $ 	#FFB6C2CB��67   $ 10Q�6I   $ 14Q�6Z   5E      �� 5G      -   5        $ 	B_Facture��5G      $��1q�6(   #��[  63   $��3��6U   ��5H      �� 5I      $ Resources/Facture.png��6!   $ Uniform=�6E    5K        5L      $	 225Q�6'   $ 	#FFB6C2CB��67   $ 10Q�6I   $ 14Q�6Z   5N      �� 5P      -   5        $ B_Exit��5P      $��1q�6%   #��[  60   $��4��6R   ��5Q      �� 5R      $ Resources/Exit.png��6!   $ Uniform=�6B    5T        5U      $	 225Q�6'   $ 	#FFB6C2CB��67   $ 10Q�6I   $ 14Q�6Z   5W      5Y       � 5Z   
   -   5          �Name$ 	F_Patient��5Z        � Source$ P_Patient.xaml?�6#   $y�Hidden=�6;   $��7��6O   $��7��6c    � 5[   
   -   5        $ F_Connexion��5[      $ Connexion.xaml?�6%   $y�Hidden=�6=   $��7��6Q   $��7��6e    � 5\   
   -	   5        $ 
F_Calendar��5\      $ Calendar.xaml?�6$   $y�Hidden=�6;   $��7��6O   $��7��6c   5^      5_      