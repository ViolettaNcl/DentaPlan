   M S B A M L   `   `   `  ����  	DentaPlan#    DentaPlan.Views.DentistWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$5  /Панель стоматолога - DentaPlan��5   	   $	��400��60   $	��600��6=    � WindowStartupLocation$ CenterScreen=�6I      BackgroundColor#��[  5   	   .��5      � $��10q�6    J� Effect 5   
   3  -System.Windows.Media.Effects.DropShadowEffect  5         
BlurRadius$ 10Q�6      ShadowDepth$ 2Q�6/      Opacity$	 0.2Q�6?    � RowDefinitions 5
   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   ��  �� Orientation$ 
Horizontal=�6   $��0,0,0,10q�6.   .+�5      �� -   5        $��
Выход��5      $	��100��6@   $��0,0,10,0q�6L   5      � 5   
   $��1��6    5      �� 5      $
��Auto �6    �� 5      $��* �6    .+�5      �� $��Мои заявки��6   $��16(�6*   $��0,0,0,10q�68   &   System.Windows.Controls.DataGrid  5      -   5          Name$ AppointmentsDataGrid��5      $��1��65   	   AutoGenerateColumns	 .  
   
IsReadOnly
 .    SelectionMode$ Single=�6p      Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ ID��6)      Width5  /System.Windows.Controls.DataGridLengthConverter$ 50 6W   1  +System.Windows.Controls.DataGridBoundColumn   Binding 65   �� *AppointmentID+6W     5      $ Пациент��6)   $	 150 6_    6:   �� *Patient.FullName+6_     5      $ Услуга��6)   $	 150 6a    69   �� *Service.ServiceName+6a     5      $ Дата и время��6)   $	 120 6�    6?   �� *AppointmentDateTime+ �� StringFormat$ dd.MM.yyyy HH:mm��6�     5      $ Статус��6)   $ * 6T    69   �� *Status+6T   5      5       5!      5"      