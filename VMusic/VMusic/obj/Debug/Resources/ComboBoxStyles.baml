   M S B A M L   `   `   `  ����A  =VMusic, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:VMusicVMusic  5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089   ComboBoxNormalBorderBrush  �� Color ! ComboBoxNormalBackgroundBrush # ComboBoxDisabledForegroundBrush # ComboBoxDisabledBackgroundBrush  ComboBoxDisabledBorderBrush �� 
TargetType   ComboBoxToggleButtonTemplate � ColumnDefinitions �� CornerRadius �� Triggers P� Property P� Value �� 
TargetName �� Property	 �� Value  ComboBoxFlatStyle
 �� 
TargetType �� 	ClickMode d� 	IsChecked �� Path �� RelativeSource �� Mode J� IsHitTestVisible �� SelectionBoxItem �� SelectionBoxItemTemplate �� 
IsReadOnly �� 
IsReadOnly � AllowsTransparency �� IsDropDownOpen J� SnapsToDevicePixels � ActualWidth �� MaxDropDownHeight  BackgroundColor-   'System.Windows.Input.KeyboardNavigation    DirectionalNavigation�� 5      D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    :x,http://schemas.microsoft.com/winfx/2006/xaml     5      localclr-namespace:VMusic  5      %N  &	        &	 #     &	 F     &	 d     &	 �     &	 �     7   7  7  7  7  7  7  &	     7  7  7   7  �� 5      $  	#FF131317��68   �� 5      $  	#FF131317��6<   �� 5      $
  #888��6>   �� 5	      $  	#FF131317��6>   �� 5
      $
  #888��6:   �� 5      " d�6   .R�5   
   �  5      �� 5      �� 5      $��20 �6#   .+�5      �� $��Border��6)   $��2��6   8��  $ 0��5      $��
1, 1, 1, 1q�6    8�� 5      �� 5      $��ButtonBorder��6L   $��1��6   $��
1, 1, 1, 1q�6%   ���#444$ 
0, 0, 0, 0��5      $��
0, 0, 0, 0q�6)   8�� 5      F� 5      $��Arrow��6   $��1��6    =����        �    �� � 	= ��[ � z �� � z     � 	=  	= 	$��Center=�5      ���#444$��Center=�5      5       5   
   P� 5        �@J�IsMouseOver . . �5!      �� $ ButtonBorder��65    � ��	 �	#FF1313176O   5"      P� 5#       �@d�	IsChecked$
 TrueY�68   . �5$      �� $ ButtonBorder��65    � ��	 �	#FF1313176O   �� 5%      $ Arrow��6/    � ��
	 ����6B   5&      P� 5'       � {� .  . �5(      �� $ Border��65   8	   � ��6I   �� 5)      $ ButtonBorder��65   8	   � ��6O   �� 5*      $ ButtonBorder��67   8	   � ��6Q   �� 5+      8	   � ��6;   �� 5,      $ Arrow��6/    � ��	 �#9996B   5-      5/      �� 51      "
 ��6'   .�52   
   ��  �@J�SnapsToDevicePixels	 . 6:   �� 53   
    �@�OverridesDefaultStyle	 . 6C   �� 54   
    � ��$
	 Auto=�6G   �� 55   
    � ��$
	 Auto=�6E   �� 56   
    � ��	 . 6:   �� 57   
    � ��
	 �   �63   �� 58   
    �@�FocusVisualStyle	 6>   Z� �� 59   
    � ��	 5:      �� 5;      " ��6"   .R�5<      � .+�5=      d� $��ToggleButton��6'   $��2��6;   $ Press=�5>      |�.  8��   5?      �� $ IsDropDownOpen� �� *TemplatedParent+$ TwoWay=�5@      �� 5B      $��ContentSite��6+   $��5, 3, 23, 3q�6>    .  $
��Left=�5C      $��Center=�6:   #��z 5D      #��z 5E      #��z��5F      �� 5G      $��PART_EditableTextBox��6"   $��3, 3, 23, 3q�6>   # z 5H      $y�Hidden=�5I      
������ $
��Left=�5J      $��Center=�61   |�. .��5K   (   ��5L      �� 5M   "   " ��62   .R�5N   &   �� $��PART_ContentHost��6-   |�.  6E   5O   #   .��5P   0   5Q      � 5S      $��Popup��6    $��Bottom=�6-   |�.   . #��z 5U      $��Slide=�5V      .��5W      � $��DropDown��6#    . #��z 5X      #��z 5Y      .+�5Z   "   �� $��DropDownBorder��6)   8�� $��
0, 1, 0, 0q�6m   $ 0��5[      $��1,1,1,1q�6*   8�� 5\      �� 5]   "   $��4q�6/    . .��5^   &   �� $ 	Contained=�65   5_   #   5`      5a      5b       5c      P� 5d       �@��HasItems .  . �5e      �� $ DropDownBorder��6K    � ��$	 95��6g   5f      P� 5g       � {� .  . �5h      �� 8	   � ��6G   5i      P� 5j       �@��
IsGrouping . . �5k      ��  � ��	 .  6N   5l      P� 5m       �@��
IsEditable . . �5n      ��  �@  	IsTabStop	 .  6M   �� 5o      $ PART_EditableTextBox��6E    � y�$	 Visible=�6g   �� 5p      $ ContentSite��6E    � y�$	 Hidden=�6^   5q      5s      5u      5v      5x      