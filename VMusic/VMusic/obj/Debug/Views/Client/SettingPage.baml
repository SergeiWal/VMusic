   M S B A M L   `   `   `  ����A  =VMusic, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:VMusic.Views.ClientVMusic.Views.Client  5      %    VMusic.Views.Client.SettingPage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      3local!clr-namespace:VMusic.Views.Client     5      b ^MaterialDesignThemes.Wpf, Version=4.0.0.2422, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5        N� Title$  SettingPage��5
      .��5      �  � ColumnDefinitions 5   
   �� 5      �� 5       � RowDefinitions 5   
   �� 5      $��50 �6   �� 5      .+�5   
   ��  �� CornerRadius$ 20��6   $��3��6#   $��5��5      ���	#FF1313175      � 5   
   $��0��6   $��1��6!   $��Right=�5      $��Center=�5      $
��10 0q�5       .+�5!      �� $��24(�63   ��6   �� *	User.Name+63   '  !MaterialDesignThemes.Wpf.PackIcon  5"         Kind$
 User=�6&   
������$��32��6H   $��32��6S   6a   5#      �� 5%   
   $��2��6   $��Bottom=�6%   
������$��2��6V   �� 5'   
   $��0��6   $��1��6%   $��Center=�5(      $��10 10q�5)      .+�5+      �� $
��0 10q�6   .+�5,      �� Новый пароль6*   G� 5-      -   5        $��NewPasswordBox��5-      6i   �� 5.         	Underline#��[  6   5/      �� 51      $
��0 10q�6   .+�52      �� !Повторите пароль6.   G� 53      -   5        $��RepeatPasswordBox��53      6o   �� 54      #��[  6   55      �� 57      $
��0 10q�6   .+�58      �� Текущий пароль6,   G� 59      -   5        $��OldPasswordBox��59      6i   �� 5:      #��[  6   5;      �� 5<      $
��0 10q�5=        EnterButton#��[ 5>      $	��140��5?      $
��Left=�5@      ��5A      �� *ChangePasswordCommand+.��5B      �� $!��Сменить пароль��6   64   5C      5D      �� 5G   
   $��1��6   $��1��5H      $��Center=�5I      $��Stretch=�5J      $��10 30q�5K      .+�5L      �� $
��0 10q�5M      #��[ 5N      ��5O      �� *DeleteUserCommand+.��5P      �� $��Удалить��6   6-   5Q      �� 5R      $
��0 10q�5S      #��[ 5T      ��5U      �� *ExitCommand+.��5V      �� $��
Выйти��6   6+   5W      �� 5X      $��Center=�6   ��65   �� *ResultString+6U   5Y      5\      5]      