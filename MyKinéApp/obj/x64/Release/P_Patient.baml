   M S B A M L   `   `   `  ���� E  AMyKinéApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:MyKinéApp
MyKinéApp  D @AM.Widget, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null@.clr-namespace:AM.Widget.WPF;assembly=AM.WidgetAM.Widget.WPF     MyKinéApp.P_Patient   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     *localclr-namespace:MyKinéApp     >WPF.clr-namespace:AM.Widget.WPF;assembly=AM.Widget     -     N� Title$  Patient�� N� 
Background �	#7FFFFFFF.���  � RowDefinitions �� $��1* ��� $��5* � � ColumnDefinitions �� $��1* ��� $��5* ��� $��1* �.+�� -    �Name$ G_IconsHeader��$��1�����	#7F3A3939 �� $��1* ��� $��1* ��� $��1* �.+��� -    ��Name$ 	I_AddUser��$��Resources/Add.ico���� -   $ I_Searsh��$��1��$��Resources/Search.png���� -   $ I_SelectedUser��$��2��$��Resources/Selected.png��� -   $ G_Create��$��1��$��1�� �� $��1* ��� $
��1.5* ��� $��1* �����  �� ImageSource$ Resources/Page.png�� j� Stretch$ Uniform=� �� RelativeTransform V� .~��� 	 �� CenterY$		 0.5Q�
 �� CenterX$	
 0.5Q���  �� CenterY$	 0.5Q� �� CenterX$	 0.5Q���  �� CenterY$	 0.5Q� �� CenterX$	 0.5Q�T� .+�� $��1�� �� $��2* ��� $��1* ��� $��1* ��� $��1* ��� $��1* ��� $��1* ��� $��2* ��� $��1* ��� $��1* ��� $��1* � �� $	��69* ��� $	��90* ��� $
��152* ��� $
��167* �.+��� -    ��Name$ L_CreateName��$��Prénom��$��24(�$��1��$��0,0,0,10q�$��2���� -    ��Name$ TB_CreateName�� �� TextWrapping$
 Wrap=�$�� ��$��24(�$��2��$��1��$��0,0,0,10q�$��2���� -	   $ L_CreateLastName��$	��Nom��$��24(�$��2��$��0,0,0,10q�$��2���� -
   $ TB_CreateLastName��$
 Wrap=�$�� ��$��24(�$��2��$��2��$��0,0,0,10q�$��2���� -   $ L_CreateCIN��$	��CIN��$��24(�$��3��$��0,0,0,10q�$��2���� -   $ TB_CreateCIN��$
 Wrap=�$�� ��$��24(�$��2��$��3��$��0,0,0,10q�$��2���� -   $ L_CreatePhone��$��	Telephone��$��24(�$��4��$��0,0,0,10q�$��2���� -   $ TB_CreatePhone��$
 Wrap=�$�� ��$��24(�$��2��$��4��$��0,0,0,10q�$��2���� -   $ L_CreateBirthday��$	��DDN��$��24(�$��2��$��5��$��	40,0,0,10q�   AM.Widget.WPF.AMDatePicker  -     Name$ EVDP_CreateBirthday��$��24(�$��3��$��5��$��0,0,0,10q��� -   $ L_CreateGender��$
��Sexe��$��24(�$��5��$��0,0,0,10q�!  AM.Widget.WPF.SliderControl  -     Name$ 	SB_Gender��$��24(�   LeftText$ F��   	RightText$ H��   IsLeftVisible .  $��5��$��0,5,0,15q�$��1��$��75���� -   $ L_CreateAddress��$��Adresse��$��24(�$��6��$��2���� -    ��Name$ TB_CreateAddress��$��24(�
������� J� OpacityMask
 �   �$��2��$��6��$��0,0,0,10q�$��2���� -   $ L_CreateHeight��$��Taille��$��24(�$��7��$��0,0,0,10q�$��2��]� $��2��$��7��$��0,0,0,10q�$��2��.+��� -   $ TB_CreateHeight��$��24(�$��Right=� �� TextAlignment$ Right=�$��50������  �� ElementName$ S_CreateHeight�� �� Path$ Value� �� UpdateSourceTrigger$ PropertyChanged=��� -    ��Name$ S_CreateHeight�� � Maximum$	 250Q� � Minimum$	 150Q�$��24(�$��30��$��Center=�  �� TickPlacement$  BottomRight=�! �� TickFrequency$! 1Q�" �� IsSnapToTickEnabled" . # J� RenderTransformOrigin$# 0.504,0.977&��� -   $ L_CreateWeight��$��Poids��$��24(�$��8��$��0,0,0,10q�$��2��]� $��2��$��8��$��0,0,0,10q�$��2��.+��� -   $ TB_CreateWeight��$��24(�$��Right=�$ Right=�$��50������ $ S_CreateWeight��$ Value�$ PropertyChanged=��� -   $ S_CreateWeight��$	 150Q�$ 15Q�$��24(�$��30��$��Center=�$  BottomRight=�$! 1Q�" . $# 0.504,0.977&��� -   $ image��$��Right=�$	��124��$��	0,0,10,10q�$��Bottom=�$	��151��$��Resources/Add.ico��$��2��$��1��� -   $ G_Search��$��1��$��1�����	#7FD1CFCF �� $��1* ��� $��5* �.+�&   System.Windows.Controls.DataGrid  -   $  Name$$ DG_Patients��$��1��$��24(�%   
IsReadOnly% . �  �� $��1* ��� $��1* � �� $��1* ��� $��1* ��� $
��1.5* ��� $��1* �.+��� -   $ L_Name��$��Prenom��$��Center=�$��Center=�$��26(�$
��Bold"��� -   $ TB_Name��$��10q�$
 Wrap=�$�� ��$��26(�$��1���� -    $ 
L_LastName��$��Nom de famille��$��Center=�$��Center=�$��26(�$
��Bold"�$��1���� -!   $ TB_LastName��$��10q�$
 Wrap=�$�� ��$��1��$��1��$��26(��� -"   $ L_Phone��$��Numero de telephone��$��Center=�$��Center=�$��26(�$
��Bold"�$��2���� -#   $ TB_Phone��$��10q�$
 Wrap=�$�� ��$��26(�$��2��$��1���� -$   $ L_CIN��$	��CIN��$��Center=�$��Center=�$��26(�$
��Bold"�$��3���� -%   $ TB_CIN��$��10q�$
 Wrap=�$�� ��$��26(�$��3��$��1��� -&   $ G_SelectedPatient��$��1��$��1�����	#7FD1CFCF �� $��70 ��� $��1* ��� $��1* ��� $��1* �.+��� -'   $ 
L_Patients��$�� ��$��26(�$��0q�$��Center=�$��Center=�{�.  $
��Bold"����	#FF2C2C2C�� -(   & ��Name$& B_PatientsUpdate��$��Update��$��26(�$��0,0,10,0q�{�.  $��Right=�$��Center=�$	��140��$��50��$
��Bold"����� ' �� EndPoint$' 0.5,1&�( �� 
StartPoint$( 0.5,0&�.��� ) � Color$) 	#FFACFF9F��* � Offset$* 1Q�� $) 	#FF187C08���� -)   $ L_Diagnostic��$��
Diagnostic��$��26(�$��0q�$
��Left=�$	��Top=�$	��140��$��1��$
��Bold"��� -*   $ TB_Diagnostic��$
 Wrap=�$�� ��
�������$��26(�$��0,50,0,0q�$��1��{�.  $
��Bold"�+ �� 	MaxLength$
+ 4501���� -+   $ L_Bilan��$��Bilan��$��2��$��26(�$
��Left=�$	��Top=�$	��140��$
��Bold"��� -,   $ TB_BalanceSheet��$
 Wrap=�$�� ��$��2��
�������$��26(�$��0,51,0,0q�{�.  $
��Bold"�$
+ 4501���� --   $ L_Traitement��$��
Traitement��$��26(�$
��Left=�$	��140��$��0q�$	��Top=�$��3��$
��Bold"��� -.   $ TB_Treatment��$
 Wrap=�$�� ��
�������$��26(�$��0,50,0,0q�$��3��{�.  $
��Bold"�$
+ 4501���� -/   $& B_PatientDelete��$��Delete��$��26(�$��0,10,200,10q�$��Right=�$y�Hidden=�$��Center=�$	��140��$��50��$
��Bold"����� $' 0.5,1&�$( 0.5,0&�.��� $) 	#FFFF2E00��$* 1Q�� $) 	#FF891C04���� -0   $ I_Close��$��1��$��Right=�$��50��$	��Top=�$��50��$��Resources/Close.png��$��	0,10,10,0q�$��2��$��2��