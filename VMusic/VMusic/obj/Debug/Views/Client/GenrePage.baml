   M S B A M L   `   `   `  ����A  =VMusic, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:VMusic.Views.ClientVMusic.Views.Client  5      #    VMusic.Views.Client.GenrePage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      3local!clr-namespace:VMusic.Views.Client     5      b ^MaterialDesignThemes.Wpf, Version=4.0.0.2422, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5        N� Title$  	GenrePage��5
      .��5      �  � RowDefinitions 5   
   �� 5      $��50 �6   �� 5      .+�5   
   ��  �� CornerRadius$ 20��6   $��3��6#   $��5��5      ���	#FF1313175      �� 5   
   $��0��6   
������$��2��5      $��Bottom=�6   �� 5   
   $��
Жанры��6   $��18(�5      $��Center=�5      $
��10 0q�5      �� 5    
   $��1��6   $��0q�5!      
������ $��Disabled=�5%      $��Hidden=�5&      ��5#      �� *	GenreList+ �� SelectedItem 5$      �� *SelectedGenre+ �� Mode$ TwoWay=���5(      �� 5)      .R�5*      � 5+      ��5.      �� 5/      .R�51      � $	��180��6   $	��150��6(   $��10 10q�64    52      �� 53      $
��2.5* �6,   �� 54      .+�57      �� $��2��6!   $ 20��58   !   ���	#FF19181F59   !   �� 5;      $��0��6     K� ZIndex$ 1��5<       $
��Left=�5=       $	��Top=�5>       $��25 15q�5?       $	��120��5@       $	��100��5A       ��5B       �� *Image+'  !MaterialDesignThemes.Wpf.PackIcon  5D         Kind$ 	MusicNote=�62   
������$��0��5F   2   $��Center=�5G   2   $��Center=�5H   2   $	��100��5I   2   $	��100��5J   2   �� 5L      
������$��2��67   $��Bottom=�6B   �� 5N      $��1��6$   $��16(�5O   $   $
��10 5q�5Q   $   $
��Wrap=�5R   $   ��5P   $   �� *Name+5R   $   5S      5U      5X      5Z      5[      