   M S B A M L   `   `   `  ���� A  =VMusic, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:VMusic.Views.AdminVMusic.Views.Admin  !    VMusic.Views.Admin.UserPage   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     2local clr-namespace:VMusic.Views.Admin     b ^MaterialDesignThemes.Wpf, Version=4.0.0.2422, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes    N� Title$  UserPage��.���  � RowDefinitions �� $��40 ��� $��50 ���  � ColumnDefinitions �� $��30 ��� $��* ��� $��* ��� $��* �.+���  �� CornerRadius$ 20��$��3��$��5�����	#FF131317� $��4��$
��Left=�$
��25 0q�.+���    ClientControlButton#��[  $��10 10q�$	��230������ *BlockUnblock+���� *SelectedUser+���� ���	#FFBDBDBD J� Opacity$	 0.3Q�$;��5Заблокривать/разблокировать���� #��[  $��10 10q�$��80������ *
RemoveUser+���� *SelectedUser+���� ���	#FFBDBDBD$	 0.3Q�$��Удалить���� #��[  $
��Left=�$��10 10q�$	��200������ *TransferAdminStatus+���� *SelectedUser+���� ���	#FFBDBDBD$	 0.3Q�$#��Сделать админом���� $��1��$��0��$��Center=�$��Center=�$��24(�$��#��
�������� $��1��$��1��$��Center=�$��Center=�$��24(�$��имя��
�������� $��1��$��2��$��center=�$��Center=�$��24(�$��cостояние��
�������� $��1��$��3��$��center=�$��Center=�$��24(�$��роль��
�������� $��5��$��0�����	#FFBDBDBD$��2��$��Bottom=�$	 0.2Q� K� ZIndex$ 1���� $��5��$��1��
������$��2��$��Bottom=�$ 2���� $��1��$��3��$��0�����	#FFBDBDBD$��2��$��Right=�$	 0.2Q�$ 1���� $��1��$��3��$��1�����	#FFBDBDBD$��2��$��Right=�$	 0.2Q�$ 1���� $��1��$��3��$��2�����	#FFBDBDBD$��2��$��Right=�$	 0.2Q�$ 1���� $��2��$��0��$��5��$��0 5 5 10q�
������ $��0q�$��Disabled=�$��Hidden=����� *Users+ �� SelectedItem �� *SelectedUser+ �� Mode$ TwoWay=�����  �� 
TargetType" ��.��� 	 �� Property	 � ��
 �� Value$
 Stretch=����� .R��� $��0q�$��60��
������ 
������ ._��  �� $��30 ��� $��* ��� $��* ��� $��* �.+��� $��Id��$��18(�$��Center=�$��Center=����� *Index+�� $
��Name��$��1��$��18(�$
��10 0q�$��Center=�$
��Left=����� *Name+�� $��State��$��2��$��18(�$
��10 0q�$��Center=�$��Center=����� *	IsBlocked+�� $
��Role��$��0��$��3��$��Center=�$��Center=�$��18(�$
��10 0q����� *	IsAdmin+�� $��5�����	#FFBDBDBD$��2��$��Bottom=�$	 0.2Q�$ 1��