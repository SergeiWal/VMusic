   M S B A M L   `   `   `  ���� A  =VMusic, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:VMusic.Views.AdminVMusic.Views.Admin  %    VMusic.Views.Admin.AddMusicPage   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     2local clr-namespace:VMusic.Views.Admin       N� Title$  AddMusicPage��.���  � ColumnDefinitions �� �� .+���  �� CornerRadius$ 20��$��3��$��5�����	#FF131317�� $��0��$��Center=�$��10 10q�.+��� $
��0 10q�.+��� Название�� -    ��Name$
 Name������ *Name+ �� Mode$ TwoWay=���    	Underline#��[  �� $
��0 10q�.+��� 
Автор�� -   $ Author������ *Author+$ TwoWay=��� #��[  �� $
��0 10q�.+��� Альбом�� -   $ Album������ *Album+$ TwoWay=��� #��[  �� $
��0 10q�.+��� 
Жанр�� -    ��Name$ Genre��  ComboBoxFlatStyle#��[  �� SelectedItem �� *Genre+$ TwoWay=�.@��� $��Поп���� $��Рок���� $&�� Инструментальная���� $��Инди���� $��Реп���� $��Фолк���� $��Джаз���� #��[  �� $��1��$��Center=�$��Stretch=�$��10 30q�.+��� $
��0 10q�  EnterButton#��[ ���� *
AddImage+.���� $��Изоброжение���� $
��0 10q�#��[ ���� *	AddSource+.���� $��Трек���� $
��0 10q�#��[ ���� *
AddMusic+.���� $��Добавить���� $��Center=�$
��Wrap=����� *ResultString+