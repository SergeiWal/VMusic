   M S B A M L   `   `   `  ���� A  =VMusic, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:VMusic.Views.ClientVMusic.Views.Client  %    VMusic.Views.Client.SettingPage   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     3local!clr-namespace:VMusic.Views.Client     b ^MaterialDesignThemes.Wpf, Version=4.0.0.2422, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes    N� Title$  SettingPage��.���  � ColumnDefinitions �� ��  � RowDefinitions �� $��50 ��� .+���  �� CornerRadius$ 20��$��3��$��5�����	#FF131317� $��0��$��1��$��Right=�$��Center=�$
��10 0q�.+��� $��24(����� *	User.Name+'  !MaterialDesignThemes.Wpf.PackIcon     Kind$
 User=�
������$��32��$��32���� $��2��$��Bottom=�
������$��2���� $��0��$��1��$��Center=�$��10 10q�.+��� $
��0 10q�.+��� Новый парольG� -   $��NewPasswordBox����    	Underline#��[  �� $
��0 10q�.+��� !Повторите парольG� -   $��RepeatPasswordBox���� #��[  �� $
��0 10q�.+��� Текущий парольG� -   $��OldPasswordBox���� #��[  �� $
��0 10q�  EnterButton#��[ $	��140��$
��Left=����� *ChangePasswordCommand+.���� $!��Сменить пароль���� $��1��$��1��$��Center=�$��Stretch=�$��10 30q�.+��� $
��0 10q�#��[ ���� *DeleteUserCommand+.���� $��Удалить���� $
��0 10q�#��[ ���� *ExitCommand+.���� $��
Выйти���� $��Center=����� *ResultString+