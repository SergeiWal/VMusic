   M S B A M L   `   `   `  ���� A  =VMusic, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:VMusic.Views.ClientVMusic.Views.Client  "    VMusic.Views.Client.HomePage   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     3local!clr-namespace:VMusic.Views.Client     b ^MaterialDesignThemes.Wpf, Version=4.0.0.2422, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes    N� Title$  HomePage��.���  � RowDefinitions �� $��40 ��� $��50 ���  � ColumnDefinitions �� $��30 ��� $
��1.5* ��� $
��1.4* ��� $��* �.+���  �� CornerRadius$ 20��$��3��$��5�����	#FF131317�� $��1��$��0��$��Center=�$��Center=�$��24(�$��#��
�������� $��1��$��1��$��Center=�$��Center=�$��24(�$��название��
�������� $��1��$��2��$��center=�$��Center=�$��24(�$��альбом��
�������� $��1��$��3��$��Center=�$��Center=�$��24(�$��жанр��
������$
��15 0q��� $��5��$��0�����	#FFBDBDBD$��2��$��Bottom=� J� Opacity$	 0.2Q� K� ZIndex$ 1���� $��5��$��1��
������$��2��$��Bottom=�$ 2���� $��1��$��3��$��0�����	#FFBDBDBD$��2��$��Right=�$	 0.2Q�$ 1���� $��1��$��3��$��1�����	#FFBDBDBD$��2��$��Right=�$	 0.2Q�$ 1���� $��1��$��3��$��2�����	#FFBDBDBD$��2��$��Right=�$	 0.2Q�$ 1���� -   $��SongCollection��$��2��$��0��$��5��
������ $��0q�$��Disabled=�$��Hidden=����� *LocalSongList+ �� SelectedItem �� *CurrentSong+����  �� 
TargetType" ��.���  �� Property � ��	 �� Value$	 Stretch=����� .R��� $��0q�$��60��$��Stretch=�
������ 
������ ._��  �� $��30 ��� $
��1.5* ��� $
��1.4* ��� $��* �.+��� $��Id��$��18(�$��Center=�$
��Left=����� *Index+� $��1��.+�� .+��� $��
MusicImage��$��36��$��36��$��Center=�$��Center=�$	��5 5q����� *Image+�� .+��� $
��Name��$��18(�$	��5 5q����� *Name+�� $��Author��$��18(�$	��5 5q����� *Author+� $��0��$��2��.+��� $��Album��$��18(�$	��5 5q����� *Album+�� $��Genre��$��0��$��3��$��Center=�$��Center=�$��18(�$	��5 5q����� *Genre+