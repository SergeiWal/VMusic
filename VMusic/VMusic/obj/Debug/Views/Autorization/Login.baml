   M S B A M L   `   `   `  ����A  =VMusic, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:VMusic.Views.AutorizationVMusic.Views.Autorization  5   	   %    VMusic.Views.Autorization.Login   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   9local'clr-namespace:VMusic.Views.Autorization     5   	   -   5          � Icon$  ../../Icon/main_icon.png��5   	    � 
ResizeMode$ NoResize=�5	   	    � WindowStyle$
 None=�5
   	    � AllowsTransparency . 
������ $	��420��5   	   $	��340��6    � Title$ Login��5   	    � WindowStartupLocation$ CenterScreen=�5   	    J� InputBindings 5      �� 5   
   
 �� Key$ Enter��6    �� Command 6"   �� *LoginCommand+	 �� CommandParameter	 5      �� 
 �� ElementName$
 PasswordBox��6L   .��5      �  � RowDefinitions 5   
   �� 5      $��45 �6   6*   �� 5      $��* �6   6)   .+�5   
   ��  �� CornerRadius$ 20��6   $��2��6#      
Background#��[  5      5      �� 5   
   -   5         ��Name$
 Exit��5      $��Right=�5         
ExitButton#��[ 5!      $
��15 5q�5"      ��5#      �� *ExitCommand+5%      �� 5'   
   $��1��6   $��Center=�6"   $	��Top=�5(      $
��0 15q�5)      .+�5*      �� $��32(�6   $
��0 15q�6&   $��Center=�64   VMUSIC6Y   �� 5+      $��12(�6   $��Center=�6'   ��6D   �� *InfoMessage+6c   �� 5,      $
��0 10q�6   .+�5-      �� 
Логин6#   �� 5.      -   5         ��Name$ LoginBox��5.      ��6,   �� *Login+ �� Mode$ TwoWay=�6R   �� 5/        	Underline#��[ 6   50      �� 51      $
��0 10q�6   .+�52      �� Пароль6$   G� 53      -   5         G�Name$ PasswordBox��53      66   �� 54      #��[ 6   55      �� 56      -   5        $ EnterButton��56        EnterButton#��[ 57      $
��0 15q�58      ��59      �� *LoginCommand+��5:      �� $
 PasswordBox����5<      �� 5=      
Войти6'   5?      �� 5@      $	��0 5q�6   .+�5A      ��   TrassissionButton#��[ 6   ��5B      �� *SwitchToLoginAsAdminCommand+.��5C      ��   
TextButton#��[ 6    Войти как админ6U   5D      �� 5E      #��[ 6   ��5F      �� *SwitchToRegistrationCommand+.��5G      �� #��[ 6    Регистрация6Q   5H      5I      5J      5K      5L      