   M S B A M L   `   `   `  ����
  MRP4ME"    MRP4ME.View.BOMDashBoardView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35n jSAPBusinessObjects.WPF.ViewerShared, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      $��Verdana)�5   "   $��1q�67   $	��560��6K     � 	Resources  5	       �� 
TargetType �� 	Resources �� Property �� Value   WrappedColumnHeaderStyle=  7System.Windows.Controls.Primitives.DataGridColumnHeader��%o  '��       &	  �     �� 5
   
   " ��6    5      ���� 5      '��       " ��6   .�5      ��  � ��$ 0,10,0,0q�6/   5      5      5      �� 5   
   "  61   .�5      ��  � �� 5      �� 5      .R�5      �� $
��Wrap=�6$   ��68   �� 6K   5      5      �� 5       � �� �	#FFE2A0236+   5      5      .��5      �� 
������ .+�5   
   �� 
������  �� Orientation$ 
Horizontal=�6.   .+�5      �� $��BOM Dashboard��6   $
��Left=�6,   $��30��6G   $	��352��6S   $
��Bold"�6_   5!      � 5#   
   $��0,0,0,30q�6   ���	#FFE2FEE2.+�5$      �� -   5         ��Name$ stpOuter��5$      
������ .+�5%      &   System.Windows.Controls.DataGrid  -   5          Name$ dgBOM��5%         AutoGenerateColumns .  	   CanUserAddRows	 .  
   
IsReadOnly
 .  
������    HorizontalGridLinesBrush �	#FF0F9D58   VerticalGridLinesBrush �	#FF0F9D58$��0,0,0,11q�5(   _      ColumnHeaderHeight$ 40Q�5)         RowBackground �	#FFE2FEE2   SelectionUnit$
 Cell=�6I      AlternatingRowBackground
 ����� �� AlternationCount$ 2��5*   A   $	��486��5+         HeadersVisibility$ Column=�6%   ��5&      �� *Salesorders+ �� NotifyOnValidationError . " �� ValidatesOnNotifyDataErrors .  �� UpdateSourceTrigger$ PropertyChanged=� �� Mode$ OneWay=�  5,      )  #System.Windows.Controls.DataGridRow*  $System.Windows.Controls.DataGridCell �� Triggers P� Property P� Value �� Path �� 
TargetType �� Padding J� SnapsToDevicePixels��%�  '        '  H     '  �     �� 5.      "  6    .�5/      ��  � ��$ 40��67   50      �� 52      "  6    .�53      ��  � �� �	#FFE2A0236;   54      �� 55      "  6     56      P� 57   "    �@ 
IsSelected . . �58   &   ��  � �� 6C   �� $ BgColor��� 59   &    � ��
 �   �6C   5:   #   .�5<      ��  � ��$ 3q�68   �� 5=       � ��$
 Left=�6K   �� 5>       � ��$ Stretch=�6I   �� 5?       � �� 5@   "   �� 5A   &   "  66   .R�5B   *   �� # z��61   #��z��6U   #��z��6�   #��z��6�    . ._�5C   .   �� # z 6?   5D   +   5E   '   5G      5H      5I         Columns 5J      0  *System.Windows.Controls.DataGridTextColumn  5K      ,  &System.Windows.Controls.DataGridColumn   Header$   Order #  ��6-   1  +System.Windows.Controls.DataGridBoundColumn    Binding  6B   �� *	so_number+  5L      $   Customer/Vendor��6-     6H   �� *customer_name+!   ElementStyle! 5M      �� 5N   "   .�5O   &   ��  � ��$
 Wrap=�6O   �� 5P   &    �@��TextAlignment$
 Left=�6P   5Q   #   5S        5T      $   Delivery date ��6-     6G   �� *required_date+" �� StringFormat$" {0:MM/dd/yyyy}��  5U      $   Item Code  ��6-     6D   �� *	item_code+  5W      $   Name  ��6-     6?   �� *name+! 5X      �� 5Y   "   .�5Z   &   ��  � ��$
 Wrap=�6O   �� 5[   &    �@��TextAlignment$
 Left=�6P   5\   #   5^        5_      $   Unit  ��6-     6?   �� *unit+  5`      $   Quantity  ��6-     6C   �� *
quantity+  5a      $   Shipped/ Received  ��6-   #   HeaderStyle## [  6L   $   Width5  /System.Windows.Controls.DataGridLengthConverter$$ 70 5b   S     6-   �� *quantity_received+6S     5c      $   Backordered  ��6-     6F   �� *back_ordered+  5d      $   Unit cost  ��6-     6D   �� *	unit_cost+$" {0:#0.00######}��  5e      $ 	  Level  ��6-   %   MinWidth$% 50Q�6Z     6@   �� *level+6Z     5f      $   User  ��6-     6?   �� *user+4	  .System.Windows.Controls.DataGridTemplateColumn	  5g      & 	  CellTemplate& 5h      �� 5i   "   .R�5j   &   �� $ 
Horizontal=�62     5k   *   ��%W   '��       �� 5l   .   " ��64   .�5m   2   ��  � ��$ 0,0,0,0q�6\   5n   /   5o   +   .+�5p   *   �� $��30��62   $��30��6=   ��6I   �� $! DataContext.DeleteSOCommand�' �� RelativeSource' �� *FindAncestor+( �� AncestorType"(  ��5r   X   �� $ 	so_number�.��5s   .   �� .+�5t   2   �� $.��(/MRP4ME;component/Images/delete_icon.jpg��68   5u   /   5v   +   5w   '   5x   #   5z      	  5{      & 5|      �� 5}   "   .R�5~   &   �� $ 
Horizontal=�62     5   *   ��%W   '��       �� 5�   .   " ��64   .�5�   2   ��  � ��$ 0,0,0,0q�6\   5�   /   5�   +   .+�5�   *   �� $��30��62   $��30��6=   ��6I   �� $ DataContext.EditSOCommand�' �� *FindAncestor+"( ����5�   b   �� $ 	so_number�.��5�   .   �� .+�5�   2   �� $,��&/MRP4ME;component/Images/edit_icon.jpg��68   5�   /   5�   +   5�   '   5�   #   5�      	  5�      & 5�      �� 5�   "   .R�5�   &   �� $ 
Horizontal=�62     5�   *   ��%W   '��       �� 5�   .   " ��64   .�5�   2   ��  � ��$ 0,0,0,0q�6\   5�   /   5�   +   .+�5�   *   �� $��30��62   $��30��6=   ��6I   �� $# DataContext.SalesOrderCommand�' �� *FindAncestor+"( ��.��5�   .   �� .+�5�   2   �� $+��%/MRP4ME;component/Images/add_icon.jpg��68   5�   /   5�   +   5�   '   5�   #   5�      5�      �� 5�      $ 
Horizontal=�6   .+�5�      �� -   5        ) ��Name$) btnPrintBOM��5�      $��Print��62   $
��Left=�6C   $��30��6^   $	��Top=�5�      $	��100��6+   ���	#FFE2A023$��	150,0,0,0q�5�      $
��Bold"�6&   * J� RenderTransformOrigin$* 1.49,0.4&�68   ��5�   N   �� $! DataContext.PrintBOMCommand�' �� *FindAncestor+"( ��5�   8   �� 5�      -   5        $) btnCancelSO��5�      $��Cancel��62   $
��Left=�6D   $��30��6_   $	��Top=�5�      $	��100��6.   ���	#FFE2A023$
��Bold"�5�      $* 1.49,0.4&�6(   $��
100,0,10,0q�6I   ��5�   Q   �� *CloseCommand+5�   I   5�      5�      5�      5�      5�      