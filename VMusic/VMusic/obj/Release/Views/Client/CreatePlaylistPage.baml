   M S B A M L   `   `   `  ���� A  =VMusic, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:VMusic.Views.ClientVMusic.Views.Client  ,    &VMusic.Views.Client.CreatePlaylistPage   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     3local!clr-namespace:VMusic.Views.Client     b ^MaterialDesignThemes.Wpf, Version=4.0.0.2422, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes    N� Title$  CreatePlaylistPage��.���  � ColumnDefinitions �� ��  � RowDefinitions �� �� $��80 ��� $��2* �.+���  �� CornerRadius$ 20��$��3��$��5�����	#FF131317�� $��0��$��0��$��Stretch=�$��Stretch=�$��20 20q� K� ZIndex$ 1������ *PlaylistImage+'  !MaterialDesignThemes.Wpf.PackIcon     Kind$ Image=�$��0��$��0��$��Center=�$��Center=�
������$	��100��$	��100���� $��0��$��1��$��2��$��0 0 0 10q�.+��� $
��0 10q�.+��� $
��Left=�Название�� -    ��Name$
 Name��$
��Left=����� *PlaylistName+ �� Mode$ TwoWay=���    	Underline#��[  $
��Left=���   EnterButton#��[ $
��Left=�$
��0 10q�$��48������ *
AddImage+.���� Изображание�� #��[ $
��Left=�$��48������ *SavePlaylist+.���� Сохранить�� $��12(�$	��2 2q�$
��Wrap=����� *InfoMessage+� $��10 20q�$��0��$��1��$��Bottom=�.+���   AppControlButton#��[ ���� *
FindSong+.��  $ Search=�
������$��20��$��20���� .+��� ���� *FindSongName+$ TwoWay=��� #��[  $
��Left=��� #��[ ���� *	AddSong+.��  $	 Add=�
������$��20��$��20���� $��0��$��1��$��2��
������$��2��$��Bottom=��� -   $��SongCollection��$��2��$��0��$��2��
������ $��0q�$��Disabled=�$��Hidden=����� *SongLocalList+ �� SelectedItem �� *SelectedSong+$ TwoWay=����� 	 �� 
TargetType"	 ��.��� 
 �� Property
 � �� �� Value$ Stretch=����� .R��� $��0q�$��60��$��Stretch=�
������ 
������ ._��  �� $��30 ��� $
��1.5* ��� $
��1.4* ��� $��* �.+��� $��Id��$��18(�$��Center=�$
��Left=����� *Index+� $��1��.+�� .+��� $��
MusicImage��$��36��$��36��$��Center=�$��Center=�$	��5 5q����� *Image+�� .+��� $��18(�$	��5 5q����� *Name+�� $��Author��$��18(�$	��5 5q����� *Author+� $��0��$��2��.+��� $��Album��$��18(�$	��5 5q����� *Album+�� $��Genre��$��0��$��3��$��Center=�$��Center=�$��18(�$	��5 5q����� *Genre+