   M S B A M L   `   `   `  ����  	DentaPlan!    DentaPlan.Views.LoginWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$(  "Авторизация - DentaPlan��5   	   $	��350��6)   $	��400��66    � WindowStartupLocation$ CenterScreen=�6B      BackgroundColor#��[  5   	   .��5      � $��20q�6    J� Effect 5   
   3  -System.Windows.Media.Effects.DropShadowEffect  5         
BlurRadius$ 10Q�6      ShadowDepth$ 2Q�6/      Opacity$	 0.2Q�6?   .+�5
   
   �� .+�5      �� $��
Логин��6   $��0,0,0,5q�6%   $��14(�66   �� 5      -   5         ��Name$ UsernameTextBox��5      $��0,0,0,10q�6/   $��14(�6A   �� 5      $��Пароль��6   $��0,0,0,5q�6&   $��14(�67   G� 5      -   5         G�Name$ PasswordBox��5      $��0,0,0,10q�6/   $��14(�6A   �� 5      $��Роль��6   $��0,0,0,5q�6$   $��14(�65   �� 5      -   5         ��Name$ RoleComboBox��5      $��0,0,0,10q�6-   $��14(�6?   .@�5      �� $ ��Администратор��6   
	 � Tag$	 Admin��67   �� 5      $��Пациент��6   $	 Patient��61   �� 5      $��Стоматолог��6   $	 Dentist��64   5      �� 5      
 �� Orientation$
 
Horizontal=�6   $��Right=�62   .+�5      �� -   5        $��
Войти��5      $	��100��6C   $��0,0,10,0q�6O   �� 5      -   5        $��Регистрация��5      $	��100��6L   5      5      5      5      