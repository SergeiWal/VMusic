   M S B A M L   `   `   `  ���� A  =VMusic, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:VMusic.Views.AutorizationVMusic.Views.Autorization  ,    &VMusic.Views.Autorization.Registration   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     9local'clr-namespace:VMusic.Views.Autorization     -     � Icon$  ../../Icon/main_icon.png�� � 
ResizeMode$ NoResize=� � WindowStyle$
 None=� � AllowsTransparency . 
������  � Title$ Registration��$��535.2��$	��340�� � WindowStartupLocation$ CenterScreen=� J� InputBindings �� 
 �� Key$ Enter�� �� Command �� *RegistrationCommand+.��� 	 � RowDefinitions	 �� $��45 ��� $��* �.+��� 
 �� CornerRadius$
 20��$��2��   
Background#��[  �� -    ��Name$
 Exit��$��Right=�  
ExitButton#��[ $
��15 5q����� *ExitCommand+�� $��1��$��Center=�$	��Top=�$
��0 15q�.+��� $��32(�$
��0 15q�$��Center=�VMUSIC�� $��12(�$��Center=�$
��Wrap=����� *InfoMessage+�� $
��0 10q�.+��� Имя�� -    ��Name$ NameBox������ *Name+ �� Mode$ TwoWay=���   	Underline#��[ �� $
��0 10q�.+��� 
Логин�� -   $ LoginBox������ *Login+$ TwoWay=��� #��[ �� $
��0 10q�.+��� ПарольG� -    G�Name$ PasswordBox���� #��[ �� $
��0 10q�.+��� !Повторите парольG� -   $ RepeatPasswordBox���� #��[ �� -   $ EnterButton��  EnterButton#��[ $
��0 10q����� *RegistrationCommand+���� Регистрация�� $	��0 5q�.+���   TrassissionButton#��[ ���� *SwitchToLoginCommand+.����   
TextButton#��[ ,*Войти как пользователь�� #��[ ���� *SwitchToLoginAsAdminCommand+.���� #��[ Войти как админ