   M S B A M L   `   `   `  ���� A  =VMusic, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:VMusic.Views.ClientVMusic.Views.Client  #    VMusic.Views.Client.GenrePage   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     3local!clr-namespace:VMusic.Views.Client     b ^MaterialDesignThemes.Wpf, Version=4.0.0.2422, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes    N� Title$  	GenrePage��.���  � RowDefinitions �� $��50 ��� .+���  �� CornerRadius$ 20��$��3��$��5�����	#FF131317�� $��0��
������$��2��$��Bottom=��� $��
Жанры��$��18(�$��Center=�$
��10 0q��� $��1��$��0q�
������ $��Disabled=�$��Hidden=����� *	GenreList+ �� SelectedItem �� *SelectedGenre+ �� Mode$ TwoWay=����� .R�� ���� .R�� $	��180��$	��150��$��10 10q� �� $
��2.5* ��� .+��� $��2��$ 20�����	#FF19181F�� $��0�� K� ZIndex$ 1��$
��Left=�$	��Top=�$��25 15q�$	��120��$	��100������ *Image+'  !MaterialDesignThemes.Wpf.PackIcon     Kind$ 	MusicNote=�
������$��0��$��Center=�$��Center=�$	��100��$	��100���� 
������$��2��$��Bottom=��� $��1��$��16(�$
��10 5q�$
��Wrap=����� *Name+