   M S B A M L   `   `   `  ����A  =VMusic, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:VMusic.Views.AutorizationVMusic.Views.Autorization  5   	   ,    &VMusic.Views.Autorization.Registration   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   9local'clr-namespace:VMusic.Views.Autorization     5   	   -   5          � Icon$  ../../Icon/main_icon.png��5   	    � 
ResizeMode$ NoResize=�5	   	    � WindowStyle$
 None=�5
   	    � AllowsTransparency . 
������  � Title$ Registration��5   	   $��535.2��6   $	��340��6-    � WindowStartupLocation$ CenterScreen=�5   	    J� InputBindings 5      �� 5   
   
 �� Key$ Enter��6    �� Command 6!   �� *RegistrationCommand+6L   .��5      � 	 � RowDefinitions	 5   
   �� 5      $��45 �6   6*   �� 5      $��* �6   6)   .+�5   
   �� 
 �� CornerRadius$
 20��6   $��2��6#      
Background#��[  5      5      �� 5   
   -   5         ��Name$
 Exit��5      $��Right=�5        
ExitButton#��[ 5      $
��15 5q�5       ��5!      �� *ExitCommand+5#      �� 5%   
   $��1��6   $��Center=�6"   $	��Top=�5&      $
��0 15q�5'      .+�5(      �� $��32(�6   $
��0 15q�6&   $��Center=�64   VMUSIC6Y   �� 5)      $��12(�6   $��Center=�6'   $
��Wrap=�6a   ��6D   �� *InfoMessage+6w   �� 5*      $
��0 10q�6   .+�5+      �� Имя6!   �� 5,      -   5         ��Name$ NameBox��5,      ��6+   �� *Name+ �� Mode$ TwoWay=�6P   �� 5-        	Underline#��[ 6   5.      �� 5/      $
��0 10q�6   .+�50      �� 
Логин6#   �� 51      -   5        $ LoginBox��51      ��6,   �� *Login+$ TwoWay=�6R   �� 52      #��[ 6   53      �� 54      $
��0 10q�6   .+�55      �� Пароль6$   G� 56      -   5         G�Name$ PasswordBox��56      6f   �� 57      #��[ 6   58      �� 59      $
��0 10q�6   .+�5:      �� !Повторите пароль6.   G� 5;      -   5        $ RepeatPasswordBox��5;      6q   �� 5<      #��[ 6   5=      �� 5A      -   5        $ EnterButton��5A        EnterButton#��[ 5B      $
��0 10q�5C      ��5D      �� *RegistrationCommand+��5F      �� 5G      Регистрация6-   5I      �� 5J      $	��0 5q�6   .+�5K      ��   TrassissionButton#��[ 6   ��5L      �� *SwitchToLoginCommand+.��5M      ��   
TextButton#��[ 6    ,*Войти как пользователь6\   5N      �� 5O      #��[ 6   ��5P      �� *SwitchToLoginAsAdminCommand+.��5Q      �� #��[ 6    Войти как админ6U   5R      5S      5T      5U      5V      