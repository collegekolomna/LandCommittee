   M S B A M L   `   `   `  ����H  DLandCommittee, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:LandCommitteeLandCommittee  5   	       LandCommittee.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   _ [MaterialDesignThemes.Wpf, Version=4.5.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   'localclr-namespace:LandCommittee  5   	   -   5          � Title$  
MainWindow��5	   	   $	��750��6   $
��1200��6)    � WindowStartupLocation$ CenterScreen=�66    � WindowStyle$
 None=�5
   	    � 
ResizeMode$ NoResize=�6    � AllowsTransparency .  � Icon$ Resources/logo.ico��5   	   ��5
   2   Z� .��5      � .+�5   
   #  MaterialDesignThemes.Wpf.Card  $��10q�6      UniformCornerRadius$ 15Q�6*   .��5      �  � ColumnDefinitions 5      �� 5      $��1* �6'   �� 5      $	��15* �6'    � 	Resources 5      '  !MaterialDesignThemes.Wpf.PackIcon	 �� 
TargetType
 �� Property �� Value��%�   '        �� 5      "	  6   .�5      �� 
 � ��$ 50��62   6?   �� 5      
 � ��$ 50��63   6@   5      5      .+�5      � $��0��6   .+�5      �� $
��0 15q�6   .@�5      �� $��Center=�6'   $
��0 10q�6D   .��5      �� -   5         ��Name$ btnHome��5   %    &  "MaterialDesignFloatingActionButton#��[  66   $
��Left=�6r   ���#388E3C��5      Z� ��65   Z� .��5   "   �� $��-5q�6-   .+�5   &        Kind$
 home=�6>   5    #   5!      5"      �� 5#      $��Center=�6'   $
��0 10q�6D   .��5$      �� -   5        $ btnOwner��5$   %   #��[  67   $
��Left=�6s   ���#388E3C��5%      Z� ��65   Z� .��5&   "   �� $��-5q�6-   .+�5'   &     $ account=�6>   5(   #   5)      5*      �� 5+      $��Center=�6'   $��0q�6D   .��5,      �� -   5        $ btnLandPlot��5,   %   #��[  6:   $
��Left=�6v   ���#388E3C��5-      Z� ��65   Z� .��5.   "   �� $��-5q�6-   .+�5/   &     $	 map=�6>   50   #   51      52      �� 53      $��Center=�6'   $
��0 10q�6D   .��54      �� -   5        $ ContractOfSale��54   %   #��[  6=   $
��Left=�6y   ���#388E3C��55      Z� ��65   Z� .��56   "   �� $��-5q�6-   .+�57   &     $ application=�6>   58   #   59      5:      �� 5;      $��Center=�6'   $��0q�6D   .��5<      �� -   5        $ LeaseContract��5<   %   #��[  6<   $
��Left=�6x   ���#388E3C��5=      Z� ��65   Z� .��5>   "   �� $��-5q�6-   .+�5?   &     $ EqualiserVertical=�6>   5@   #   5A      5B      �� 5D      $��Center=�6'   $��0 60 0 0q�6D   .��5E      �� -   5        $ btnExit��5F   `   #��[  5E   %   $
��Left=�6a   ���#388E3C � Cursor$
 Hand��5F   �   ��6   Z� ��65   Z� .��5G   "   �� $��-5q�6-   .+�5H   &     $ 	ExitToApp=�6>   5I   #   5J      5K      5M      5N      � 5O      -   5        $��RenderPages��5O   <   $��1��6   ���#F6F5F85Q      5R      5S      5T      5U      