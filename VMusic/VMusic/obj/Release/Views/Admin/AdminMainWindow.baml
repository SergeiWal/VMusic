   M S B A M L   `   `   `  ���� A  =VMusic, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:VMusic.Views.AdminVMusic.Views.Admin  (    "VMusic.Views.Admin.AdminMainWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     2local clr-namespace:VMusic.Views.Admin     b ^MaterialDesignThemes.Wpf, Version=4.0.0.2422, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  -     � Icon$  ../../Icon/main_icon.png�� � 
ResizeMode$ NoResize=� � WindowStyle$
 None=� � AllowsTransparency . 
������  � Title$ AdminMainWindow��$	��495��$	��920��$	��495��$	��920�� � WindowStartupLocation$ CenterScreen=�.���    BackgroundColor#��[   � ColumnDefinitions �� $	��180 ��� ��  � RowDefinitions �� $��40 ��� .+� � $��1��$��1��$��2��$��Stretch=�$��15 15q�  � NavigationUIVisibility$ Hidden=����� *CurrentPage+� $��0��$��0��$��2�����	#FF131317.+��� $	��Top=�$
��Left=�.+��� $��0��$��0��$
��25 5q�$��24(�$��VMusic����   	Underline#��[ �� $	��Top=�$
��Left=�.+��� $��10 10q�  ClientControlButton#��[ ���� *SwitchToMusicList+.��� $	��160��$��32��.+��� 	 �� Orientation$	 
Horizontal=�.+��� #��[ $��32��$��32��.��'  !MaterialDesignThemes.Wpf.PackIcon  
   Kind$
 Music=�
������$��32��$��32���� $��Center=�$��18(�$	��5 0q�$��Музыка���� #��[ $��10 10q����� *SwitchToTopMusicList+.��� $	��160��$��32��.+��� $	 
Horizontal=�.+��� #��[ $��32��$��32��.��  $

 Star=�
������$��32��$��32���� $��Center=�$��18(�$	��5 0q�$��Популярное���� #��[ $��10 10q����� *SwitchToAddMusic+.��� $	��160��$��32��.+��� $	 
Horizontal=�.+��� #��[ $��32��$��32��.��  $	
 Add=�
������$��32��$��32���� $��Center=�$��18(�$	��5 0q�$��Добавить трек���� #��[ $��10 10q����� *SwitchToUserList+.��� $	��160��$��32��.+��� $	 
Horizontal=�.+��� #��[ $��32��$��32��.��  $

 User=�
������$��32��$��32���� $��Center=�$��18(�$	��5 0q�$��Пользователи��� $��0��$��2�� �� �� $
��0.5* �.+��� $��1��$��2�����	#FF131317 �� CornerRadius$ 12��$	��2 2q�� $��2��$��Center=�$��Right=�$��10q�.+���   AppControlButton#��[ ���� *CollapseWindow+.��  $
 Minus=�  ElementColor#��[ $��20��$��20���� #��[ ���� *WindowSizeChangeCommand+.��  $
 FullscreenExit=�#��[ $��20��$��20���� #��[ ���� *ExitCommand+.��  $
 PresenceExit=�#��[ $��20��$��20��