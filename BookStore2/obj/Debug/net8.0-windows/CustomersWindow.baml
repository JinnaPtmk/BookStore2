   M S B A M L   `   `   `  ����E  ABookStore2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:BookStore2
BookStore2  5   	   e aPresentationFramework.AeroLite, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35kNclr-namespace:Microsoft.Windows.Themes;assembly=PresentationFramework.AeroLiteMicrosoft.Windows.Themes 5   	        BookStore2.CustomersWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:BookStore2     5   	   aThemesNclr-namespace:Microsoft.Windows.Themes;assembly=PresentationFramework.AeroLite     5   	     � Title$  CustomersWindow��5	   	   $	��450��6!   $	��800��6.   .��5
      � .+�5   
   �� -   5         ��Name$ CustomersLst��5      $
��Left=�6(   $	��289��6C   $	��Top=�6P   $	��221��6h   $��
10,120,0,0q�6t   5      �� 5   
   -   5         ��Name$ Sort_Cbx��5      $
��Left=�6%   $��30��6@   $��	10,36,0,0q�6L   $	��Top=�6_   $	��176��6w    �� SelectedIndex$ 0��6�   .@�5      �� $*��$เรียงตามชื่อ��6   �� 5      $<��6เรียงตามรหัสลูกค้า��6   5      �� 5   
   -   5         ��Name$ CustomerId_Txt��5      $
��Left=�6*   $��38��6E   $��
350,66,0,0q�6Q    �� TextWrapping$
 Wrap=�6e   $	��Top=�6y   $	��288��6�    �� 	MaxLength$ 10��6�   6�   �� 5   
   -   5        $ CustomerName_Txt��5      $
��Left=�6,   $��38��6G   $��350,120,0,0q�6S   $
 Wrap=�6h   $	��Top=�6|   $	��288��6�   �� 5   
   -   5        $ CustomerAddress_Txt��5      $
��Left=�6/   $��65��6J   $��350,179,0,0q�6V   $
 Wrap=�6k   $	��Top=�6   $	��288��6�   �� 5   
   -   5        $ CustomerPhone_Txt��5      $
��Left=�6-   $��38��6H   $��350,253,0,0q�6T   $
 Wrap=�6i   $	��Top=�6}   $	��288��6�   $ 10��6�   6�   �� 5   
   -   5        $ CustomerEmail_Txt��5      $
��Left=�6-   $��38��6H   $��350,298,0,0q�6T   $
 Wrap=�6i   $	��Top=�6}   $	��288��6�   �� 5   
   -   5         ��Name$ AddCustomer_Btn��5      $��เพิ่ม��6*   $
��Left=�6:   $��43��6U   $��350,351,0,0q�6a   $	��Top=�6v   $��90��6�   6�   �� 5   
   -	   5        $ EditCustomer_Btn��5      $��แก้ไข��6+   $
��Left=�6;   $��43��6V   $��468,351,0,0q�6b   $	��Top=�6w   $��90��6�   6�   �� 5   
   -
   5        $ RemoveCustomer_Btn��5      $��ลบ��6-   $
��Left=�6:   $��43��6U   $��574,351,0,0q�6a   $	��Top=�6v   $��90��6�   6�   �� 5   
   $��ที่อยู่ :��6   $
��Left=�6$   $��38��6?   $��258,179,0,0q�6K   $	��Top=�6`   $��76��6x   �� 5   
   $��อีเมล :��6   $
��Left=�6"   $��38��6=   $��258,299,0,0q�6I   $	��Top=�6^   $��76��6v   �� 5   
   $/��)เบอร์โทรศัพท์ :��6   $
��Left=�6*   $��38��6E   $��258,256,0,0q�6Q   $	��Top=�6f   $��76��6~   �� 5   
   -   5        $ Refresh_Btn��5      $��Refresh��6&   $
��Left=�68   $��
198,41,0,0q�6S   $	��Top=�6g   6   �� 5   
   -   5        $ CustomerSearch_Btn��5      $��ค้นหา��6-   $
��Left=�6=   $��
204,84,0,0q�6X   $	��Top=�6l   6�   �� 5   
   -   5        $ 
search_Txt��5      $
��Left=�6&   $��31��6A   $��	10,78,0,0q�6M   $
 Wrap=�6`   $	��Top=�6t   $	��176��6�   �� 5   
   $��ชื่อ :��6   $
��Left=�6!   $��38��6<   $��258,120,0,0q�6H   $	��Top=�6]   $��76��6u   �� 5    
   $&�� รหัสลูกค้า :��6   $
��Left=�6'   $��38��6B   $��
258,66,0,0q�6N   $	��Top=�6b   $��76��6z   5"      5#      