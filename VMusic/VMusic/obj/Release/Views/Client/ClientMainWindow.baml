   M S B A M L   `   `   `  ���� A  =VMusic, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:VMusic.Views.ClientVMusic.Views.Client  *    $VMusic.Views.Client.ClientMainWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     3local!clr-namespace:VMusic.Views.Client     b ^MaterialDesignThemes.Wpf, Version=4.0.0.2422, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  -     � Icon$  ../../Icon/main_icon.png�� � 
ResizeMode$ NoResize=� � WindowStyle$
 None=� � AllowsTransparency . 
������  � Title$ ClientMainWindow��$	��600��$
��1024��$	��600��$
��1024�� � WindowStartupLocation$ CenterScreen=� J� InputBindings �� 
 �� Key$
 Left�� �� Command �� *
PrevSong+�� $ Right�� �� *
NextSong+.���    BackgroundColor#��[  	 � ColumnDefinitions	 �� $	��200 ��� �� 
 � RowDefinitions
 �� $��40 ��� �� $��72 �.+� � $��1��$��1��$��2��  � NavigationUIVisibility$ Hidden=�$��15 15q����� *CurrentPage+� $��0��$��2��	 �� $��2* ��� $
��1.5* ��� .+��� $��1��$��2�����	#FF131317 �� CornerRadius$ 12��$	��2 2q��� $��1�� �� Orientation$ 
Horizontal=�$	��Top=�$
��Left=�$��5q�$��32��.+���   AppControlButton#��[ ���� *
FindSong+.��'  !MaterialDesignThemes.Wpf.PackIcon     Kind$ Search=�
������$��24��$��24���� $��12(�$��18��$	��120��$��0 0 5 2q����	#FF131317���� *FindSongString+ �� Mode$ TwoWay=�.��� $��2��$��Center=�$��Right=�$��0 0 10 0q�.+���   ClientControlButton#��[ ���� *CollapseWindow+.��  $ Minus=�  ElementColor#��[ $��20��$��20���� #��[ ���� *WindowSizeChangeCommand+.��  $ FullscreenExit=�#��[ $��20��$��20���� #��[ ���� *ExitCommand+.��  $ PresenceExit=�#��[ $��20��$��20��� $��0��$��0��$��2�����	#FF131317.+��� $	��Top=�$
��Left=�.+��� $��0��$��0��$
��25 5q�$��24(�$��VMusic����   	Underline#��[ �� $	��Top=�$
��Left=�.+��� #��[ $��10 10q����� *SwitchToHomePage+.��� $	��200��$��32��.+��� $ 
Horizontal=�.+��� #��[ $��32��$��32��.��  $
 Home=�
������$��32��$��32���� $��Center=�$��18(�$	��5 0q�$��Главная���� #��[ $��10 10q����� *SwitchToTopMusicPAge+.��� $	��200��$��32��.+��� $ 
Horizontal=�.+��� #��[ $��32��$��32��.��  $
 Star=�
������$��32��$��32���� $��Center=�$��18(�$	��5 0q�$��Популярное���� #��[ $��10 10q����� *SwitchToGenrePage+.��� $	��200��$��32��.+��� $ 
Horizontal=�.+��� #��[ $��32��$��32��.��  $ 	MusicNote=�
������$��32��$��32���� $��Center=�$��18(�$	��5 0q�$��
Жанры���� #��[ $��10 10q����� *SwitchToPlaylistsPage+.��� $	��200��$��32��.+��� $ 
Horizontal=�.+��� #��[ $��32��$��32��.��  $ PlaylistMusic=�
������$��32��$��32���� $��Center=�$��18(�$	��5 0q�$��Плэйлисты���� #��[ $��10 10q����� *SwitchToCreatePlaylistPage+.��� $	��200��$��32��.+��� $ 
Horizontal=�.+��� #��[ $��32��$��32��.��  $ PlaylistAdd=�
������$��32��$��32���� $��Center=�$��18(�$	��5 0q�$!��Новый плэйлист���� #��[ $��10 10q����� *SwitchToSettingPage+.��� $	��200��$��32��.+��� $ 
Horizontal=�.+��� #��[ $��32��$��32��.��  $ Settings=�
������$��32��$��32���� $��Center=�$��18(�$	��5 0q�$��Настройки��� $��2��$��0��$��3�����	#FF19181F	 �� $	��300 ��� �� $	��200 �.+�� $��0��$��0��	 �� �� $��2* �.+��� $��0�� K� ZIndex$ 1��$��Center=�$��Center=�$��48��$��48������  �� Path$ CurrentSong.Image��� $��1��$	��2 7q�.+��� $��18(�$
��Wrap=����� $ CurrentSong.Name��� $��18(�$
��Wrap=����� $ CurrentSong.Author��� $��1��  VerticalLine#��[ $��Right=��� $��1��#��[ $
��Left=�$��32��$��32��$
��10 5q����� *
LikeSong+.��  $ Heart=�
������$��32��$��32���� $��1��$��Center=�$��Center=�.+�� $��Center=�.+��� #��[ $��32��$��32��$	��2 2q����� *
PrevSong+.��  $ PreviousTitle=�
������$��32��$��32���� -   $��PlayStopButton��#��[ $��32��$��32��$	��2 2q����� *StopAndPlay+.��  
������$��32��$��32�� �� *PlayStopButton+�� #��[ $��32��$��32��$	��2 2q����� *
NextSong+.��  $ 	NextTitle=�
������$��32��$��32��� $	��360��
������ � Minimum$ 0Q� � Maximum �� *Player.Duration+ � Value �� *Player.Progress+� $��2��$
��Left=�$��Center=�.+��� #��[ �� #��[ $��32��$��32��$	��7 2q����� *SwitchToCurrentSongList+.��  $ 
QueueMusic=�
������$��32��$��32���� -   $��VolumeButton��#��[ $��32��$��32��$	��2 2q����� *VolumeOnOff+.��  
������$��32��$��32�� �� *VolumeButton+�� $��Center=�$	��100��$	��2 0q�$ 1Q� �� *Player.Volume+��Z� 