   M S B A M L   `   `   `  ����A  =VMusic, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:VMusic.Views.AdminVMusic.Views.Admin  5   	   (    "VMusic.Views.Admin.AdminMainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   2local clr-namespace:VMusic.Views.Admin     5   	   b ^MaterialDesignThemes.Wpf, Version=4.0.0.2422, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   -   5          � Icon$  ../../Icon/main_icon.png��5   	    � 
ResizeMode$ NoResize=�5
   	    � WindowStyle$
 None=�5   	    � AllowsTransparency . 
������  � Title$ AdminMainWindow��5   	   $	��495��5   	   $	��920��6   $	��495��5   	   $	��920��6    � WindowStartupLocation$ CenterScreen=�5   	   .��5      �    BackgroundColor#��[  6    � ColumnDefinitions 5   
   �� 5      $	��180 �6   �� 5      �� 5       � RowDefinitions 5   
   �� 5      $��40 �6   �� 5      .+�5    
    � $��1��6   $��1��5!      $��2��6    $��Stretch=�5"      $��15 15q�5#        � NavigationUIVisibility$ Hidden=�5$      ��5%      �� *CurrentPage+� 5'   
   $��0��6   $��0��6   $��2��5(      ���	#FF131317.+�5+      �� $	��Top=�6   $
��Left=�5,      .+�5-      �� $��0��6   $��0��6)   $
��25 5q�5.      $��24(�5/      $��VMusic��50      �� 51        	Underline#��[ 6   �� 52      $	��Top=�6   $
��Left=�53      .+�54      �� $��10 10q�6     ClientControlButton#��[ 55      ��56      �� *SwitchToMusicList+.��57      � $	��160��6   $��32��58      .+�59      �� 	 �� Orientation$	 
Horizontal=�6)   .+�5:   "   �� #��[ 6)   $��32��5;   )   $��32��64   .��5<   &   '  !MaterialDesignThemes.Wpf.PackIcon  
   Kind$
 Music=�6>   
������$��32��5=   :   $��32��6E   5>   #   �� 5?   "   $��Center=�6,   $��18(�5@   (   $	��5 0q�5A   (   $��Музыка��5B   (   5C      5D      5E      �� 5F      #��[ 6   $��10 10q�5G      ��5H      �� *SwitchToTopMusicList+.��5J      � $	��160��6   $��32��5K      .+�5L      �� $	 
Horizontal=�6)   .+�5M   "   �� #��[ 6)   $��32��5N   )   $��32��64   .��5O   &     $

 Star=�6>   
������$��32��5P   :   $��32��6E   5Q   #   �� 5R   "   $��Center=�6,   $��18(�5S   (   $	��5 0q�5T   (   $��Популярное��5U   (   5V      5W      5X      �� 5Y      #��[ 6   $��10 10q�5Z      ��5[      �� *SwitchToAddMusic+.��5\      � $	��160��6   $��32��5]      .+�5^      �� $	 
Horizontal=�6)   .+�5_   "   �� #��[ 6)   $��32��5`   %   $��32��60   .��5a   &     $	
 Add=�6>   
������$��32��5b   :   $��32��6E   5c   #   �� 5d   "   $��Center=�6,   $��18(�5e   (   $	��5 0q�5f   (   $��Добавить трек��5g   (   5h      5i      5j      �� 5k      #��[ 6   $��10 10q�5l      ��5m      �� *SwitchToUserList+.��5n      � $	��160��6   $��32��5o      .+�5p      �� $	 
Horizontal=�6)   .+�5q   "   �� #��[ 6)   $��32��5r   %   $��32��60   .��5s   &     $

 User=�6>   
������$��32��5t   :   $��32��6E   5u   #   �� 5v   "   $��Center=�6,   $��18(�5w   (   $	��5 0q�5x   (   $��Пользователи��5y   (   5z      5{      5|      5}      5~      5�      � 5�   
   $��0��6   $��2��6    5�      �� 5�      �� 5�      $
��0.5* �6#   .+�5�      �� $��1��6   $��2��5�      ���	#FF131317 �� CornerRadius$ 12��5�      $	��2 2q�5�      5�      � 5�      $��2��6   $��Center=�5�      $��Right=�5�      $��10q�5�      .+�5�      ��   AppControlButton#��[ 6   ��5�      �� *CollapseWindow+.��5�        $
 Minus=�6.     ElementColor#��[ 6;   $��20��5�   .   $��20��69   5�      �� 5�      #��[ 6   ��5�      �� *WindowSizeChangeCommand+.��5�        $
 FullscreenExit=�6.   #��[ 6D   $��20��5�   .   $��20��69   5�      �� 5�      #��[ 6   ��5�      �� *ExitCommand+.��5�        $
 PresenceExit=�6.   #��[ 6B   $��20��5�   .   $��20��69   5�      5�      5�      5�      5�      